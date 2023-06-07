using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BOL.SHARED
{
    [Table("Messages", Schema = "shared")]
    public class Messages
    {
        [Key]
        public int MessageID { get; set; }

        [Display(Name = "Name", Prompt = "Maharashtra, Delhi etc.")]
        [Required(ErrorMessage = "Name required.")]
        [MaxLength(100, ErrorMessage = "Maximum 100 characters allowed.")]
        [RegularExpression("^[a-zA-Z0-9_-]*$")]
        public string Name { get; set; }

        [Display(Name = "SMS Message", Prompt = "SMS Message")]
        [Required(ErrorMessage = "SMS Message Required.")]
        [DataType(DataType.MultilineText)]
        [MaxLength(160, ErrorMessage = "Maximum 160 characters allowed.")]
        public string SmsMessage { get; set; }

        [Display(Name = "Email Subject", Prompt = "Thank you for registration etc.")]
        [Required(ErrorMessage = "Email Subject Required.")]
        [MaxLength(750, ErrorMessage = "Maximum 750 characters allowed.")]
        public string EmailSubject { get; set; }

        [Display(Name = "Email Message", Prompt = "Email Message")]
        [Required(ErrorMessage = "Email Message Required.")]
        [DataType(DataType.MultilineText)]
        public string EmailMessage { get; set; }

        [Display(Name = "Variables", Prompt = "{Model.Name} {Model.Email} {Model.Mobile} etc.")]
        [DataType(DataType.MultilineText)]
        public string Variables { get; set; }

        [Display(Name = "Active", Prompt = "Active")]
        public bool Active { get; set; }
    }
}
