using BOL.CATEGORIES;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BOL.BRANDS
{
    [Table("KeywordBrand", Schema = "brands")]
    public class KeywordBrand
    {
        [Key]
        [Display(Name = "ID")]
        public int BrandKeywordID { get; set; }

        [Required(ErrorMessage = "First Category Required")]
        public Nullable<int> FirstCategoryID { get; set; }

        [Required(ErrorMessage = "Second Category Required")]
        public Nullable<int> SecondCategoryID { get; set; }

        [Required(ErrorMessage = "Brand Category Required")]
        public Nullable<int> BrandCategoryID { get; set; }

        [Required(ErrorMessage = "Keyword Required")]
        public string Keyword { get; set; }

        // Shafi: 1 to 1 relationship
        public virtual FirstCategory FirstCategory { get; set; }
        public virtual SecondCategory SecondCategory { get; set; }
        public virtual BrandCategory BrandCategory { get; set; }
        // End:
    }
}
