using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BOL.CATEGORIES
{
    [Table("NewSearchTerm", Schema = "cat")]
    public class NewSearchTerm
    {
        [Key]
        public int NewSearchTermID { get; set; }

        [Display(Name = "Search Term")]
        [Required(ErrorMessage = "Search Term Required")]
        public string SearchTerm { get; set; }

        // Shafi: Date and time
        [Display(Name = "Search Date Time")]
        [DataType(DataType.Date, ErrorMessage = "Date format issue.")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime SearchDateTime { get; set; }
    }
}
