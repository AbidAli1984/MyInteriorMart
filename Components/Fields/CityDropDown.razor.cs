using BOL.ComponentModels.MyAccount.ListingWizard;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace Components.Fields
{
    public partial class CityDropDown
    {
        [Parameter] public LWAddressVM LWAddressVM { get; set; }
        [Parameter] public EventCallback ExecuteStateHasChanged { get; set; }
        [Inject] BAL.HelperFunctions helperFunction { get; set; }

        public async Task GetLocalityByCityId(ChangeEventArgs events)
        {
            LWAddressVM.IsCityChange = true;
            LWAddressVM.CityId = Convert.ToInt32(events.Value.ToString());
            await ExecuteStateHasChanged.InvokeAsync();
        }

        protected async override void OnParametersSet()
        {
            if (LWAddressVM != null && (LWAddressVM.IsStateChange || LWAddressVM.StateId > 0))
            {
                await helperFunction.GetCityByStateId(LWAddressVM);
                LWAddressVM.IsStateChange = false;
                StateHasChanged();
            }
        }
    }
}
