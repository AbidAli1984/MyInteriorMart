using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BOL.PLAN
{
    [Table("Period", Schema = "plan")]
    public class Period
    {
        [Key]
        [Display(Name = "Period ID")]
        public int PeriodID { get; set; }

        [Display(Name = "Name", Prompt = "Quarterly, Annually etc.")]
        [Required(ErrorMessage = "Name Required")]
        public string Name { get; set; }

        [Display(Name = "Duration in Months", Prompt = "1, 6, 12, 24, 36 etc")]
        [Required(ErrorMessage = "Duration in Months Required")]
        public int DurationInMonths { get; set; }

        // Shafi: Show plans in
        public IList<Subscription> Subscription { get; set; }
        // End:
    }
}
