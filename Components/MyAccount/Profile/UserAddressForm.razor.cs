using BOL.ComponentModels.MyAccount.Profile;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace Components.MyAccount.Profile
{
    public partial class UserAddressForm
    {
        [Parameter]
        public UserAddressVM UserAddressVM { get; set; }
        [Parameter]
        public EventCallback GetCityByStateIdEvent { get; set; }
        [Parameter]
        public EventCallback GetAreaByCityIdEvent { get; set; }
        [Parameter]
        public EventCallback GetPincodesByAreaIdEvent { get; set; }

        public async Task GetCityByStateId(ChangeEventArgs e)
        {
            UserAddressVM.City = 0;
            UserAddressVM.Area = 0;
            UserAddressVM.Pincode = 0;
            UserAddressVM.State = Convert.ToInt32(e.Value.ToString());
            await GetCityByStateIdEvent.InvokeAsync();
        }

        public async Task GetAreaByCityId(ChangeEventArgs e)
        {
            UserAddressVM.Area = 0;
            UserAddressVM.Pincode = 0;
            UserAddressVM.City = Convert.ToInt32(e.Value.ToString());
            await GetAreaByCityIdEvent.InvokeAsync();
        }

        public async Task GetPincodesByAreaId(ChangeEventArgs e)
        {
            UserAddressVM.Pincode = 0;
            UserAddressVM.Area = Convert.ToInt32(e.Value.ToString());
            await GetPincodesByAreaIdEvent.InvokeAsync();
        }

        public void SetPinCode(ChangeEventArgs e)
        {
            UserAddressVM.Pincode = Convert.ToInt32(e.Value.ToString());
        }
    }
}
