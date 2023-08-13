using BOL.ComponentModels.MyAccount.ListingWizard;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace Components.Fields
{
    public partial class PincodeDropdown
    {
        [Parameter] public LWAddressVM ListWizAddressVM { get; set; }
        [Parameter] public EventCallback ExecuteStateHasChanged { get; set; }
        [Inject] BAL.HelperFunctions helperFunction { get; set; }

        public async Task GetAreasByPincodeId(ChangeEventArgs events)
        {
            ListWizAddressVM.IsPincodeChange = true;
            ListWizAddressVM.PincodeId = Convert.ToInt32(events.Value.ToString());
            await helperFunction.GetAreasByPincodeId(ListWizAddressVM);
            await ExecuteStateHasChanged.InvokeAsync();
        }

        protected async override void OnParametersSet()
        {
            if (isAllowToGetData)
            {
                await helperFunction.GetPincodesByLocalityId(ListWizAddressVM);
                StateHasChanged();
            }
        }

        private bool isAllowToGetData
        {
            get { return ListWizAddressVM != null && ListWizAddressVM.LocalityId > 0 && ListWizAddressVM.IsFirstLoad; }
        }
    }
}
