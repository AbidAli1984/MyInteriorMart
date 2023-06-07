using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using DAL.AUDIT;
using BOL.AUDITTRAIL;
using BAL.Messaging.Notify;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using DAL.LISTING;
using BOL.VIEWMODELS;
using System.Text.Encodings.Web;
using Hangfire;
using Microsoft.AspNetCore.Identity;

namespace BAL.Claims.Listing
{
    public class ClaimListing : IClaimListing
    {
        public IConfiguration Configuration;
        public IWebHostEnvironment HostEnvironment;
        public AuditDbContext AuditContext;
        public INotification Notification;
        public ListingDbContext ListingContext;
        public UserManager<IdentityUser> UserManager;
        private readonly IWebHostEnvironment webHost;
        public ClaimListing(IConfiguration configuration, IWebHostEnvironment env, AuditDbContext auditContext, INotification notification, ListingDbContext listingContext, IWebHostEnvironment webHost, UserManager<IdentityUser> userManager)
        {
            Configuration = configuration;
            HostEnvironment = env;
            AuditContext = auditContext;
            Notification = notification;
            ListingContext = listingContext;
            this.webHost = webHost;
            UserManager = userManager;
        }

        public async Task<bool> CheckIfUserAlreadyClaimedAnyListing(string userGuid)
        {
            var result = await AuditContext.ListingClaim.Where(i => i.ClaimedBy.Contains(userGuid)).ToListAsync();

            if (result.Count == 0)
            {
                return false;
            }
            else if (result.Count != 0 && result.Count <= 2 && result.Any(i => i.Status == "Pending"))
            {
                return false;
            }
            else if (result.Count == 1 && result.Any(i => i.Status != "Pending"))
            {
                return false;
            }
            else if (result.Count == 2 && result.All(i => i.Status != "Pending"))
            {
                return true;
            }
            else
            {
                return true;
            }
        }

        public async Task<int> GenerateMobileOTP(string mobileNumber, string userGuid, int listingId, string message)
        {
            Random random = new Random();
            int otp = random.Next(100000, 900000);

            ListingClaim claim = new ListingClaim();
            claim.OTP = otp;
            claim.DateTime = DateTime.Now;
            claim.Mobile = mobileNumber;
            claim.ListingID = listingId;
            claim.ClaimedBy = userGuid;
            claim.Message = message;
            claim.ClaimType = "Mobile";
            claim.Status = "Pending";

            await AuditContext.ListingClaim.AddAsync(claim);
            await AuditContext.SaveChangesAsync();

            if (Configuration["EnableClaimByMobile"] == "Enabled")
            {
                Notification.SendSMS(mobileNumber, $"OTP for Claim Listing is {otp}, never share your OTP with anyone. Thankyou for using My Interior Mart.");

                Notification.SendEmail("myinteriormart@gmail.com", "OTP from My Interior Mart", $"OTP for Claim Listing is {otp}, never share your OTP with anyone. Thankyou for using My Interior Mart.");
            }

            return otp;
        }

        public async Task<int> GenerateEmailOTP(string email, string userGuid, int listingId, string message)
        {
            Random random = new Random();
            int otp = random.Next(100000, 900000);

            ListingClaim claim = new ListingClaim();
            claim.OTP = otp;
            claim.DateTime = DateTime.Now;
            claim.Email = email;
            claim.ListingID = listingId;
            claim.ClaimedBy = userGuid;
            claim.Message = message;
            claim.ClaimType = "Email";
            claim.Status = "Pending";

            await AuditContext.ListingClaim.AddAsync(claim);
            await AuditContext.SaveChangesAsync();

            if (Configuration["EnableClaimByEmail"] == "Enabled")
            {
                var listingCompanyname = await ListingContext.Listing.Where(l => l.ListingID == listingId).Select(l => l.CompanyName).FirstOrDefaultAsync();

                // Shafi: Get newsletter template
                var webRoot = webHost.WebRootPath;
                var notificationTemplate = System.IO.Path.Combine(webRoot, "Email-Templates", "Claim-Business-Listing-OTP.html");
                string emailMsg = System.IO.File.ReadAllText(notificationTemplate, Encoding.UTF8).Replace("{name}", email).Replace("{listingName}", listingCompanyname).Replace("{otp}", otp.ToString());
                // End:

                Notification.SendEmail(email, "OTP from My Interior Mart", emailMsg);
            }
            return otp;
        }

