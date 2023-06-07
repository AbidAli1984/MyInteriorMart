using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BOL.LISTING
{
    [Table("Certification", Schema = "listing")]
    public class Certification
    {
        [Key]
        [Display(Name = "Certification ID")]
        public int CertificationID { get; set; }

        [Display(Name = "Listing ID")]
        [Required(ErrorMessage = "Listing ID required.")]
        public int ListingID { get; set; }

        [Display(Name = "Owner Guid", Prompt = "Owner ID")]
        [Required(ErrorMessage = "Owner Guid required.")]
        public string OwnerGuid { get; set; }

        [Display(Name = "IP Address")]
        [Required(ErrorMessage = "IP Address Required")]
        public string IPAddress { get; set; }

        [Display(Name = "GST")]
        public bool GST { get; set; }

        [Display(Name = "ISO Certified")]
        public bool ISOCertified { get; set; }

        [Display(Name = "Company Pan Card")]
        public bool CompanyPanCard { get; set; }

        [Display(Name = "ROC Certification")]
        public bool ROCCertification { get; set; }

        [Display(Name = "Gomasta License")]
        public bool GomastaLicense { get; set; }

        [Display(Name = "Accept Tender Work")]
        public bool AcceptTenderWork { get; set; }
    }
}
