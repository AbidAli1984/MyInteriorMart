﻿using AntDesign;
using BAL.FileManager;
using BAL.Services.Contracts;
using BOL.ComponentModels.MyAccount.Profile;
using DAL.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Hosting;
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
        private bool isImageChange { get; set; }
        public string CurrentUserGuid { get; set; }
        public string IpAddressUser { get; set; }
        public string TimeZoneOfCountry { get; set; } = "India Standard Time";

        protected async override Task OnInitializedAsync()
        {
            try
            {
                UserProfileVM = new UserProfileVM();
                var authstate = await authenticationState.GetAuthenticationStateAsync();
                var user = authstate.User;
                if (user.Identity.IsAuthenticated)
                {
                    IpAddressUser = httpConAccess.HttpContext.Connection.RemoteIpAddress.ToString();
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
                UserProfileVM.ImgUrl = userProfile.ImageUrl;
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

            if (userProfile == null)
            {
                try
                {
                    IDUserProfile userProfile = new IDUserProfile
                    {
                        OwnerGuid = CurrentUserGuid,
                        IPAddress = IpAddressUser,
                        Name = UserProfileVM.Name,
                        Gender = UserProfileVM.Gender,
                        CreatedDate = helper.GetCurrentDateTime(TimeZoneOfCountry),
                        TimeZoneOfCountry = TimeZoneOfCountry,
                        ImageUrl = await MoveProfileImage()
                    };

                    await userProfileService.AddUserProfile(userProfile);
                    await helper.ShowNotification(_notice, NotificationType.Success, NotificationPlacement.BottomRight, "Success", "Your profile created successfully.");
                    navManager.NavigateTo("/MyAccount/UserAddress");
                }
                catch (Exception exc)
                {
                    await helper.ShowNotification(_notice, NotificationType.Error, NotificationPlacement.BottomRight, "Error", exc.Message);
                }
                return;
            }

            await helper.ShowNotification(_notice, NotificationType.Error, NotificationPlacement.BottomRight, "Error", "User profile already exists.");
        }

        public async Task UpdateProfileAsync()
        {
            if (!(await isValidFields()))
                return;

            if (userProfile != null)
            {
                try
                {
                    userProfile.Name = UserProfileVM.Name;
                    userProfile.Gender = UserProfileVM.Gender;
                    userProfile.UpdatedDate = helper.GetCurrentDateTime(TimeZoneOfCountry);
                    userProfile.ImageUrl = await MoveProfileImage();

                    await userProfileService.UpdateUserProfile(userProfile);
                    await helper.ShowNotification(_notice, NotificationType.Success, NotificationPlacement.BottomRight, "Success", "Your profile updated successfully.");
                }
                catch (Exception exc)
                {
                    await helper.ShowNotification(_notice, NotificationType.Error, NotificationPlacement.BottomRight, "Error", exc.Message);
                }
                return;
            }

            await helper.ShowNotification(_notice, NotificationType.Error, NotificationPlacement.BottomRight, "Error", "User profile does not exists.");
        }

        public async Task UploadProfileImage()
        {
            UserProfileVM.ImgUrl = await helper.UploadProfileImage(UserProfileVM.file, CurrentUserGuid);
            isImageChange = true;
        }

        private async Task<string> MoveProfileImage()
        {
            if (isImageChange)
            {
                isImageChange = false;
                UserProfileVM.ImgUrl = await helper.MoveProfileImage(UserProfileVM, CurrentUserGuid);
            }
            return UserProfileVM.ImgUrl;
        }
    }
}