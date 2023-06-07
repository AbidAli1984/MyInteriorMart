using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BOL.PLAN
{
    [Table("Product", Schema = "plan")]
    public class Product
    {
        [Key]
        [Display(Name = "ProductID")]
        public int ProductID { get; set; }

        [Display(Name = "Product Type", Prompt = "Listing Plan, Banner Plans etc.")]
        [Required(ErrorMessage = "Product Type Required")]
        public string ProductType { get; set; }

        [Display(Name = "Plan", Prompt = "Select Plan")]
        [Required(ErrorMessage = "Plan Required")]
        public Nullable<int> PlanID { get; set; }

        [Display(Name = "Plan Amount", Prompt = "3300, 5000 etc.")]
        [Required(ErrorMessage = "Plan Required")]
        public int PlanAmount { get; set; }

        [Display(Name = "Product Name", Prompt = "Product Name")]
        [Required(ErrorMessage = "Product Name Required")]
        public string ProductName { get; set; }

        [Display(Name = "Description", Prompt = "Description")]
        [Required(ErrorMessage = "Description Required")]
        public string Description { get; set; }

        // Shafi: Show product in
        public IList<Subscription> Subscription { get; set; }
        // End:
    }
}
