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
using BOL.CATEGORIES;
using BOL.LISTING;

namespace FRONTEND.BLAZOR.MyAccount.ListingWizard
{
    public partial class WorkingHours
    {
        // Begin: Check if record exisit with listingId
        [Parameter]
        public int? listingId { get; set; }

        public string currentPage = "nav-workinghours";
        public bool buttonBusy { get; set; }

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
        public DateTime? MondayFrom { get; set; }
        public DateTime? MondayTo { get; set; }
        public DateTime? TuesdayFrom { get; set; }
        public DateTime? TuesdayTo { get; set; }
        public DateTime? WednesdayFrom { get; set; }
        public DateTime? WednesdayTo { get; set; }
        public DateTime? ThursdayFrom { get; set; }
        public DateTime? ThursdayTo { get; set; }
        public DateTime? FridayFrom { get; set; }
        public DateTime? FridayTo { get; set; }
        public bool SaturdayHoliday { get; set; }
        public DateTime? SaturdayFrom { get; set; }
        public DateTime? SaturdayTo { get; set; }
        public bool SundayHoliday { get; set; }
        public DateTime? SundayFrom { get; set; }
        public DateTime? SundayTo { get; set; }
        public bool CopyToAll { get; set; }

        // Begin: Check if specialisation exist

        public async Task ToggleCopyToAllAsync()
        {
            if(MondayFrom != null && MondayTo != null)
            {
                CopyToAll = !CopyToAll;

                if(CopyToAll == true)
                {
                    TuesdayFrom = MondayFrom;
                    TuesdayTo = MondayTo;

                    WednesdayFrom = MondayFrom;
                    WednesdayTo = MondayTo;

                    ThursdayFrom = MondayFrom;
                    ThursdayTo = MondayTo;

                    FridayFrom = MondayFrom;
                    FridayTo = MondayTo;

                    SaturdayFrom = MondayFrom;
                    SaturdayTo = MondayTo;

                    SundayFrom = MondayFrom;
                    SundayTo = MondayTo;
                }
                else
                {
                    TuesdayFrom = null;
                    TuesdayTo = null;

                    WednesdayFrom = null;
                    WednesdayTo = null;

                    ThursdayFrom = null;
                    ThursdayTo = null;

                    FridayFrom = null;
                    FridayTo = null;

                    SaturdayFrom = null;
                    SaturdayTo = null;

                    SundayFrom = null;
                    SundayTo = null;
                }
            }
            else
            {
                // Show notification
                await NoticeWithIcon(NotificationType.Error, NotificationPlacement.BottomRight, "Error", "Please Select Monday From & Monday To Timing First.");
            }

            await Task.Delay(1);
        }

        // Begin: Create Specialisation
        public async Task CreateWorkingHoursAsync()
        {
            buttonBusy = true;

            if (listingId != null)
            {
                // Check if specialisation already exists
                var duplicate = await listingContext.WorkingHours.Where(i => i.ListingID == listingId).FirstOrDefaultAsync();

                try
                {
                    if (duplicate == null)
                    {
                        try
                        {
                            if(MondayFrom != null && MondayTo != null && TuesdayFrom != null && TuesdayTo != null && WednesdayFrom != null & WednesdayTo != null && ThursdayFrom != null && ThursdayTo != null && FridayFrom != null && FridayTo != null)
                            {
                                BOL.LISTING.WorkingHours wh = new BOL.LISTING.WorkingHours
                                {
                                    ListingID = listingId.Value,
                                    OwnerGuid = CurrentUserGuid,
                                    IPAddress = IpAddress,
                                    MondayFrom = MondayFrom.Value,
                                    MondayTo = MondayTo.Value,
                                    TuesdayFrom = TuesdayFrom.Value,
                                    TuesdayTo = TuesdayTo.Value,
                                    WednesdayFrom = WednesdayFrom.Value,
                                    WednesdayTo = WednesdayTo.Value,
                                    ThursdayFrom = ThursdayFrom.Value,
                                    ThursdayTo = ThursdayTo.Value,
                                    FridayFrom = FridayFrom.Value,
                                    FridayTo = FridayTo.Value,
                                    SaturdayHoliday = SaturdayHoliday,
                                    SaturdayFrom = SaturdayFrom.Value,
                                    SaturdayTo = SaturdayTo.Value,
                                    SundayHoliday = SundayHoliday,
                                    SundayFrom = SundayFrom.Value,
                                    SundayTo = SundayTo.Value
                                };

                                await listingContext.AddAsync(wh);
                                await listingContext.SaveChangesAsync();

                                // Navigate To
                                navManager.NavigateTo($"/MyAccount/ListingWizard/PaymentMode/{listingId}");

                                buttonBusy = false;
                            }
                            else
                            {
                                // Show notification
                                await NoticeWithIcon(NotificationType.Success, NotificationPlacement.BottomRight, "Error", "From and To Timings are Compulsory for Monday To Friday.");

                                buttonBusy = false;
                            }
                        }
                        catch(Exception exc)
                        {
                            // Show notification
                            await NoticeWithIcon(NotificationType.Success, NotificationPlacement.BottomRight, "Error", exc.Message);

                            buttonBusy = false;
                        }
                    }
                    else
                    {
                        // Show notification
                        await NoticeWithIcon(NotificationType.Success, NotificationPlacement.BottomRight, "Error", $"Working Hours for Listing ID {duplicate.ListingID} already exists.");

                        buttonBusy = false;
                    }
                }
                catch(Exception exc)
                {
                    // Show notification
                    await NoticeWithIcon(NotificationType.Success, NotificationPlacement.BottomRight, "Error", exc.Message);

                    buttonBusy = false;
                }
            }
            else
            {
                // Show notification
                await NoticeWithIcon(NotificationType.Success, NotificationPlacement.BottomRight, "Error", $"Please provide Listing ID into the URL");

                buttonBusy = false;
            }
        }
        // End: Create Specialisation

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

                    if (workingHoursExist == true)
                    {
                        string url = "/MyAccount/ListingWizard/WorkingHoursEdit/" + listingId;
                        navManager.NavigateTo(url);
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
