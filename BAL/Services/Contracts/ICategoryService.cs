using BOL.CATEGORIES;
using BOL.ComponentModels.MyAccount.ListingWizard;
using BOL.ComponentModels.Pages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BAL.Services.Contracts
{
    public interface ICategoryService
    {
        Task GetCategoriesForIndexPage(IndexVM indexVM);

        Task<IList<FirstCategory>> GetFirstCategoriesAsync();
        Task GetSecCategoriesByFirstCategoryId(CategoryVM categoryVM);
        Task GetOtherCategoriesBySeconCategoryId(CategoryVM categoryVM);
        void GetOtherCategoriesToUpdate(CategoryVM categoryVM);
        void MarkAllCategoriesSelected(CategoryVM categoryVM);
    }
}
