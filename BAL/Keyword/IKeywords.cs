using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BOL.CATEGORIES;

namespace BAL.Keyword
{
    public interface IKeywords
    {
        Task<IEnumerable<KeywordFirstCategory>> GetKeywordFirstCategoryDeepAsync(int firstCatId);
        Task<IEnumerable<KeywordSecondCategory>> GetKeywordSecondCategoryDeepAsync(int secondCatId);
        Task<IEnumerable<KeywordThirdCategory>> GetKeywordThirdCategoryDeepAsync(int thirdCatId);
        Task<IEnumerable<KeywordFourthCategory>> GetKeywordFourthCategoryDeepAsync(int fourthCatId);
        Task<IEnumerable<KeywordFifthCategory>> GetKeywordFifthCategoryDeepAsync(int fifthCatId);
        Task<IEnumerable<KeywordSixthCategory>> GetKeywordSixthCategoryDeepAsync(int sixthCatId);

        Task<int> CountKeywordSecondCategoryDeepAsync(int secondCatId);
        Task<int> CountKeywordThirdCategoryDeepAsync(int thirdCatId);
        Task<int> CountKeywordFourthCategoryDeepAsync(int fourthCatId);
        Task<int> CountKeywordFifthCategoryDeepAsync(int fifthCatId);
        Task<int> CountKeywordSixthCategoryDeepAsync(int sixthCatId);

        Task<bool> CheckIfKeywordExistInAnyKeywordCategoriesAsync(string keyword);
        Task<bool> CheckIfKeywordFirstCategoryExist(string keyword);
        Task<bool> CheckIfKeywordSecondCategoryExist(string keyword);
        Task<bool> CheckIfKeywordThirdCategoryExist(string keyword);
        Task<bool> CheckIfKeywordFourthCategoryExist(string keyword);
        Task<bool> CheckIfKeywordFifthCategoryExist(string keyword);
        Task<bool> CheckIfKeywordSixthCategoryExist(string keyword);

        int CountKeywordsInFirstCategory(int FirstCatId);
        int CountKeywordsInSecondCategory(int SecondCatId);
        int CountKeywordsInThirdCategory(int ThirdCatId);
        int CountKeywordsInFourthCategory(int FourthCatId);
        int CountKeywordsInFifthCategory(int FifthCatId);
        int CountKeywordsInSixthCategory(int SixthCatId);
    }
}
