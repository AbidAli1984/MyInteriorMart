using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BOL.SHARED
{
    [Table("Location")]
    public class Location
    {
        [Key]
        [Display(Name = "Location ID")]
        public int Id { get; set; }

        [Display(Name = "Name", Prompt = "Andheri, Vashi, Mulund etc.")]
        [Required(ErrorMessage = "Name required.")]
        public string Name { get; set; }

        [Display(Name = "City", Prompt = "Select city")]
        [Required(ErrorMessage = "Select city")]
        public Nullable<int> CityID { get; set; }

        public Location()
        {
            Pincodes = new HashSet<Pincode>();
        }

        public virtual City City { get; set; }
        public virtual ICollection<Pincode> Pincodes { get; set; }
    }
}
