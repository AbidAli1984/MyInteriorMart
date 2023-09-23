using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Threading.Tasks;

namespace FRONTEND.BLAZOR.MyAccount.FreeListing
{
    public partial class Company
    {
        [Inject] private AuthenticationStateProvider authenticationState { get; set; }
        public string Url { get { return BOL.Constants.getListingUrl(BOL.Constants.CreateListing) + "/Company";  } }

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
