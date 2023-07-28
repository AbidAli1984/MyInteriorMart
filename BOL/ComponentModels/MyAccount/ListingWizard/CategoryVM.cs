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
        public IList<SelectItem> ThirdCategories { get; set; }
        public IList<SelectItem> FourthCategories { get; set; }
        public IList<SelectItem> FifthCategories { get; set; }
        public IList<SelectItem> SixthCategories { get; set; }

        public CategoryVM()
        {
            Category = new Categories();
            FirstCategories = new List<FirstCategory>();
            SecondCategories = new List<SecondCategory>();
            ThirdCategories = new List<SelectItem>();
            FourthCategories = new List<SelectItem>();
            FifthCategories = new List<SelectItem>();
            SixthCategories = new List<SelectItem>();
        }
    }
}
