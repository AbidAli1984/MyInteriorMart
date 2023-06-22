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
        public bool isPasswordGenerated = false;

        public async Task GenerateOTP()
        {
            var test = await userService.GenerateOTP(phoneNumber);
            bool value = test;
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