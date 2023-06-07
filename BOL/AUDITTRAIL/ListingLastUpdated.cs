using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BOL.AUDITTRAIL
{
    [Table("ListingLastUpdated", Schema = "audit")]
    public class ListingLastUpdated
    {
        [Key]
        [Display(Name = "Last Updated ID")]
        public int LastUpdatedID { get; set; }

        [Display(Name = "Listing ID")]
        public int ListingID { get; set; }

        [Display(Name = "Updated By Guid", Prompt = "Updated By Guid")]
        [Required(ErrorMessage = "Updated By Guid required.")]
        public string UpdatedByGuid { get; set; }

        [Display(Name = "Email", Prompt = "Email")]
        public string Email { get; set; }

        [Display(Name = "Mobile", Prompt = "Mobile")]
        public string Mobile { get; set; }

        [Display(Name = "IP Address")]
        public string IPAddress { get; set; }

        [Display(Name = "User Role")]
        public string UserRole { get; set; }

        [Display(Name = "Section Updated")]
        public string SectionUpdated { get; set; }

        [Display(Name = "Updated Date")]
        [DataType(DataType.Date, ErrorMessage = "Date format issue.")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime UpdatedDate { get; set; }

        [Display(Name = "Updated Time")]
        [DataType(DataType.Date, ErrorMessage = "Date format issue.")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime UpdatedTime { get; set; }

        [Display(Name = "Updated URL")]
        public string UpdatedURL { get; set; }

        [Display(Name = "User Agent")]
        public string UserAgent { get; set; }

        [Display(Name = "Activity")]
        public string Activity { get; set; }
    }
}
