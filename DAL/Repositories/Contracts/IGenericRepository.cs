using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Contracts
{
    public interface IGenericRepository<TModel, Tcontext> where TModel : class where Tcontext : class
    {
        Task<List<TModel>> GetAllAsync();
    }
}
