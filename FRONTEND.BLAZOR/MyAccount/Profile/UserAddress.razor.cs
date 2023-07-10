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

        public IDUserProfile userProfile { get; set; }

        UserAddressVM UserAddressVM { get; set; }

        public bool isProfileCompleted { get; set; }
        public string CurrentUserGuid { get; set; }

        public async Task GetCityByStateId()
        {
            UserAddressVM.Areas.Clear();
            UserAddressVM.Pincodes.Clear();
            if (UserAddressVM.State > 0)
            {
                UserAddressVM.Cities = await sharedService.GetCitiesByStateId(UserAddressVM.State);
                StateHasChanged();
            }
        }

        public async Task GetAreaByCityId()
        {
            UserAddressVM.Pincodes.Clear();
            if (UserAddressVM.City > 0)
            {
                UserAddressVM.Areas = await sharedService.GetAreasByCityId(UserAddressVM.City);
                StateHasChanged();
            }
        }

        public async Task GetPincodesByAreaId()
        {
            if (UserAddressVM.Area > 0)
            {
                UserAddressVM.Pincodes = await sharedService.GetPincodesByAreaId(UserAddressVM.Area);
                StateHasChanged();
            }
        }

        protected async override Task OnInitializedAsync()
        {
            try
            {
                UserAddressVM = new UserAddressVM();
                var authstate = await authenticationState.GetAuthenticationStateAsync();
                var user = authstate.User;
                if (user.Identity.IsAuthenticated)
                {
                    ApplicationUser applicationUser = await userService.GetUserByUserNameOrEmail(user.Identity.Name);
                    CurrentUserGuid = applicationUser.Id;
                    UserAddressVM.isVendor = applicationUser.IsVendor;

                    userProfile = await userProfileService.GetProfileByOwnerGuid(CurrentUserGuid);
                    if (userProfile == null)
                        navManager.NavigateTo("/MyAccount/UserProfile");
                    else
                    {
                        isProfileCompleted = userProfile.IsProfileCompleted;
                        UserAddressVM.DOB = userProfile.DateOfBirth;
                        UserAddressVM.MaritialStatus = userProfile.MaritalStatus;
                        UserAddressVM.Qualification = userProfile.Qualification;
                        UserAddressVM.Address = userProfile.Address;
                        UserAddressVM.State = userProfile.StateID;
                        UserAddressVM.City = userProfile.CityID;
                        UserAddressVM.Area = userProfile.AssemblyID;
                        UserAddressVM.Pincode = userProfile.PincodeID;
                        UserAddressVM.States = await sharedService.GetStatesByCountryId();
                        if (UserAddressVM.States.Count > 0)
                        {
                            int.TryParse(Convert.ToString(UserAddressVM.States[0].CountryID), out int countryId);
                            userProfile.CountryID = countryId;
                        }
                    }
                    await GetCityByStateId();
                    await GetAreaByCityId();
                    await GetPincodesByAreaId();
                }
            }
            catch (Exception exc)
            {

            }
        }

        private async Task<bool> isValidFields()
        {
            if (UserAddressVM.DOB == null || string.IsNullOrEmpty(UserAddressVM.MaritialStatus) ||
                string.IsNullOrEmpty(UserAddressVM.Qualification) || string.IsNullOrEmpty(UserAddressVM.Address) ||
                UserAddressVM.State <= 0 || UserAddressVM.City <= 0 || UserAddressVM.Area <= 0 || UserAddressVM.Pincode <= 0)
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

            if (userProfile == null)
            {
                await helper.ShowNotification(_notice, NotificationType.Error, NotificationPlacement.BottomRight, "Error", "User profile does not exists.");
                return;
            }

            try
            {
                userProfile.DateOfBirth = Convert.ToDateTime(UserAddressVM.DOB);
                userProfile.MaritalStatus = UserAddressVM.MaritialStatus;
                userProfile.Qualification = UserAddressVM.Qualification;
                userProfile.Address = UserAddressVM.Address;
                userProfile.StateID = UserAddressVM.State;
                userProfile.CityID = UserAddressVM.City;
                userProfile.AssemblyID = UserAddressVM.Area;
                userProfile.PincodeID = UserAddressVM.Pincode;
                userProfile.IsProfileCompleted = true;
                await userProfileService.UpdateUserProfile(userProfile);
                isProfileCompleted = true;

                await helper.ShowNotification(_notice, NotificationType.Success, NotificationPlacement.BottomRight, "Success", "Your profile updated successfully.");
            }
            catch (Exception exc)
            {
                await helper.ShowNotification(_notice, NotificationType.Error, NotificationPlacement.BottomRight, "Error", exc.Message);
            }
        }
    }
}
