using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BOL.CATEGORIES
{
    [Table("KeywordFirstCategory", Schema = "cat")]
    public class KeywordFirstCategory
    {
        [Key]
        [Display(Name = "Category ID")]
        public int KeywordFirstCategoryID { get; set; }

        [Display(Name = "First Category", Prompt = "Select First Category")]
        [Required(ErrorMessage = "Select First Category")]
        public Nullable<int> FirstCategoryID { get; set; }

        [Display(Name = "Keyword", Prompt = "Keyword related category etc.")]
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
        // End:
    }
}
