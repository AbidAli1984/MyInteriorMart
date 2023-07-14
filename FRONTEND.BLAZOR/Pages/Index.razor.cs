using BAL.Services.Contracts;
using BOL.BANNERADS;
using BOL.CATEGORIES;
using BOL.ComponentModels.Pages;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FRONTEND.BLAZOR.Pages
{
    public partial class Index
    {
        [Inject]
        IListingService listingService { get; set; }

        [Inject]
        ICategoryService categoryService { get; set; }

        public IndexVM indexVM { get; set; } = new IndexVM();

        protected async override Task OnInitializedAsync()
        {
            indexVM = await listingService.GetHomeBannerList();
            await categoryService.GetCategoriesForIndexPage(indexVM);
            StateHasChanged();
        }

        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await jsRuntime.InvokeVoidAsync("initializeHomePageCarousel");
            }
        }

        IList<HomeBanner> HomeBannerList { get; set; }
    }
}
