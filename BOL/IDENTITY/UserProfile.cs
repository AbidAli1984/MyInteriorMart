using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BOL.IDENTITY
{
    [Table("UserProfile", Schema = "dbo")]
    public class UserProfile
    {
        [Key]
        [Display(Name = "Profile ID")]
        public int ProfileID { get; set; }

        [Display(Name = "OwnerGuid", Prompt = "Owner ID")]
        [Required(ErrorMessage = "Owner Guid required.")]
        public string OwnerGuid { get; set; }

        [Display(Name = "IP Address")]
        [Required(ErrorMessage = "IP Address Required")]
        public string IPAddress { get; set; }

        [Display(Name = "Image Url")]
        [Required(ErrorMessage = "Image Url Required")]
        public string ImageUrl { get; set; }

        [Display(Name = "Name", Prompt = "Full Name")]
        [MaxLength(50, ErrorMessage = "Maximum 50 characters allowed.")]
        [MinLength(3, ErrorMessage = "Minimum 3 characters rerquired.")]
        [Required(ErrorMessage = "Name required.")]
        public string Name { get; set; }

        [Display(Name = "Gender", Prompt = "Male, Female etc")]
        [Required(ErrorMessage = "Gender required.")]
        public string Gender { get; set; }

        // Shafi: Date of Birth
        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date, ErrorMessage = "Date format issue.")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "MaritalStatus")]
        public string MaritalStatus { get; set; }

        [Display(Name = "Qualification")]
        public string Qualification { get; set; }

        [Display(Name = "Address")]
        public string Address { get; set; }

        [Display(Name = "Country")]
        public int CountryID { get; set; }

        [Display(Name = "State")]
        public int StateID { get; set; }

        [Display(Name = "City")]
        public int CityID { get; set; }

        [Display(Name = "Assembly")]
        public int AssemblyID { get; set; }

        [Display(Name = "Pincode")]
        public int PincodeID { get; set; }

        [Display(Name = "Time Zone")]
        [Required(ErrorMessage = "Time Zone Required")]
        public string TimeZoneOfCountry { get; set; }

        public bool IsProfileCompleted { get; set; }

        [Display(Name = "Created Date")]
        [DataType(DataType.Date, ErrorMessage = "Date format issue.")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "Updated Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime UpdatedDate { get; set; }
    }
}
