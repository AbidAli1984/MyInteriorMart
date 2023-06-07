using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BOL.CATEGORIES
{
    [Table("KeywordFifthCategory", Schema = "cat")]
    public class KeywordFifthCategory
    {
        [Key]
        [Display(Name = "Keyword ID")]
        public int KeywordFifthCategoryID { get; set; }

        [Display(Name = "First Category", Prompt = "Select First Category")]
        [Required(ErrorMessage = "Select First Category")]
        public Nullable<int> FirstCategoryID { get; set; }

        [Display(Name = "Second Category", Prompt = "Select Second Category")]
        [Required(ErrorMessage = "Select Second Category")]
        public Nullable<int> SecondCategoryID { get; set; }

        [Display(Name = "Third Category", Prompt = "Select Third Category")]
        [Required(ErrorMessage = "Select Third Category")]
        public Nullable<int> ThirdCategoryID { get; set; }

        [Display(Name = "Fourth Category", Prompt = "Select Fourth Category")]
        [Required(ErrorMessage = "Select Fourth Category")]
        public Nullable<int> FourthCategoryID { get; set; }

        [Display(Name = "Fifth Category", Prompt = "Select Fifth Category")]
        [Required(ErrorMessage = "Select Fifth Category")]
        public Nullable<int> FifthCategoryID { get; set; }

        [Display(Name = "Keyword", Prompt = "Keyword related to category.")]
        [MaxLength(100, ErrorMessage = "Maximum 100 characters allowed.")]
        [MinLength(3, ErrorMessage = "Minimum 3 characters rerquired.")]
        [Required(ErrorMessage = "Keyword name required.")]
        public string Keyword { get; set; }

        [Display(Name = "URL", Prompt = "ACP Dealers etc.")]
        [MaxLength(100, ErrorMessage = "Maximum 100 characters allowed.")]
        [MinLength(3, ErrorMessage = "Minimum 3 characters rerquired.")]
        [Required(ErrorMessage = "URL required.")]
        [RegularExpression("^[a-zA-Z0-9_-]*$")]
        public string URL { get; set; }

        [Display(Name = "Title", Prompt = "Title for this keyword.")]
        [MaxLength(153, ErrorMessage = "Maximum 153 characters allowed.")]
        [MinLength(3, ErrorMessage = "Minimum 3 characters rerquired.")]
        [Required(ErrorMessage = "Title required.")]
        [DataType(DataType.MultilineText)]
        public string Title { get; set; }

        [Display(Name = "Description", Prompt = "Something about this category.")]
        [MaxLength(153, ErrorMessage = "Maximum 153 characters allowed.")]
        [MinLength(3, ErrorMessage = "Minimum 3 characters rerquired.")]
        [Required(ErrorMessage = "Category name required.")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Display(Name = "Search Count")]
        public int SearchCount { get; set; }

        // Shafi: Navigation properties
        public virtual FirstCategory FirstCategory { get; set; }
        public virtual SecondCategory SecondCategory { get; set; }
        public virtual ThirdCategory ThirdCategory { get; set; }
        public virtual FourthCategory FourthCategory { get; set; }
        public virtual FifthCategory FifthCategory { get; set; }
        // End:
    }
}
