using AntDesign;
using BAL.Services.Contracts;
using BOL.BANNERADS;
using BOL.ComponentModels.Listings;
using BOL.SHARED;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FRONTEND.BLAZOR.Listings
{
    public partial class ListingResults
    {
        [Parameter] public string url { get; set; }
        [Parameter] public string level { get; set; }

        [Inject] private IListingService listingService { get; set; }
        [Inject] private AuthenticationStateProvider authenticationState { get; set; }

        public IList<ListingResultVM> ListingResultVM = new List<ListingResultVM>();

        public ListingResultBannerVM ListingResultBannerVM { get; set; } = new ListingResultBannerVM();

        public PageVM PageVM { get; set; } = new PageVM();

        // Begin: Get All Category Banner
        public int Banner1Count { get; set; }
        public int Banner2Count { get; set; }
        public int Banner3Count { get; set; }
        public bool userAuthenticated { get; set; }


        protected async override Task OnInitializedAsync()
        {
            await GetListings(null);
            ListingResultBannerVM = await listingService.GetListingResultBannersByUrl(url);

            var authstate = await authenticationState.GetAuthenticationStateAsync();
            var user = authstate.User;
            userAuthenticated = user.Identity.IsAuthenticated;
        }

        protected override async Task OnAfterRenderAsync(bool render)
        {
            if (render)
            {
                await jsRuntime.InvokeVoidAsync("InitializeCarousel");
            }
        }

        public bool showModal { get; set; } = false;
        public int listingid { get; set; }

        public async Task ShowModal(int listingId)
        {
            showModal = true;
            listingid = listingid;
            await Task.Delay(5);
        }

        public async Task GetListings(PaginationEventArgs e)
        {
            if (e != null)
                PageVM.CurrentPage = e.Page;
            ListingResultVM = await listingService.GetListings(url, level, PageVM);
            await Task.Delay(50);
        }

        public IEnumerable<CategoryBanner> CategoryBannerList { get; set; }
    }
}
