using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BOL.LISTING
{
    [Table("AssembliesAndCities", Schema = "listing")]
    public class AssembliesAndCities
    {
        [Key]
        [Display(Name = "ID")]
        public int AssemblyAndCityID { get; set; }

        [Display(Name = "Listing ID")]
        [Required(ErrorMessage = "Listing ID required.")]
        public int ListingID { get; set; }

        [Display(Name = "Owner Guid", Prompt = "Owner ID")]
        [Required(ErrorMessage = "Owner Guid required.")]
        public string OwnerGuid { get; set; }

        [Display(Name = "IP Address")]
        [Required(ErrorMessage = "IP Address Required")]
        public string IPAddress { get; set; }

        [Display(Name = "Country ID")]
        [Required(ErrorMessage = "Country ID required.")]
        public int CountryID { get; set; }

        [Display(Name = "State ID")]
        [Required(ErrorMessage = "State required.")]
        public int StateID { get; set; }

        [Display(Name = "City IDs")]
        [Required(ErrorMessage = "City required.")]
        public int CityIDs { get; set; }

        [Display(Name = "Assembly IDs")]
        [Required(ErrorMessage = "Assembly required.")]
        public int AssemblyIDs { get; set; }
    }
}
