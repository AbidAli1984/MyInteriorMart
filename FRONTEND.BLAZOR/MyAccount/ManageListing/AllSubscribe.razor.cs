using BAL.Services.Contracts;
using BOL.AUDITTRAIL;
using BOL.VIEWMODELS;
using DAL.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FRONTEND.BLAZOR.MyAccount.ManageListing
{
    public partial class AllSubscribe
    {
        [Inject]
        public IUserService userService { get; set; }

        public string CurrentUserGuid { get; set; }
        public bool isVendor { get; set; } = false;

        public IEnumerable<Subscribes> userSubscribes { get; set; }

        public IList<SubscribeListingViewModel> listSLVM = new List<SubscribeListingViewModel>();

        protected async override Task OnInitializedAsync()
        {
            try
            {
                // Get User Name
                var authstate = await authenticationState.GetAuthenticationStateAsync();
                var user = authstate.User;
                if (user.Identity.IsAuthenticated)
                {
                    var applicationUser = await userService.GetUserByUserName(user.Identity.Name);
                    CurrentUserGuid = applicationUser.Id;
                    isVendor = applicationUser.IsVendor;
                    await GetUsersSubscribesAsync();
                }
            }
            catch (Exception exc)
            {
                string ErrorMessage = exc.Message;
            }
        }

        public async Task GetUsersSubscribesAsync()
        {
            userSubscribes = await auditContext.Subscribes.Where(i => i.UserGuid == CurrentUserGuid && i.Subscribe == true).OrderByDescending(i => i.SubscribeID).ToListAsync();

            foreach (var i in userSubscribes)
            {
                var listing = await listingContext.Listing.Where(x => x.ListingID == i.ListingID).FirstOrDefaultAsync();
                var category = await listingContext.Categories.Where(x => x.ListingID == i.ListingID).FirstOrDefaultAsync();
                var firstCategory = await categoriesContext.FirstCategory.Where(x => x.FirstCategoryID == category.FirstCategoryID).FirstOrDefaultAsync();
                var secondCategory = await categoriesContext.SecondCategory.Where(x => x.SecondCategoryID == category.SecondCategoryID).FirstOrDefaultAsync();

                if (listing != null)
                {
                    SubscribeListingViewModel slvm = new SubscribeListingViewModel
                    {
                        SubscribeID = i.SubscribeID,
                        ListingID = i.ListingID,
                        UserGuid = i.UserGuid,
                        VisitDate = i.VisitDate.ToString("dd/MM/yyyy"),
                        CompanyName = listing.CompanyName,
                        NameFirstLetter = listing.CompanyName[0].ToString(),
                        ListingUrl = listing.ListingURL,
                        FirstCat = firstCategory.Name,
                        SecondCat = secondCategory.Name
                    };

                    listSLVM.Add(slvm);
                }
            }
        }
    }
}