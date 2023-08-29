using BOL.LISTING;
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
        public string BusinessCategory { get; set; }
        public string Description { get; set; }

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

        public void SetViewModel(Listing listing)
        {
            Name = listing.Name;
            Gender = listing.Gender;
            YearOfEstablishment = listing.YearOfEstablishment;
            CompanyName = listing.CompanyName;
            GSTNumber = listing.GSTNumber;
            Turnover = listing.Turnover;
            NumberOfEmployees = listing.NumberOfEmployees;
            NatureOfBusiness = listing.NatureOfBusiness;
            Designation = listing.Designation;
            BusinessCategory = listing.BusinessCategory;
            Description = listing.Description;
        }

        public void SetContextModel(Listing listing)
        {
            listing.Name = Name;
            listing.Gender = Gender;
            listing.YearOfEstablishment = YearOfEstablishment.Value;
            listing.CompanyName = CompanyName;
            listing.GSTNumber = GSTNumber;
            listing.Turnover = Turnover;
            listing.NumberOfEmployees = NumberOfEmployees;
            listing.NatureOfBusiness = NatureOfBusiness;
            listing.Designation = Designation;
            listing.ListingURL = CompanyName.Replace(" ", "-");
            listing.BusinessCategory = BusinessCategory;
            listing.Description = Description;

            if (listing.Steps < Constants.CompanyComplete)
                listing.Steps = Constants.CompanyComplete;
        }

        public bool isValid()
        {
            return !string.IsNullOrWhiteSpace(Name) && !string.IsNullOrWhiteSpace(Gender) && YearOfEstablishment != null &&
                !string.IsNullOrWhiteSpace(CompanyName) && !string.IsNullOrWhiteSpace(GSTNumber) && NumberOfEmployees > 0 &&
                !string.IsNullOrWhiteSpace(Designation) && !string.IsNullOrWhiteSpace(NatureOfBusiness) && !string.IsNullOrWhiteSpace(Turnover) &&
                !string.IsNullOrWhiteSpace(BusinessCategory) && !string.IsNullOrWhiteSpace(Description);
        }
    }
}
