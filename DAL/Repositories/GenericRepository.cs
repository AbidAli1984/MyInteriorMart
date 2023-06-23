using DAL.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class GenericRepository<TModel, Tcontext> : IGenericRepository<TModel, Tcontext> where TModel: class where Tcontext : DbContext
    {
        private readonly Tcontext _dbContext;
        public GenericRepository(Tcontext dbContext)
        {
            this._dbContext = dbContext;
        }
        public Task<List<TModel>> GetAllAsync()
        {
            try
            {
                return _dbContext.Set<TModel>().ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }
            throw new NotImplementedException();
        }
    }
}
