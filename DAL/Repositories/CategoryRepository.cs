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

        public async Task<IEnumerable<FirstCategory>> GetFirstCategoriesAsync()
        {
            return await categoryContext.FirstCategory
                .OrderByDescending(i => i.SortOrder)
                .ToListAsync();
        }

        public async Task<IEnumerable<SecondCategory>> GetSecondCategoriesAsync()
        {
            return await categoryContext.SecondCategory
                .OrderByDescending(i => i.SortOrder)
                .ToListAsync();
        }

        public async Task<IEnumerable<ThirdCategory>> GetThirdCategoryAsync()
        {
            return await categoryContext.ThirdCategory
                .OrderByDescending(i => i.SortOrder)
                .ToListAsync();
        }

        public async Task<IEnumerable<FourthCategory>> GetFourthCategoryAsync()
        {
            return await categoryContext.FourthCategory
                .OrderByDescending(i => i.SortOrder)
                .ToListAsync();
        }

        public async Task<IEnumerable<FifthCategory>> GetFifthCategoryAsync()
        {
            return await categoryContext.FifthCategory
                .OrderByDescending(i => i.SortOrder)
                .ToListAsync();
        }

        public async Task<IEnumerable<SixthCategory>> GetSixthCategoryAsync()
        {
            return await categoryContext.SixthCategory
                .OrderByDescending(i => i.SortOrder)
                .ToListAsync();
        }
        // End:

        // Shafi:
        public async Task<FirstCategory> FirstCategoryDetailsAsync(int id)
        {
            return await categoryContext.FirstCategory
                .Where(i => i.FirstCategoryID == id)
                .FirstOrDefaultAsync();
        }

        public async Task<SecondCategory> SecondCategoryDetailsAsync(int id)
        {
            return await categoryContext.SecondCategory
                .Where(i => i.SecondCategoryID == id)
                .Include(i => i.FirstCategory)
                .FirstOrDefaultAsync();
        }

        public async Task<ThirdCategory> ThirdCategoryDetailsAsync(int id)
        {
            return await categoryContext.ThirdCategory
                .Where(i => i.ThirdCategoryID == id)
                .Include(i => i.FirstCategory)
                .Include(i => i.SecondCategory)
                .FirstOrDefaultAsync();
        }

        public async Task<FourthCategory> FourthCategoriesDetailsAsync(int id)
        {
            return await categoryContext.FourthCategory
                .Where(i => i.FourthCategoryID == id)
                .Include(i => i.FirstCategory)
                .Include(i => i.SecondCategory)
                .Include(i => i.ThirdCategory).FirstOrDefaultAsync();
        }

        public async Task<FifthCategory> FifthCategoriesDetailsAsync(int id)
        {
            return await categoryContext.FifthCategory
                .Where(i => i.FifthCategoryID == id)
                .Include(i => i.FirstCategory)
                .Include(i => i.SecondCategory)
                .Include(i => i.ThirdCategory)
                .Include(i => i.FourthCategory)
                .FirstOrDefaultAsync();
        }

        public async Task<SixthCategory> SixthCategoriesDetailsAsync(int id)
        {
            return await categoryContext.SixthCategory
                .Where(i => i.SixthCategoryID == id)
                .Include(i => i.FirstCategory)
                .Include(i => i.SecondCategory)
                .Include(i => i.ThirdCategory)
                .Include(i => i.FourthCategory)
                .Include(i => i.FifthCategory)
                .FirstOrDefaultAsync();
        }
        // End:

        // Shafi: Home Second Categories Where First Category Name is Parameter
        public async Task<IList<SecondCategory>> GetSecCategoriesByFirstCategoryId(int firstCategoryId)
        {
            return await categoryContext.SecondCategory
                .Where(c => c.FirstCategoryID == firstCategoryId)
                .OrderByDescending(c => c.SecondCategoryID)
                .ToListAsync();
        }
        // End:

        // Shafi: Check if current category contains more than 1 category and return true false
        public async Task<bool> ThirdHaveFourthAsync(int thirdId)
        {
            var result = await categoryContext.FourthCategory
                .Where(c => c.ThirdCategoryID == thirdId)
                .CountAsync();

            return result != 0;
        }

        public async Task<bool> FourthHaveFifthAsync(int fourthId)
        {
            var result = await categoryContext.FifthCategory
                .Where(c => c.FourthCategoryID == fourthId)
                .CountAsync();

            return result != 0;
        }

        public async Task<bool> FifthHaveSixthAsync(int fifthId)
        {
            var result = await categoryContext.SixthCategory
                .Where(c => c.FifthCategoryID == fifthId)
                .CountAsync();

            return result != 0;
        }
        // End:

        // Shafi: Get Deep Categories
        public async Task<IEnumerable<SecondCategory>> GetSecondCategoriesDeepAsync(int firstCatId)
        {
            return await categoryContext.SecondCategory
                .Where(i => i.FirstCategoryID == firstCatId)
                .OrderByDescending(i => i.SearchCount)
                .Include(i => i.FirstCategory)
                .ToListAsync();
        }

        public async Task<IEnumerable<ThirdCategory>> GetThirdCategoryDeepAsync(int secondCatId)
        {
            return await categoryContext.ThirdCategory
                .Where(i => i.SecondCategoryID == secondCatId)
                .OrderByDescending(i => i.SearchCount)
                .Include(i => i.FirstCategory)
                .Include(i => i.SecondCategory)
                .ToListAsync();
        }

        public async Task<IEnumerable<FourthCategory>> GetFourthCategoryDeepAsync(int thirdCatId)
        {
            return await categoryContext.FourthCategory
                .Where(i => i.ThirdCategoryID == thirdCatId)
                .OrderByDescending(i => i.SearchCount)
                .Include(i => i.FirstCategory)
                .Include(i => i.SecondCategory)
                .Include(i => i.ThirdCategory)
                .ToListAsync();
        }

        public async Task<IEnumerable<FifthCategory>> GetFifthCategoryDeepAsync(int fourthCatId)
        {
            return await categoryContext.FifthCategory
                .Where(i => i.FourthCategoryID == fourthCatId)
                .OrderByDescending(i => i.SearchCount)
                .Include(i => i.FirstCategory)
                .Include(i => i.SecondCategory)
                .Include(i => i.ThirdCategory)
                .Include(i => i.FourthCategory)
                .ToListAsync();
        }

        public async Task<IEnumerable<SixthCategory>> GetSixthCategoryDeepAsync(int fifthCatId)
        {
            return await categoryContext.SixthCategory
                .Where(i => i.FifthCategoryID == fifthCatId)
                .OrderByDescending(i => i.SearchCount)
                .Include(i => i.FirstCategory)
                .Include(i => i.SecondCategory)
                .Include(i => i.ThirdCategory)
                .Include(i => i.FourthCategory)
                .Include(i => i.FifthCategory)
                .ToListAsync();
        }

        // Shafi: Count Deep Categories
        public async Task<int> CountSecondCategoriesDeepAsync(int firstCatId)
        {
            return await categoryContext.SecondCategory
                .Where(i => i.FirstCategoryID == firstCatId)
                .CountAsync();

        }

        public async Task<int> CountThirdCategoryDeepAsync(int secondCatId)
        {
            return await categoryContext.ThirdCategory
                .Where(i => i.SecondCategoryID == secondCatId)
                .CountAsync();
        }

        public async Task<int> CountFourthCategoryDeepAsync(int thirdCatId)
        {
            return await categoryContext.FourthCategory
                .Where(i => i.ThirdCategoryID == thirdCatId)
                .CountAsync();
        }

        public async Task<int> CountFifthCategoryDeepAsync(int fourthCatId)
        {
            return await categoryContext.FifthCategory
                .Where(i => i.FourthCategoryID == fourthCatId)
                .CountAsync();
        }

        public async Task<int> CountSixthCategoryDeepAsync(int fifthCatId)
        {
            return await categoryContext.SixthCategory
                .Where(i => i.FirstCategoryID == fifthCatId)
                .CountAsync();
        }

        public string FirstCategoryName(int firstCatId)
        {
            return categoryContext.FirstCategory
                .Where(i => i.FirstCategoryID == firstCatId)
                .Select(i => i.Name)
                .FirstOrDefault();
        }

        public string SecondCategoryName(int secondCatId)
        {
            return categoryContext.SecondCategory
                .Where(i => i.SecondCategoryID == secondCatId)
                .Select(i => i.Name)
                .FirstOrDefault();
        }

        public async Task<bool> FirstCategoryDuplicate(string businessCategoryName)
        {
            return await categoryContext.FirstCategory
                .AnyAsync(i => i.SearchKeywordName == businessCategoryName);
        }

        public async Task<bool> SecondCategoryDuplicate(string businessCategoryName)
        {
            return await categoryContext.SecondCategory
                .AnyAsync(i => i.SearchKeywordName == businessCategoryName);
        }

        public async Task<bool> ThirdCategoryDuplicate(string businessCategoryName)
        {
            return await categoryContext.ThirdCategory
                .AnyAsync(i => i.SearchKeywordName == businessCategoryName);
        }

        public async Task<bool> FourthCategoryDuplicate(string businessCategoryName)
        {
            return await categoryContext.FourthCategory
                .AnyAsync(i => i.SearchKeywordName == businessCategoryName);
        }

        public async Task<bool> FifthCategoryDuplicate(string businessCategoryName)
        {
            return await categoryContext.FifthCategory
                .AnyAsync(i => i.SearchKeywordName == businessCategoryName);
        }

        public async Task<bool> SixthCategoryDuplicate(string businessCategoryName)
        {
            return await categoryContext.SixthCategory
                .AnyAsync(i => i.SearchKeywordName == businessCategoryName);
        }
        // End:

        // Shafi: Categories has childrens or not
        public bool FirstCatHasSecondCat(int firstCatId)
        {
            return categoryContext.SecondCategory
                .Any(i => i.FirstCategoryID == firstCatId);
        }

        public bool SecondCatHasThirdCat(int secondCatId)
        {
            return categoryContext.ThirdCategory
                .Any(i => i.SecondCategoryID == secondCatId);
        }

        public bool ThirdCatHasFourthCat(int thirdCatId)
        {
            return categoryContext.FourthCategory
                .Any(i => i.ThirdCategoryID == thirdCatId);
        }

        public bool FourthCatHasFifthCat(int fourthCatId)
        {
            return categoryContext.FifthCategory
                .Any(i => i.FourthCategoryID == fourthCatId);
        }

        public bool FifthCatHasSixthCat(int fifthCatId)
        {
            return categoryContext.SixthCategory
                .Any(i => i.FifthCategoryID == fifthCatId);
        }
        // End:

        // Shafi: Check if categories are empty or not
        public bool FirstCategoryIsNotEmpty(int firstCatId)
        {
            var secondCategoryExist = categoryContext.SecondCategory.Any(i => i.FirstCategoryID == firstCatId);
            var keywordExists = categoryContext.KeywordFirstCategory.Any(i => i.FirstCategoryID == firstCatId);
            var featuredKeywordExists = categoryContext.ListingTitle.Any(i => i.FirstCategoryID == firstCatId);

            return secondCategoryExist == true || keywordExists == true || featuredKeywordExists == true;
        }

        public bool SecondCategoryIsNotEmpty(int secondCatId)
        {
            var thirdCategoryExist = categoryContext.ThirdCategory.Any(i => i.SecondCategoryID == secondCatId);
            var keywordExists = categoryContext.KeywordSecondCategory.Any(i => i.SecondCategoryID == secondCatId);
            var featuredKeywordExists = categoryContext.ListingTitle.Any(i => i.SecondCategoryID == secondCatId);

            return thirdCategoryExist == true || keywordExists == true || featuredKeywordExists == true;
        }

        public bool ThirdCategoryIsNotEmpty(int thirdCatId)
        {
            var fourthCategoryExist = categoryContext.FourthCategory.Any(i => i.ThirdCategoryID == thirdCatId);
            var keywordExists = categoryContext.KeywordThirdCategory.Any(i => i.ThirdCategoryID == thirdCatId);

            return fourthCategoryExist == true || keywordExists == true;
        }

        public bool FourthCategoryIsNotEmpty(int fourthCatId)
        {
            var fifthCategoryExist = categoryContext.FifthCategory.Any(i => i.FourthCategoryID == fourthCatId);
            var keywordExists = categoryContext.KeywordFourthCategory.Any(i => i.FourthCategoryID == fourthCatId);

            return fifthCategoryExist == true || keywordExists == true;
        }

        public bool FifthCategoryIsNotEmpty(int fifthCatId)
        {
            var sixthCategoryExist = categoryContext.SixthCategory.Any(i => i.FifthCategoryID == fifthCatId);
            var keywordExists = categoryContext.KeywordFifthCategory.Any(i => i.FifthCategoryID == fifthCatId);

            return sixthCategoryExist == true || keywordExists == true;
        }

        public bool SixthCategoryIsNotEmpty(int sixthCatId)
        {
            var keywordExists = categoryContext.KeywordSixthCategory.Any(i => i.SixthCategoryID == sixthCatId);

            return keywordExists == true;
        }
        // End:

        // Shafi: Count syncronously child categories
        public int CountChildOfFirstCategory(int firstCatId)
        {
            var count = categoryContext.SecondCategory.Where(i => i.FirstCategoryID == firstCatId).Count();
            return count;
        }

        public int CountChildOfSecondCategory(int secondCatId)
        {
            var count = categoryContext.ThirdCategory.Where(i => i.SecondCategoryID == secondCatId).Count();
            return count;
        }

        public int CountChildOfThirdCategory(int thirdCatId)
        {
            var count = categoryContext.FourthCategory.Where(i => i.ThirdCategoryID == thirdCatId).Count();
            return count;
        }

        public int CountChildOfFourthCategory(int fourthCatId)
        {
            var count = categoryContext.FifthCategory.Where(i => i.FourthCategoryID == fourthCatId).Count();
            return count;
        }

        public int CountChildOfFifthCategory(int fifthCatId)
        {
            var count = categoryContext.SixthCategory.Where(i => i.FifthCategoryID == fifthCatId).Count();
            return count;
        }
        // End:
    }
}
