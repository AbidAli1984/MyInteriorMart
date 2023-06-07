using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BOL.LABOURNAKA
{
    [Table("Document", Schema = "lab")]
    public class Document
    {
        [Key]
        [Display(Name = "Document Id")]
        public int DocumentId { get; set; }

        [Display(Name = "User Guid")]
        [Required(ErrorMessage = "User Guid Required")]
        public string UserGuid { get; set; }

        [Display(Name = "Aadhar Number")]
        public string AadharNumber { get; set; }

        [Display(Name = "Pan Number")]
        public string PanNumber { get; set; }

        [Display(Name = "Aadhar Card")]
        public bool AadharCard { get; set; }

        [Display(Name = "Pan Card")]
        public bool PanCard { get; set; }

        [Display(Name = "Voter Id")]
        public bool VoterId { get; set; }

        [Display(Name = "Driving License")]
        public bool DrivingLicense { get; set; }

        [Display(Name = "Electricity Bill")]
        public bool ElectricityBill { get; set; }
    }
}
