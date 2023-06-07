using System.Collections;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using BOL.LISTING;

namespace BOL.SHARED
{
    [Table("Designation", Schema = "shared")]
    public class Designation
    {
        [Key]
        public int DesignationID { get; set; }

        [Display(Name = "Name", Prompt = "Proprietor, Owner etc.")]
        [Required(ErrorMessage = "Name required.")]
        public string Name { get; set; }
    }
}
