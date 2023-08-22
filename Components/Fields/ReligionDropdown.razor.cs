using BAL.Services.Contracts;
using BOL.ComponentModels.Shared;
using BOL.SHARED;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Components.Fields
{
    public partial class ReligionDropdown
    {
        [Parameter] public ReligionsDropdownVM ReligionsDropdownVM { get; set; }
        [Parameter] public EventCallback ExecuteStateHasChanged { get; set; }

        [Inject] private ISharedService sharedService { get; set; }
        [Inject] BAL.HelperFunctions helperFunction { get; set; }

        public IList<Religion> Religions { get; set; } = new List<Religion>();
        protected async override void OnInitialized()
        {
            Religions = await sharedService.GetReligions();
            StateHasChanged();
        }

        public async Task GetCastesByReligionId(ChangeEventArgs events)
        {
            ReligionsDropdownVM.SelectedReligionId = Convert.ToInt32(events.Value.ToString());
            await helperFunction.GetCastesByReligionId(ReligionsDropdownVM);
            await ExecuteStateHasChanged.InvokeAsync();
        }
    }
}
