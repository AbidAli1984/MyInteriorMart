using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BOL.AUDITTRAIL
{
    [Table("Bookmarks", Schema = "audit")]
    public class Bookmarks : ListingActivity
    {
        [Key]
        [Display(Name = "BookmarksID")]
        public int BookmarksID { get; set; }

        [Display(Name = "Bookmark Status")]
        public bool Bookmark { get; set; }
    }
}
