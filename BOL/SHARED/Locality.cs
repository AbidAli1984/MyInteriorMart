using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BOL.SHARED
{
    [Table("Locality", Schema = "shared")]
    public class Locality
    {
        [Key]
        [Display(Name = "Locality ID")]
        public int LocalityID { get; set; }

        [Display(Name = "Locality", Prompt = "Landmark or street name")]
        [Required(ErrorMessage = "Locality required.")]
        public string LocalityName { get; set; }

        [Display(Name = "Station", Prompt = "Select Station")]
        [Required(ErrorMessage = "Select Station")]
        public Nullable<int> StationID { get; set; }

        [Display(Name = "Pincode", Prompt = "Select pincode")]
        [Required(ErrorMessage = "Pincode required.")]
        public Nullable<int> PincodeID { get; set; }

        // Shafi: Navigation properties
        public virtual Station Station { get; set; }
        public virtual Pincode Pincode { get; set; }
        // End:
    }
}
