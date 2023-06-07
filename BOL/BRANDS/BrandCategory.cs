using BOL.CATEGORIES;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BOL.BRANDS
{
    [Table("BrandsCategory", Schema = "brands")]
    public class BrandCategory
    {
        [Key]
        [Display(Name = "ID")]
        public int BrandCategoryID { get; set; }
        public Nullable<int> BrandID { get; set; }

        [Required(ErrorMessage = "First Category Required")]
        public Nullable<int> FirstCategoryID { get; set; }

        [Required(ErrorMessage = "Second Category Required")]
        public Nullable<int> SecondCategoryID { get; set; }

        [Required(ErrorMessage = "Brand Category Name Required")]
        public string BrandCategoryName { get; set; }

        // 1 * 1 relationship
        public virtual Brand Brand { get; set; }
        public virtual FirstCategory FirstCategory { get; set; }
        public virtual SecondCategory SecondCategory { get; set; }

        // Show brands in
        public IList<KeywordBrand> KeywordBrand { get; set; }
        // End:
    }
}
