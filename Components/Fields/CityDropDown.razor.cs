using BOL.ComponentModels.MyAccount.ListingWizard;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace Components.Fields
{
    public partial class CityDropDown
    {
        [Parameter] public LWAddressVM ListWizAddressVM { get; set; }
        [Parameter] public EventCallback ExecuteStateHasChanged { get; set; }
        [Inject] BAL.HelperFunctions helperFunction { get; set; }

        public async Task GetLocalitiesByCityId(ChangeEventArgs events)
        {
            ListWizAddressVM.IsCityChange = true;
            ListWizAddressVM.CityId = Convert.ToInt32(events.Value.ToString());
            await helperFunction.GetLocalitiesByCityId(ListWizAddressVM);
            await ExecuteStateHasChanged.InvokeAsync();
        }

        protected async override void OnParametersSet()
        {
            if (isAllowToGetData)
            {
                await helperFunction.GetCitiesByStateId(ListWizAddressVM);
                StateHasChanged();
            }
        }

        private bool isAllowToGetData
        {
            get { return ListWizAddressVM != null && ListWizAddressVM.StateId > 0 && ListWizAddressVM.IsFirstLoad; }
        }
    }
}
