using BAL;
using BAL.Services.Contracts;
using BOL.VIEWMODELS;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FRONTEND.BLAZOR.Modules.Search
{
    public partial class BasicSearch
    {
        [Inject] private IListingService listingService { get; set; }

        private SearchResultViewModel SelectedListing;
        protected async override Task OnInitializedAsync()
        {
            Constants.Listings = await listingService.GetSearchListings();
        }
        private async Task<IEnumerable<SearchResultViewModel>> SearchListings(string searchText)
        {
            return await Task.FromResult(Constants.Listings.Where(x => x.label.ToLower().Contains(searchText.ToLower())).ToList());
        }
    }
}
