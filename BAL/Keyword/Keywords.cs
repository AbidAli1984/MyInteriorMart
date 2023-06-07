using BOL.CATEGORIES;
using DAL.CATEGORIES;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Keyword
{
    public class Keywords : IKeywords
    {
        private readonly CategoriesDbContext categoryContext;
        public Keywords(CategoriesDbContext categoryContext)
        {
            this.categoryContext = categoryContext;
        }

        public async Task<IEnumerable<KeywordFirstCategory>> GetKeywordFirstCategoryDeepAsync(int firstCatId)
        {
            var result = await categoryContext.KeywordFirstCategory.Where(i => i.FirstCategoryID == firstCatId).OrderByDescending(i => i.FirstCategoryID).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<KeywordSecondCategory>> GetKeywordSecondCategoryDeepAsync(int secondCatId)
        {
            var result = await categoryContext.KeywordSecondCategory.Where(i => i.SecondCategoryID == secondCatId).OrderByDescending(i => i.SecondCategoryID).Include(i => i.SecondCategory).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<KeywordThirdCategory>> GetKeywordThirdCategoryDeepAsync(int thirdCatId)
        {
            var result = await categoryContext.KeywordThirdCategory.Where(i => i.ThirdCategoryID == thirdCatId).OrderByDescending(i => i.ThirdCategoryID).Include(i => i.ThirdCategory).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<KeywordFourthCategory>> GetKeywordFourthCategoryDeepAsync(int fourthCatId)
        {
            var result = await categoryContext.KeywordFourthCategory.Where(i => i.FourthCategoryID == fourthCatId).OrderByDescending(i => i.FourthCategoryID).Include(i => i.FourthCategory).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<KeywordFifthCategory>> GetKeywordFifthCategoryDeepAsync(int fifthCatId)
        {
            var result = await categoryContext.KeywordFifthCategory.Where(i => i.FifthCategoryID == fifthCatId).OrderByDescending(i => i.FifthCategoryID).Include(i => i.FifthCategory).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<KeywordSixthCategory>> GetKeywordSixthCategoryDeepAsync(int sixthCatId)
        {
            var result = await categoryContext.KeywordSixthCategory.Where(i => i.SixthCategoryID == sixthCatId).OrderByDescending(i => i.SixthCategoryID).Include(i => i.SixthCategory).ToListAsync();
            return result;
        }

        public async Task<int> CountKeywordSecondCategoryDeepAsync(int firstCatId)
        {
            var result = await categoryContext.KeywordSecondCategory.Where(i => i.FirstCategoryID == firstCatId).CountAsync();
            return result;
        }

        public async Task<int> CountKeywordThirdCategoryDeepAsync(int secondCatId)
        {
            var result = await categoryContext.KeywordThirdCategory.Where(i => i.SecondCategoryID == secondCatId).CountAsync();
            return result;
        }

        public async Task<int> CountKeywordFourthCategoryDeepAsync(int thirdCatId)
        {
            var result = await categoryContext.KeywordFourthCategory.Where(i => i.FourthCategoryID == thirdCatId).CountAsync();
            return result;
        }

        public async Task<int> CountKeywordFifthCategoryDeepAsync(int fourthCatId)
        {
            var result = await categoryContext.KeywordFifthCategory.Where(i => i.FifthCategoryID == fourthCatId).CountAsync();
            return result;
        }

        public async Task<int> CountKeywordSixthCategoryDeepAsync(int fifthCatId)
        {
            var result = await categoryContext.KeywordSixthCategory.Where(i => i.FifthCategoryID == fifthCatId).CountAsync();
            return result;
        }

        public async Task<bool> CheckIfKeywordExistInAnyKeywordCategoriesAsync(string keyword)
        {
            var keywordFirstCat = await categoryContext.KeywordFirstCategory.AnyAsync(i => i.Keyword == keyword);
            var keywordSecondCat = await categoryContext.KeywordSecondCategory.AnyAsync(i => i.Keyword == keyword);
            var keywordThirdCat = await categoryContext.KeywordThirdCategory.AnyAsync(i => i.Keyword == keyword);
            var keywordFourthCat = await categoryContext.KeywordFourthCategory.AnyAsync(i => i.Keyword == keyword);
            var keywordFifthCat = await categoryContext.KeywordFifthCategory.AnyAsync(i => i.Keyword == keyword);
            var keywordSixthCat = await categoryContext.KeywordSixthCategory.AnyAsync(i => i.Keyword == keyword);

            if(keywordFirstCat == false && keywordSecondCat == false && keywordThirdCat == false && keywordFourthCat == false && keywordFifthCat == false && keywordSixthCat == false)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public async Task<bool> CheckIfKeywordFirstCategoryExist(string keyword)
        {
            var keywordFirstCat = await categoryContext.KeywordFirstCategory.AnyAsync(i => i.Keyword == keyword);
            if(keywordFirstCat == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> CheckIfKeywordSecondCategoryExist(string keyword)
        {
            var keywordSecondCat = await categoryContext.KeywordSecondCategory.AnyAsync(i => i.Keyword == keyword);
            if (keywordSecondCat == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> CheckIfKeywordThirdCategoryExist(string keyword)
        {
            var keywordThirdCat = await categoryContext.KeywordThirdCategory.AnyAsync(i => i.Keyword == keyword);
            if (keywordThirdCat == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> CheckIfKeywordFourthCategoryExist(string keyword)
        {
            var keywordFourthCat = await categoryContext.KeywordFourthCategory.AnyAsync(i => i.Keyword == keyword);
            if (keywordFourthCat == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> CheckIfKeywordFifthCategoryExist(string keyword)
        {
            var keywordFifthCat = await categoryContext.KeywordFifthCategory.AnyAsync(i => i.Keyword == keyword);
            if (keywordFifthCat == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> CheckIfKeywordSixthCategoryExist(string keyword)
        {
            var keywordSixthCat = await categoryContext.KeywordSixthCategory.AnyAsync(i => i.Keyword == keyword);
            if (keywordSixthCat == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int CountKeywordsInFirstCategory(int FirstCatId)
        {
            var result = categoryContext.KeywordFirstCategory.Where(i => i.FirstCategoryID == FirstCatId).Count();
            return result;
        }

        public int CountKeywordsInSecondCategory(int SecondCatId)
        {
            var result = categoryContext.KeywordSecondCategory.Where(i => i.SecondCategoryID == SecondCatId).Count();
            return result;
        }

        public int CountKeywordsInThirdCategory(int ThirdCatId)
        {
            var result = categoryContext.KeywordThirdCategory.Where(i => i.ThirdCategoryID == ThirdCatId).Count();
            return result;
        }

        public int CountKeywordsInFourthCategory(int FourthCatId)
        {
            var result = categoryContext.KeywordFourthCategory.Where(i => i.FourthCategoryID == FourthCatId).Count();
            return result;
        }
        public int CountKeywordsInFifthCategory(int FifthCatId)
        {
            var result = categoryContext.KeywordFifthCategory.Where(i => i.FifthCategoryID == FifthCatId).Count();
            return result;
        }

        public int CountKeywordsInSixthCategory(int SixthCatId)
        {
            var result = categoryContext.KeywordSixthCategory.Where(i => i.SixthCategoryID == SixthCatId).Count();
            return result;
        }
    }
}
