using BOL.ComponentModels.MyAccount.ListingWizard;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace Components.Fields
{
    public partial class LocalityDropdown
    {
        [Parameter] public LWAddressVM ListWizAddressVM { get; set; }
        [Parameter] public EventCallback ExecuteStateHasChanged { get; set; }
        [Inject] BAL.HelperFunctions helperFunction { get; set; }

        public async Task GetPincodesByLocalityId(ChangeEventArgs events)
        {
            ListWizAddressVM.IsLocalityChange = true;
            ListWizAddressVM.LocalityId = Convert.ToInt32(events.Value.ToString());
            await helperFunction.GetPincodesByLocalityId(ListWizAddressVM);
            await ExecuteStateHasChanged.InvokeAsync();
        }

        protected async override void OnParametersSet()
        {
            if (isAllowToGetData)
            {
                await helperFunction.GetLocalitiesByCityId(ListWizAddressVM);
                StateHasChanged();
            }
        }

        private bool isAllowToGetData
        {
            get { return ListWizAddressVM != null && ListWizAddressVM.CityId > 0 && ListWizAddressVM.IsFirstLoad; }
        }
    }
}
