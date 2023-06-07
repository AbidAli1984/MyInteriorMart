using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BOL.LABOURNAKA
{
    [Table("Profession", Schema = "lab")]
    public class Profession
    {
        [Key]
        [Display(Name = "Profession Id")]
        public int ProfessionId { get; set; }

        [Display(Name = "User Guid")]
        [Required(ErrorMessage = "User Guid Required")]
        public string UserGuid { get; set; }

        [Display(Name = "Qualification")]
        [Required(ErrorMessage = "Qualification Required")]
        public string Qualification { get; set; }

        [Display(Name = "English")]
        public bool English { get; set; }

        [Display(Name = "Hindi")]
        public bool Hindi { get; set; }

        [Display(Name = "Marathi")]
        public bool Marathi { get; set; }

        [Display(Name = "Gujrati")]
        public bool Gujrati { get; set; }

        [Display(Name = "Urdu")]
        public bool Urdu { get; set; }

        [Display(Name = "Tamil")]
        public bool Tamil { get; set; }

        [Display(Name = "Kannada")]
        public bool Kannada { get; set; }

        [Display(Name = "Telegu")]
        public bool Telegu { get; set; }
    }
}
