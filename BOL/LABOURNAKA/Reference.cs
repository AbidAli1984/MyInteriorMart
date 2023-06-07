using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BOL.LABOURNAKA
{
    [Table("Reference", Schema = "lab")]
    public class Reference
    {
        [Key]
        [Display(Name = "Reference Id")]
        public int ReferenceId { get; set; }

        [Display(Name = "User Guid")]
        [Required(ErrorMessage = "User Guid Required")]
        public string UserGuid { get; set; }

        // 1st Reference
        [Display(Name = "Ref 1st Name")]
        [Required(ErrorMessage = "Ref 1st Name Required")]
        public string Ref1stName { get; set; }

        [Display(Name = "Ref 1st Mobile")]
        [Required(ErrorMessage = "Ref 1st Mobile Required")]
        public string Ref1stMobile { get; set; }

        [Display(Name = "Ref 1st Address")]
        [Required(ErrorMessage = "Ref 1st Address Required")]
        public string Ref1stAddress { get; set; }

        [Display(Name = "Ref 1st Relationship")]
        [Required(ErrorMessage = "Ref 1st Relationship Required")]
        public string Ref1stRelationship { get; set; }

        // 2nd Reference
        [Display(Name = "Ref 2nd Name")]
        [Required(ErrorMessage = "Ref 2nd Name Required")]
        public string Ref2ndName { get; set; }

        [Display(Name = "Ref 2nd Mobile")]
        [Required(ErrorMessage = "Ref 2nd Mobile Required")]
        public string Ref2ndMobile { get; set; }

        [Display(Name = "Ref 2nd Address")]
        [Required(ErrorMessage = "Ref 2nd Address Required")]
        public string Ref2ndAddress { get; set; }

        [Display(Name = "Ref 2nd Relationship")]
        [Required(ErrorMessage = "Ref 2nd Relationship Required")]
        public string Ref2ndRelationship { get; set; }
    }
}
