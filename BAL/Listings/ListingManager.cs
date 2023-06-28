using BOL.AUDITTRAIL;
using BOL.LISTING;
using BOL.PLAN;
using DAL.BILLING;
using DAL.LISTING;
using DAL.SHARED;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BAL.Messaging.Notify;
using BAL.Audit;
using DAL.AUDIT;
using BOL.VIEWMODELS.Dashboards;
using System.IO;
using System.Globalization;

namespace BAL.Listings
{
    public class ListingManager : IListingManager
    {
        private readonly ListingDbContext listingContext;
        private readonly BillingDbContext billingContext;
        private readonly SharedDbContext sharedManager;
        private readonly IWebHostEnvironment webHost;
        private readonly INotification notification;
        private readonly AuditDbContext auditContext;

        public ListingManager(ListingDbContext listingContext, SharedDbContext sharedManager, BillingDbContext billingContext, IWebHostEnvironment webHost, INotification notification, AuditDbContext auditContext)
        {
            this.listingContext = listingContext;
            this.sharedManager = sharedManager;
            this.billingContext = billingContext;
            this.webHost = webHost;
            this.notification = notification;
            this.auditContext = auditContext;
        }

        // Shafi: Check if listing fullfill free listing criteria
        // To fullfill [Free Listing Criteria] a user's listing must have fill all 7 free listing forms
        // This service is mainly used in search result of portal
        public bool CheckIfListingFullfillFreeListingCrieteria(int id)
        {
            var companyExist = listingContext.Listing.Any(i => i.ListingID == id);
            var communicationExist = listingContext.Communication.Any(i => i.CommunicationID == id);
            var addressExist = listingContext.Address.Any(i => i.AddressID == id);
            var categoryExist = listingContext.Categories.Any(i => i.CategoryID == id);
            var specialisationExist = listingContext.Specialisation.Any(i => i.SpecialisationID == id);
            var workingHoursExist = listingContext.WorkingHours.Any(i => i.WorkingHoursID == id);
            var paymentExist = listingContext.PaymentMode.Any(i => i.PaymentID == id);

            if(companyExist == true && communicationExist == true && addressExist == true && categoryExist == true && specialisationExist == true && workingHoursExist == true && paymentExist == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        // End:

        // Shafi: Check if record exists
        public bool CompanyExists(int id)
        {
            var result = listingContext.Listing.Any(i => i.ListingID == id);
            return result;
        }

        public bool CommunicationExists(int id)
        {
            var result = listingContext.Communication.Any(i => i.CommunicationID == id);
            return result;
        }

        public bool AddressExists(int id)
        {
            throw new NotImplementedException();
        }

        public bool CategoriesExists(int id)
        {
            throw new NotImplementedException();
        }

        public bool PaymentExists(int id)
        {
            var result = listingContext.PaymentMode.Any(i => i.PaymentID == id);
            return result;
        }

        public bool SocialExists(int id)
        {
            var result = listingContext.SocialNetwork.Any(i => i.SocialNetworkID == id);
            return result;
        }

        public bool CertificationsExists(int id)
        {
            var result = listingContext.Certification.Any(i => i.CertificationID == id);
            return result;
        }

        public bool ProfileExists(int id)
        {
            var result = listingContext.Profile.Any(i => i.ProfileID == id);
            return result;
        }

        public bool SpecialisationExists(int id)
        {
            var result = listingContext.Specialisation.Any(i => i.SpecialisationID == id);
            return result;
        }

        public bool WorkingExists(int id)
        {
            var result = listingContext.WorkingHours.Any(i => i.WorkingHoursID == id);
            return result;
        }

        public bool Kilometer10Exists(int id)
        {
            throw new NotImplementedException();
        }

        public bool BranchesExists(int id)
        {
            var result = listingContext.Branches.Any(i => i.BranchID == id);
            return result;
        }

        public bool LogoExists(int id)
        {
            string directoryPath = webHost.WebRootPath + "\\FileManager\\ListingLogo\\";
            DirectoryInfo directory = new DirectoryInfo(directoryPath);
            string filePath = directoryPath + id + ".jpg";
            if(File.Exists(filePath))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ThumbnailExists(int id)
        {
            string directoryPath = webHost.WebRootPath + "\\FileManager\\ListingThumbnail\\";
            DirectoryInfo directory = new DirectoryInfo(directoryPath);
            string filePath = directoryPath + id + ".jpg";
            if (File.Exists(filePath))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool OwnerPhotoExists(int id)
        {
            string directoryPath = webHost.WebRootPath + "\\FileManager\\ListingOwnerPhoto\\";
            DirectoryInfo directory = new DirectoryInfo(directoryPath);
            string filePath = directoryPath + id + ".jpg";
            if (File.Exists(filePath))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        // End:

        // Shafi: Ownership Verification
        public async Task<bool> CompanyOwnerAsync(int ListingID, string UserGuid)
        {
            var OwnerGuid = await listingContext.Listing.Where(p => p.ListingID == ListingID).Select(i => i.OwnerGuid).FirstOrDefaultAsync();

            if (UserGuid == OwnerGuid)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> SocialOwnerAsync(int SocialNetworkID, int ListingID, string UserGuid)
        {
            var OwnerGuid = await listingContext.SocialNetwork.Where(p => p.SocialNetworkID == SocialNetworkID && p.ListingID == ListingID).Select(i => i.OwnerGuid).FirstOrDefaultAsync();

            if (UserGuid == OwnerGuid)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> CertificationOwnerAsync(int CertificationID, int ListingID, string UserGuid)
        {
            var OwnerGuid = await listingContext.Certification.Where(p => p.CertificationID == CertificationID && p.ListingID == ListingID).Select(i => i.OwnerGuid).FirstOrDefaultAsync();

            if (UserGuid == OwnerGuid)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> CommunicationOwnerAsync(int CommunicationID, int ListingID, string UserGuid)
        {
            var OwnerGuid = await listingContext.Communication.Where(p => p.CommunicationID == CommunicationID && p.ListingID == ListingID).Select(i => i.OwnerGuid).FirstOrDefaultAsync();

            if (UserGuid == OwnerGuid)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> AddressOwnerAsync(int AddressID, int ListingID, string UserGuid)
        {
            var OwnerGuid = await listingContext.Address.Where(a => a.AddressID == AddressID && a.ListingID == ListingID).Select(i => i.OwnerGuid).FirstOrDefaultAsync();

            if (UserGuid == OwnerGuid)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> CategoryOwnerAsync(int CategoryID, int ListingID, string UserGuid)
        {
            var OwnerGuid = await listingContext.Categories.Where(p => p.CategoryID == CategoryID && p.ListingID == ListingID).Select(i => i.OwnerGuid).FirstOrDefaultAsync();

            if (UserGuid == OwnerGuid)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<bool> ProfileOwnerAsync(int ProfileID, int ListingID, string UserGuid)
        {
            var OwnerGuid = await listingContext.Profile.Where(p => p.ProfileID == ProfileID && p.ListingID == ListingID).Select(i => i.OwnerGuid).FirstOrDefaultAsync();

            if (UserGuid == OwnerGuid)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> PaymentOwnerAsync(int PaymentID, int ListingID, string UserGuid)
        {
            var OwnerGuid = await listingContext.PaymentMode.Where(p => p.PaymentID == PaymentID && p.ListingID == ListingID).Select(i => i.OwnerGuid).FirstOrDefaultAsync();

            if (UserGuid == OwnerGuid)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> WorkingOwnerAsync(int WorkingHoursID, int ListingID, string UserGuid)
        {
            var OwnerGuid = await listingContext.WorkingHours.Where(p => p.WorkingHoursID == WorkingHoursID && p.ListingID == ListingID).Select(i => i.OwnerGuid).FirstOrDefaultAsync();

            if (UserGuid == OwnerGuid)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> SpecialisationOwnerAsync(int SpecialisationID, int ListingID, string UserGuid)
        {
            var OwnerGuid = await listingContext.Specialisation.Where(p => p.SpecialisationID == SpecialisationID && p.ListingID == ListingID).Select(i => i.OwnerGuid).FirstOrDefaultAsync();

            if (UserGuid == OwnerGuid)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> BranchesOwnerAsync(int BranchID, int ListingID, string UserGuid)
        {
            var OwnerGuid = await listingContext.Branches.Where(p => p.BranchID == BranchID && p.ListingID == ListingID).Select(i => i.OwnerGuid).FirstOrDefaultAsync();

            if (UserGuid == OwnerGuid)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        // End:

        // Shafi: Get Plans
        public async Task<IEnumerable<Plan>> GetPlansAsync()
        {
            var plans = await billingContext.Plan.ToListAsync();
            return plans;
        }
        // End:

        // Shafi: Get Plan Name
        public string GetPlanName(string PlanType, int? PlanID)
        {
            if (PlanType == "Listing Plans")
            {
                var planName = billingContext.Plan.Where(p => p.PlanID == PlanID).Select(p => p.Name).FirstOrDefault();
                return planName.ToString();
            }
            else if (PlanType == "Banner Plans")
            {
                var planName = billingContext.BannerPlan.Where(p => p.PlanID == PlanID).Select(p => p.Name).FirstOrDefault();
                return planName.ToString();
            }
            else if (PlanType == "Advertisement Plans")
            {
                var planName = billingContext.AdvertisementPlan.Where(p => p.PlanID == PlanID).Select(p => p.Name).FirstOrDefault();
                return planName.ToString();
            }
            else if (PlanType == "Data Plans")
            {
                var planName = billingContext.DataPlan.Where(p => p.PlanID == PlanID).Select(p => p.Name).FirstOrDefault();
                return planName.ToString();
            }
            else if (PlanType == "SMS Plans")
            {
                var planName = billingContext.SMSPlans.Where(p => p.PlanID == PlanID).Select(p => p.Name).FirstOrDefault();
                return planName.ToString();
            }
            else if (PlanType == "Email Plans")
            {
                var planName = billingContext.EmailPlans.Where(p => p.PlanID == PlanID).Select(p => p.Name).FirstOrDefault();
                return planName.ToString();
            }
            else
            {
                var planName = billingContext.MagazinePlan.Where(p => p.PlanID == PlanID).Select(p => p.Name).FirstOrDefault();
                return planName.ToString();
            }
        }
        // End:

        // Shafi: Manage Rating
        public async Task CreateRatingAsync(int ListingID, string OwnerGuid, string IPAddress, DateTime Date, DateTime Time, int Ratings, string Comment, string UserEmail)
        {

            Rating rating = new Rating()
            {
                ListingID = ListingID,
                OwnerGuid = OwnerGuid,
                IPAddress = IPAddress,
                Date = Date,
                Time = Time,
                Ratings = Ratings,
                Comment = Comment
            };


            await listingContext.AddAsync(rating);
            await listingContext.SaveChangesAsync();

            // Shafi: Find listing and communication details
            var listing = await listingContext.Listing.Where(l => l.ListingID == ListingID).FirstOrDefaultAsync();
            var communication = await listingContext.Communication.Where(l => l.ListingID == ListingID).FirstOrDefaultAsync();
            // End:

            // Shafi: User Notification
            string listingUrl = "https://localhost:44314/Listing/Index/" + listing.ListingID;
            var webRoot = webHost.WebRootPath;
            var notificationUser = System.IO.Path.Combine(webRoot, "Email-Templates", "Review-Received-User.html");
            string emailMsgUser = System.IO.File.ReadAllText(notificationUser, Encoding.UTF8).Replace("{company}", listing.CompanyName).Replace("{listingUrl}", listingUrl).Replace("{comment}", Comment);
            notification.SendEmail(UserEmail, $"You posted review for {listing.CompanyName}", emailMsgUser);
            // End:

            // Shafi: Self Notification
            // Email
            var notificationSelf = System.IO.Path.Combine(webRoot, "Email-Templates", "Review-Received-Self.html");
            string emailMsgSelf = System.IO.File.ReadAllText(notificationSelf, Encoding.UTF8).Replace("{company}", listing.CompanyName).Replace("{listingUrl}", listingUrl).Replace("{comment}", Comment).Replace("{UserEmail}", UserEmail);
            notification.SendEmail("myinteriormart@gmail.com", $"Notification: User posted review for {listing.CompanyName}", emailMsgSelf);

            // SMS
            notification.SendSMS("9819007720", $"Review received for listing {listing.CompanyName} by user {UserEmail}. Open listing {listingUrl}");
            // End:

            // Shafi: Customer Notification
            var notificationCustomer = System.IO.Path.Combine(webRoot, "Email-Templates", "Review-Received-Customer.html");
            string emailMsgCustomer = System.IO.File.ReadAllText(notificationCustomer, Encoding.UTF8).Replace("{company}", listing.CompanyName).Replace("{listingUrl}", listingUrl).Replace("{comment}", Comment).Replace("name", listing.Name);
            notification.SendEmail(communication.Email, $"You received review for {listing.CompanyName}", emailMsgCustomer);
            // SMS
            notification.SendSMS(communication.Mobile, $"Review received for your listing {listing.CompanyName}. Open listing {listingUrl} for any help call us on 7700995500.");
            // End:
        }

        public async Task<bool> ReviewExistsAsync(int ListingID, string OwnerGuid)
        {
            var result = await listingContext.Rating.Where(r => r.ListingID == ListingID && r.OwnerGuid == OwnerGuid).FirstOrDefaultAsync();

            if (result != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<IEnumerable<Rating>> GetRatingAsync(int ListingID)
        {
            var ratings = await listingContext.Rating.Where(r => r.ListingID == ListingID).ToListAsync();
            return ratings;
        }

        public async Task<Rating> RatingDetailsAsync(int ListingID)
        {
            var rating = await listingContext.Rating.Where(r => r.ListingID == ListingID).FirstOrDefaultAsync();
            return rating;
        }

        public async Task<string> RatingAverageAsync(int ListingID)
        {
            // Shafi: Get rating average
            var allRating = await listingContext.Rating.Where(r => r.ListingID == ListingID).ToListAsync();
            var ratingCount = allRating.Count();

            if (ratingCount > 0)
            {
                var R1 = await CountRatingAsync(ListingID, 1);
                var R2 = await CountRatingAsync(ListingID, 2);
                var R3 = await CountRatingAsync(ListingID, 3);
                var R4 = await CountRatingAsync(ListingID, 4);
                var R5 = await CountRatingAsync(ListingID, 5);

                decimal averageCount = 5 * R5 + 4 * R4 + 3 * R3 + 2 * R2 + 1 * R1;
                decimal weightedCount = R5 + R4 + R3 + R2 + R1;
                decimal ratingAverage = averageCount / weightedCount;

                return ratingAverage.ToString("0.0");
            }
            else
            {
                return "0";
            }
            // End:
        }

        public async Task<int> CountRatingAsync(int ListingID, int rating)
        {
            var count = await listingContext.Rating.Where(r => r.ListingID == ListingID && r.Ratings == rating).CountAsync();
            return count;
        }

        public async Task<int> CountListingReviewsAsync(int ListingID)
        {
            var count = await listingContext.Rating.Where(r => r.ListingID == ListingID).CountAsync();
            return count;
        }

        public async Task<int> CountListingLikesAsync(int ListingID)
        {
            var count = await auditContext.ListingLikeDislike.Where(r => r.ListingID == ListingID).CountAsync();
            return count;
        }

        public async Task<int> CountListingSubscribeAsync(int ListingID)
        {
            var count = await auditContext.Subscribes.Where(r => r.ListingID == ListingID).CountAsync();
            return count;
        }

        public async Task<int> CountListingBookmarkAsync(int ListingID)
        {
            var count = await auditContext.Bookmarks.Where(r => r.ListingID == ListingID).CountAsync();
            return count;
        }

        public async Task<int> CountListingViewsAsync(int ListingID)
        {
            var count = await listingContext.ListingViews.Where(r => r.ListingID == ListingID).CountAsync();
            return count;
        }

        // Shafi: Get all listing details
        public async Task<Listing> GetListingDetailsAsync(int ListingID)
        {
            var listing = await listingContext.Listing.Where(r => r.ListingID == ListingID).FirstOrDefaultAsync();

            return listing;
        }

        public async Task<Communication> CommunicationDetailsAsync(int ListingID)
        {
            var communication = await listingContext.Communication.Where(r => r.ListingID == ListingID).FirstOrDefaultAsync();
            return communication;
        }
        // End:

        // Shafi: Begin analytics
        public async Task IncrementViewCountByOneAsync(int? ListingID)
        {
            if (ListingID != null)
            {
                var record = await listingContext.ListingViewCount.Where(c => c.ListingID == ListingID).FirstOrDefaultAsync();

                // Shafi: Check if record exisits
                if (record != null)
                {
                    // Shafi: Get last count then increment by 1
                    var lastCount = record.ViewCount;
                    record.ViewCount = lastCount + 1;
                    // End:

                    // Shafi: Update record
                    listingContext.Update(record);
                    await listingContext.SaveChangesAsync();
                    // End:
                }
                // Shafi: Execute this if record does not exists
                else
                {
                    // Shafi: Create new record
                    ListingViewCount count = new ListingViewCount();

                    // Shafi: Add details to record
                    count.ListingID = ListingID.Value;
                    count.ViewCount = 1;

                    // Shafi: Create record and save changes
                    await listingContext.AddAsync(count);
                    await listingContext.SaveChangesAsync();
                    // End:
                }
                // End:
            }
        }

        public async Task RecordListingViewAsync(int? ListingID, string UserType, string OwnerGuid, string IPAddress, DateTime Date, string Country, string City, string Pincode, string State, string IPV4, string Latitude, string Longitude)
        {
            if (ListingID != null)
            {
                ListingViews listingView = new ListingViews();
                listingView.ListingID = ListingID.Value;
                listingView.UserType = UserType;
                listingView.OwnerGuid = OwnerGuid;
                listingView.IPAddress = IPAddress;
                listingView.Date = Date;
                listingView.Country = Country;
                listingView.City = City;
                listingView.Pincode = Pincode;
                listingView.State = State;
                listingView.IPv4 = IPV4;
                listingView.Latitude = Latitude;
                listingView.Longitude = Longitude;

                // Shafi: Update record
                await listingContext.AddAsync(listingView);
                await listingContext.SaveChangesAsync();
                // End:
            }
        }

        public async Task<IEnumerable<ListingViews>> GetListingViewsAsync(int ListingID)
        {

            //DateTime date = DateTime.Now.AddDays(-Days);
            var result = await listingContext.ListingViews
                .Where(x => x.ListingID == ListingID)
                .OrderByDescending(x => x.ListingID)
                .ToListAsync();
            return result;
        }
        // End:

        // Shafi: Check if business is open or close
        public async Task<string> BusinessOpenOrCloseAsync(int ListingID)
        {
            // Shafi: Begin: Get timing of listing
            var listingTiming = await listingContext.WorkingHours.Where(w => w.ListingID == ListingID).FirstOrDefaultAsync();
            // End:

            // Shafi: Get Time Zone
            DateTime timeZoneDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
            string day = timeZoneDate.ToString("dddd");
            string time = timeZoneDate.ToString("hh:mm tt");
            DateTime currentTime = DateTime.Parse(time, System.Globalization.CultureInfo.CurrentCulture);
            // End:

            // Shafi: Business Open Now & Close
            if (day == "Monday")
            {
                // Shafi: Get ToTime in ("hh:mm tt") format then convert it to string then create date time from it
                string timeString = listingTiming.MondayTo.ToString("hh:mm tt");
                DateTime ToTime = DateTime.Parse(timeString, System.Globalization.CultureInfo.CurrentCulture);
                // End:

                if (currentTime > ToTime)
                {
                    return "Closed Now";
                }
                else
                {
                    return "Open Now";
                }
            }
            else if (day == "Tuesday")
            {
                // Shafi: Get ToTime in ("hh:mm tt") format then convert it to string then create date time from it
                string timeString = listingTiming.TuesdayTo.ToString("hh:mm tt");
                DateTime ToTime = DateTime.Parse(timeString, System.Globalization.CultureInfo.CurrentCulture);
                // End:

                if (currentTime > ToTime)
                {
                    return "Closed Now";
                }
                else
                {
                    return "Open Now";
                }
            }
            else if (day == "Wednesday")
            {
                // Shafi: Get ToTime in ("hh:mm tt") format then convert it to string then create date time from it
                string timeString = listingTiming.WednesdayTo.ToString("hh:mm tt");
                DateTime ToTime = DateTime.Parse(timeString, System.Globalization.CultureInfo.CurrentCulture);
                // End:

                if (currentTime > ToTime)
                {
                    return "Closed Now";
                }
                else
                {
                    return "Open Now";
                }
            }
            else if (day == "Thursday")
            {
                // Shafi: Get ToTime in ("hh:mm tt") format then convert it to string then create date time from it
                string timeString = listingTiming.ThursdayTo.ToString("hh:mm tt");
                DateTime ToTime = DateTime.Parse(timeString, System.Globalization.CultureInfo.CurrentCulture);
                // End:

                if (currentTime > ToTime)
                {
                    return "Closed Now";
                }
                else
                {
                    return "Open Now";
                }
            }
            else if (day == "Friday")
            {
                // Shafi: Get ToTime in ("hh:mm tt") format then convert it to string then create date time from it
                string timeString = listingTiming.FridayTo.ToString("hh:mm tt");
                DateTime ToTime = DateTime.Parse(timeString, System.Globalization.CultureInfo.CurrentCulture);
                // End:

                if (currentTime > ToTime)
                {
                    return "Closed Now";
                }
                else
                {
                    return "Open Now";
                }
            }
            else if (day == "Saturday" && listingTiming.SaturdayHoliday == false)
            {
                // Shafi: Get ToTime in ("hh:mm tt") format then convert it to string then create date time from it
                string timeString = listingTiming.SaturdayTo.ToString("hh:mm tt");
                DateTime ToTime = DateTime.Parse(timeString, System.Globalization.CultureInfo.CurrentCulture);
                // End:

                if (currentTime > ToTime)
                {
                    return "Closed Now";
                }
                else
                {
                    return "Open Now";
                }
            }
            else if (day == "Saturday" && listingTiming.SaturdayHoliday != true)
            {
                return "Closed Now";
            }
            else if (day == "Sunday" && listingTiming.SundayHoliday == false)
            {
                // Shafi: Get ToTime in ("hh:mm tt") format then convert it to string then create date time from it
                string timeString = listingTiming.SundayTo.ToString("hh:mm tt");
                DateTime ToTime = DateTime.Parse(timeString, System.Globalization.CultureInfo.CurrentCulture);
                // End:

                if (currentTime > ToTime)
                {
                    return "Closed Now";
                }
                else
                {
                    return "Open Now";
                }
            }
            else if (day == "Sunday" && listingTiming.SundayHoliday != true)
            {
                return "Closed Now";
            }
            else
            {
                return "Open Now";
            }
        }

        // Shafi: Function to count views and get day name
        public string GetDayName(int days)
        {
            DateTime date = DateTime.Now.AddDays(-days);
            return date.ToString("d ddd");
        }

        public async Task<int> CountViewsAsync(int days, int ListingId)
        {
            DateTime date = DateTime.Now.AddDays(-days);
            var result = await listingContext.ListingViews.Where(x => x.Date.Day == date.Day && x.Date.Month == date.Month && x.Date.Year == date.Year && x.ListingID == ListingId).CountAsync();
            return result;
        }

        public async Task<int> CountLikesAsync(int days, int ListingId)
        {
            DateTime date = DateTime.Now.AddDays(-days);
            var result = await auditContext.ListingLikeDislike.Where(x => x.VisitDate.Day == date.Day && x.VisitDate.Month == x.VisitDate.Month && x.VisitDate.Year == date.Year && x.ListingID == ListingId).CountAsync();
            return result;
        }

        public async Task<int> CountSubscribesAsync(int days, int ListingId)
        {
            DateTime date = DateTime.Now.AddDays(-days);
            var result = await auditContext.Subscribes.Where(x => x.VisitDate.Day == date.Day && x.VisitDate.Month == x.VisitDate.Month && x.VisitDate.Year == date.Year && x.ListingID == ListingId).CountAsync();
            return result;
        }

        public async Task<int> CountBookmarksAsync(int days, int ListingId)
        {
            DateTime date = DateTime.Now.AddDays(-days);
            var result = await auditContext.Bookmarks.Where(x => x.VisitDate.Day == date.Day && x.VisitDate.Month == x.VisitDate.Month && x.VisitDate.Year == date.Year && x.ListingID == ListingId).CountAsync();
            return result;
        }

        public async Task<int> CountReviewAsync(int days, int ListingId)
        {
            DateTime date = DateTime.Now.AddDays(-days);
            var result = await listingContext.Rating.Where(x => x.Date.Day == date.Day && x.Date.Month == x.Date.Month && x.Date.Year == date.Year && x.ListingID == ListingId).CountAsync();
            return result;
        }
        // End:

        public async Task<DashboardListingLast30DaysViews> GetLast30DaysListingViewsAsync(int ListingID)
        {
            var count1 = await CountViewsAsync(0, ListingID);
            var count2 = await CountViewsAsync(1, ListingID);
            var count3 = await CountViewsAsync(2, ListingID);
            var count4 = await CountViewsAsync(3, ListingID);
            var count5 = await CountViewsAsync(4, ListingID);
            var count6 = await CountViewsAsync(5, ListingID);
            var count7 = await CountViewsAsync(6, ListingID);
            var count8 = await CountViewsAsync(7, ListingID);
            var count9 = await CountViewsAsync(8, ListingID);
            var count10 = await CountViewsAsync(9, ListingID);
            var count11 = await CountViewsAsync(10, ListingID);
            var count12 = await CountViewsAsync(11, ListingID);
            var count13 = await CountViewsAsync(12, ListingID);
            var count14 = await CountViewsAsync(13, ListingID);
            var count15 = await CountViewsAsync(14, ListingID);
            var count16 = await CountViewsAsync(15, ListingID);
            var count17 = await CountViewsAsync(16, ListingID);
            var count18 = await CountViewsAsync(17, ListingID);
            var count19 = await CountViewsAsync(18, ListingID);
            var count20 = await CountViewsAsync(19, ListingID);
            var count21 = await CountViewsAsync(20, ListingID);
            var count22 = await CountViewsAsync(21, ListingID);
            var count23 = await CountViewsAsync(22, ListingID);
            var count24 = await CountViewsAsync(23, ListingID);
            var count25 = await CountViewsAsync(24, ListingID);
            var count26 = await CountViewsAsync(25, ListingID);
            var count27 = await CountViewsAsync(26, ListingID);
            var count28 = await CountViewsAsync(27, ListingID);
            var count29 = await CountViewsAsync(28, ListingID);
            var count30 = await CountViewsAsync(29, ListingID);
            DashboardListingLast30DaysViews view = new DashboardListingLast30DaysViews() {
                // One:
                One = GetDayName(0),
                OneCount = count1,

                // Two:
                Two = GetDayName(1),
                TwoCount = count2,

                // Three:
                Three = GetDayName(2),
                ThreeCount = count3,

                // Four:
                Four = GetDayName(3),
                FourCount = count4,

                // Five:
                Five = GetDayName(4),
                FiveCount = count5,

                // Six:
                Six = GetDayName(5),
                SixCount = count6,

                // Seven:
                Seven = GetDayName(6),
                SevenCount = count7,

                // Eight:
                Eight = GetDayName(7),
                EightCount = count8,

                // Nine:
                Nine = GetDayName(8),
                NineCount = count9,

                // Ten:
                Ten = GetDayName(9),
                TenCount = count10,

                // Eleven:
                Eleven = GetDayName(10),
                ElevenCount = count11,

                // Twelve:
                Twelve = GetDayName(11),
                TwelveCount = count12,

                // Thirteen:
                Thirteen = GetDayName(12),
                ThirteenCount = count13,

                // Fourteen:
                Fourteen = GetDayName(13),
                FourteenCount = count14,

                // Fifteen:
                Fifteen = GetDayName(14),
                FifteenCount = count15,

                // Sixteen:
                Sixteen = GetDayName(15),
                SixteenCount = count16,

                // Seventeen:
                Seventeen = GetDayName(16),
                SeventeenCount = count17,

                // Eighteen:
                Eighteen = GetDayName(17),
                EighteenCount = count18,

                // Nineteen:
                Nineteen = GetDayName(18),
                NineteenCount = count19,

                // Twenty:
                Twenty = GetDayName(19),
                TwentyCount = count20,

                // TwentyOne:
                TwentyOne = GetDayName(20),
                TwentyOneCount = count21,

                // TwentyTwo:
                TwentyTwo = GetDayName(21),
                TwentyTwoCount = count22,

                // TwentyThree:
                TwentyThree = GetDayName(22),
                TwentyThreeCount = count23,

                // TwentyFour:
                TwentyFour = GetDayName(23),
                TwentyFourCount = count24,

                // TwentyFive:
                TwentyFive = GetDayName(24),
                TwentyFiveCount = count25,

                // TwentySix:
                TwentySix = GetDayName(25),
                TwentySixCount = count26,

                // TwentySeven:
                TwentySeven = GetDayName(26),
                TwentySevenCount = count27,

                // TwentyEight:
                TwentyEight = GetDayName(27),
                TwentyEightCount = count28,

                // TwentyNine:
                TwentyNine = GetDayName(28),
                TwentyNineCount = count29,

                // TwentyNine:
                Thirty = GetDayName(29),
                ThirtyCount = count30
            };

            return view;
        }

        public async Task<DashboardListingLast30DaysLikes> GetLast30DaysListingLikeAsync(int ListingID)
        {
            var count1 = await CountLikesAsync(0, ListingID);
            var count2 = await CountLikesAsync(1, ListingID);
            var count3 = await CountLikesAsync(2, ListingID);
            var count4 = await CountLikesAsync(3, ListingID);
            var count5 = await CountLikesAsync(4, ListingID);
            var count6 = await CountLikesAsync(5, ListingID);
            var count7 = await CountLikesAsync(6, ListingID);
            var count8 = await CountLikesAsync(7, ListingID);
            var count9 = await CountLikesAsync(8, ListingID);
            var count10 = await CountLikesAsync(9, ListingID);
            var count11 = await CountLikesAsync(10, ListingID);
            var count12 = await CountLikesAsync(11, ListingID);
            var count13 = await CountLikesAsync(12, ListingID);
            var count14 = await CountLikesAsync(13, ListingID);
            var count15 = await CountLikesAsync(14, ListingID);
            var count16 = await CountLikesAsync(15, ListingID);
            var count17 = await CountLikesAsync(16, ListingID);
            var count18 = await CountLikesAsync(17, ListingID);
            var count19 = await CountLikesAsync(18, ListingID);
            var count20 = await CountLikesAsync(19, ListingID);
            var count21 = await CountLikesAsync(20, ListingID);
            var count22 = await CountLikesAsync(21, ListingID);
            var count23 = await CountLikesAsync(22, ListingID);
            var count24 = await CountLikesAsync(23, ListingID);
            var count25 = await CountLikesAsync(24, ListingID);
            var count26 = await CountLikesAsync(25, ListingID);
            var count27 = await CountLikesAsync(26, ListingID);
            var count28 = await CountLikesAsync(27, ListingID);
            var count29 = await CountLikesAsync(28, ListingID);
            var count30 = await CountLikesAsync(29, ListingID);
            DashboardListingLast30DaysLikes likes = new DashboardListingLast30DaysLikes()
            {
                // One:
                One = GetDayName(0),
                OneCount = count1,

                // Two:
                Two = GetDayName(1),
                TwoCount = count2,

                // Three:
                Three = GetDayName(2),
                ThreeCount = count3,

                // Four:
                Four = GetDayName(3),
                FourCount = count4,

                // Five:
                Five = GetDayName(4),
                FiveCount = count5,

                // Six:
                Six = GetDayName(5),
                SixCount = count6,

                // Seven:
                Seven = GetDayName(6),
                SevenCount = count7,

                // Eight:
                Eight = GetDayName(7),
                EightCount = count8,

                // Nine:
                Nine = GetDayName(8),
                NineCount = count9,

                // Ten:
                Ten = GetDayName(9),
                TenCount = count10,

                // Eleven:
                Eleven = GetDayName(10),
                ElevenCount = count11,

                // Twelve:
                Twelve = GetDayName(11),
                TwelveCount = count12,

                // Thirteen:
                Thirteen = GetDayName(12),
                ThirteenCount = count13,

                // Fourteen:
                Fourteen = GetDayName(13),
                FourteenCount = count14,

                // Fifteen:
                Fifteen = GetDayName(14),
                FifteenCount = count15,

                // Sixteen:
                Sixteen = GetDayName(15),
                SixteenCount = count16,

                // Seventeen:
                Seventeen = GetDayName(16),
                SeventeenCount = count17,

                // Eighteen:
                Eighteen = GetDayName(17),
                EighteenCount = count18,

                // Nineteen:
                Nineteen = GetDayName(18),
                NineteenCount = count19,

                // Twenty:
                Twenty = GetDayName(19),
                TwentyCount = count20,

                // TwentyOne:
                TwentyOne = GetDayName(20),
                TwentyOneCount = count21,

                // TwentyTwo:
                TwentyTwo = GetDayName(21),
                TwentyTwoCount = count22,

                // TwentyThree:
                TwentyThree = GetDayName(22),
                TwentyThreeCount = count23,

                // TwentyFour:
                TwentyFour = GetDayName(23),
                TwentyFourCount = count24,

                // TwentyFive:
                TwentyFive = GetDayName(24),
                TwentyFiveCount = count25,

                // TwentySix:
                TwentySix = GetDayName(25),
                TwentySixCount = count26,

                // TwentySeven:
                TwentySeven = GetDayName(26),
                TwentySevenCount = count27,

                // TwentyEight:
                TwentyEight = GetDayName(27),
                TwentyEightCount = count28,

                // TwentyNine:
                TwentyNine = GetDayName(28),
                TwentyNineCount = count29,

                // TwentyNine:
                Thirty = GetDayName(29),
                ThirtyCount = count30
            };

            return likes;
        }

        public async Task<DashboardListingLast30DaysSubscribes> GetLast30DaysListingSubscribesAsync(int ListingID)
        {
            var count1 = await CountSubscribesAsync(0, ListingID);
            var count2 = await CountSubscribesAsync(1, ListingID);
            var count3 = await CountSubscribesAsync(2, ListingID);
            var count4 = await CountSubscribesAsync(3, ListingID);
            var count5 = await CountSubscribesAsync(4, ListingID);
            var count6 = await CountSubscribesAsync(5, ListingID);
            var count7 = await CountSubscribesAsync(6, ListingID);
            var count8 = await CountSubscribesAsync(7, ListingID);
            var count9 = await CountSubscribesAsync(8, ListingID);
            var count10 = await CountSubscribesAsync(9, ListingID);
            var count11 = await CountSubscribesAsync(10, ListingID);
            var count12 = await CountSubscribesAsync(11, ListingID);
            var count13 = await CountSubscribesAsync(12, ListingID);
            var count14 = await CountSubscribesAsync(13, ListingID);
            var count15 = await CountSubscribesAsync(14, ListingID);
            var count16 = await CountSubscribesAsync(15, ListingID);
            var count17 = await CountSubscribesAsync(16, ListingID);
            var count18 = await CountSubscribesAsync(17, ListingID);
            var count19 = await CountSubscribesAsync(18, ListingID);
            var count20 = await CountSubscribesAsync(19, ListingID);
            var count21 = await CountSubscribesAsync(20, ListingID);
            var count22 = await CountSubscribesAsync(21, ListingID);
            var count23 = await CountSubscribesAsync(22, ListingID);
            var count24 = await CountSubscribesAsync(23, ListingID);
            var count25 = await CountSubscribesAsync(24, ListingID);
            var count26 = await CountSubscribesAsync(25, ListingID);
            var count27 = await CountSubscribesAsync(26, ListingID);
            var count28 = await CountSubscribesAsync(27, ListingID);
            var count29 = await CountSubscribesAsync(28, ListingID);
            var count30 = await CountSubscribesAsync(29, ListingID);
            DashboardListingLast30DaysSubscribes subscribes = new DashboardListingLast30DaysSubscribes()
            {
                // One:
                One = GetDayName(0),
                OneCount = count1,

                // Two:
                Two = GetDayName(1),
                TwoCount = count2,

                // Three:
                Three = GetDayName(2),
                ThreeCount = count3,

                // Four:
                Four = GetDayName(3),
                FourCount = count4,

                // Five:
                Five = GetDayName(4),
                FiveCount = count5,

                // Six:
                Six = GetDayName(5),
                SixCount = count6,

                // Seven:
                Seven = GetDayName(6),
                SevenCount = count7,

                // Eight:
                Eight = GetDayName(7),
                EightCount = count8,

                // Nine:
                Nine = GetDayName(8),
                NineCount = count9,

                // Ten:
                Ten = GetDayName(9),
                TenCount = count10,

                // Eleven:
                Eleven = GetDayName(10),
                ElevenCount = count11,

                // Twelve:
                Twelve = GetDayName(11),
                TwelveCount = count12,

                // Thirteen:
                Thirteen = GetDayName(12),
                ThirteenCount = count13,

                // Fourteen:
                Fourteen = GetDayName(13),
                FourteenCount = count14,

                // Fifteen:
                Fifteen = GetDayName(14),
                FifteenCount = count15,

                // Sixteen:
                Sixteen = GetDayName(15),
                SixteenCount = count16,

                // Seventeen:
                Seventeen = GetDayName(16),
                SeventeenCount = count17,

                // Eighteen:
                Eighteen = GetDayName(17),
                EighteenCount = count18,

                // Nineteen:
                Nineteen = GetDayName(18),
                NineteenCount = count19,

                // Twenty:
                Twenty = GetDayName(19),
                TwentyCount = count20,

                // TwentyOne:
                TwentyOne = GetDayName(20),
                TwentyOneCount = count21,

                // TwentyTwo:
                TwentyTwo = GetDayName(21),
                TwentyTwoCount = count22,

                // TwentyThree:
                TwentyThree = GetDayName(22),
                TwentyThreeCount = count23,

                // TwentyFour:
                TwentyFour = GetDayName(23),
                TwentyFourCount = count24,

                // TwentyFive:
                TwentyFive = GetDayName(24),
                TwentyFiveCount = count25,

                // TwentySix:
                TwentySix = GetDayName(25),
                TwentySixCount = count26,

                // TwentySeven:
                TwentySeven = GetDayName(26),
                TwentySevenCount = count27,

                // TwentyEight:
                TwentyEight = GetDayName(27),
                TwentyEightCount = count28,

                // TwentyNine:
                TwentyNine = GetDayName(28),
                TwentyNineCount = count29,

                // TwentyNine:
                Thirty = GetDayName(29),
                ThirtyCount = count30
            };

            return subscribes;
        }

        public async Task<DashboardListingLast30DaysBookmarks> GetLast30DaysListingBookmarksAsync(int ListingID)
        {
            var count1 = await CountBookmarksAsync(0, ListingID);
            var count2 = await CountBookmarksAsync(1, ListingID);
            var count3 = await CountBookmarksAsync(2, ListingID);
            var count4 = await CountBookmarksAsync(3, ListingID);
            var count5 = await CountBookmarksAsync(4, ListingID);
            var count6 = await CountBookmarksAsync(5, ListingID);
            var count7 = await CountBookmarksAsync(6, ListingID);
            var count8 = await CountBookmarksAsync(7, ListingID);
            var count9 = await CountBookmarksAsync(8, ListingID);
            var count10 = await CountBookmarksAsync(9, ListingID);
            var count11 = await CountBookmarksAsync(10, ListingID);
            var count12 = await CountBookmarksAsync(11, ListingID);
            var count13 = await CountBookmarksAsync(12, ListingID);
            var count14 = await CountBookmarksAsync(13, ListingID);
            var count15 = await CountBookmarksAsync(14, ListingID);
            var count16 = await CountBookmarksAsync(15, ListingID);
            var count17 = await CountBookmarksAsync(16, ListingID);
            var count18 = await CountBookmarksAsync(17, ListingID);
            var count19 = await CountBookmarksAsync(18, ListingID);
            var count20 = await CountBookmarksAsync(19, ListingID);
            var count21 = await CountBookmarksAsync(20, ListingID);
            var count22 = await CountBookmarksAsync(21, ListingID);
            var count23 = await CountBookmarksAsync(22, ListingID);
            var count24 = await CountBookmarksAsync(23, ListingID);
            var count25 = await CountBookmarksAsync(24, ListingID);
            var count26 = await CountBookmarksAsync(25, ListingID);
            var count27 = await CountBookmarksAsync(26, ListingID);
            var count28 = await CountBookmarksAsync(27, ListingID);
            var count29 = await CountBookmarksAsync(28, ListingID);
            var count30 = await CountBookmarksAsync(29, ListingID);
            DashboardListingLast30DaysBookmarks bookmarks = new DashboardListingLast30DaysBookmarks()
            {
                // One:
                One = GetDayName(0),
                OneCount = count1,

                // Two:
                Two = GetDayName(1),
                TwoCount = count2,

                // Three:
                Three = GetDayName(2),
                ThreeCount = count3,

                // Four:
                Four = GetDayName(3),
                FourCount = count4,

                // Five:
                Five = GetDayName(4),
                FiveCount = count5,

                // Six:
                Six = GetDayName(5),
                SixCount = count6,

                // Seven:
                Seven = GetDayName(6),
                SevenCount = count7,

                // Eight:
                Eight = GetDayName(7),
                EightCount = count8,

                // Nine:
                Nine = GetDayName(8),
                NineCount = count9,

                // Ten:
                Ten = GetDayName(9),
                TenCount = count10,

                // Eleven:
                Eleven = GetDayName(10),
                ElevenCount = count11,

                // Twelve:
                Twelve = GetDayName(11),
                TwelveCount = count12,

                // Thirteen:
                Thirteen = GetDayName(12),
                ThirteenCount = count13,

                // Fourteen:
                Fourteen = GetDayName(13),
                FourteenCount = count14,

                // Fifteen:
                Fifteen = GetDayName(14),
                FifteenCount = count15,

                // Sixteen:
                Sixteen = GetDayName(15),
                SixteenCount = count16,

                // Seventeen:
                Seventeen = GetDayName(16),
                SeventeenCount = count17,

                // Eighteen:
                Eighteen = GetDayName(17),
                EighteenCount = count18,

                // Nineteen:
                Nineteen = GetDayName(18),
                NineteenCount = count19,

                // Twenty:
                Twenty = GetDayName(19),
                TwentyCount = count20,

                // TwentyOne:
                TwentyOne = GetDayName(20),
                TwentyOneCount = count21,

                // TwentyTwo:
                TwentyTwo = GetDayName(21),
                TwentyTwoCount = count22,

                // TwentyThree:
                TwentyThree = GetDayName(22),
                TwentyThreeCount = count23,

                // TwentyFour:
                TwentyFour = GetDayName(23),
                TwentyFourCount = count24,

                // TwentyFive:
                TwentyFive = GetDayName(24),
                TwentyFiveCount = count25,

                // TwentySix:
                TwentySix = GetDayName(25),
                TwentySixCount = count26,

                // TwentySeven:
                TwentySeven = GetDayName(26),
                TwentySevenCount = count27,

                // TwentyEight:
                TwentyEight = GetDayName(27),
                TwentyEightCount = count28,

                // TwentyNine:
                TwentyNine = GetDayName(28),
                TwentyNineCount = count29,

                // TwentyNine:
                Thirty = GetDayName(29),
                ThirtyCount = count30
            };

            return bookmarks;
        }

        public async Task<DashboardListingLast30DaysReviews> GetLast30DaysListingReviewsAsync(int ListingID)
        {
            var count1 = await CountReviewAsync(0, ListingID);
            var count2 = await CountReviewAsync(1, ListingID);
            var count3 = await CountReviewAsync(2, ListingID);
            var count4 = await CountReviewAsync(3, ListingID);
            var count5 = await CountReviewAsync(4, ListingID);
            var count6 = await CountReviewAsync(5, ListingID);
            var count7 = await CountReviewAsync(6, ListingID);
            var count8 = await CountReviewAsync(7, ListingID);
            var count9 = await CountReviewAsync(8, ListingID);
            var count10 = await CountReviewAsync(9, ListingID);
            var count11 = await CountReviewAsync(10, ListingID);
            var count12 = await CountReviewAsync(11, ListingID);
            var count13 = await CountReviewAsync(12, ListingID);
            var count14 = await CountReviewAsync(13, ListingID);
            var count15 = await CountReviewAsync(14, ListingID);
            var count16 = await CountReviewAsync(15, ListingID);
            var count17 = await CountReviewAsync(16, ListingID);
            var count18 = await CountReviewAsync(17, ListingID);
            var count19 = await CountReviewAsync(18, ListingID);
            var count20 = await CountReviewAsync(19, ListingID);
            var count21 = await CountReviewAsync(20, ListingID);
            var count22 = await CountReviewAsync(21, ListingID);
            var count23 = await CountReviewAsync(22, ListingID);
            var count24 = await CountReviewAsync(23, ListingID);
            var count25 = await CountReviewAsync(24, ListingID);
            var count26 = await CountReviewAsync(25, ListingID);
            var count27 = await CountReviewAsync(26, ListingID);
            var count28 = await CountReviewAsync(27, ListingID);
            var count29 = await CountReviewAsync(28, ListingID);
            var count30 = await CountReviewAsync(29, ListingID);
            DashboardListingLast30DaysReviews reviews = new DashboardListingLast30DaysReviews()
            {
                // One:
                One = GetDayName(0),
                OneCount = count1,

                // Two:
                Two = GetDayName(1),
                TwoCount = count2,

                // Three:
                Three = GetDayName(2),
                ThreeCount = count3,

                // Four:
                Four = GetDayName(3),
                FourCount = count4,

                // Five:
                Five = GetDayName(4),
                FiveCount = count5,

                // Six:
                Six = GetDayName(5),
                SixCount = count6,

                // Seven:
                Seven = GetDayName(6),
                SevenCount = count7,

                // Eight:
                Eight = GetDayName(7),
                EightCount = count8,

                // Nine:
                Nine = GetDayName(8),
                NineCount = count9,

                // Ten:
                Ten = GetDayName(9),
                TenCount = count10,

                // Eleven:
                Eleven = GetDayName(10),
                ElevenCount = count11,

                // Twelve:
                Twelve = GetDayName(11),
                TwelveCount = count12,

                // Thirteen:
                Thirteen = GetDayName(12),
                ThirteenCount = count13,

                // Fourteen:
                Fourteen = GetDayName(13),
                FourteenCount = count14,

                // Fifteen:
                Fifteen = GetDayName(14),
                FifteenCount = count15,

                // Sixteen:
                Sixteen = GetDayName(15),
                SixteenCount = count16,

                // Seventeen:
                Seventeen = GetDayName(16),
                SeventeenCount = count17,

                // Eighteen:
                Eighteen = GetDayName(17),
                EighteenCount = count18,

                // Nineteen:
                Nineteen = GetDayName(18),
                NineteenCount = count19,

                // Twenty:
                Twenty = GetDayName(19),
                TwentyCount = count20,

                // TwentyOne:
                TwentyOne = GetDayName(20),
                TwentyOneCount = count21,

                // TwentyTwo:
                TwentyTwo = GetDayName(21),
                TwentyTwoCount = count22,

                // TwentyThree:
                TwentyThree = GetDayName(22),
                TwentyThreeCount = count23,

                // TwentyFour:
                TwentyFour = GetDayName(23),
                TwentyFourCount = count24,

                // TwentyFive:
                TwentyFive = GetDayName(24),
                TwentyFiveCount = count25,

                // TwentySix:
                TwentySix = GetDayName(25),
                TwentySixCount = count26,

                // TwentySeven:
                TwentySeven = GetDayName(26),
                TwentySevenCount = count27,

                // TwentyEight:
                TwentyEight = GetDayName(27),
                TwentyEightCount = count28,

                // TwentyNine:
                TwentyNine = GetDayName(28),
                TwentyNineCount = count29,

                // TwentyNine:
                Thirty = GetDayName(29),
                ThirtyCount = count30
            };

            return reviews; 
        }

        public async Task<IEnumerable<DashboardListingViewCountByCountryViewModel>> GetListingViewsCountByCountry(int ListingID)
        {
            var result = await (from x in listingContext.ListingViews
                          where x.ListingID == ListingID
                          group x by x.Country into y
                          select new DashboardListingViewCountByCountryViewModel
                          {
                              Country = y.Key,
                              Count = y.Count()
                          })
                          .ToListAsync();

            return result;
        }

        public async Task<DashboardListingViewByMonth> CountListingViewsByMonth(int subtractMonth, int ListingID)
        {
            var date = DateTime.Now.AddMonths(-subtractMonth);
            var result = await listingContext.ListingViews.Where(x => x.Date.Month == date.Month && x.Date.Year == date.Year && x.ListingID == ListingID).CountAsync();
            DashboardListingViewByMonth model = new DashboardListingViewByMonth()
            {
                MonthName = date.ToString("MMM"),
                Count = result
            };

            return model;
        }

        public async Task<bool> CheckIfUserHas5Listings(string ownerGuid)
        {
            var count = await listingContext.Listing.Where(i => i.OwnerGuid == ownerGuid).CountAsync();
            if(count > 3)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public string ListingCompanyName(int listingId)
        {
            string CompanyName = listingContext.Listing.Where(i => i.ListingID == listingId).Select(i => i.CompanyName).FirstOrDefault();

            if(CompanyName == "")
            {
                CompanyName = "Name Not Available";
                return CompanyName;
            }
            else
            {
                return CompanyName;
            }
        }
    }
}
