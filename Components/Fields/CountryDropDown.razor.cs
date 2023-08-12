using BAL.Services.Contracts;
using BOL.ComponentModels.MyAccount.ListingWizard;
using BOL.SHARED;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Components.Fields
{
    public partial class CountryDropDown
    {
        [Parameter] public LWAddressVM LWAddressVM { get; set; }
        [Parameter] public EventCallback ExecuteStateHasChanged { get; set; }
        [Inject] BAL.HelperFunctions helperFunction { get; set; }

        public async Task GetStateByCountryId(ChangeEventArgs events)
        {
            LWAddressVM.IsCountryChange = true;
            LWAddressVM.CountryId = Convert.ToInt32(events.Value.ToString());
            await ExecuteStateHasChanged.InvokeAsync();
        }

        public IList<Country> Countries { get; set; } = new List<Country>();
        [Inject]
        private ISharedService sharedService { get; set; }

        protected async override void OnInitialized()
        {
            Countries = await sharedService.GetCountries();
            StateHasChanged();
        }
    }
}
