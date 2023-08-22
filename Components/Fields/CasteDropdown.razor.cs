using BOL.ComponentModels.Shared;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Components.Fields
{
    public partial class CasteDropdown
    {
        [Parameter] public ReligionsDropdownVM ReligionsDropdownVM { get; set; }

        public async Task SetCasteId(ChangeEventArgs events)
        {
            ReligionsDropdownVM.SelectedCasteId = Convert.ToInt32(events.Value.ToString());
        }
    }
}
