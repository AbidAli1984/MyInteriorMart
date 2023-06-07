using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BOL.SHARED
{
    [Table("Pincode", Schema = "shared")]
    public class Pincode
    {
        [Key]
        [Display(Name = "Pincode ID")]
        public int PincodeID { get; set; }

        [Display(Name = "Pincode", Prompt = "400058, 400102 etc.")]
        [Required(ErrorMessage = "Name required.")]
        public int PincodeNumber { get; set; }

        [Display(Name = "Station", Prompt = "Select Station")]
        [Required(ErrorMessage = "Select Station")]
        public Nullable<int> StationID { get; set; }

        // Shafi: Navigation properties
        public virtual Station Station { get; set; }
        // End:

        // Shafi: Show pincode in
        public IList<Locality> Locality { get; set; }
        // End:
    }
}
