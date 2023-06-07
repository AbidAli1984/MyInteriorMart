using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BOL.AUDITTRAIL
{
    [Table("ListingNotification", Schema = "notif")]
    public class ListingNotification
    {
        [Key]
        [Display(Name = "Listing Notification ID")]
        public int ListingNotificationID { get; set; }

        [Display(Name = "Date Time")]
        [DataType(DataType.Date, ErrorMessage = "Date format issue.")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateTime { get; set; }

        [Display(Name = "Actor GUID")]
        [Required(ErrorMessage = "ActorGUID required.")]
        public string ActorGUID { get; set; }

        [Display(Name = "Notifier GUID")]
        [Required(ErrorMessage = "ActorGUID required.")]
        public string NotifierGUID { get; set; }

        [Display(Name = "Entity Type")]
        [Required(ErrorMessage = "Entity Type required.")]
        public string EntityType { get; set; }

        [Display(Name = "Entity ID")]
        [Required(ErrorMessage = "Entity ID required.")]
        public int EntityID { get; set; }

        [Display(Name = "Action")]
        [Required(ErrorMessage = "Action required.")]
        public string Action { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "Description required.")]
        public string Description { get; set; }

        [Display(Name = "Mark As Read")]
        public bool MarkAsRead { get; set; }
    }
}
