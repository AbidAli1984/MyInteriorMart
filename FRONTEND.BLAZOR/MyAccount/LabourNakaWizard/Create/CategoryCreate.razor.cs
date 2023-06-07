using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BOL.LABOURNAKA;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FRONTEND.BLAZOR.MyAccount.LabourNakaWizard.Create
{
    public partial class CategoryCreate
    {
        [Inject]
        private IHttpContextAccessor httpConAccess { get; set; }
        public string CurrentUserGuid { get; set; }
        public string ErrorMessage { get; set; }
        public bool userAuthenticated { get; set; } = false;
        public string IpAddress { get; set; }
        public IdentityUser iUser { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime CreatedTime { get; set; }
        public bool Busy { get; set; }

        // Category Properties
        public int? ParentCategoryId { get; set; }
        public int? ChildCategoryId { get; set; }
        public string MistryOrLabour { get; set; }



        public IEnumerable<LaborCategory> ListParentCategories { get; set; }
        public IEnumerable<LaborCategory> ListChildCategories { get; set; }

        public async Task GetListParentCategoryAsync()
        {
            ListParentCategories = await listingContext.LabourCategory
                .Where(i => i.IsChild == false)
                .ToListAsync();
        }

        public async Task GetListChildCategoryAsync(int parentCatId)
        {
            ListChildCategories = await listingContext.LabourCategory
                .Where(i => i.IsChild == true)
                .Where(i => i.ParentCategoryId == parentCatId)
                .ToListAsync();
        }

        public async Task UpdateChildCategoryAsync(ChangeEventArgs e)
        {
            int parentCatId = Int32.Parse(e.Value.ToString());
            await GetListChildCategoryAsync(parentCatId);

            // Assign Value to Parent Category ID
            ParentCategoryId = parentCatId;
        }

        // Begin: Create
        public async Task CreateAsync()
        {
            try
            {
                Busy = true;

                // Get Existing Record
                var recordExit = await listingContext.Classification
                    .Where(i => i.UserGuid == CurrentUserGuid)
                    .FirstOrDefaultAsync();

                if (recordExit == null)
                {

                    Classification clf = new Classification
                    {
                        ParentCategoryId = ParentCategoryId.Value,
                        ChildCategoryId = ChildCategoryId,
                        MistryORLabour = MistryOrLabour,
                        UserGuid = CurrentUserGuid
                    };

                    await listingContext.AddAsync(clf);
                    await listingContext.SaveChangesAsync();


                    // Navigate To Create
                    navManager.NavigateTo("/MyAccount/LabourNakaWizard/Finish");

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
            RecordExist = await listingContext.Classification
                .AnyAsync(i => i.UserGuid == CurrentUserGuid);

            if (RecordExist == true)
            {
                navManager.NavigateTo("/MyAccount/LabourNakaWizard/CategoryEdit");
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

                    iUser = await applicationContext.Users.Where(i => i.UserName == user.Identity.Name).FirstOrDefaultAsync();
                    CurrentUserGuid = iUser.Id;

                    // Execute Method
                    await CheckRecordExistAsync();
                    await GetListParentCategoryAsync();
                }
            }
            catch (Exception exc)
            {
                ErrorMessage = exc.Message;
            }
        }
    }
}