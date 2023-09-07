using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BOL.AUDITTRAIL
{
    [Table("LikeDislike", Schema = "audit")]
    public class ListingLikeDislike : ListingActivity
    {
        [Key]
        [Display(Name = "ID")]
        public int LikeDislikeID { get; set; }

        [Display(Name = "Like")]
        public bool Like { get; set; }
    }
}
