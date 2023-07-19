using BOL.CATEGORIES;
using BOL.LISTING;
using System;
using System.Collections.Generic;
using System.Text;

namespace BOL.ComponentModels.MyAccount.ListingWizard
{
    public class CategoryVM
    {
        public Categories Category { get; set; }
        public IList<FirstCategory> FirstCategories { get; set; }
        public IList<SecondCategory> SecondCategories { get; set; }
        public IList<OtherCategories> ThirdCategories { get; set; }
        public IList<OtherCategories> FourthCategories { get; set; }
        public IList<OtherCategories> FifthCategories { get; set; }
        public IList<OtherCategories> SixthCategories { get; set; }

        public CategoryVM()
        {
            Category = new Categories();
            FirstCategories = new List<FirstCategory>();
            SecondCategories = new List<SecondCategory>();
            ThirdCategories = new List<OtherCategories>();
            FourthCategories = new List<OtherCategories>();
            FifthCategories = new List<OtherCategories>();
            SixthCategories = new List<OtherCategories>();
        }
    }

    public class OtherCategories
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsSelected { get; set; }
    }
}
