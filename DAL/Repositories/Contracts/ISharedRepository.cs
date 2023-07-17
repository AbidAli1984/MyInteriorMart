using BOL.SHARED;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Repositories.Contracts
{
    public interface ISharedRepository
    {
        #region Profile Info
        Task<IList<Qualification>> GetQualifications();
        Task<IList<Country>> GetCountries();
        Task<IList<State>> GetStatesByCountryId(int countryId);
        Task<IList<City>> GetCitiesByStateId(int stateId);
        Task<IList<Station>> GetAreasByCityId(int cityId);
        Task<IList<Pincode>> GetPincodesByAreaId(int areaId);
        Task<IList<Locality>> GetLocalitiesByPincode(int pincodeId);
        #endregion
    }
}
