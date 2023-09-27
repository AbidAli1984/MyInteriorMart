using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BOL.SHARED
{
    [Table("Language")]
    public class Language
    {
        [Key]
        [Display(Name = "Language ID")]
        public int Id { get; set; }

        [Display(Name = "Language Name", Prompt = "Language name")]
        [Required(ErrorMessage = "Language required.")]
        public string Name { get; set; }
    }
}
