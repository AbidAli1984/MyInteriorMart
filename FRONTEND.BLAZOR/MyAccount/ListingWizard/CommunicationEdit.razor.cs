using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AntDesign;
using BOL.LISTING;
using BOL.SHARED;
using FRONTEND.BLAZOR.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FRONTEND.BLAZOR.MyAccount.ListingWizard
{
    public partial class CommunicationEdit
    {
        // Begin: Check if record exisit with listingId
        [Parameter]
        public int? listingId { get; set; }

        public string currentPage = "nav-communication";
        public bool buttonBusy { get; set; }

        // Begin: Toggle Edit
        public bool toggleEdit { get; set; } = true;

        public async Task ToggleEditAsync()
        {
            toggleEdit = !toggleEdit;
            await Task.Delay(5);
        }
        // End: Toggle Edit

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

        // Begin: Get Communication
        public async Task GetCommunicationAsync()
        {
            var com = await listingContext.Communication
                .Where(i => i.ListingID == listingId).FirstOrDefaultAsync();

            if(com != null)
            {
                email = com.Email;
                telephone1st = com.Telephone;
                telephone2nd = com.TelephoneSecond;
                website = com.Website;
                tollFree = com.TollFree;
                mobile = com.Mobile;
                whatsapp = com.Whatsapp;
                fax = com.Fax;
                skypeId = com.SkypeID;    }
        }
        // End: Get Communication

        // Begin: Update Communication
        public async Task UpdateCommunicationAsync()
        {
            buttonBusy = true;

            try
            {
                var communication = await listingContext.Communication.Where(i => i.ListingID == listingId).FirstOrDefaultAsync();

                if(communication != null)
                {
                    communication.Email = email;
                    communication.Telephone = telephone1st;
                    communication.TelephoneSecond = telephone2nd;
                    communication.Website = website;
                    communication.TollFree = tollFree;
                    communication.Mobile = mobile;
                    communication.Whatsapp = whatsapp;
                    communication.TollFree = tollFree;
                    communication.Fax = fax;
                    communication.SkypeID = skypeId;
                    communication.IPAddress = IpAddress;
                    communication.OwnerGuid = CurrentUserGuid;

                    listingContext.Update(communication);
                    await listingContext.SaveChangesAsync();

                    if(addressExist == true)
                    {
                        navManager.NavigateTo($"/MyAccount/ListingWizard/AddressEdit/{listingId}");
                    }
                    else
                    {
                        navManager.NavigateTo($"/MyAccount/ListingWizard/Address/{listingId}");
                    }
                }
            }
            catch (Exception exc)
            {
                await NoticeWithIcon(NotificationType.Error, NotificationPlacement.BottomRight, "Error", exc.Message);

                buttonBusy = false;
            }
        }
        // End: Update Communication

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

                    await GetCommunicationAsync();
                }
            }
            catch (Exception exc)
            {
                ErrorMessage = exc.Message;
            }
        }
    }
}
