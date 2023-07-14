using BAL.Services.Contracts;
using BOL.BANNERADS;
using BOL.CATEGORIES;
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
        ICategoryService categoryService { get; set; }

        public IEnumerable<SecondCategory> catServices { get; set; }
        public IEnumerable<SecondCategory> catContractors { get; set; }
        public IEnumerable<SecondCategory> catDealers { get; set; }
        public IEnumerable<SecondCategory> catManufacturers { get; set; }

        public IList<HomeBanner> HomeBannerTop { get; set; }
        public IList<HomeBanner> HomeBannerMiddle1 { get; set; }
        public IList<HomeBanner> HomeBannerMiddle2 { get; set; }
        public IList<HomeBanner> HomeBannerBottom { get; set; }
        public int HomeBannerTopLimit { get; set; } = 4;
        public int HomeBannerMiddle1Limit { get; set; } = 2;
        public int HomeBannerMiddle2Limit { get; set; } = 2;
        public int HomeBannerBottomLimit { get; set; } = 2;

        protected async override Task OnInitializedAsync()
        {
            await GetHomeBannerListAsync();
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
            HomeBannerList = await listingContext.HomeBanner
                .OrderBy(i => i.Priority)
                .ToListAsync();

            HomeBannerTop = HomeBannerList.Where(i => i.Placement == "HomeTop").ToList();
            HomeBannerTopLimit = HomeBannerTop.Count > HomeBannerTopLimit ? HomeBannerTop.Count : HomeBannerTopLimit;
            HomeBannerMiddle1 =  HomeBannerList.Where(i => i.Placement == "HomeMiddle1").ToList();
            HomeBannerMiddle1Limit = HomeBannerMiddle1.Count > HomeBannerMiddle1Limit ? HomeBannerMiddle1.Count : HomeBannerMiddle1Limit;
            HomeBannerMiddle2 = HomeBannerList.Where(i => i.Placement == "HomeMiddle2").ToList();
            HomeBannerMiddle2Limit = HomeBannerMiddle2.Count > HomeBannerMiddle2Limit ? HomeBannerMiddle2.Count : HomeBannerMiddle2Limit;
            HomeBannerBottom = HomeBannerList.Where(i => i.Placement == "HomeBottom").ToList();
            HomeBannerBottomLimit = HomeBannerBottom.Count > HomeBannerBottomLimit ? HomeBannerBottom.Count : HomeBannerBottomLimit;
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
