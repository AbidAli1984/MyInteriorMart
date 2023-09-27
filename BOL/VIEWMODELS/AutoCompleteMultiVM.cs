using System;
using System.Collections.Generic;
using System.Text;

namespace BOL.VIEWMODELS
{
    public class AutoCompleteMultiVM
    {
        public IList<SearchResultViewModel> items = new List<SearchResultViewModel>();
        public IEnumerable<SearchResultViewModel> itemsSelected = new List<SearchResultViewModel>();
    }
}
