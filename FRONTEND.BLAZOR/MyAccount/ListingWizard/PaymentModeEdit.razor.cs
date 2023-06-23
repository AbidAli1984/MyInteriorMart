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
    public partial class PaymentModeEdit
    {
        // Begin: Check if record exisit with listingId
        [Parameter]
        public int? listingId { get; set; }

        public string currentPage = "nav-paymentmode";
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
        public bool Cash { get; set; }
        public bool NetBanking { get; set; }
        public bool Cheque { get; set; }
        public bool RtgsNeft { get; set; }
        public bool DebitCard { get; set; }
        public bool CreditCard { get; set; }
        public bool PayTM { get; set; }
        public bool PhonePay { get; set; }
        public bool Paypal { get; set; }

        public bool SelectAll { get; set; }

        public async Task ToggleAllAsync()
        {
            SelectAll = !SelectAll;

            if (SelectAll == true)
            {
                Cash = true;
                NetBanking = true;
                Cheque = true;
                RtgsNeft = true;
                DebitCard = true;
                CreditCard = true;
                PayTM = true;
                PhonePay = true;
                Paypal = true;
            }
            else
            {
                Cash = false;
                NetBanking = false;
                Cheque = false;
                RtgsNeft = false;
                DebitCard = false;
                CreditCard = false;
                PayTM = false;
                PhonePay = false;
                Paypal = false;
            }


            await Task.Delay(1);
        }

        // Begin: Get Working Hours
        public async Task GetPaymentModeAsync()
        {
            if (listingId != null)
            {
                // Get Working Hours
                var paymentMode = await listingContext.PaymentMode.Where(i => i.ListingID == listingId).FirstOrDefaultAsync();

                if (paymentMode != null)
                {
                    IpAddress = paymentMode.IPAddress;
                    Cash = paymentMode.Cash;
                    NetBanking = paymentMode.NetBanking;
                    Cheque = paymentMode.Cheque;
                    RtgsNeft = paymentMode.RtgsNeft;
                    DebitCard = paymentMode.DebitCard;
                    CreditCard = paymentMode.CreditCard;
                    PayTM = paymentMode.PayTM;
                    PhonePay = paymentMode.PhonePay;
                    Paypal = paymentMode.Paypal;
                }
            }
        }
        // End: Get Working Hours

        // Begin: Update Payment Mode
        public async Task UpdatePaymentModeAsync()
        {
            buttonBusy = true;

            // Check if specialisation already exists
            var paymentMode = await listingContext.PaymentMode.Where(i => i.ListingID == listingId).FirstOrDefaultAsync();

            try
            {
                if (paymentMode != null)
                {
                    try
                    {
                        paymentMode.IPAddress = IpAddress;
                        paymentMode.Cash = Cash;
                        paymentMode.NetBanking = NetBanking;
                        paymentMode.Cheque = Cheque;
                        paymentMode.RtgsNeft = RtgsNeft;
                        paymentMode.DebitCard = DebitCard;
                        paymentMode.CreditCard = CreditCard;
                        paymentMode.PayTM = PayTM;
                        paymentMode.PhonePay = PhonePay;
                        paymentMode.Paypal = Paypal;

                        listingContext.Update(paymentMode);
                        await listingContext.SaveChangesAsync();

                        // Navigate To
                        navManager.NavigateTo($"/MyAccount/ListingWizard/Logo/{listingId}");
                    }
                    catch (Exception exc)
                    {
                        // Show notification
                        await NoticeWithIcon(NotificationType.Error, NotificationPlacement.BottomRight, "Error", exc.Message);

                        buttonBusy = false;
                    }
                }
                else
                {
                    // Show notification
                    await NoticeWithIcon(NotificationType.Error, NotificationPlacement.BottomRight, "Error", $"Payment Mode with Listing ID {listingId} does not exists.");

                    buttonBusy = false;
                }
            }
            catch (Exception exc)
            {
                // Show notification
                await NoticeWithIcon(NotificationType.Success, NotificationPlacement.BottomRight, "Error", exc.Message);

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

                    await GetPaymentModeAsync();

                    if (paymentModeExist == false)
                    {
                        string url = "/MyAccount/ListingWizard/PaymentMode/" + listingId;
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
