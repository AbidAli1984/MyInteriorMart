using AntDesign;
using BAL.Services.Contracts;
using BOL.AUDITTRAIL;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Threading.Tasks;

namespace FRONTEND.BLAZOR.MyAccount.Profile
{
    public partial class Suggestion
    {
        [Inject] public IAuditService auditService { get; set; }
        [Inject] public IUserService userService { get; set; }
        [Inject] public IUserProfileService userProfileService { get; set; }
        [Inject] AuthenticationStateProvider authenticationState { get; set; }
        [Inject] NotificationService _notification { get; set; }
        [Inject] Helper helper { get; set; }

        public Suggestions Suggestions { get; set; } = new Suggestions();

        public bool buttonBusy { get; set; }
        public bool isVendor { get; set; } = false;

        protected async override Task OnInitializedAsync()
        {
            try
            {
                var authstate = await authenticationState.GetAuthenticationStateAsync();
                var user = authstate.User;
                if (user.Identity.IsAuthenticated)
                {
                    var applicationUser = await userService.GetUserByUserName(user.Identity.Name);
                    var userProfile = await userProfileService.GetProfileByOwnerGuid(applicationUser.Id);
                    isVendor = applicationUser.IsVendor;

                    Suggestions.OwnerGuid = applicationUser.Id;
                    Suggestions.Mobile = applicationUser.PhoneNumber;
                    Suggestions.Email = applicationUser.Email;

                    if (userProfile != null)
                        Suggestions.Name = userProfile.Name;
                }
            }
            catch (Exception exc)
            {

            }
        }

        private async Task AddSuggestion()
        {
            if (string.IsNullOrEmpty(Suggestions.Title) || string.IsNullOrEmpty(Suggestions.Suggestion))
            {
                helper.ShowNotification(_notification, "All fields are compulsory.", NotificationType.Info);
                return;
            }

            try
            {
                buttonBusy = true;
                Suggestions.Date = DateTime.Now;
                await auditService.AddAsync(Suggestions);
                ResetSuggestion();
                helper.ShowNotification(_notification, "Your suggestion submitted successfully!");
            }
            catch (Exception exc)
            {
                helper.ShowNotification(_notification, exc.Message, NotificationType.Error);
            }
            finally
            {
                buttonBusy = false;
            }
        }

        private void ResetSuggestion()
        {
            Suggestions.SuggestionID = 0;
            Suggestions.Title = string.Empty;
            Suggestions.Suggestion = string.Empty;
        }
    }
}
