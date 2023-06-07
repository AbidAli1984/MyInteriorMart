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
    public partial class Fifth
    {
        [Parameter]
        public string fourthCatUrl { get; set; }

        public IList<CategoryVM> listCategoryVM = new List<CategoryVM>();

        public IEnumerable<FifthCategory> fifthCategories { get; set; }

        public async Task GetFifthCategories()
        {
            fifthCategories = await categoryContext.FifthCategory.Where(c => c.FourthCategory.URL == fourthCatUrl).ToListAsync();

            if (fifthCategories != null)
            {
                foreach (var cat in fifthCategories)
                {
                    // Begin: If contain child
                    bool hasChild = await categoryContext.SixthCategory.AnyAsync(i => i.FifthCategoryID == cat.FifthCategoryID);
                    // End: If contain child

                    CategoryVM categoryVM = new CategoryVM
                    {
                        ID = cat.FifthCategoryID,
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
