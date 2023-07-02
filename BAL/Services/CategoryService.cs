using BAL.Services.Contracts;
using BOL.CATEGORIES;
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
        public async Task<IEnumerable<SecondCategory>> GetSecondCategoriesHomeAsync(string firstCategory)
        {
            return await _categoryRepository.GetSecondCategoriesHomeAsync(firstCategory);
        }
    }
}
