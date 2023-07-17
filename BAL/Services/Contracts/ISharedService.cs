using BOL.SHARED;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BAL.Services.Contracts
{
    public interface ISharedService
    {
        #region Profile Info
        Task<IList<Qualification>> GetQualifications();
        Task<IList<Country>> GetCountries();
        Task<IList<State>> GetStatesByCountryId(int countryId = 101);
        Task<IList<City>> GetCitiesByStateId(int stateId);
        Task<IList<Station>> GetAreasByCityId(int cityId);
        Task<IList<Pincode>> GetPincodesByAreaId(int areaId);
        Task<IList<Locality>> GetLocalitiesByPincode(int pincodeId);
        #endregion
    }
}
