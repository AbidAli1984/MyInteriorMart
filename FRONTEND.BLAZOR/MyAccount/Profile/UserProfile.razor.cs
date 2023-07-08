using AntDesign;
using BAL.Services.Contracts;
using BOL.ComponentModels.MyAccount.Profile;
using DAL.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using IDUserProfile = BOL.IDENTITY.UserProfile;

namespace FRONTEND.BLAZOR.MyAccount.Profile
{
    public partial class UserProfile
    {
        [Inject]
        private IHttpContextAccessor httpConAccess { get; set; }
        [Inject]
        public IUserService userService { get; set; }
        [Inject]
        private IUserProfileService userProfileService { get; set; }
        [Inject]
        AuthenticationStateProvider authenticationState { get; set; }
        [Inject]
        Helper helper { get; set; }
        [Inject]
        NavigationManager navManager { get; set; }

        public IDUserProfile userProfile { get; set; }

        UserProfileVM UserProfileVM { get; set; }

        public bool disable { get; set; }
        public string CurrentUserGuid { get; set; }
        public DateTime CreatedDate { get; set; }
        public string IpAddressUser { get; set; }
        public string TimeZoneOfCountry { get; set; }

        protected async override Task OnInitializedAsync()
        {
            try
            {
                UserProfileVM = new UserProfileVM();
                var authstate = await authenticationState.GetAuthenticationStateAsync();
                var user = authstate.User;
                if (user.Identity.IsAuthenticated)
                {
                    TimeZoneOfCountry = "India Standard Time";
                    IpAddressUser = httpConAccess.HttpContext.Connection.RemoteIpAddress.ToString();
                    disable = true;

                    DateTime timeZoneDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById(TimeZoneOfCountry));
                    CreatedDate = timeZoneDate.Date;

                    ApplicationUser applicationUser = await userService.GetUserByUserNameOrEmail(user.Identity.Name);
                    CurrentUserGuid = applicationUser.Id;
                    UserProfileVM.Email = applicationUser.Email;
                    UserProfileVM.Phone = applicationUser.PhoneNumber;
                    UserProfileVM.isVendor = applicationUser.IsVendor;

                    await GetCurrentUserProfile();
                }
            }
            catch (Exception exc)
            {

            }
        }

        public async Task GetCurrentUserProfile()
        {
            userProfile = await userProfileService.GetProfileByOwnerGuid(CurrentUserGuid);

            if (userProfile != null)
            {
                UserProfileVM.Gender = userProfile.Gender;
                UserProfileVM.Name = userProfile.Name;
            }
        }

        private async Task<bool> isValidFields()
        {
            if (string.IsNullOrEmpty(UserProfileVM.Gender) || string.IsNullOrEmpty(UserProfileVM.Name))
            {
                await helper.ShowNotification(_notice, NotificationType.Error, NotificationPlacement.BottomRight, "Error", "All fields are compulsory.");
                return false;
            }

            return true;
        }

        public async Task CreateProfileAsync()
        {
            if (!(await isValidFields()))
                return;

            if (userProfile != null)
            {
                await helper.ShowNotification(_notice, NotificationType.Error, NotificationPlacement.BottomRight, "Error", "User profile already exists.");
                return;
            }

            try
            {
                IDUserProfile userProfile = new IDUserProfile
                {
                    OwnerGuid = CurrentUserGuid,
                    IPAddress = IpAddressUser,
                    Name = UserProfileVM.Name,
                    Gender = UserProfileVM.Gender,
                    CreatedDate = CreatedDate,
                    TimeZoneOfCountry = TimeZoneOfCountry
                };

                await userProfileService.AddUserProfile(userProfile);
                await helper.ShowNotification(_notice, NotificationType.Success, NotificationPlacement.BottomRight, "Success", "Your profile created successfully.");
            }
            catch (Exception exc)
            {
                await helper.ShowNotification(_notice, NotificationType.Error, NotificationPlacement.BottomRight, "Error", exc.Message);
            }
        }

        public async Task UpdateProfileAsync()
        {
            if (!(await isValidFields()))
                return;

            if (userProfile == null)
            {
                await helper.ShowNotification(_notice, NotificationType.Error, NotificationPlacement.BottomRight, "Error", "User profile does not exists.");
                return;
            }

            try
            {
                userProfile.OwnerGuid = CurrentUserGuid;
                userProfile.IPAddress = IpAddressUser;
                userProfile.Name = UserProfileVM.Name;
                userProfile.Gender = UserProfileVM.Gender;
                CreatedDate = CreatedDate;
                userProfile.TimeZoneOfCountry = TimeZoneOfCountry;

                await userProfileService.UpdateUserProfile(userProfile);

                await helper.ShowNotification(_notice, NotificationType.Success, NotificationPlacement.BottomRight, "Success", "Your profile updated successfully.");
            }
            catch (Exception exc)
            {
                await helper.ShowNotification(_notice, NotificationType.Error, NotificationPlacement.BottomRight, "Error", exc.Message);
            }
        }
    }
}