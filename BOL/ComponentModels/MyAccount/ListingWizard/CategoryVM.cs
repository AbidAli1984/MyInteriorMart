using BOL.CATEGORIES;
using BOL.LISTING;
using System;
using System.Collections.Generic;
using System.Text;

namespace BOL.ComponentModels.MyAccount.ListingWizard
{
    public class CategoryVM
    {
        public int FirstCategoryID { get; set; }
        public int SecondCategoryID { get; set; }
        public string ThirdCategory { get; set; }
        public string FourthCategory { get; set; }
        public string FifthCategory { get; set; }
        public string SixthCategory { get; set; }

        public IList<FirstCategory> FirstCategories { get; set; }
        public IList<SecondCategory> SecondCategories { get; set; }
        public IList<SelectItem> ThirdCategories { get; set; }
        public IList<SelectItem> FourthCategories { get; set; }
        public IList<SelectItem> FifthCategories { get; set; }
        public IList<SelectItem> SixthCategories { get; set; }

        public CategoryVM()
        {
            FirstCategories = new List<FirstCategory>();
            SecondCategories = new List<SecondCategory>();
            ThirdCategories = new List<SelectItem>();
            FourthCategories = new List<SelectItem>();
            FifthCategories = new List<SelectItem>();
            SixthCategories = new List<SelectItem>();
        }

        public bool isValid()
        {
            return FirstCategoryID > 0 && SecondCategoryID > 0;
        }
    }
}
