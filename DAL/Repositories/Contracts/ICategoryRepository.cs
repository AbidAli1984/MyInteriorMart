using BOL.CATEGORIES;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Repositories.Contracts
{
    public interface ICategoryRepository
    {
        Task<IList<SecondCategory>> GetSecCategoriesByFirstCategoryId(int firstCategoryId);
    }
}
