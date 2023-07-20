using BAL.Services.Contracts;
using BOL.AUDITTRAIL;
using BOL.VIEWMODELS;
using DAL.AUDIT;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace FRONTEND.BLAZOR.MyAccount.ManageListing
{
    public partial class AllLikes
    {
        [Inject]
        public IUserService userService { get; set; }

        public string CurrentUserGuid { get; set; }
        public bool isVendor { get; set; } = false;

        public IEnumerable<ListingLikeDislike> userLikes { get; set; }

        public IList<LikeListingViewModel> listLLVM = new List<LikeListingViewModel>();

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
                    await GetUsersLikesAsync();
                }
            }
            catch (Exception exc)
            {
                string ErrorMessage = exc.Message;
            }
        }

        public async Task GetUsersLikesAsync()
        {
            userLikes = await auditContext.ListingLikeDislike.Where(i => i.UserGuid == CurrentUserGuid && i.Like == true).OrderByDescending(i => i.LikeDislikeID).ToListAsync();

            foreach (var i in userLikes)
            {
                var listing = await listingContext.Listing.Where(x => x.ListingID == i.ListingID).FirstOrDefaultAsync();
                var category = await listingContext.Categories.Where(x => x.ListingID == i.ListingID).FirstOrDefaultAsync();
                var firstCategory = await categoriesContext.FirstCategory.Where(x => x.FirstCategoryID == category.FirstCategoryID).FirstOrDefaultAsync();
                var secondCategory = await categoriesContext.SecondCategory.Where(x => x.SecondCategoryID == category.SecondCategoryID).FirstOrDefaultAsync();

                if (listing != null)
                {
                    LikeListingViewModel rlvm = new LikeListingViewModel
                    {
                        LikeDislikeID = i.LikeDislikeID,
                        ListingID = i.ListingID,
                        OwnerGuid = i.UserGuid,
                        VisitDate = i.VisitDate.ToString("dd/MM/yyyy"),
                        Name = listing.CompanyName,
                        NameFirstLetter = listing.CompanyName[0].ToString(),
                        ListingUrl = listing.ListingURL,
                        FirstCat = firstCategory.Name,
                        SecondCat = secondCategory.Name
                    };

                    listLLVM.Add(rlvm);
                }
            }
        }
    }
}