using BOL.SHARED;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BOL.AUDITTRAIL
{
    [Table("UserHistory", Schema = "audit")]
    public class UserHistory
    {
        [Key]
        [Display(Name = "History ID")]
        public int HistoryID { get; set; }

        [Display(Name = "User Guid", Prompt = "User Guid")]
        [Required(ErrorMessage = "User Guid required.")]
        public string UserGuid { get; set; }

        [Display(Name = "Email", Prompt = "Email")]
        public string Email { get; set; }

        [Display(Name = "Mobile", Prompt = "Mobile")]
        public string Mobile { get; set; }

        [Display(Name = "IP Address")]
        public string IPAddress { get; set; }

        [Display(Name = "User Role")]
        public string UserRole { get; set; }

        [Display(Name = "Visit Date")]
        [DataType(DataType.Date, ErrorMessage = "Date format issue.")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime VisitDate { get; set; }

        [Display(Name = "Visit Time")]
        [DataType(DataType.Date, ErrorMessage = "Date format issue.")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime VisitTime { get; set; }

        //[Display(Name = "Visit Date")]
        //public string VisitDate { get; set; }

        //[Display(Name = "Visit Time")]
        //public string VisitTime { get; set; }

        [Display(Name = "User Agent")]
        public string UserAgent { get; set; }

        [Display(Name = "Referrer URL")]
        public string ReferrerURL { get; set; }

        [Display(Name = "Visited URL")]
        public string VisitedURL { get; set; }

        [Display(Name = "Activity")]
        public string Activity { get; set; }
    }
}
