using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BOL.AUDITTRAIL
{
    public class Suggestions
    {
        [Key]
        [Display(Name = "SuggestionID")]
        public int SuggestionID { get; set; }

        [Display(Name = "Date")]
        [DataType(DataType.Date, ErrorMessage = "Date format issue.")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [Display(Name = "Name", Prompt = "Name")]
        [Required(ErrorMessage = "Name required.")]
        public string Name { get; set; }

        [Display(Name = "Email", Prompt = "Email")]
        public string Email { get; set; }

        [Display(Name = "Mobile", Prompt = "Mobile")]
        public string Mobile { get; set; }

        [Display(Name = "Suggestion", Prompt = "Suggestion")]
        [Required(ErrorMessage = "Suggestion required.")]
        public string Suggestion { get; set; }
    }
}