        public async Task GenerateDocumentOTP(string userGuid, int listingId, string message)
        {
            Random random = new Random();
            int otp = random.Next(100000, 900000);

            ListingClaim claim = new ListingClaim();
            claim.OTP = otp;
            claim.DateTime = DateTime.Now;
            claim.ListingID = listingId;
            claim.ClaimedBy = userGuid;
            claim.Message = message;
            claim.ClaimType = "Document";
            claim.Status = "Pending";

            await AuditContext.ListingClaim.AddAsync(claim);
            await AuditContext.SaveChangesAsync();

            if (Configuration["EnableClaimByEmail"] == "Enabled")
            {
                var listingCompanyname = await ListingContext.Listing.Where(l => l.ListingID == listingId).Select(l => l.CompanyName).FirstOrDefaultAsync();

                var user = await UserManager.FindByIdAsync(userGuid);

                // Shafi: Get newsletter template
                var webRoot = webHost.WebRootPath;
                var notificationTemplate = System.IO.Path.Combine(webRoot, "Email-Templates", "Claim-Business-Listing-OTP.html");
                string emailMsg = System.IO.File.ReadAllText(notificationTemplate, Encoding.UTF8).Replace("{name}", "Humza Habib").Replace("{listingName}", listingCompanyname).Replace("{otp}", otp.ToString());
                // End:

                //Notification.SendSMS(Configuration["AdminMobile"], $"Dear Admin, a user just requsted for LISTING CLAIM by documents. His/her OTP is {otp} Please check email or admin panel for more details.");

                //Notification.SendEmail(Configuration["AdminEmail"], "OTP from My Interior Mart", emailMsg);

                Notification.SendSMS(user.PhoneNumber, $"Dear user, thank your for uploading document on My Interior Mart for claim business listing of {listingCompanyname}. We will revert back to you within 7 working days.");


                var notificationTemplateForUser = System.IO.Path.Combine(webRoot, "Email-Templates", "Claim-Business-Listing-Document.html");
                string emailMsgUser = System.IO.File.ReadAllText(notificationTemplateForUser, Encoding.UTF8).Replace("{name}", user.Email).Replace("{listingName}", listingCompanyname).Replace("{otp}", otp.ToString());

                Notification.SendEmail(user.Email, "Thanks for Clisting Claim", emailMsgUser);
            }
        }

        public async Task<ValidationResponseViewModel> VerifyMobileOTP(string mobileNumber, string userGuid, int listingId, int otp)
        {
            var claim = await AuditContext.ListingClaim.Where(i => i.ListingID == listingId && i.Mobile == mobileNumber && i.ClaimedBy == userGuid).OrderByDescending(i => i.ClaimID).FirstOrDefaultAsync();

            var listingCommunication = await ListingContext.Communication.Where(i => i.ListingID == listingId).FirstOrDefaultAsync();

            if (listingCommunication.Mobile == mobileNumber || listingCommunication.Whatsapp == mobileNumber)
            {
                if (claim.OTP == otp)
                {
                    claim.Status = "Claim Successfull";
                    claim.VerificationDate = DateTime.Now;
                    claim.OTPVerified = true;
                    AuditContext.Update(claim);
                    await AuditContext.SaveChangesAsync();

                    var jobId = BackgroundJob.Schedule(() => TransferListing(userGuid, listingId, "", mobileNumber), TimeSpan.FromSeconds(1));

                    ValidationResponseViewModel response = new ValidationResponseViewModel();
                    response.Successfull = true;
                    response.Message = "Success: OTP verified successfully!";
                    response.Information = "Listing claimed successfully.";
                    return response;
                }
                else
                {
                    claim.Status = "OTP Not Matched";
                    claim.VerificationDate = DateTime.Now;
                    claim.OTPVerified = false;
                    AuditContext.Update(claim);
                    await AuditContext.SaveChangesAsync();

                    ValidationResponseViewModel response = new ValidationResponseViewModel();
                    response.Successfull = false;
                    response.Message = "Error: OTP verification failed!";
                    response.Information = "Sorry! OTP Does not matched.";
                    return response;
                }
            }
            else
            {
                ValidationResponseViewModel response = new ValidationResponseViewModel();
                response.Successfull = false;
                response.Message = "Sorry! Use Different Mobile Number";
                response.Information = "This mobile number does not match with listing number.";
                return response;
            }


        }

