using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BOL.PLAN
{
    [Table("MagazinePlan", Schema = "plan")]
   public class MagazinePlan
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
        [Required(ErrorMessage = "Price Required")]
        public int Price { get; set; }
    }
}
