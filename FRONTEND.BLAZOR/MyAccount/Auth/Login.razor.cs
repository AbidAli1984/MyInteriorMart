using BOL.AUDITTRAIL;
using BOL.VIEWMODELS;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace FRONTEND.BLAZOR.MyAccount.Auth
{
    public partial class Login
    {
        // Begin: Check if record exisit with listingId
        public string currentPage = "nav-address";
        public bool buttonBusy { get; set; }
        public bool disable { get; set; }

        [Inject]
        private IHttpContextAccessor httpConAccess { get; set; }
        public string CurrentUserGuid { get; set; }
        public string ErrorMessage { get; set; }
        public bool userAuthenticated { get; set; } = false;
        public string IpAddress { get; set; }
        public IdentityUser iUser { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime CreatedTime { get; set; }
        public string OwnerGuid { get; set; }
        public string IpAddressUser { get; set; }

        public IEnumerable<Bookmarks> userBookmarks { get; set; }

        public IList<BookmarkListingViewModel> listBLVM = new List<BookmarkListingViewModel>();

        //public async Task GetUsersBookmarksAsync()
        //{
        //    //var secondCategory = await categoriesContext.SecondCategory.Where(x => x.SecondCategoryID == category.SecondCategoryID).FirstOrDefaultAsync();
        //    string value = "123";
        //    var secondCategory = await value.Where(x => x.Equals(""));
        //}

        //protected async override Task OnInitializedAsync()
        //{
        //    try
        //    {
        //        await GetUsersBookmarksAsync();
        //    }
        //    catch (Exception exc)
        //    {
        //        ErrorMessage = exc.Message;
        //    }
        //}
    }
}