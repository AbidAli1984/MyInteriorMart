using BOL.ComponentModels.MyAccount.ListingWizard;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace Components.Fields
{
    public partial class StateDropDown
    {
        [Parameter] public LWAddressVM LWAddressVM { get; set; }
        [Parameter] public EventCallback ExecuteStateHasChanged { get; set; }
        [Inject] BAL.HelperFunctions helperFunction { get; set; }

        public async Task GetCityByStateId(ChangeEventArgs events)
        {
            LWAddressVM.IsStateChange = true;
            LWAddressVM.StateId = Convert.ToInt32(events.Value.ToString());
            await ExecuteStateHasChanged.InvokeAsync();
        }

        protected async override void OnParametersSet()
        {
            if (LWAddressVM != null && (LWAddressVM.IsCountryChange || LWAddressVM.CountryId > 0))
            {
                await helperFunction.GetStateByCountryId(LWAddressVM);
                LWAddressVM.IsCountryChange = false;
                StateHasChanged();
            }
        }
    }
}
