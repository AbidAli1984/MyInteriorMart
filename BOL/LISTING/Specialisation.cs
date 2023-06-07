using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BOL.LISTING
{
    [Table("Specialisation", Schema = "listing")]
    public class Specialisation
    {
        [Key]
        [Display(Name = "Specialisation ID")]
        public int SpecialisationID { get; set; }

        [Display(Name = "Listing ID")]
        [Required(ErrorMessage = "Listing ID required.")]
        public int ListingID { get; set; }

        [Display(Name = "Owner Guid", Prompt = "Owner ID")]
        [Required(ErrorMessage = "Owner Guid required.")]
        public string OwnerGuid { get; set; }

        [Display(Name = "IP Address")]
        [Required(ErrorMessage = "IP Address Required")]
        public string IPAddress { get; set; }

        [Display(Name = "Accept Tender Work")]
        public bool AcceptTenderWork { get; set; }

        [Display(Name = "Banks")]
        public bool Banks { get; set; }

        [Display(Name = "Beauty Parlors")]
        public bool BeautyParlors { get; set; }

        [Display(Name = "Bungalow")]
        public bool Bungalow { get; set; }

        [Display(Name = "Call Center")]
        public bool CallCenter { get; set; }

        [Display(Name = "Church")]
        public bool Church { get; set; }

        [Display(Name = "Company")]
        public bool Company { get; set; }

        [Display(Name = "Computer Institute")]
        public bool ComputerInstitute { get; set; }

        [Display(Name = "Dispensary")]
        public bool Dispensary { get; set; }

        [Display(Name = "Exhibition Stall")]
        public bool ExhibitionStall { get; set; }

        [Display(Name = "Factory")]
        public bool Factory { get; set; }

        [Display(Name = "Farmhouse")]
        public bool Farmhouse { get; set; }

        [Display(Name = "Gurudwara")]
        public bool Gurudwara { get; set; }

        [Display(Name = "Gym")]
        public bool Gym { get; set; }

        [Display(Name = "Health Club")]
        public bool HealthClub { get; set; }

        [Display(Name = "Home")]
        public bool Home { get; set; }

        [Display(Name = "Hospital")]
        public bool Hospital { get; set; }

        [Display(Name = "Hotel")]
        public bool Hotel { get; set; }

        [Display(Name = "Laboratory")]
        public bool Laboratory { get; set; }

        [Display(Name = "Mandir")]
        public bool Mandir { get; set; }

        [Display(Name = "Mosque")]
        public bool Mosque { get; set; }

        [Display(Name = "Office")]
        public bool Office { get; set; }

        [Display(Name = "Plazas")]
        public bool Plazas { get; set; }

        [Display(Name = "Residential Society")]
        public bool ResidentialSociety { get; set; }

        [Display(Name = "Resorts")]
        public bool Resorts { get; set; }

        [Display(Name = "Restaurants")]
        public bool Restaurants { get; set; }

        [Display(Name = "Salons")]
        public bool Salons { get; set; }

        [Display(Name = "Shop")]
        public bool Shop { get; set; }

        [Display(Name = "Shopping Mall")]
        public bool ShoppingMall { get; set; }

        [Display(Name = "Showroom")]
        public bool Showroom { get; set; }

        [Display(Name = "Warehouse")]
        public bool Warehouse { get; set; }

    }
}
