using BOL.CATEGORIES;
using FRONTEND.BLAZOR.Models.ViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FRONTEND.BLAZOR.Categories
{
    public partial class Third
    {
        [Parameter]
        public string secondCatUrl { get; set; }

        public IList<CategoryVM> listCategoryVM = new List<CategoryVM>();

        public IEnumerable<ThirdCategory> thirdCategories { get; set; }

        public async Task GetThirdCategories()
        {
            thirdCategories = await categoryContext.ThirdCategory.Where(c => c.SecondCategory.URL == secondCatUrl).ToListAsync();

            if(thirdCategories != null)
            {
                foreach(var cat in thirdCategories)
                {
                    // Begin: If contain child
                    bool hasChild = await categoryContext.FourthCategory.AnyAsync(i => i.ThirdCategoryID == cat.ThirdCategoryID);
                    // End: If contain child

                    CategoryVM categoryVM = new CategoryVM
                    {
                        ID = cat.ThirdCategoryID,
                        Name = cat.Name,
                        Url = cat.URL,
                        HasChild = hasChild
                    };

                    listCategoryVM.Add(categoryVM);
                }
            }
        }

        protected async override Task OnInitializedAsync()
        {
            await GetThirdCategories();
        }
    }
}
