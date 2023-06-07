using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BOL.CATEGORIES
{
    [Table("Pages", Schema = "cms")]
    public class Pages
    {
        [Key]
        [Display(Name = "Page ID")]
        public int PageID { get; set; }

        [Display(Name = "Page Name")]
        [Required(ErrorMessage = "Page Name Required")]
        public string PageName { get; set; }

        [Display(Name = "Priority")]
        [Required(ErrorMessage = "Priority Required")]
        public string Priority { get; set; }

        [Display(Name = "URL")]
        [Required(ErrorMessage = "URL Required")]
        public string URL { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "Description Required")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Display(Name = "Keywords", Prompt = "Keywords")]
        [Required(ErrorMessage = "Keywords required.")]
        [DataType(DataType.MultilineText)]
        public string Keywords { get; set; }
    }
}
