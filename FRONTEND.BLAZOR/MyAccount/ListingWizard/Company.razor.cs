using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;

namespace FRONTEND.BLAZOR.MyAccount.ListingWizard
{
    public partial class Company
    {
        [Inject] private AuthenticationStateProvider authenticationState { get; set; }


        protected async override Task OnInitializedAsync()
        {
            try
            {
                var authstate = await authenticationState.GetAuthenticationStateAsync();
                var user = authstate.User;
                if (user.Identity.IsAuthenticated)
                {
                    
                }
            }
            catch (Exception exc)
            {
                string ErrorMessage = exc.Message;
            }
        }
    }
}
