using BOL.CATEGORIES;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Repositories.Contracts
{
    public interface ICategoryRepository
    {
        Task<IList<FirstCategory>> GetFirstCategoriesAsync();

        Task<IList<SecondCategory>> GetSecCategoriesByFirstCategoryId(int firstCategoryId);

        Task<IList<ThirdCategory>> GetThirdCategoriesBySeconCategoryId(int secondCategoryId);

        Task<IList<FourthCategory>> GetForthCategoriesBySecondCategoryId(int secondCategoryId);

        Task<IList<FifthCategory>> GetFifthCategoriesBySecondCategoryId(int secondCategoryId);

        Task<IList<SixthCategory>> GetSixthCategoriesBySecondCategoryId(int secondCategoryId);
    }
}
