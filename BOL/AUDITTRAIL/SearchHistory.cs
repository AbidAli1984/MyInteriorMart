using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BOL.AUDITTRAIL
{
    [Table("SearchHistory", Schema = "audit")]
    public class SearchHistory
    {
        [Key]
        [Display(Name = "Search ID")]
        public int HistoryID { get; set; }

        [Display(Name = "User Guid", Prompt = "User Guid")]
        [Required(ErrorMessage = "User Guid required.")]
        public string UserGuid { get; set; }

        [Display(Name = "IP Address")]
        public string IPAddress { get; set; }

        [Display(Name = "Country Code")]
        public string CountryCode { get; set; }

        [Display(Name = "Country")]
        public string Country { get; set; }

        [Display(Name = "City")]
        public string City { get; set; }

        [Display(Name = "State")]
        public string State { get; set; }

        [Display(Name = "Pincode")]
        public string Pincode { get; set; }

        [Display(Name = "Latitude")]
        public string Latitude { get; set; }

        [Display(Name = "Longitude")]
        public string Longitude { get; set; }

        [Display(Name = "Search Date")]
        [DataType(DataType.Date, ErrorMessage = "Date format issue.")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime VisitDate { get; set; }

        [Display(Name = "Search Time")]
        [DataType(DataType.Date, ErrorMessage = "Date format issue.")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime VisitTime { get; set; }

        [Display(Name = "Search Term")]
        public string SearchTerm { get; set; }

        [Display(Name = "Search Term ID")]
        public int SearchTermID { get; set; }
    }
}