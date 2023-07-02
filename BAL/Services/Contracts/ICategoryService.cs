using BOL.CATEGORIES;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BAL.Services.Contracts
{
    public interface ICategoryService
    {
        Task<IEnumerable<SecondCategory>> GetSecondCategoriesHomeAsync(string FirstCategory);
    }
}
