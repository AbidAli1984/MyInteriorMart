using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BOL.AUDITTRAIL
{
    [Table("Subscribes", Schema = "audit")]
    public class Subscribes : ListingActivity
    {
        [Key]
        [Display(Name = "Subscribe ID")]
        public int SubscribeID { get; set; }

        [Display(Name = "Subscribe Status")]
        public bool Subscribe { get; set; }
    }
}
