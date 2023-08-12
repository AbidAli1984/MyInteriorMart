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

        public async Task AddAsync(object data)
        {
            await sharedDbContext.AddAsync(data);
            await sharedDbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(object data)
        {
            sharedDbContext.Update(data);
            await sharedDbContext.SaveChangesAsync();
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

        public async Task<IList<Location>> GetAreasByCityId(int cityId)
        {
            return await sharedDbContext.Location
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

        public IList<Area> listLocality = new List<Area>();
        public async Task<IList<Area>> GetLocalitiesByPincode(int pincodeId)
        {
            return await sharedDbContext.Area
                .OrderBy(i => i.Name)
                .Where(i => i.PincodeID == pincodeId)
                .ToListAsync();
        }
        #endregion

        #region Address Info
        public async Task<Country> GetCountryByCountryId(int countryId)
        {
            return await sharedDbContext.Country.FindAsync(countryId);
        }

        public async Task<State> GetStateByStateId(int stateId)
        {
            return await sharedDbContext.State.FindAsync(stateId);
        }
        public async Task<City> GetCityByCityId(int cityId)
        {
            return await sharedDbContext.City.FindAsync(cityId);
        }
        public async Task<Location> GetAreaByAreaId(int areaId)
        {
            return await sharedDbContext.Location.FindAsync(areaId);
        }
        public async Task<Pincode> GetPincodeByPincodeId(int pincodeId)
        {
            return await sharedDbContext.Pincode.FindAsync(pincodeId);
        }
        public async Task<Area> GetLocalityByLocalityId(int localityId)
        {
            return await sharedDbContext.Area.FindAsync(localityId);
        }
        public async Task<Location> GetAreaByAreaName(string area)
        {
            return await sharedDbContext.Location
                .FirstOrDefaultAsync(x => x.Name == area);
        }
        public async Task<Pincode> GetPincodeByPinNumber(int pinNumber)
        {
            return await sharedDbContext.Pincode
                .FirstOrDefaultAsync(x => x.PincodeNumber == pinNumber);
        }
        public async Task<Area> GetLocalityByLocalityName(string localityName)
        {
            return await sharedDbContext.Area
                .FirstOrDefaultAsync(x => x.Name == localityName);
        }
        #endregion

        #region Company Info
        public async Task<IList<NatureOfBusiness>> GetNatureOfBusinesses()
        {
            return await sharedDbContext.NatureOfBusiness.ToListAsync();
        }
        public async Task<IList<Designation>> GetDesignations()
        {
            return await sharedDbContext.Designation.ToListAsync();
        }
        #endregion
    }
}
