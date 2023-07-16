using AntDesign;
using BAL.Services.Contracts;
using BOL.ComponentModels.MyAccount.Profile;
using BOL.SHARED;
using DAL.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IDUserProfile = BOL.IDENTITY.UserProfile;

namespace FRONTEND.BLAZOR.MyAccount.Profile
{
    public partial class UserAddress
    {
        [Inject]
        public IUserService userService { get; set; }
        [Inject]
        private IUserProfileService userProfileService { get; set; }
        [Inject]
        private ISharedService sharedService { get; set; }
        [Inject]
        AuthenticationStateProvider authenticationState { get; set; }
        [Inject]
        Helper helper { get; set; }
        [Inject]
        NavigationManager navManager { get; set; }
        [Inject]
        NotificationService _notice { get; set; }

        ProfileInfo ProfileInfo { get; set; }

        protected async override Task OnInitializedAsync()
        {
            try
            {
                ProfileInfo = new ProfileInfo();
                var authstate = await authenticationState.GetAuthenticationStateAsync();
                var user = authstate.User;
                if (user.Identity.IsAuthenticated)
                {
                    ApplicationUser applicationUser = await userService.GetUserByUserName(user.Identity.Name);
                    string CurrentUserGuid = applicationUser.Id;
                    ProfileInfo = await userProfileService.GetProfileInfo(CurrentUserGuid);
                    ProfileInfo.isVendor = applicationUser.IsVendor;

                    if (ProfileInfo.UserProfile == null)
                        navManager.NavigateTo("/MyAccount/UserProfile");
                    else
                    {
                        ProfileInfo.Countries = await sharedService.GetCountries();
                    }
                    await GetStateByCountryId();
                    await GetCityByStateId();
                    await GetAreaByCityId();
                    await GetPincodesByAreaId();
                    await GetLocalitiesByPincodeId();
                }
            }
            catch (Exception exc)
            {

            }
        }

        public async Task GetStateByCountryId()
        {
            ProfileInfo.States.Clear();
            ProfileInfo.Cities.Clear();
            ProfileInfo.Areas.Clear();
            ProfileInfo.Pincodes.Clear();
            ProfileInfo.Localities.Clear();

            if (ProfileInfo.UserProfile.CountryID > 0)
                ProfileInfo.States = await sharedService.GetStatesByCountryId(ProfileInfo.UserProfile.CountryID);

            StateHasChanged();
        }

        public async Task GetCityByStateId()
        {
            ProfileInfo.Cities.Clear();
            ProfileInfo.Areas.Clear();
            ProfileInfo.Pincodes.Clear();
            ProfileInfo.Localities.Clear();

            if (ProfileInfo.UserProfile.StateID > 0)
                ProfileInfo.Cities = await sharedService.GetCitiesByStateId(ProfileInfo.UserProfile.StateID);

            StateHasChanged();
        }

        public async Task GetAreaByCityId()
        {
            ProfileInfo.Areas.Clear();
            ProfileInfo.Pincodes.Clear();
            ProfileInfo.Localities.Clear();

            if (ProfileInfo.UserProfile.CityID > 0)
                ProfileInfo.Areas = await sharedService.GetAreasByCityId(ProfileInfo.UserProfile.CityID);

            StateHasChanged();
        }

        public async Task GetPincodesByAreaId()
        {
            ProfileInfo.Pincodes.Clear();
            ProfileInfo.Localities.Clear();

            if (ProfileInfo.UserProfile.AssemblyID > 0)
                ProfileInfo.Pincodes = await sharedService.GetPincodesByAreaId(ProfileInfo.UserProfile.AssemblyID);

            StateHasChanged();
        }

        public async Task GetLocalitiesByPincodeId()
        {
            ProfileInfo.Localities.Clear();

            if (ProfileInfo.UserProfile.PincodeID > 0)
                ProfileInfo.Localities = await sharedService.GetLocalitiesByPincode(ProfileInfo.UserProfile.PincodeID);

            StateHasChanged();
        }

        private async Task<bool> isValidFields()
        {
            if (ProfileInfo.UserProfile.DateOfBirth == null || string.IsNullOrEmpty(ProfileInfo.UserProfile.MaritalStatus) ||
                string.IsNullOrEmpty(ProfileInfo.UserProfile.Qualification) || ProfileInfo.UserProfile.CountryID <= 0 ||
                ProfileInfo.UserProfile.StateID <= 0 || ProfileInfo.UserProfile.CityID <= 0 || ProfileInfo.UserProfile.AssemblyID <= 0 || 
                ProfileInfo.UserProfile.PincodeID <= 0 || ProfileInfo.UserProfile.LocalityID <= 0 || string.IsNullOrEmpty(ProfileInfo.UserProfile.Address))
            {
                await helper.ShowNotification(_notice, NotificationType.Error, NotificationPlacement.BottomRight, "Error", "All fields are compulsory.");
                return false;
            }

            return true;
        }

        public async Task UpdateProfileAsync()
        {
            if (!(await isValidFields()))
                return;

            if (ProfileInfo.UserProfile == null)
            {
                await helper.ShowNotification(_notice, NotificationType.Error, NotificationPlacement.BottomRight, "Error", "User profile does not exists.");
                return;
            }

            try
            {
                ProfileInfo.UserProfile.IsProfileCompleted = true;

                await userProfileService.UpdateUserProfile(ProfileInfo.UserProfile);
                await helper.ShowNotification(_notice, NotificationType.Success, NotificationPlacement.BottomRight, "Success", "Your profile updated successfully.");
            }
            catch (Exception exc)
            {
                await helper.ShowNotification(_notice, NotificationType.Error, NotificationPlacement.BottomRight, "Error", exc.Message);
            }
        }
    }
}
