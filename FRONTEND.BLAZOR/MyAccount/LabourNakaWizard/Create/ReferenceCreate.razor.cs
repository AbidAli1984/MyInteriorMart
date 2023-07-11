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
    public partial class ReferenceCreate
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

        // Personal Properties
        public string Ref1stName { get; set; }
        public string Ref1stMobile { get; set; }
        public string Ref1stAddress { get; set; }
        public string Ref1stRelationship { get; set; }
        public string Ref2ndName { get; set; }
        public string Ref2ndMobile { get; set; }
        public string Ref2ndAddress { get; set; }
        public string Ref2ndRelationship { get; set; }

        // Begin: Create
        public async Task CreateAsync()
        {
            try
            {
                Busy = true;

                // Get Existing Record
                var recordExit = await listingContext.Reference
                    .Where(i => i.UserGuid == CurrentUserGuid)
                    .FirstOrDefaultAsync();

                if (recordExit == null)
                {
                    Reference refrence = new Reference
                    {
                        UserGuid = CurrentUserGuid,
                        Ref1stName = Ref1stName,
                        Ref1stMobile = Ref1stMobile,
                        Ref1stAddress = Ref1stAddress,
                        Ref1stRelationship = Ref1stRelationship,
                        Ref2ndName = Ref2ndName,
                        Ref2ndMobile = Ref2ndMobile,
                        Ref2ndAddress = Ref2ndAddress,
                        Ref2ndRelationship = Ref2ndRelationship
                    };

                    await listingContext.AddAsync(refrence);
                    await listingContext.SaveChangesAsync();

                    // Navigate To Edit
                    navManager.NavigateTo("/MyAccount/LabourNakaWizard/DocumentCreate");

                    Busy = false;
                }
                else
                {
                    ErrorMessage = "Record already exists. Refresh this page.";
                }
            }
            catch (Exception exc)
            {
                ErrorMessage = exc.InnerException.ToString();
            }
        }
        // End: Create Personal

        // Begin: Check Record Exist
        public bool RecordExist { get; set; }
        public bool Disable { get; set; }
        public bool Edit { get; set; }
        public Personal PersonalEdit { get; set; }
        public async Task CheckRecordExistAsync()
        {
            // Linq Query
            RecordExist = await listingContext.Reference
                .AnyAsync(i => i.UserGuid == CurrentUserGuid);

            if (RecordExist == true)
            {
                navManager.NavigateTo("/MyAccount/LabourNakaWizard/ReferenceEdit");
            }
        }
        // End: Check Record Exist        

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

                    // Execute Method
                    await CheckRecordExistAsync();
                }
            }
            catch (Exception exc)
            {
                ErrorMessage = exc.Message;
            }
        }
    }
}