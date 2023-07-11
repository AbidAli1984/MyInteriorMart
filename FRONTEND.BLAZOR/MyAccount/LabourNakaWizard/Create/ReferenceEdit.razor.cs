using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BAL.Services.Contracts;
using BOL.LABOURNAKA;
using DAL.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FRONTEND.BLAZOR.MyAccount.LabourNakaWizard.Create
{
    public partial class ReferenceEdit
    {
        [Inject]
        private IHttpContextAccessor httpConAccess { get; set; }
        [Inject]
        public IUserService userService { get; set; }

        public string CurrentUserGuid { get; set; }
        public string ErrorMessage { get; set; }
        public bool userAuthenticated { get; set; } = false;
        public string IpAddress { get; set; }
        public ApplicationUser iUser { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime CreatedTime { get; set; }
        public bool Busy { get; set; }

        // Begin: Check Record Exist
        public bool Disable { get; set; } = true;
        public bool Edit { get; set; }

        public Reference Ref = new Reference();
        public async Task CheckRecordExistAsync()
        {
            // Get Existing Record
            Ref = await listingContext.Reference
                .Where(i => i.UserGuid == CurrentUserGuid)
                .FirstOrDefaultAsync();
        }
        // End: Check Record Exist

        // Begin: Edit Record
        public void ToggleEdit()
        {
            Disable = !Disable;
            Edit = !Edit;
        }
        // Begin: Edit Record

        // Begin: Save Record
        public async Task SaveRecordAsync()
        {
            try
            {
                listingContext.Update(Ref);
                await listingContext.SaveChangesAsync();


                // Change Disable Value
                Disable = true;

                // Change Edit Value
                Edit = false;
            }
            catch (Exception exc)
            {
                ErrorMessage = exc.InnerException.ToString();
                Disable = false;
            }
        }
        // Begin: Save Record

        protected async override Task OnInitializedAsync()
        {
            try
            {
                // Get User Name
                var authstate = await authenticationState.GetAuthenticationStateAsync();
                var user = authstate.User;
                if (user.Identity.IsAuthenticated)
                {
                    // Shafi: Assign Time Zone to CreatedDate & Created Time
                    DateTime timeZoneDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
                    IpAddress = httpConAccess.HttpContext.Connection.RemoteIpAddress.ToString();
                    CreatedDate = timeZoneDate;
                    CreatedTime = timeZoneDate;
                    // End:

                    iUser = await userService.GetUserByUserName(user.Identity.Name);
                    CurrentUserGuid = iUser.Id;
                }

                // Execute Method
                await CheckRecordExistAsync();
            }
            catch (Exception exc)
            {
                ErrorMessage = exc.Message;
            }
        }
    }
}