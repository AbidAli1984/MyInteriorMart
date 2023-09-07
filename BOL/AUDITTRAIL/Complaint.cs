using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BOL.AUDITTRAIL
{
    public class Complaint
    {
        [Key]
        [Display(Name = "ComplaintID")]
        public int Id { get; set; }

        [Display(Name = "OwnerGuid", Prompt = "Owner ID")]
        public string OwnerGuid { get; set; }

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

        [Display(Name = "Title")]
        public string Title { get; set; }

        [Display(Name = "Description", Prompt = "Description")]
        [Required(ErrorMessage = "Description required.")]
        public string Description { get; set; }
    }
}