        public async Task<ValidationResponseViewModel> VerifyEmailOTP(string email, string userGuid, int listingId, int otp)
        {
            var claim = await AuditContext.ListingClaim.Where(i => i.ListingID == listingId && i.Email == email && i.ClaimedBy == userGuid).OrderByDescending(i => i.ClaimID).FirstOrDefaultAsync();

            var listingCommunication = await ListingContext.Communication.Where(i => i.ListingID == listingId).FirstOrDefaultAsync();

            if (listingCommunication.Email == email)
            {
                if (claim.OTP == otp)
                {
                    claim.Status = "Claim Successfull";
                    claim.VerificationDate = DateTime.Now;
                    claim.OTPVerified = true;
                    AuditContext.Update(claim);
                    await AuditContext.SaveChangesAsync();

                    var jobId = BackgroundJob.Schedule(() => TransferListing(userGuid, listingId, "", email), TimeSpan.FromSeconds(1));

                    ValidationResponseViewModel response = new ValidationResponseViewModel();
                    response.Successfull = true;
                    response.Message = "Success: OTP verified successfully!";
                    response.Information = "Listing claimed successfully.";
                    return response;
                }
                else
                {
                    claim.Status = "OTP Not Matched";
                    claim.VerificationDate = DateTime.Now;
                    claim.OTPVerified = false;
                    AuditContext.Update(claim);
                    await AuditContext.SaveChangesAsync();

                    ValidationResponseViewModel response = new ValidationResponseViewModel();
                    response.Successfull = false;
                    response.Message = "Error: OTP verification failed!";
                    response.Information = "Sorry! OTP Does not matched.";
                    return response;
                }
            }
            else
            {
                ValidationResponseViewModel response = new ValidationResponseViewModel();
                response.Successfull = false;
                response.Message = "Sorry! Use Different Email";
                response.Information = "This email does not match with listing email.";
                return response;
            }
        }

        public async Task<ValidationResponseViewModel> VerifyDocumentOTP(string shortLink, string mobile, string email)
        {
            var claim = await AuditContext.ListingClaim.Where(i => i.ClaimVerificationShortLink == shortLink).FirstOrDefaultAsync();
            claim.Status = "Claim Successfull";
            claim.VerificationDate = DateTime.Now;
            claim.OTPVerified = true;
            AuditContext.Update(claim);
            await AuditContext.SaveChangesAsync();

            var jobId = BackgroundJob.Schedule(() => TransferListing(claim.ClaimedBy, claim.ListingID, mobile, email), TimeSpan.FromSeconds(1));

            ValidationResponseViewModel response = new ValidationResponseViewModel();
            response.Successfull = true;
            response.Message = "Success: OTP verified successfully!";
            response.Information = "Listing claimed successfully.";
            return response;
        }

        public Task UploadBusinessProof()
        {
            throw new NotImplementedException();
        }

        public async Task TransferListing(string userGuid, int listingId, string mobile, string email)
        {
            var listing = await ListingContext.Listing.Where(i => i.ListingID == listingId).FirstOrDefaultAsync();
            if (listing != null)
            {
                listing.OwnerGuid = userGuid;
                ListingContext.Update(listing);
                await ListingContext.SaveChangesAsync();
            }

            var communication = await ListingContext.Communication.Where(i => i.ListingID == listingId).FirstOrDefaultAsync();
            if (communication != null)
            {
                communication.OwnerGuid = userGuid;
                ListingContext.Update(communication);
                await ListingContext.SaveChangesAsync();
            }

            var address = await ListingContext.Address.Where(i => i.ListingID == listingId).FirstOrDefaultAsync();
            if (address != null)
            {
                address.OwnerGuid = userGuid;
                ListingContext.Update(address);
                await ListingContext.SaveChangesAsync();
            }

            var categories = await ListingContext.Categories.Where(i => i.ListingID == listingId).FirstOrDefaultAsync();
            if (categories != null)
            {
                categories.OwnerGuid = userGuid;
                ListingContext.Update(categories);
                await ListingContext.SaveChangesAsync();
            }

            var specialisation = await ListingContext.Specialisation.Where(i => i.ListingID == listingId).FirstOrDefaultAsync();
            if (specialisation != null)
            {
                specialisation.OwnerGuid = userGuid;
                ListingContext.Update(specialisation);
                await ListingContext.SaveChangesAsync();
            }

            var workingHours = await ListingContext.WorkingHours.Where(i => i.ListingID == listingId).FirstOrDefaultAsync();
            if (workingHours != null)
            {
                workingHours.OwnerGuid = userGuid;
                ListingContext.Update(workingHours);
                await ListingContext.SaveChangesAsync();
            }

            var paymentDetails = await ListingContext.PaymentMode.Where(i => i.ListingID == listingId).FirstOrDefaultAsync();
            if (paymentDetails != null)
            {
                paymentDetails.OwnerGuid = userGuid;
                ListingContext.Update(paymentDetails);
                await ListingContext.SaveChangesAsync();
            }
        }

        public async Task<bool> CheckIfListingAlreadyClaimed(int listingId)
        {
            var result = await AuditContext.ListingClaim.AnyAsync(i => i.ListingID == listingId);
            return result;
        }
    }
}
