using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BOL.SHARED
{
    [Table("State", Schema = "shared")]
    public class State
    {
        [Key]
        public int StateID { get; set; }

        [Display(Name = "Name", Prompt = "Maharashtra, Delhi etc.")]
        [Required(ErrorMessage = "Name required.")]
        //[MaxLength(100, ErrorMessage = "Maximum 100 characters allowed.")]
        //[MinLength(3, ErrorMessage = "Minimum 3 characters required.")]
        public string Name { get; set; }

        [Display(Name = "Country", Prompt = "Select country")]
        [Required(ErrorMessage = "Select country")]
        public Nullable<int> CountryID { get; set; }


        public State()
        {
            Cities = new HashSet<City>();
        }

        public virtual Country Country { get; set; }
        public virtual ICollection<City> Cities { get; set; }
    }
}
