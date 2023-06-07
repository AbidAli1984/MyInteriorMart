using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BOL.CATEGORIES
{
    [Table("ListingTitle", Schema = "cat")]
    public class ListingTitle
    {
        [Key]
        [Display(Name = "Title ID")]
        public int TitleID { get; set; }

        [Required(ErrorMessage = "First Category Required")]
        [Display(Name = "First Category")]
        public Nullable<int> FirstCategoryID { get; set; }

        [Required(ErrorMessage = "Second Category Required")]
        [Display(Name = "Second Category")]
        public Nullable<int> SecondCategoryID { get; set; }

        [Required(ErrorMessage = "Name Required")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "URL Required")]
        [Display(Name = "URL Name")]
        public string URL { get; set; }

        [Display(Name = "SortOrder")]
        public int SortOrder { get; set; }

        [Display(Name = "Description")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Display(Name = "Title")]
        [DataType(DataType.MultilineText)]
        public string Title { get; set; }

        [Display(Name = "Keyword")]
        [DataType(DataType.MultilineText)]
        public string Keyword { get; set; }

        // 1 * 1 relationship
        public virtual FirstCategory FirstCategory { get; set; }
        public virtual SecondCategory SecondCategory { get; set; }
    }
}
