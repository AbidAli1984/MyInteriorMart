using AntDesign;
using BAL.Services.Contracts;
using BOL.ComponentModels.MyAccount.Auth;
using BOL.LISTING;
using BOL.SHARED;
using BOL.VIEWMODELS;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utils;

namespace FRONTEND.BLAZOR.Modal
{
    public partial class ClaimListingModal
    {
        [Parameter] public int listingId { get; set; }
        [Inject] private IUserService userService { get; set; }
        [Inject] private IListingService listingService { get; set; }
        [Inject] Helper helper { get; set; }
        [Inject] NotificationService _notice { get; set; }

        public UserRegisterVM UserRegisterVM { get; set; } = new UserRegisterVM();

        public int Steps { get; set; } = 1;
        public string message;
        public string Header { get; set; } = "Claim Listing";
        public bool isError { get; set; } = false;

        public bool showModal { get; set; }

        public async Task HideModal()
        {
            showModal = false;
            await Task.Delay(5);
        }

        public async Task ShowModal()
        {
            showModal = true;
            await Task.Delay(5);
        }


        public async Task ValidateDetail()
        {
            UserRegisterVM.EmailErrMessage = FieldValidator.emailErrMessage(UserRegisterVM.Email);
            UserRegisterVM.MobileErrMessage = FieldValidator.mobileErrMessage(UserRegisterVM.Email);

            if (!string.IsNullOrWhiteSpace(UserRegisterVM.EmailErrMessage) && !string.IsNullOrWhiteSpace(UserRegisterVM.MobileErrMessage))
            {
                UserRegisterVM.EmailErrMessage = "Please provide valid mobile number or email id";
                return;
            }

            var isOtpGenerated = await userService.IsOTUpdatedIfMobileOrEmailValidForTheListing(listingId, UserRegisterVM);

            if (!isOtpGenerated)
            {
                UserRegisterVM.EmailErrMessage = "You cannot claim this listing as detail does not match";
                return;
            }

            Header = "OTP Verification";
            Steps = 2;
            StateHasChanged();
        }

        public async Task VerifyOTP()
        {
            if (await userService.IsValidOTP(UserRegisterVM))
            {
                isError = false;
                message = "Otp sent on your registered mobile number or email";
                Header = "Change Password";
                Steps = 3;
            }
            else
            {
                isError = true;
                message = "Invalid OTP!";
            }
            StateHasChanged();
        }

        public async Task UpdatePassword()
        {
            UserRegisterVM.NewPasswordErrMessage = FieldValidator.passwordErrorMessage(UserRegisterVM.NewPassword);
            UserRegisterVM.ConfirmPasswordErrMessage = FieldValidator.confirmPasswordErrMessage(UserRegisterVM.NewPassword, UserRegisterVM.ConfirmPassword);

            if (!string.IsNullOrEmpty(UserRegisterVM.NewPasswordErrMessage) || !string.IsNullOrEmpty(UserRegisterVM.ConfirmPasswordErrMessage))
            {
                return;
            }

            if (await userService.IsVerifiedAndPasswordChanged(UserRegisterVM))
            {
                await HideModal();
                Steps = 1;
                Header = "Claim Listing";
                //await listingService.UpdateListingStatus(listingId, Listing.Claimed);
                helper.ShowNotification(_notice, "Your approval has been sent for verification.");
            }
        }
    }
}
