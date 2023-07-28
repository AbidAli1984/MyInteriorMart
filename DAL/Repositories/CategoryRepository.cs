using BOL.CATEGORIES;
using DAL.CATEGORIES;
using DAL.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly CategoriesDbContext categoryContext;

        public CategoryRepository(CategoriesDbContext categoryContext)
        {
            this.categoryContext = categoryContext;
        }

        public async Task<IList<FirstCategory>> GetFirstCategoriesAsync()
        {
            return await categoryContext.FirstCategory
                .OrderByDescending(i => i.SortOrder)
                .ToListAsync();
        }

        public async Task<IList<SecondCategory>> GetSecCategoriesByFirstCategoryId(int firstCategoryId)
        {
            return await categoryContext.SecondCategory
                .Where(c => c.FirstCategoryID == firstCategoryId)
                .OrderByDescending(c => c.SecondCategoryID)
                .ToListAsync();
        }

        public async Task<IList<ThirdCategory>> GetThirdCategoriesBySeconCategoryId(int secondCategoryId)
        {
            return await categoryContext.ThirdCategory
                .Where(c => c.SecondCategoryID == secondCategoryId)
                .OrderByDescending(c => c.ThirdCategoryID)
                .ToListAsync();
        }

        public async Task<IList<FourthCategory>> GetForthCategoriesBySecondCategoryId(int secondCategoryId)
        {
            return await categoryContext.FourthCategory
                .Where(c => c.SecondCategoryID == secondCategoryId)
                .OrderByDescending(c => c.FourthCategoryID)
                .ToListAsync();
        }

        public async Task<IList<FifthCategory>> GetFifthCategoriesBySecondCategoryId(int secondCategoryId)
        {
            return await categoryContext.FifthCategory
                .Where(c => c.SecondCategoryID == secondCategoryId)
                .OrderByDescending(c => c.FifthCategoryID)
                .ToListAsync();
        }

        public async Task<IList<SixthCategory>> GetSixthCategoriesBySecondCategoryId(int secondCategoryId)
        {
            return await categoryContext.SixthCategory
                .Where(c => c.SecondCategoryID == secondCategoryId)
                .OrderByDescending(c => c.SixthCategoryID)
                .ToListAsync();
        }

        public async Task<FirstCategory> GetFirstCategoryById(int id)
        {
            return await categoryContext.FirstCategory.FindAsync(id);
        }

        public async Task<SecondCategory> GetSecondCategoryById(int id)
        {
            return await categoryContext.SecondCategory.FindAsync(id);
        }

        public async Task<FirstCategory> GetFirstCategoryByURL(string url)
        {
            return await categoryContext.FirstCategory
                .Where(c => c.URL == url)
                .FirstOrDefaultAsync();
        }

        public async Task<SecondCategory> GetSecondCategoryByURL(string url)
        {
            return await categoryContext.SecondCategory
                .Where(c => c.URL == url)
                .FirstOrDefaultAsync();
        }

        public async Task<ThirdCategory> GetThirdCategoryByURL(string url)
        {
            return await categoryContext.ThirdCategory
                .Where(c => c.URL == url)
                .FirstOrDefaultAsync();
        }

        public async Task<FourthCategory> GetFourthCategoryByURL(string url)
        {
            return await categoryContext.FourthCategory
                .Where(c => c.URL == url)
                .FirstOrDefaultAsync();
        }

        public async Task<FifthCategory> GetFifthCategoryByURL(string url)
        {
            return await categoryContext.FifthCategory
                .Where(c => c.URL == url)
                .FirstOrDefaultAsync();
        }

        public async Task<SixthCategory> GetSixthCategoryByURL(string url)
        {
            return await categoryContext.SixthCategory
                .Where(c => c.URL == url)
                .FirstOrDefaultAsync();
        }
    }
}
