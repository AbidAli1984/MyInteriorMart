using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BOL.SHARED
{
    [Table("Caste")]
    public class Caste
    {
        [Key]
        [Display(Name = "Caste ID")]
        public int Id { get; set; }

        [Display(Name = "Caste Name", Prompt = "Caste name")]
        [Required(ErrorMessage = "Caste required.")]
        public string Name { get; set; }

        [Display(Name = "Religion")]
        [Required(ErrorMessage = "Religion required.")]
        public int ReligionId { get; set; }

        public virtual Religion Religion { get; set; }
    }
}
