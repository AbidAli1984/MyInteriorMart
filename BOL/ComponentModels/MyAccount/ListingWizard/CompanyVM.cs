using BOL.SHARED;
using System;
using System.Collections.Generic;
using System.Text;

namespace BOL.ComponentModels.MyAccount.ListingWizard
{
    public class CompanyVM
    {
        public string Name { get; set; }
        public string Gender { get; set; }
        public DateTime? YearOfEstablishment { get; set; }
        public string CompanyName { get; set; }
        public string GSTNumber { get; set; }
        public string Turnover { get; set; }
        public int NumberOfEmployees { get; set; }
        public string NatureOfBusiness { get; set; }
        public string Designation { get; set; }

        public IList<SelectItem> GenderList { get; set; }
        public IList<SelectItem> TurnoverList { get; set; }
        public IList<NatureOfBusiness> NatureOfBusinesses { get; set; }
        public IList<Designation> Designations;

        public CompanyVM()
        {
            GenderList = new List<SelectItem>
            {
                new SelectItem { Name = "Male" },
                new SelectItem { Name = "Female" },
                new SelectItem { Name = "Undisclosed" },
            };

            TurnoverList = new List<SelectItem>
            {
                new SelectItem { Name = "Upto 1 Lac" },
                new SelectItem { Name = "Upto 2 Lac" },
                new SelectItem { Name = "Upto 3 Lac" },
                new SelectItem { Name = "Upto 5 Lac" },
                new SelectItem { Name = "Upto 10 Lac" },
                new SelectItem { Name = "Upto 50 Lac" },
                new SelectItem { Name = "Upto 1 Crore" },
                new SelectItem { Name = "Upto 10 Crore & Above" }
            };
        }

        public bool isValid()
        {
            return !string.IsNullOrWhiteSpace(Name) && !string.IsNullOrWhiteSpace(Gender) && !string.IsNullOrWhiteSpace(CompanyName) &&
                !string.IsNullOrWhiteSpace(CompanyName) && YearOfEstablishment != null && NumberOfEmployees <= 0 &&
                !string.IsNullOrWhiteSpace(Designation) && !string.IsNullOrWhiteSpace(NatureOfBusiness) && !string.IsNullOrWhiteSpace(Turnover) &&
                !string.IsNullOrWhiteSpace(GSTNumber);
        }
    }
}
