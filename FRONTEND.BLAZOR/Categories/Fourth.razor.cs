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
    public partial class Fourth
    {
        [Parameter]
        public string thirdCatUrl { get; set; }

        public IList<CategoryVM> listCategoryVM = new List<CategoryVM>();

        public IEnumerable<FourthCategory> fourthCategories { get; set; }

        public async Task GetFourthCategories()
        {
            fourthCategories = await categoryContext.FourthCategory.Where(c => c.ThirdCategory.URL == thirdCatUrl).ToListAsync();

            if (fourthCategories != null)
            {
                foreach (var cat in fourthCategories)
                {
                    // Begin: If contain child
                    bool hasChild = await categoryContext.FifthCategory.AnyAsync(i => i.FourthCategoryID == cat.FourthCategoryID);
                    // End: If contain child

                    CategoryVM categoryVM = new CategoryVM
                    {
                        ID = cat.FourthCategoryID,
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
            await GetFourthCategories();
        }
    }
}
