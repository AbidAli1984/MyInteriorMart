using BOL.SHARED;
using DAL.Repositories.Contracts;
using DAL.SHARED;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class SharedRepository : ISharedRepository
    {
        private readonly SharedDbContext sharedDbContext;
        public SharedRepository(SharedDbContext sharedDbContext)
        {
            this.sharedDbContext = sharedDbContext;
        }

        #region States
        public async Task<IList<Qualification>> GetQualifications()
        {
            return await sharedDbContext.Qualifications
                .ToListAsync();
        }

        public async Task<IList<Country>> GetCountries()
        {
            return await sharedDbContext.Country
                .OrderBy(i => i.Name).ToListAsync();
        }

        public async Task<IList<State>> GetStatesByCountryId(int countryId)
        {
            return await sharedDbContext.State
                .OrderBy(i => i.Name)
                .Where(i => i.CountryID == countryId)
                .ToListAsync();
        }

        public async Task<IList<City>> GetCitiesByStateId(int stateId)
        {
            return await sharedDbContext.City
                .OrderBy(i => i.Name)
                .Where(i => i.StateID == stateId)
                .ToListAsync();
        }

        public async Task<IList<Station>> GetAreasByCityId(int cityId)
        {
            return await sharedDbContext.Station
                .OrderBy(i => i.Name)
                .Where(i => i.CityID == cityId)
                .ToListAsync();
        }

        public async Task<IList<Pincode>> GetPincodesByAreaId(int areaId)
        {
            return await sharedDbContext.Pincode
                .OrderBy(i => i.PincodeNumber)
                .Where(i => i.StationID == areaId)
                .ToListAsync();
        }

        public IList<Locality> listLocality = new List<Locality>();
        public async Task<IList<Locality>> GetLocalitiesByPincode(int pincodeId)
        {
            return await sharedDbContext.Locality
                .OrderBy(i => i.LocalityName)
                .Where(i => i.PincodeID == pincodeId)
                .ToListAsync();
        }
        #endregion
    }
}
