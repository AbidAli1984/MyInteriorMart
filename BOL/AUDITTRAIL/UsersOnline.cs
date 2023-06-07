using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BOL.AUDITTRAIL
{
    public class UsersOnline
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "User Name Required")]
        [Display(Name = "User Name", Prompt = "Please type User ID")]
        public string UserID { get; set; }

        [Display(Name = "Name", Prompt = "Please type Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Connection ID Required")]
        [Display(Name = "Connection ID", Prompt = "Please type Connection ID")]
        public string ConnectionID { get; set; }
    }
}
