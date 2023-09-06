using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BOL.LISTING
{
    [Table("RatingReply")]
    public class RatingReply
    {
        [Key]
        public int Id { get; set; }

        public string Message { get; set; }

        public int RatingId { get; set; }
        public Rating Rating { get; set; }
    }
}
