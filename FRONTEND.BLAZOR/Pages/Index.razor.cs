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

        public IEnumerable<SecondCategory> catServices { get; set; }
        public IEnumerable<SecondCategory> catContractors { get; set; }
        public IEnumerable<SecondCategory> catDealers { get; set; }
        public IEnumerable<SecondCategory> catManufacturers { get; set; }

        public IndexVM indexVM { get; set; }

        protected async override Task OnInitializedAsync()
        {
            indexVM = await listingService.GetHomeBannerList();
            await GetServices();
            await GetContractors();
            await GetDealers();
            await GetManufacturers();
        }

        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await jsRuntime.InvokeVoidAsync("Coursel.initializeHomePageCarousel");
            }
        }

        IList<HomeBanner> HomeBannerList { get; set; }

        public async Task GetHomeBannerListAsync()
        {
            
            StateHasChanged();
        }

        public async Task GetServices()
        {
            catServices = await categoryService.GetSecondCategoriesHomeAsync("Services");
        }

        public async Task GetContractors()
        {
            catContractors = await categoryService.GetSecondCategoriesHomeAsync("Contractors");
        }

        public async Task GetDealers()
        {
            catDealers = await categoryService.GetSecondCategoriesHomeAsync("Dealers");
        }

        public async Task GetManufacturers()
        {
            catManufacturers = await categoryService.GetSecondCategoriesHomeAsync("Manufacturers");
        }
    }
}
