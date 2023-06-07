using BOL.CATEGORIES;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Category
{
    public interface ICategory
    {
        // Shafi: Get All
        Task<IEnumerable<FirstCategory>> GetFirstCategoriesAsync();
        Task<IEnumerable<SecondCategory>> GetSecondCategoriesAsync();
        Task<IEnumerable<ThirdCategory>> GetThirdCategoryAsync();
        Task<IEnumerable<FourthCategory>> GetFourthCategoryAsync();
        Task<IEnumerable<FifthCategory>> GetFifthCategoryAsync();
        Task<IEnumerable<SixthCategory>> GetSixthCategoryAsync();
        // End:

        // Shafi: Get All Home Category
        Task<IEnumerable<SecondCategory>> GetSecondCategoriesHomeAsync(string FirstCategory);
        // End:

        // Shafi: Get FirstOrDefault
        Task<FirstCategory> FirstCategoryDetailsAsync(int id);
        Task<SecondCategory> SecondCategoryDetailsAsync(int id);
        Task<ThirdCategory> ThirdCategoryDetailsAsync(int id);
        Task<FourthCategory> FourthCategoriesDetailsAsync(int id);
        Task<FifthCategory> FifthCategoriesDetailsAsync(int id);
        Task<SixthCategory> SixthCategoriesDetailsAsync(int id);
        // End:

        // Shafi: Check if current category contains more than 1 category and return true false
        Task<bool> ThirdHaveFourthAsync(int thirdId);
        Task<bool> FourthHaveFifthAsync(int fourthId);
        Task<bool> FifthHaveSixthAsync(int fifthId);
        // End:

        // Shafi: Get Deep Categories
        Task<IEnumerable<SecondCategory>> GetSecondCategoriesDeepAsync(int firstCatId);
        Task<IEnumerable<ThirdCategory>> GetThirdCategoryDeepAsync(int secondCatId);
        Task<IEnumerable<FourthCategory>> GetFourthCategoryDeepAsync(int thirdCatId);
        Task<IEnumerable<FifthCategory>> GetFifthCategoryDeepAsync(int fourthCatId);
        Task<IEnumerable<SixthCategory>> GetSixthCategoryDeepAsync(int fifthCatId);
        // End:

        // Shafi: Get Deep Categories
        Task<int> CountSecondCategoriesDeepAsync(int firstCatId);
        Task<int> CountThirdCategoryDeepAsync(int secondCatId);
        Task<int> CountFourthCategoryDeepAsync(int thirdCatId);
        Task<int> CountFifthCategoryDeepAsync(int fourthCatId);
        Task<int> CountSixthCategoryDeepAsync(int fifthCatId);
        // End:

        // Shafi: Get category names
        string FirstCategoryName(int firstCatId);
        string SecondCategoryName(int secondCatId);
        // End:

        Task<bool> FirstCategoryDuplicate(string businessCategoryName);
        Task<bool> SecondCategoryDuplicate(string businessCategoryName);
        Task<bool> ThirdCategoryDuplicate(string businessCategoryName);
        Task<bool> FourthCategoryDuplicate(string businessCategoryName);
        Task<bool> FifthCategoryDuplicate(string businessCategoryName);
        Task<bool> SixthCategoryDuplicate(string businessCategoryName);

        // Shafi: Categories has childrens or not
        bool FirstCatHasSecondCat(int firstCatId);
        bool SecondCatHasThirdCat(int secondCatId);
        bool ThirdCatHasFourthCat(int thirdCatId);
        bool FourthCatHasFifthCat(int fourthCatId);
        bool FifthCatHasSixthCat(int fifthCatId);
        // End:

        // Shafi: Check if categories are empty or not
        bool FirstCategoryIsNotEmpty(int firstCatId);
        bool SecondCategoryIsNotEmpty(int secondCatId);
        bool ThirdCategoryIsNotEmpty(int thirdCatId);
        bool FourthCategoryIsNotEmpty(int fourthCatId);
        bool FifthCategoryIsNotEmpty(int fifthCatId);
        bool SixthCategoryIsNotEmpty(int sixthCatId);
        // End:

        // Shafi: Count syncronously child categories
        int CountChildOfFirstCategory(int firstCatId);
        int CountChildOfSecondCategory(int secondCatId);
        int CountChildOfThirdCategory(int thirdCatId);
        int CountChildOfFourthCategory(int fourthCatId);
        int CountChildOfFifthCategory(int fifthCatId);
        // End:
    }
}
