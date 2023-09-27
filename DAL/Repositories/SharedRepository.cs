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

        public async Task<IList<Location>> GetLocalitiesByCityId(int cityId)
        {
            return await sharedDbContext.Location
                .OrderBy(i => i.Name)
                .Where(i => i.CityID == cityId)
                .ToListAsync();
        }

        public async Task<IList<Pincode>> GetPincodesByLocalityId(int localityId)
        {
            return await sharedDbContext.Pincode
                .OrderBy(i => i.PincodeNumber)
                .Where(i => i.LocationId == localityId)
                .ToListAsync();
        }

        public async Task<IList<Area>> GetAreasByPincodeId(int pincodeId)
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
            return await sharedDbContext.State
                .Include(x => x.Country)
                .FirstOrDefaultAsync(x => x.StateID == stateId);
        }
        public async Task<City> GetCityByCityId(int cityId)
        {
            return await sharedDbContext.City.FindAsync(cityId);
        }
        public async Task<Location> GetLocalityByLocalityId(int localityId)
        {
            return await sharedDbContext.Location
                .Include(x => x.City)
                .FirstOrDefaultAsync(x => x.Id == localityId);
        }
        public async Task<Pincode> GetPincodeByPincodeId(int pincodeId)
        {
            return await sharedDbContext.Pincode.FindAsync(pincodeId);
        }
        public async Task<Area> GetAreaByAreaId(int areaId)
        {
            return await sharedDbContext.Area.FindAsync(areaId);
        }
        public async Task<Location> GetLocalityByLocalityName(string locality)
        {
            return await sharedDbContext.Location
                .FirstOrDefaultAsync(x => x.Name == locality);
        }
        public async Task<Pincode> GetPincodeByPinNumber(int pinNumber)
        {
            return await sharedDbContext.Pincode
                .FirstOrDefaultAsync(x => x.PincodeNumber == pinNumber);
        }
        public async Task<Area> GetAreaByAreaName(string area)
        {
            return await sharedDbContext.Area
                .FirstOrDefaultAsync(x => x.Name == area);
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


        public async Task<IList<Religion>> GetReligions()
        {
            return await sharedDbContext.Religions
                .ToListAsync();
        }

        public async Task<IList<Caste>> GetCastesByReligionId(int religionId)
        {
            return await sharedDbContext.Castes.Where(x => x.ReligionId == religionId)
                .ToListAsync();
        }

        public async Task<Caste> GetCasteByCasteId(int casteId)
        {
            return await sharedDbContext.Castes
                .Include(x => x.Religion)
                .FirstOrDefaultAsync(x => x.Id == casteId);
        }
        public async Task<IList<Location>> GetLocaliiesByLocalityIds(int[] localityIds)
        {
            return await sharedDbContext.Location
                .Include(x => x.City)
                .Where(x => localityIds.Contains(x.Id))
                .ToListAsync();
        }

        public async Task<IList<Language>> GetLanguages()
        {
            return await sharedDbContext.Languages
                .ToListAsync();
        }
    }
}
