using System.Linq;
using BAL.Listings;
using DAL.LISTING;
using DAL.SHARED;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ADMIN.Controllers
{
    public class CascadeDropdownAddressController : Controller
    {
        private readonly SharedDbContext sharedManager;

        public CascadeDropdownAddressController(SharedDbContext sharedManager)
        {
            this.sharedManager = sharedManager;
        }

        // Begin: Cascade Dropdown For Countries
        public JsonResult fetchCountries()
        {
            var selCountries = sharedManager.Country
                .OrderBy(c => c.Name)
                .Select(c => new { value = c.CountryID, text = c.Name });
            return Json(new SelectList(selCountries, "value", "text"));
        }
        // End:

        // Begin: Cascade Dropdown For States
        public JsonResult fetchStates(int JsonCountryValueId)
        {
            var selStates = sharedManager.State
                .OrderBy(s => s.Name)
                .Where(s => s.CountryID == JsonCountryValueId)
                .Select(s => new { value = s.StateID, text = s.Name });
            return Json(new SelectList(selStates, "value", "text"));
        }
        // End:

        // Begin: Cascade Dropdown For Cities
        public JsonResult fetchCities(int JsonStateValueId)
        {
            var selCities = sharedManager.City
                .OrderBy(c => c.Name)
                .Where(c => c.StateID == JsonStateValueId)
                .Select(c => new { value = c.CityID, text = c.Name });
            return Json(new SelectList(selCities, "value", "text"));
        }

        // Begin: Cascade Dropdown For States
        public JsonResult fetchStations(int JsonCityValueId)
        {
            var selStations = sharedManager.Station
                .OrderBy(c => c.Name)
                .Where(c => c.CityID == JsonCityValueId)
                .Select(c => new { value = c.StationID, text = c.Name });
            return Json(new SelectList(selStations, "value", "text"));
        }
        // End:

        // Begin: Cascade Dropdown For Localities
        public JsonResult fetchPincode(int JsonStationValueId)
        {
            var selPincodes = sharedManager.Pincode
                .OrderBy(c => c.PincodeNumber)
                .Where(c => c.StationID == JsonStationValueId)
                .Select(c => new { value = c.PincodeID, text = c.PincodeNumber });
            return Json(new SelectList(selPincodes, "value", "text"));
        }
        // End:
    }
}
