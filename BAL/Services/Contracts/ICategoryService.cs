using BOL.CATEGORIES;
using BOL.ComponentModels.Pages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BAL.Services.Contracts
{
    public interface ICategoryService
    {
        Task GetCategoriesForIndexPage(IndexVM indexVM);
    }
}
