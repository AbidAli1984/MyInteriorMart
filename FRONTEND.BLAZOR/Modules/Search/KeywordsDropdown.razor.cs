using BAL;
using BAL.Services.Contracts;
using BOL.VIEWMODELS;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FRONTEND.BLAZOR.Modules.Search
{
    public partial class KeywordsDropdown
    {
        [Parameter] public SearchResultViewModel SelectedListing { get; set; }
        [Parameter] public EventCallback CreateKeywordClick { get; set; }
        [Inject] private IListingService listingService { get; set; }
        public string Keywords { get; set; }

        public SearchResultViewModel SelectedKeyword { get; set; } = new SearchResultViewModel();

        protected async override Task OnInitializedAsync()
        {
            Constants.Keywords = await listingService.GetKeywords();
        }

        private async Task<IEnumerable<SearchResultViewModel>> SearchListings(string searchText)
        {
            Keywords = searchText;
            return await Task.FromResult(Constants.Keywords.Where(x => x.label.ToLower().Contains(searchText.ToLower())).ToList());
        }

        public async void CreateKeyword()
        {
            if (SelectedKeyword != null && !string.IsNullOrWhiteSpace(SelectedKeyword.label))
                Keywords = SelectedKeyword.label;

            SelectedListing.label = Keywords;
            await CreateKeywordClick.InvokeAsync();
            Keywords = string.Empty;
            SelectedKeyword = null;
            Constants.Keywords = await listingService.GetKeywords();
        }
    }
}
