using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BOL.LABOURNAKA
{
    [Table("Personal", Schema = "lab")]
    public class Personal
    {
        [Key]
        [Display(Name = "Personal Id")]
        public int PersonalId { get; set; }

        [Display(Name = "User Guid")]
        [Required(ErrorMessage = "User Guid Required")]
        public string UserGuid { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "First Name Required")]
        public string FirstName { get; set; }

        [Display(Name = "Middle Name")]
        [Required(ErrorMessage = "Middle Name Required")]
        public string MiddleName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Last Name Required")]
        public string LastName { get; set; }

        [Display(Name = "Date Of Birth")]
        [Required(ErrorMessage = "Date Of Birth Required")]
        public DateTime DOB { get; set; }

        [Display(Name = "Gender")]
        [Required(ErrorMessage = "Gender Required")]
        public string Gender { get; set; }

        [Display(Name = "Married")]
        [Required(ErrorMessage = "Married Required")]
        public string Married { get; set; }
    }
}
