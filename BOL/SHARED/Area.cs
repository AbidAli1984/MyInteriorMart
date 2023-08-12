using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BOL.SHARED
{
    [Table("Area")]
    public class Area
    {
        [Key]
        [Display(Name = "Area ID")]
        public int Id { get; set; }

        [Display(Name = "Area Name", Prompt = "Landmark or street name")]
        [Required(ErrorMessage = "Locality required.")]
        public string Name { get; set; }

        [Display(Name = "Location", Prompt = "Select Location")]
        [Required(ErrorMessage = "Select Station")]
        public Nullable<int> LocationId { get; set; }

        [Display(Name = "Pincode", Prompt = "Select pincode")]
        [Required(ErrorMessage = "Pincode required.")]
        public Nullable<int> PincodeID { get; set; }

        // Shafi: Navigation properties
        public virtual Location Location { get; set; }
        public virtual Pincode Pincode { get; set; }
        // End:
    }
}
