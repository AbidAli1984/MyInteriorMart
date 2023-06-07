using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using FRONTEND.BLAZOR.Services;
using AntDesign;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace FRONTEND.BLAZOR.MyAccount.ListingWizard
{
    public partial class Communication
    {
        // Begin: Check if record exisit with listingId
        [Parameter]
        public int? listingId { get; set; }

        public string currentPage = "nav-communication";
        public bool buttonBusy { get; set; }

        [Inject]
        private IListingService listingService { get; set; }

        [Inject]
        private IHttpContextAccessor httpConAccess { get; set; }
        public string CurrentUserGuid { get; set; }
        public string ErrorMessage { get; set; }
        public bool userAuthenticated { get; set; } = false;
        public string IpAddress { get; set; }
        public IdentityUser iUser { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime CreatedTime { get; set; }

        // Begin: Check if record exists
        public bool companyExist { get; set; }
        public bool communicationExist { get; set; }
        public bool addressExist { get; set; }
        public bool categoryExist { get; set; }
        public bool specialisationExist { get; set; }
        public bool workingHoursExist { get; set; }
        public bool paymentModeExist { get; set; }

        public async Task CompanyExistAsync()
        {
            if (listingId != null)
            {
                var company = await listingContext.Listing.Where(i => i.ListingID == listingId).FirstOrDefaultAsync();

                if (company != null)
                {
                    companyExist = true;
                }
                else
                {
                    companyExist = false;
                }
            }
        }

        public async Task CommunicationExistAsync()
        {
            if (listingId != null)
            {
                var communication = await listingContext.Communication.Where(i => i.ListingID == listingId).FirstOrDefaultAsync();

                if (communication != null)
                {
                    communicationExist = true;
                }
                else
                {
                    communicationExist = false;
                }
            }
        }

        public async Task AddressExistAsync()
        {
            if (listingId != null)
            {
                var address = await listingContext.Address.Where(i => i.ListingID == listingId).FirstOrDefaultAsync();

                if (address != null)
                {
                    addressExist = true;
                }
                else
                {
                    addressExist = false;
                }
            }
        }

        public async Task CategoryExistAsync()
        {
            if (listingId != null)
            {
                var category = await listingContext.Categories.Where(i => i.ListingID == listingId).FirstOrDefaultAsync();

                if (category != null)
                {
                    categoryExist = true;
                }
                else
                {
                    categoryExist = false;
                }
            }
        }

        public async Task SpecialisationExistAsync()
        {
            if (listingId != null)
            {
                var specialisation = await listingContext.Specialisation.Where(i => i.ListingID == listingId).FirstOrDefaultAsync();

                if (specialisation != null)
                {
                    specialisationExist = true;
                }
                else
                {
                    specialisationExist = false;
                }
            }
        }

        public async Task WorkingHoursExistAsync()
        {
            if (listingId != null)
            {
                var wh = await listingContext.WorkingHours.Where(i => i.ListingID == listingId).FirstOrDefaultAsync();

                if (wh != null)
                {
                    workingHoursExist = true;
                }
                else
                {
                    workingHoursExist = false;
                }
            }
        }

        public async Task PaymentModeExistAsync()
        {
            if (listingId != null)
            {
                var pm = await listingContext.PaymentMode.Where(i => i.ListingID == listingId).FirstOrDefaultAsync();

                if (pm != null)
                {
                    paymentModeExist = true;
                }
                else
                {
                    paymentModeExist = false;
                }
            }
        }
        // End: Check if record exists

        // Properties
        public string email { get; set; }
        public string telephone1st { get; set; }
        public string telephone2nd { get; set; }
        public string website { get; set; }
        public string tollFree { get; set; }
        public string mobile { get; set; }
        public string whatsapp { get; set; }
        public string fax { get; set; }
        public string skypeId { get; set; }

        // Begin: Validate Email
        public bool IsValidEmail(string email)
        {
            if(email.Contains(".") == true && new EmailAddressAttribute().IsValid(email) == true && email.Contains(" ") == false)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        // End: Validate Email

        // Begin: Validate Mobile
        public bool IsValidMobile(string mobile)
        {
            if (Regex.IsMatch(mobile, @"^\d+$") == true)
            {
                return true;
            }
            else
            {
                return false;
            }
         }
        // End: Validate Mobile

        // Begin: Validate Website
        public bool IsValidWebsite(string website)
        {
            if (website.Contains("www."))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        // End: Validate Website

        public async Task CreateCommunicationAsync()
        {
            buttonBusy = true;

            try
            {
                var communication = await listingContext.Communication.FindAsync(listingId);
                if (communication == null)
                {
                    if (string.IsNullOrEmpty(email) == false && string.IsNullOrEmpty(mobile) == false && string.IsNullOrEmpty(whatsapp) == false)
                    {
                        if(IsValidEmail(email) == true && IsValidMobile(mobile) == true && IsValidMobile(whatsapp) == true)
                        {
                            try
                            {
                                if(string.IsNullOrEmpty(website) == false)
                                {
                                    if(IsValidWebsite(website) == true)
                                    {
                                        BOL.LISTING.Communication com = new()
                                        {
                                            ListingID = listingId.Value,
                                            Email = email,
                                            Telephone = telephone1st,
                                            TelephoneSecond = telephone2nd,
                                            Website = website,
                                            Mobile = mobile,
                                            Whatsapp = whatsapp,
                                            TollFree = tollFree,
                                            Fax = fax,
                                            SkypeID = skypeId,
                                            IPAddress = IpAddress,
                                            OwnerGuid = CurrentUserGuid
                                        };

                                        await listingContext.AddAsync(com);
                                        await listingContext.SaveChangesAsync();

                                        navManager.NavigateTo($"/MyAccount/ListingWizard/Address/{listingId}");
                                    }
                                    else
                                    {
                                        buttonBusy = false;

                                        // Exception Notification
                                        await NoticeWithIcon(NotificationType.Error, NotificationPlacement.BottomRight, "Error", "Website musch contain wwww.");
                                    }
                                }
                                else
                                {
                                    BOL.LISTING.Communication com = new()
                                    {
                                        ListingID = listingId.Value,
                                        Email = email,
                                        Telephone = telephone1st,
                                        TelephoneSecond = telephone2nd,
                                        Website = website,
                                        Mobile = mobile,
                                        Whatsapp = whatsapp,
                                        TollFree = tollFree,
                                        Fax = fax,
                                        SkypeID = skypeId,
                                        IPAddress = IpAddress,
                                        OwnerGuid = CurrentUserGuid
                                    };

                                    await listingContext.AddAsync(com);
                                    await listingContext.SaveChangesAsync();

                                    navManager.NavigateTo($"/MyAccount/ListingWizard/Address/{listingId}");
                                }
                            }
                            catch (Exception exc)
                            {
                                buttonBusy = false;

                                // Exception Notification
                                await NoticeWithIcon(NotificationType.Error, NotificationPlacement.BottomRight, "Error", exc.Message);

                                // Inner Exception Notification
                                await NoticeWithIcon(NotificationType.Error, NotificationPlacement.BottomRight, "Error", exc.InnerException.ToString());
                            }
                        }
                        else
                        {
                            buttonBusy = false;

                            // Exception Notification
                            await NoticeWithIcon(NotificationType.Error, NotificationPlacement.BottomRight, "Error", "Email, Mobile and Whatsapp must be in proper format.");
                        }
                    }
                    else
                    {
                        buttonBusy = false;

                        await NoticeWithIcon(NotificationType.Error, NotificationPlacement.BottomRight, "Error", "Email, Mobile and Whatsapp is compulsory.");
                    }
                }
            }
            catch(Exception exc)
            {
                // Exception Notification
                await NoticeWithIcon(NotificationType.Error, NotificationPlacement.BottomRight, "Error", exc.Message);

                // Inner Exception Notification
                await NoticeWithIcon(NotificationType.Error, NotificationPlacement.BottomRight, "Error", exc.InnerException.ToString());

                buttonBusy = false;
            }
        }

        // Begin: Antdesign Blazor Notification
        private async Task NoticeWithIcon(NotificationType type, NotificationPlacement placement, string message, string description)
        {
            await _notice.Open(new NotificationConfig()
            {
                Message = message,
                Description = description,
                NotificationType = type,
                Placement = placement
            });
        }
        // End: Antdesign Blazor Notification

        protected async override Task OnInitializedAsync()
        {
            try
            {
                // Get User Name
                var authstate = await authenticationState.GetAuthenticationStateAsync();
                var user = authstate.User;
                if (user.Identity.IsAuthenticated)
                {
                    // Shafi: Assign Time Zone to CreatedDate & Created Time
                    DateTime timeZoneDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
                    IpAddress = httpConAccess.HttpContext.Connection.RemoteIpAddress.ToString();
                    CreatedDate = timeZoneDate;
                    CreatedTime = timeZoneDate;
                    // End:

                    iUser = await applicationContext.Users.Where(i => i.UserName == user.Identity.Name).FirstOrDefaultAsync();
                    CurrentUserGuid = iUser.Id;

                    userAuthenticated = true;

                    // Begin: Check if record exists
                    await CompanyExistAsync();
                    await CommunicationExistAsync();
                    await AddressExistAsync();
                    await CategoryExistAsync();
                    await SpecialisationExistAsync();
                    await WorkingHoursExistAsync();
                    await PaymentModeExistAsync();
                    // End: Check if record exists

                    if (communicationExist == true)
                    {
                        navManager.NavigateTo($"/MyAccount/ListingWizard/CommunicationEdit/{listingId}");
                    }
                }
            }
            catch (Exception exc)
            {
                ErrorMessage = exc.Message;
            }
        }
    }
}
