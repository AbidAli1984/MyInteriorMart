using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace FRONTEND.BLAZOR.MyAccount.ListingWizard
{
    public partial class PaymentMode
    {
        [Inject] AuthenticationStateProvider authenticationState { get; set; }
 
        protected async override Task OnInitializedAsync()
        {
            try
            {
                // Get User Name
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
