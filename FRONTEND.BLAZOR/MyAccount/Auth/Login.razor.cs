using BOL.AUDITTRAIL;
using BOL.VIEWMODELS;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FRONTEND.BLAZOR.MyAccount.Auth
{
    public partial class Login
    {
        // Begin: Check if record exisit with listingId
        public string currentPage = "nav-address";

        public string phoneNumber { get; set; }

        public async Task GenerateOTP()
        {
            //var secondCategory = await categoriesContext.SecondCategory.Where(x => x.SecondCategoryID == category.SecondCategoryID).FirstOrDefaultAsync();
            var test = await user.GenerateOTP(phoneNumber);
            bool value = test;
            //var secondCategory = await value.Where(x => x.Equals(""));
        }

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