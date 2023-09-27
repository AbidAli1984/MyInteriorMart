using BAL.Services.Contracts;
using BOL.VIEWMODELS;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FRONTEND.BLAZOR.Component.Fields
{
    public partial class AutoCompleteMultiple
    {
        [Parameter] public AutoCompleteMultiVM AutoCompleteMultiVM { get; set; }
      
        IEnumerable<string> _selectedValues;

        protected override void OnInitialized()
        {
            
        }

        private void OnSelectedItemsChangedHandler(IEnumerable<SearchResultViewModel> values)
        {
            if (values != null)
                AutoCompleteMultiVM.itemsSelected = values;
            else
                AutoCompleteMultiVM.itemsSelected = null;
        }

        protected async override void OnParametersSet()
        {
            if (AutoCompleteMultiVM.itemsSelected.Count() > 0)
                _selectedValues = AutoCompleteMultiVM.itemsSelected.Select(x => x.value).ToList();
        }
    }
}
