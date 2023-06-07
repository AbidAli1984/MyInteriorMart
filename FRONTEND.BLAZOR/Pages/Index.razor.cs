using BOL.BANNERADS;
using BOL.CATEGORIES;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FRONTEND.BLAZOR.Pages
{
    public partial class Index
    {
        public IEnumerable<SecondCategory> catServices { get; set; }
        public IEnumerable<SecondCategory> catContractors { get; set; }
        public IEnumerable<SecondCategory> catDealers { get; set; }
        public IEnumerable<SecondCategory> catManufacturers { get; set; }

        public int HomeBanner1Count { get; set; }
        public int HomeBanner2Count { get; set; }
        public int HomeBanner3Count { get; set; }
        public int HomeBanner4Count { get; set; }
        public int HomeBanner5Count { get; set; }
        public int HomeBanner6Count { get; set; }
        public int HomeBanner7Count { get; set; }
        public int HomeBanner8Count { get; set; }
        public int HomeBanner9Count { get; set; }
        public int HomeBanner10Count { get; set; }
        public int HomeBanner11Count { get; set; }

        public async Task GetServices()
        {
            catServices = await categoryRepo.GetSecondCategoriesHomeAsync("Services");
        }

        public async Task GetContractors()
        {
            catContractors = await categoryRepo.GetSecondCategoriesHomeAsync("Contractors");
        }

        public async Task GetDealers()
        {
            catDealers = await categoryRepo.GetSecondCategoriesHomeAsync("Dealers");
        }

        public async Task GetManufacturers()
        {
            catManufacturers = await categoryRepo.GetSecondCategoriesHomeAsync("Manufacturers");
        }

        // Begin: Get All Home Banner
         public IEnumerable<HomeBanner> HomeBannerList { get; set; }
        public async Task GetHomeBannerListAsync()
        {
            HomeBannerList = await listingContext.HomeBanner
                .OrderBy(i => i.Priority)
                .ToListAsync();

            HomeBanner1Count = HomeBannerList.Where(i => i.Placement == "banner-1").Count();
            HomeBanner2Count = HomeBannerList.Where(i => i.Placement == "banner-2").Count();
            HomeBanner3Count = HomeBannerList.Where(i => i.Placement == "banner-3").Count();
            HomeBanner4Count = HomeBannerList.Where(i => i.Placement == "banner-4").Count();
            HomeBanner5Count = HomeBannerList.Where(i => i.Placement == "banner-5").Count();
            HomeBanner6Count = HomeBannerList.Where(i => i.Placement == "banner-6").Count();
            HomeBanner7Count = HomeBannerList.Where(i => i.Placement == "banner-7").Count();
            HomeBanner8Count = HomeBannerList.Where(i => i.Placement == "banner-8").Count();
            HomeBanner9Count = HomeBannerList.Where(i => i.Placement == "banner-9").Count();
            HomeBanner10Count = HomeBannerList.Where(i => i.Placement == "banner-10").Count();
            HomeBanner11Count = HomeBannerList.Where(i => i.Placement == "banner-11").Count();
        }
        // End: Get All Home Banner

        protected async override Task OnInitializedAsync()
        {
            await GetServices();
            await GetContractors();
            await GetDealers();
            await GetManufacturers();
            await GetHomeBannerListAsync();
        }

        protected override async Task OnAfterRenderAsync(bool render)
        {
            if(render)
            {
                await jsRuntime.InvokeVoidAsync("InitializeCarousel");
            }
        }
    }
}
