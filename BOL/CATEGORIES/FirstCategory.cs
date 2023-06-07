using BOL.BANNERADS;
using BOL.BRANDS;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BOL.CATEGORIES
{
    [Table("FirstCategory", Schema = "cat")]
    public class FirstCategory
    {
        [Key]
        [Display(Name = "Category ID")]
        public int FirstCategoryID { get; set; }

        [Display(Name = "Name", Prompt = "ACP Dealers, Suppliers etc.")]
        [MaxLength(70, ErrorMessage = "Maximum 70 characters allowed.")]
        [MinLength(3, ErrorMessage = "Minimum 3 characters rerquired.")]
        [Required(ErrorMessage = "Category name required.")]
        public string Name { get; set; }

        [Display(Name = "Search Keyword Name", Prompt = "ACP Dealers etc.")]
        [MaxLength(70, ErrorMessage = "Maximum 70 characters allowed.")]
        [MinLength(3, ErrorMessage = "Minimum 3 characters rerquired.")]
        [Required(ErrorMessage = "Category name required.")]
        public string SearchKeywordName { get; set; }

        [Display(Name = "Category URL", Prompt = "ACP-Dealers etc.")]
        [MaxLength(70, ErrorMessage = "Maximum 70 characters allowed.")]
        [MinLength(3, ErrorMessage = "Minimum 3 characters rerquired.")]
        [Required(ErrorMessage = "Category name required.")]
        [RegularExpression("^[a-zA-Z0-9_-]*$")]
        public string URL { get; set; }

        [Display(Name = "Sort Order", Prompt = "1,10,100,500 etc.")]
        public int SortOrder { get; set; }

        [Display(Name = "Description", Prompt = "Something about this category.")]
        [MaxLength(153, ErrorMessage = "Maximum 153 characters allowed.")]
        [MinLength(3, ErrorMessage = "Minimum 3 characters rerquired.")]
        [Required(ErrorMessage = "Category name required.")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Display(Name = "Keyword", Prompt = "acp dealers, acp suppliers etc.")]
        [DataType(DataType.MultilineText)]
        public string Keyword { get; set; }

        [Display(Name = "Search Count")]
        public int SearchCount { get; set; }

        // Shafi: Show First Category Categories
        public IList<SecondCategory> SecondCategory { get; set; }
        public IList<ThirdCategory> ThirdCategory { get; set; }
        public IList<FourthCategory> FourthCategory { get; set; }
        public IList<FifthCategory> FifthCategory { get; set; }
        public IList<SixthCategory> SixthCategory { get; set; }

        // Show First Category in Following Keywords
        public IList<KeywordFirstCategory> KeywordFirstCategory { get; set; }
        public IList<KeywordSecondCategory> KeywordSecondCategory { get; set; }
        public IList<KeywordThirdCategory> KeywordThirdCategory { get; set; }
        public IList<KeywordFourthCategory> KeywordFourthCategory { get; set; }
        public IList<KeywordFifthCategory> KeywordFifthCategory { get; set; }
        public IList<KeywordSixthCategory> KeywordSixthCategory { get; set; }
        // End:

        // Shafi: Show category in BrandsCategory
        public IList<BrandCategory> BrandsCategory { get; set; }
        public IList<KeywordBrand> KeywordBrand { get; set; }
        public IList<ListingTitle> ListingTitle { get; set; }
        // End:
    }
}
