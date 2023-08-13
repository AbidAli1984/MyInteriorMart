using BOL.ComponentModels.MyAccount.ListingWizard;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace Components.Fields
{
    public partial class AreaDropdown
    {
        [Parameter] public LWAddressVM ListWizAddressVM { get; set; }
        [Parameter] public EventCallback ExecuteStateHasChanged { get; set; }
        [Inject] BAL.HelperFunctions helperFunction { get; set; }

        public async Task SetAreaId(ChangeEventArgs events)
        {
            ListWizAddressVM.AreaId = Convert.ToInt32(events.Value.ToString());
            //ListWizAddressVM.IsAreaChange = true;
            //await ExecuteStateHasChanged.InvokeAsync();
        }

        protected async override void OnParametersSet()
        {
            if (isAllowToGetData)
            {
                await helperFunction.GetAreasByPincodeId(ListWizAddressVM);
                StateHasChanged();
            }
        }

        private bool isAllowToGetData
        {
            get { return ListWizAddressVM != null && ListWizAddressVM.PincodeId > 0 && ListWizAddressVM.IsFirstLoad; }
        }
    }
}
