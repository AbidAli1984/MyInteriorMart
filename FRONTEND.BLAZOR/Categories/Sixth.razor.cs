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
    public partial class Sixth
    {
        [Parameter]
        public string fifthCatUrl { get; set; }

        public IList<CategoryVM> listCategoryVM = new List<CategoryVM>();

        public IEnumerable<SixthCategory> sixthCategories { get; set; }

        public async Task GetFifthCategories()
        {
            sixthCategories = await categoryContext.SixthCategory.Where(c => c.FifthCategory.URL == fifthCatUrl).ToListAsync();

            if (sixthCategories != null)
            {
                foreach (var cat in sixthCategories)
                {
                    // Begin: If contain child
                    bool hasChild = await categoryContext.SixthCategory.AnyAsync(i => i.FifthCategoryID == cat.FifthCategoryID);
                    // End: If contain child

                    CategoryVM categoryVM = new CategoryVM
                    {
                        ID = cat.SixthCategoryID,
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
            await GetFifthCategories();
        }
    }
}
