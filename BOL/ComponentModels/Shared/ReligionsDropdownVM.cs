using BOL.SHARED;
using System;
using System.Collections.Generic;
using System.Text;

namespace BOL.ComponentModels.Shared
{
    public class ReligionsDropdownVM
    {
        public ReligionsDropdownVM()
        {
            Castes = new List<Caste>();
        }
        public int SelectedReligionId { get; set; }
        public int SelectedCasteId { get; set; }
        public IList<Caste> Castes { get; set; }

        public bool isValid()
        {
            return SelectedReligionId > 0 && SelectedCasteId > 0;
        }
    }
}
