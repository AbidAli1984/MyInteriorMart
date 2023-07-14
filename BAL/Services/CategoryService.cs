using BAL.Services.Contracts;
using BOL.CATEGORIES;
using BOL.ComponentModels.Pages;
using DAL.Repositories.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BAL.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task GetCategoriesForIndexPage(IndexVM indexVM)
        {
            indexVM.Services = await _categoryRepository.GetSecCategoriesByFirstCategoryId(Constants.Cat_Services);
            indexVM.Contractors = await _categoryRepository.GetSecCategoriesByFirstCategoryId(Constants.Cat_Contractors);
            indexVM.Dealers = await _categoryRepository.GetSecCategoriesByFirstCategoryId(Constants.Cat_Dealers);
        }
    }
}
