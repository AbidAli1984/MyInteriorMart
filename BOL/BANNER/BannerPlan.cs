using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BOL.BANNER
{
    [Table("BannerPlan", Schema = "banner")]
    public class BannerPlan
    {
        [Key]
        [Display(Name = "Banner Plan ID")]
        public int BannerPlanID { get; set; }

        [Display(Name = "Plan Name", Prompt = "Plan Name")]
        [Required(ErrorMessage = "Plan Name required.")]
        public string PlanName { get; set; }

        [Display(Name = "Impressions", Prompt = "Impressions in Pixels")]
        [Required(ErrorMessage = "Impressions required.")]
        public int Impressions { get; set; }

        [Display(Name = "Clicks", Prompt = "Clicks")]
        [Required(ErrorMessage = "Clicks required.")]
        public int Clicks { get; set; }

        [Display(Name = "CPM", Prompt = "CPM")]
        [Required(ErrorMessage = "CPM required.")]
        public int CPM { get; set; }

        [Display(Name = "CPC", Prompt = "CPC")]
        [Required(ErrorMessage = "CPC required.")]
        public int CPC { get; set; }

        // Shafi: Show Banner Plan in
        // End:
    }
}
