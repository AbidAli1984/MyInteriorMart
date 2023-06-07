using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BOL.PLAN
{
    [Table("BannerPlan", Schema = "plan")]
    public class BannerPlan
    {
        [Key]
        [Display(Name = "PlanID")]
        public int PlanID { get; set; }

        [Display(Name = "Plan Name", Prompt = "Top Banner, Side Banner etc.")]
        [Required(ErrorMessage = "Plan Name Required")]
        public string Name { get; set; }

        [Display(Name = "Priority", Prompt = "0, 1, 2 etc.")]
        [Required(ErrorMessage = "Priority Required")]
        public int Priority { get; set; }

        [Display(Name = "Monthly Price", Prompt = "0, 1, 2 etc.")]
        [Required(ErrorMessage = "Monthly Price Required")]
        public int MonthlyPrice { get; set; }
    }
}
