using BAL.Services.Contracts;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using Utils;

namespace FRONTEND.BLAZOR.MyAccount.Auth
{
    public partial class Register
    {
        [Inject]
        private IUserService userService { get; set; }

        public string Email { get; set; }
        public string emailErrMessage { get; set; }

        public string Mobile { get; set; }
        public string mobileErrMessage { get; set; }

        public string Password { get; set; }
        public string passwordErrMessage { get; set; }

        public string ConfirmPassword { get; set; }
        public string confirmPasswordErrMessage { get; set; }

        public string errorMessage { get; set; }

        public string otp { get; set; }
        public bool isOtpGenerated { get; set; }
        public bool isOtpVerified { get; set; }

        public async Task GenerateOTP()
        {
            mobileErrMessage = FieldValidator.mobileErrMessage(Mobile);
            if (!string.IsNullOrEmpty(mobileErrMessage))
            {
                return;
            }

            await userService.GenerateOTP(Mobile);
            isOtpGenerated = true;// await userService.GenerateOTP(phoneNumber);
        }

        public async Task VerifyOTP()
        {
            isOtpVerified = await userService.VerifyOTP(Mobile, otp);

            //if (isUserVerified)
            //{
            //    navManager.NavigateTo("/");
            //}

        }

        public async Task RegisterUser()
        {
            emailErrMessage = FieldValidator.emailErrMessage(Email);
            mobileErrMessage = FieldValidator.mobileErrMessage(Mobile);
            passwordErrMessage = FieldValidator.passwordErrorMessage(Password);
            confirmPasswordErrMessage = FieldValidator.confirmPasswordErrMessage(Password, ConfirmPassword);

            if (!string.IsNullOrEmpty(emailErrMessage) || !string.IsNullOrEmpty(mobileErrMessage)
                || !string.IsNullOrEmpty(passwordErrMessage) || !string.IsNullOrEmpty(confirmPasswordErrMessage))
            {
                return;
            }
        }
    }
}
