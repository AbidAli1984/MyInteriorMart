using BOL.CATEGORIES;
using DAL.CATEGORIES;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REPO
{
    public class CategoryRepo
    {
        private readonly CategoriesDbContext categoryContext;
        public CategoryRepo(CategoriesDbContext categoryContext)
        {
            this.categoryContext = categoryContext;
        }

        // Shafi: Get All
        public async Task<IEnumerable<FirstCategory>> GetFirstCategoriesAsync()
        {
            var result = await categoryContext.FirstCategory.OrderByDescending(i => i.SortOrder).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<SecondCategory>> GetSecondCategoriesAsync()
        {
            var result = await categoryContext.SecondCategory.OrderByDescending(i => i.SortOrder).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<ThirdCategory>> GetThirdCategoryAsync()
        {
            var result = await categoryContext.ThirdCategory.OrderByDescending(i => i.SortOrder).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<FourthCategory>> GetFourthCategoryAsync()
        {
            var result = await categoryContext.FourthCategory.OrderByDescending(i => i.SortOrder).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<FifthCategory>> GetFifthCategoryAsync()
        {
            var result = await categoryContext.FifthCategory.OrderByDescending(i => i.SortOrder).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<SixthCategory>> GetSixthCategoryAsync()
        {
            var result = await categoryContext.SixthCategory.OrderByDescending(i => i.SortOrder).ToListAsync();
            return result;
        }
        // End:

        // Shafi:
        public async Task<FirstCategory> FirstCategoryDetailsAsync(int id)
        {
            var result = await categoryContext.FirstCategory.Where(i => i.FirstCategoryID == id).FirstOrDefaultAsync();
            return result;
        }

        public async Task<SecondCategory> SecondCategoryDetailsAsync(int id)
        {
            var result = await categoryContext.SecondCategory.Where(i => i.SecondCategoryID == id).Include(i => i.FirstCategory).FirstOrDefaultAsync();
            return result;
        }

        public async Task<ThirdCategory> ThirdCategoryDetailsAsync(int id)
        {
            var result = await categoryContext.ThirdCategory.Where(i => i.ThirdCategoryID == id).Include(i => i.FirstCategory).Include(i => i.SecondCategory).FirstOrDefaultAsync();
            return result;
        }

        public async Task<FourthCategory> FourthCategoriesDetailsAsync(int id)
        {
            var result = await categoryContext.FourthCategory.Where(i => i.FourthCategoryID == id).Include(i => i.FirstCategory).Include(i => i.SecondCategory).Include(i => i.ThirdCategory).FirstOrDefaultAsync();
            return result;
        }

        public async Task<FifthCategory> FifthCategoriesDetailsAsync(int id)
        {
            var result = await categoryContext.FifthCategory.Where(i => i.FifthCategoryID == id).Include(i => i.FirstCategory).Include(i => i.SecondCategory).Include(i => i.ThirdCategory).Include(i => i.FourthCategory).FirstOrDefaultAsync();
            return result;
        }

        public async Task<SixthCategory> SixthCategoriesDetailsAsync(int id)
        {
            var result = await categoryContext.SixthCategory.Where(i => i.SixthCategoryID == id).Include(i => i.FirstCategory).Include(i => i.SecondCategory).Include(i => i.ThirdCategory).Include(i => i.FourthCategory).Include(i => i.FirstCategory).FirstOrDefaultAsync();
            return result;
        }
        // End:

        // Shafi: Home Second Categories Where First Category Name is Parameter
        public async Task<IEnumerable<SecondCategory>> GetSecondCategoriesHomeAsync(string FirstCategory)
        {
            var result = await categoryContext.SecondCategory.OrderByDescending(c => c.SecondCategoryID).Where(c => c.FirstCategory.Name == FirstCategory).ToListAsync();
            return result;
        }
        // End:

        // Shafi: Check if current category contains more than 1 category and return true false
        public async Task<bool> ThirdHaveFourthAsync(int thirdId)
        {
            var result = await categoryContext.FourthCategory.Where(c => c.ThirdCategoryID == thirdId).CountAsync();

            if (result != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> FourthHaveFifthAsync(int fourthId)
        {
            var result = await categoryContext.FifthCategory.Where(c => c.FourthCategoryID == fourthId).CountAsync();

            if (result != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> FifthHaveSixthAsync(int fifthId)
        {
            var result = await categoryContext.SixthCategory.Where(c => c.FifthCategoryID == fifthId).CountAsync();

            if (result != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        // End:

        // Shafi: Get Deep Categories
        public async Task<IEnumerable<SecondCategory>> GetSecondCategoriesDeepAsync(int firstCatId)
        {
            var result = await categoryContext.SecondCategory.Where(i => i.FirstCategoryID == firstCatId).OrderByDescending(i => i.SearchCount).Include(i => i.FirstCategory).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<ThirdCategory>> GetThirdCategoryDeepAsync(int secondCatId)
        {
            var result = await categoryContext.ThirdCategory.Where(i => i.SecondCategoryID == secondCatId).OrderByDescending(i => i.SearchCount).Include(i => i.FirstCategory).Include(i => i.SecondCategory).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<FourthCategory>> GetFourthCategoryDeepAsync(int thirdCatId)
        {
            var result = await categoryContext.FourthCategory.Where(i => i.ThirdCategoryID == thirdCatId).OrderByDescending(i => i.SearchCount).Include(i => i.FirstCategory).Include(i => i.SecondCategory).Include(i => i.ThirdCategory).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<FifthCategory>> GetFifthCategoryDeepAsync(int fourthCatId)
        {
            var result = await categoryContext.FifthCategory.Where(i => i.FourthCategoryID == fourthCatId).OrderByDescending(i => i.SearchCount).Include(i => i.FirstCategory).Include(i => i.SecondCategory).Include(i => i.ThirdCategory).Include(i => i.FourthCategory).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<SixthCategory>> GetSixthCategoryDeepAsync(int fifthCatId)
        {
            var result = await categoryContext.SixthCategory.Where(i => i.FifthCategoryID == fifthCatId).OrderByDescending(i => i.SearchCount).Include(i => i.FirstCategory).Include(i => i.SecondCategory).Include(i => i.ThirdCategory).Include(i => i.FourthCategory).Include(i => i.FifthCategory).ToListAsync();
            return result;
        }

        // Shafi: Count Deep Categories
        public async Task<int> CountSecondCategoriesDeepAsync(int firstCatId)
        {
            var result = await categoryContext.SecondCategory.Where(i => i.FirstCategoryID == firstCatId).CountAsync();
            return result;
        }

        public async Task<int> CountThirdCategoryDeepAsync(int secondCatId)
        {
            var result = await categoryContext.ThirdCategory.Where(i => i.SecondCategoryID == secondCatId).CountAsync();
            return result;
        }

        public async Task<int> CountFourthCategoryDeepAsync(int thirdCatId)
        {
            var result = await categoryContext.FourthCategory.Where(i => i.ThirdCategoryID == thirdCatId).CountAsync();
            return result;
        }

        public async Task<int> CountFifthCategoryDeepAsync(int fourthCatId)
        {
            var result = await categoryContext.FifthCategory.Where(i => i.FourthCategoryID == fourthCatId).CountAsync();
            return result;
        }

        public async Task<int> CountSixthCategoryDeepAsync(int fifthCatId)
        {
            var result = await categoryContext.SixthCategory.Where(i => i.FirstCategoryID == fifthCatId).CountAsync();
            return result;
        }

        public string FirstCategoryName(int firstCatId)
        {
            var result = categoryContext.FirstCategory.Where(i => i.FirstCategoryID == firstCatId).Select(i => i.Name).FirstOrDefault();
            return result;
        }

        public string SecondCategoryName(int secondCatId)
        {
            var result = categoryContext.SecondCategory.Where(i => i.SecondCategoryID == secondCatId).Select(i => i.Name).FirstOrDefault();
            return result;
        }

        public async Task<bool> FirstCategoryDuplicate(string businessCategoryName)
        {
            var result = await categoryContext.FirstCategory.AnyAsync(i => i.SearchKeywordName == businessCategoryName);
            return result;
        }

        public async Task<bool> SecondCategoryDuplicate(string businessCategoryName)
        {
            var result = await categoryContext.SecondCategory.AnyAsync(i => i.SearchKeywordName == businessCategoryName);
            return result;
        }

        public async Task<bool> ThirdCategoryDuplicate(string businessCategoryName)
        {
            var result = await categoryContext.ThirdCategory.AnyAsync(i => i.SearchKeywordName == businessCategoryName);
            return result;
        }

        public async Task<bool> FourthCategoryDuplicate(string businessCategoryName)
        {
            var result = await categoryContext.FourthCategory.AnyAsync(i => i.SearchKeywordName == businessCategoryName);
            return result;
        }

        public async Task<bool> FifthCategoryDuplicate(string businessCategoryName)
        {
            var result = await categoryContext.FifthCategory.AnyAsync(i => i.SearchKeywordName == businessCategoryName);
            return result;
        }

        public async Task<bool> SixthCategoryDuplicate(string businessCategoryName)
        {
            var result = await categoryContext.SixthCategory.AnyAsync(i => i.SearchKeywordName == businessCategoryName);
            return result;
        }
        // End:

        // Shafi: Categories has childrens or not
        public bool FirstCatHasSecondCat(int firstCatId)
        {
            var result = categoryContext.SecondCategory.Any(i => i.FirstCategoryID == firstCatId);
            return result;
        }

        public bool SecondCatHasThirdCat(int secondCatId)
        {
            var result = categoryContext.ThirdCategory.Any(i => i.SecondCategoryID == secondCatId);
            return result;
        }

        public bool ThirdCatHasFourthCat(int thirdCatId)
        {
            var result = categoryContext.FourthCategory.Any(i => i.ThirdCategoryID == thirdCatId);
            return result;
        }

        public bool FourthCatHasFifthCat(int fourthCatId)
        {
            var result = categoryContext.FifthCategory.Any(i => i.FourthCategoryID == fourthCatId);
            return result;
        }

        public bool FifthCatHasSixthCat(int fifthCatId)
        {
            var result = categoryContext.SixthCategory.Any(i => i.FifthCategoryID == fifthCatId);
            return result;
        }
        // End:

        // Shafi: Check if categories are empty or not
        public bool FirstCategoryIsNotEmpty(int firstCatId)
        {
            // Shafi: Check if second category exists
            var secondCategoryExist = categoryContext.SecondCategory.Any(i => i.FirstCategoryID == firstCatId);
            // End:

            // Shafi: Check if keyword exists
            var keywordExists = categoryContext.KeywordFirstCategory.Any(i => i.FirstCategoryID == firstCatId);
            // End:

            // Shafi: Check if featured keyword exists
            var featuredKeywordExists = categoryContext.ListingTitle.Any(i => i.FirstCategoryID == firstCatId);
            // End:

            if (secondCategoryExist == true || keywordExists == true || featuredKeywordExists == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool SecondCategoryIsNotEmpty(int secondCatId)
        {
            // Shafi: Check if third category exists
            var thirdCategoryExist = categoryContext.ThirdCategory.Any(i => i.SecondCategoryID == secondCatId);
            // End:

            // Shafi: Check if keyword exists
            var keywordExists = categoryContext.KeywordSecondCategory.Any(i => i.SecondCategoryID == secondCatId);
            // End:

            // Shafi: Check if featured keyword exists
            var featuredKeywordExists = categoryContext.ListingTitle.Any(i => i.SecondCategoryID == secondCatId);
            // End:

            if (thirdCategoryExist == true || keywordExists == true || featuredKeywordExists == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ThirdCategoryIsNotEmpty(int thirdCatId)
        {
            // Shafi: Check if fourth category exists
            var fourthCategoryExist = categoryContext.FourthCategory.Any(i => i.ThirdCategoryID == thirdCatId);
            // End:

            // Shafi: Check if keyword exists
            var keywordExists = categoryContext.KeywordThirdCategory.Any(i => i.ThirdCategoryID == thirdCatId);
            // End:

            if (fourthCategoryExist == true || keywordExists == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool FourthCategoryIsNotEmpty(int fourthCatId)
        {
            // Shafi: Check if fifth category exists
            var fifthCategoryExist = categoryContext.FifthCategory.Any(i => i.FourthCategoryID == fourthCatId);
            // End:

            // Shafi: Check if keyword exists
            var keywordExists = categoryContext.KeywordFourthCategory.Any(i => i.FourthCategoryID == fourthCatId);
            // End:

            if (fifthCategoryExist == true || keywordExists == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool FifthCategoryIsNotEmpty(int fifthCatId)
        {
            // Shafi: Check if sixth category exists
            var sixthCategoryExist = categoryContext.SixthCategory.Any(i => i.FifthCategoryID == fifthCatId);
            // End:

            // Shafi: Check if keyword exists
            var keywordExists = categoryContext.KeywordFifthCategory.Any(i => i.FifthCategoryID == fifthCatId);
            // End:

            if (sixthCategoryExist == true || keywordExists == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool SixthCategoryIsNotEmpty(int sixthCatId)
        {
            // Shafi: Check if keyword exists
            var keywordExists = categoryContext.KeywordSixthCategory.Any(i => i.SixthCategoryID == sixthCatId);
            // End:

            if (keywordExists == true)
            {
                return true;
            }
            else
            {
                return false;
            }
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