using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace IDENTITY.Models
{
    [Table("SuspendedUser", Schema = "dbo")]
    public class SuspendedUser
    {
        [Key]
        [Display(Name = "Suspended ID")]
        public int SuspendedId { get; set; }

        [Display(Name = "Suspended To", Prompt = "Suspended To")]
        [Required(ErrorMessage = "Suspended To required.")]
        public string SuspendedTo { get; set; }

        [Display(Name = "Suspended By", Prompt = "Suspended By")]
        [Required(ErrorMessage = "Suspended By required.")]
        public string SuspendedBy { get; set; }

        // Shafi: Date and time
        [Display(Name = "Suspended Date")]
        [DataType(DataType.Date, ErrorMessage = "Date format issue.")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime SuspendedDate { get; set; }

        [Display(Name = "Suspended", Prompt = "Suspended")]
        public bool Suspended { get; set; }

        [Display(Name = "Reason for Suspending")]
        [DataType(DataType.MultilineText)]
        public string ReasonForSuspending { get; set; }

        [Display(Name = "Unsuspended Date")]
        [DataType(DataType.Date, ErrorMessage = "Date format issue.")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime UnsuspendedDate { get; set; }

        [Display(Name = "Unsuspended By", Prompt = "Unsuspended By")]
        public string UnsuspendedBy { get; set; }

        [Display(Name = "Reason for Unsuspending")]
        [DataType(DataType.MultilineText)]
        public string ReasonForUnsuspending { get; set; }
    }
}
