using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BOL.SHARED
{
    [Table("Station", Schema = "shared")]
    public class Station
    {
        [Key]
        [Display(Name = "Station ID")]
        public int StationID { get; set; }

        [Display(Name = "Name", Prompt = "Andheri, Vashi, Mulund etc.")]
        [Required(ErrorMessage = "Name required.")]
        public string Name { get; set; }

        [Display(Name = "City", Prompt = "Select city")]
        [Required(ErrorMessage = "Select city")]
        public Nullable<int> CityID { get; set; }

        // Shafi: Navigation properties
        public virtual City City { get; set; }
        // End:

        // Shafi: Show assemblies in
        public IList<Pincode> Pincode { get; set; }
        public IList<Locality> Locality { get; set; }
        // End:
    }
}
