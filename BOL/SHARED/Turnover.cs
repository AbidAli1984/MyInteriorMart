using BOL.LISTING;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BOL.SHARED
{
    [Table("Turnover", Schema = "shared")]
    public class Turnover
    {
        [Key]
        public int TurnoverID { get; set; }

        [Display(Name = "Name", Prompt = "Above Rs.1 Lac etc.")]
        [Required(ErrorMessage = "Name required.")]
        [MaxLength(100, ErrorMessage = "Maximum 100 characters allowed.")]
        [MinLength(3, ErrorMessage = "Minimum 6 characters required.")]
        public string Name { get; set; }
    }
}
