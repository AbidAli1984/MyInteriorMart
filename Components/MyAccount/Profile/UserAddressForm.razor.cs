using BOL.ComponentModels.MyAccount.Profile;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace Components.MyAccount.Profile
{
    public partial class UserAddressForm
    {
        [Parameter]
        public ProfileInfo ProfileInfo { get; set; }
        [Parameter]
        public EventCallback GetStateByCountryIdEvent { get; set; }
        [Parameter]
        public EventCallback GetCityByStateIdEvent { get; set; }
        [Parameter]
        public EventCallback GetAreaByCityIdEvent { get; set; }
        [Parameter]
        public EventCallback GetPincodesByAreaIdEvent { get; set; }
        [Parameter]
        public EventCallback GetLocalitiesByPincodeIdEvent { get; set; }

        public async Task GetStateByCountryId(ChangeEventArgs e)
        {
            ProfileInfo.UserProfile.CountryID = Convert.ToInt32(e.Value.ToString());
            ProfileInfo.UserProfile.StateID = 0;
            ProfileInfo.UserProfile.CityID = 0;
            ProfileInfo.UserProfile.AssemblyID = 0;
            ProfileInfo.UserProfile.PincodeID = 0;
            ProfileInfo.UserProfile.LocalityID = 0;
            await GetStateByCountryIdEvent.InvokeAsync();
        }

        public async Task GetCityByStateId(ChangeEventArgs e)
        {
            ProfileInfo.UserProfile.StateID = Convert.ToInt32(e.Value.ToString());
            ProfileInfo.UserProfile.CityID = 0;
            ProfileInfo.UserProfile.AssemblyID = 0;
            ProfileInfo.UserProfile.PincodeID = 0;
            ProfileInfo.UserProfile.LocalityID = 0;
            await GetCityByStateIdEvent.InvokeAsync();
        }

        public async Task GetAreaByCityId(ChangeEventArgs e)
        {
            ProfileInfo.UserProfile.CityID = Convert.ToInt32(e.Value.ToString());
            ProfileInfo.UserProfile.AssemblyID = 0;
            ProfileInfo.UserProfile.PincodeID = 0;
            ProfileInfo.UserProfile.LocalityID = 0;
            await GetAreaByCityIdEvent.InvokeAsync();
        }

        public async Task GetPincodesByAreaId(ChangeEventArgs e)
        {
            ProfileInfo.UserProfile.AssemblyID = Convert.ToInt32(e.Value.ToString());
            ProfileInfo.UserProfile.PincodeID = 0;
            ProfileInfo.UserProfile.LocalityID = 0;
            await GetPincodesByAreaIdEvent.InvokeAsync();
        }

        public async void GetLocalitiesByPincodeId(ChangeEventArgs e)
        {
            ProfileInfo.UserProfile.PincodeID = Convert.ToInt32(e.Value.ToString());
            ProfileInfo.UserProfile.LocalityID = 0;
            await GetLocalitiesByPincodeIdEvent.InvokeAsync();
        }

        public void SetLocalityId(ChangeEventArgs e)
        {
            ProfileInfo.UserProfile.LocalityID = Convert.ToInt32(e.Value.ToString());
        }

        public void SetQualificationId(ChangeEventArgs e)
        {
            ProfileInfo.UserProfile.QualificationId = Convert.ToInt32(e.Value.ToString());
        }
    }
}
