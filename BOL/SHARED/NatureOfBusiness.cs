using BOL.LISTING;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BOL.SHARED
{
    [Table("NatureOfBusiness", Schema = "shared")]
    public class NatureOfBusiness
    {
        [Key]
        public int NatureOfBusinessID { get; set; }

        [Display(Name = "Name", Prompt = "Pvt., Ltd. etc.")]
        [Required(ErrorMessage = "Name required.")]
        [MaxLength(100, ErrorMessage = "Maximum 100 characters allowed.")]
        [MinLength(3, ErrorMessage = "Minimum 3 characters required.")]
        public string Name { get; set; }
    }
}
