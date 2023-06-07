using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;


namespace BOL.LISTING
{
    [Table("WorkingHours", Schema = "listing")]
    public class WorkingHours
    {
        [Key]
        [Display(Name = "Working Hours ID")]
        public int WorkingHoursID { get; set; }

        [Display(Name = "Listing ID")]
        [Required(ErrorMessage = "Listing ID required.")]
        public int ListingID { get; set; }

        [Display(Name = "OwnerGuid", Prompt = "Owner ID")]
        [Required(ErrorMessage = "Owner Guid required.")]
        public string OwnerGuid { get; set; }

        [Display(Name = "IP Address")]
        [Required(ErrorMessage = "IP Address Required")]
        public string IPAddress { get; set; }

        [Display(Name = "Monday From")]
        [DataType(DataType.Time, ErrorMessage = "Time format issue.")]
        [Required(ErrorMessage = "Please select open time.")]
        public DateTime MondayFrom { get; set; }

        [Display(Name = "Monday To")]
        [DataType(DataType.Time, ErrorMessage = "Time format issue.")]
        [Required(ErrorMessage = "Please select close time.")]
        public DateTime MondayTo { get; set; }

        [Display(Name = "Tuesday From")]
        [DataType(DataType.Time, ErrorMessage = "Time format issue.")]
        [Required(ErrorMessage = "Please select open time.")]
        public DateTime TuesdayFrom { get; set; }

        [Display(Name = "Tuesday To")]
        [DataType(DataType.Time, ErrorMessage = "Time format issue.")]
        [Required(ErrorMessage = "Please select close time.")]
        public DateTime TuesdayTo { get; set; }

        [Display(Name = "Wednesday From")]
        [DataType(DataType.Time, ErrorMessage = "Time format issue.")]
        [Required(ErrorMessage = "Please select open time.")]
        public DateTime WednesdayFrom { get; set; }

        [Display(Name = "Wednesday To")]
        [DataType(DataType.Time, ErrorMessage = "Time format issue.")]
        [Required(ErrorMessage = "Please select close time.")]
        public DateTime WednesdayTo { get; set; }

        [Display(Name = "Thursday From")]
        [DataType(DataType.Time, ErrorMessage = "Time format issue.")]
        [Required(ErrorMessage = "Please select open time.")]
        public DateTime ThursdayFrom { get; set; }

        [Display(Name = "Thursday To")]
        [DataType(DataType.Time, ErrorMessage = "Time format issue.")]
        [Required(ErrorMessage = "Please select close time.")]
        public DateTime ThursdayTo { get; set; }

        [Display(Name = "Friday From")]
        [DataType(DataType.Time, ErrorMessage = "Time format issue.")]
        [Required(ErrorMessage = "Please select open time.")]
        public DateTime FridayFrom { get; set; }

        [Display(Name = "Friday To")]
        [DataType(DataType.Time, ErrorMessage = "Time format issue.")]
        [Required(ErrorMessage = "Please select close time.")]
        public DateTime FridayTo { get; set; }

        [Display(Name = "Saturday Holiday")]
        public bool SaturdayHoliday { get; set; }

        [Display(Name = "Saturday From")]
        [DataType(DataType.Time, ErrorMessage = "Time format issue.")]
        public DateTime SaturdayFrom { get; set; }

        [Display(Name = "Saturday To")]
        [DataType(DataType.Time, ErrorMessage = "Time format issue.")]
        public DateTime SaturdayTo { get; set; }

        [Display(Name = "Sunday Holiday")]
        public bool SundayHoliday { get; set; }

        [Display(Name = "Sunday From")]
        [DataType(DataType.Time, ErrorMessage = "Time format issue.")]
        public DateTime SundayFrom { get; set; }

        [Display(Name = "Sunday To")]
        [DataType(DataType.Time, ErrorMessage = "Time format issue.")]
        public DateTime SundayTo { get; set; }
    }
}
