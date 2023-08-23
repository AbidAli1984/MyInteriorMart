using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace BOL.SHARED
{
    [Table("Country", Schema = "shared")]
    public class Country
    {
        [Key]
        public int CountryID { get; set; }

        [Display(Name = "Name", Prompt = "India, China etc.")]
        [Required(ErrorMessage = "Name Required.")]
        public string Name { get; set; }

        [Display(Name = "Sort Name", Prompt = "IN, PK, SA, Etc.")]
        [Required(ErrorMessage = "Sort Name Required.")]
        public string SortName { get; set; }

        [Display(Name = "ISO3 Name", Prompt = "IND, PAK etc.")]
        [Required(ErrorMessage = "ISO3 Name Required.")]
        public string ISO3Name { get; set; }

        [Display(Name = "Capital", Prompt = "IN, PK, SA, Etc.")]
        public string Capital { get; set; }

        [Display(Name = "Currency", Prompt = "INR, USD Etc.")]
        public string Currency { get; set; }

        [Display(Name = "Phone Code", Prompt = "91, 93 etc.")]
        public string PhoneCode { get; set; }

        public Country()
        {
            States = new HashSet<State>();
        }
        public virtual ICollection<State> States { get; set; }
    }
}
