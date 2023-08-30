using AntDesign;
using BAL.Services.Contracts;
using BOL.ComponentModels.Listings;
using BOL.LISTING;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FRONTEND.BLAZOR.Modal
{
    public partial class ListingEnquiryModal
    {
        [Parameter] public string CurrentUserGuid { get; set; }
        [Parameter] public int ListingId { get; set; }

        [Inject] private IUserService userService { get; set; }
        [Inject] private IUserProfileService userProfileService { get; set; }
        [Inject] private IListingService listingService { get; set; }
        [Inject] Helper helper { get; set; }
        [Inject] NavigationManager navigationManager { get; set; }

        public bool showEnquiryModal { get; set; } = false;
        private bool isRegisteredUser { get; set; } = false;

        public ListingEnquiryVM ListingEnquiryVM { get; set; } = new ListingEnquiryVM();

        protected async override void OnAfterRender(bool firstRender)
        {
            if (!string.IsNullOrWhiteSpace(CurrentUserGuid))
            {
                var applicationUser = await userService.GetUserById(CurrentUserGuid);
                isRegisteredUser = applicationUser != null;

                if (isRegisteredUser)
                {
                    var userProfile = await userProfileService.GetProfileByOwnerGuid(CurrentUserGuid);
                    if (userProfile != null)
                        ListingEnquiryVM.ListingEnquiry.FullName = userProfile.Name;
                    ListingEnquiryVM.ListingEnquiry.Email = applicationUser.Email;
                    ListingEnquiryVM.ListingEnquiry.MobileNumber = applicationUser.PhoneNumber;
                }
            }
        }

        public async Task HideEnquiryModal()
        {
            showEnquiryModal = false;
            await Task.Delay(5);
        }

        public async Task ShowEnquiryModal()
        {
            showEnquiryModal = true;
            await Task.Delay(5);
        }

        private void NavigateToLogin()
        {
            showEnquiryModal = false;
            navigationManager.NavigateTo("/Auth/Login", true);
        }

        private void NavigateToRegister()
        {
            showEnquiryModal = false;
            navigationManager.NavigateTo("/Auth/Register", true);
        }

        public async Task CreateEnquiry()
        {
            if (!!isRegisteredUser)
                return;

            if (!ListingEnquiryVM.isValid())
            {
                helper.ShowNotification(_notice, NotificationType.Error, NotificationPlacement.BottomRight, "Error", $"All Fields are mandatory!.");
                return;
            }
            try
            {
                ListingEnquiryVM.ListingEnquiry.OwnerGuid = Convert.ToString(CurrentUserGuid);
                ListingEnquiryVM.ListingEnquiry.ListingID = ListingId;
                await listingService.AddAsync(ListingEnquiryVM.ListingEnquiry);

                await HideEnquiryModal();
                helper.ShowNotification(_notice, NotificationType.Success, NotificationPlacement.BottomRight, "Success", $"Your Enquiry has been sent.");
                ListingEnquiryVM.ListingEnquiry = new ListingEnquiry();
            }
            catch (Exception exc)
            {
                helper.ShowNotification(_notice, NotificationType.Error, NotificationPlacement.BottomRight, "Error", exc.Message);
            }
        }
    }
}
