using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BOL.LISTING
{
    [Table("Address", Schema = "listing")]
   public class Address
    {
        [Key]
        [Display(Name = "Address ID")]
        public int AddressID { get; set; }

        [Display(Name = "Listing ID")]
        [Required(ErrorMessage = "Listing ID required.")]
        public int ListingID { get; set; }

        [Display(Name = "OwnerGuid", Prompt = "Owner ID")]
        [Required(ErrorMessage = "Owner Guid required.")]
        public string OwnerGuid { get; set; }

        [Display(Name = "IP Address")]
        [Required(ErrorMessage = "IP Address Required")]
        public string IPAddress { get; set; }

        [Display(Name = "Country")]
        [Required(ErrorMessage = "Select Country")]
        public int CountryID { get; set; }

        [Display(Name = "State")]
        [Required(ErrorMessage = "Select State")]
        public int StateID { get; set; }

        [Display(Name = "City")]
        [Required(ErrorMessage = "Select City")]
        public int City { get; set; }

        [Display(Name = "Assembly")]
        [Required(ErrorMessage = "Select Assembly")]
        public int AssemblyID { get; set; }

        [Display(Name = "Pincode")]
        [Required(ErrorMessage = "Select Pincode")]
        public int PincodeID { get; set; }

        [Display(Name = "Locality")]
        //[Required(ErrorMessage = "Select Locality")]
        public int LocalityID { get; set; }

        [Display(Name = "Local Address")]
        [Required(ErrorMessage = "Address Required")]
        public string LocalAddress { get; set; }


    }
}
