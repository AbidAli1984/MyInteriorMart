using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BOL.PLAN
{
    [Table("AdvertisementPlan", Schema = "plan")]
    public class AdvertisementPlan
    {
        [Key]
        [Display(Name = "PlanID")]
        public int PlanID { get; set; }

        [Display(Name = "Plan Name", Prompt = "Plan Name")]
        [Required(ErrorMessage = "Plan Name Required")]
        public string Name { get; set; }

        [Display(Name = "Priority", Prompt = "0, 1, 2 etc.")]
        [Required(ErrorMessage = "Priority Required")]
        public int Priority { get; set; }

        [Display(Name = "Price", Prompt = "0, 1, 2 etc.")]
        [Required(ErrorMessage = "Monthly Price Required")]
        public int Price { get; set; }
    }
}
