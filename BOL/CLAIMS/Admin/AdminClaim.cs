using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BOL.CLAIMS.Admin
{
    public class AdminClaim
    {
        public class Dashboard
        {
            [Key]
            public int ID { get; set; }
            public string Name { get; set; }
            public string Type { get; set; }
            public string View { get; set; }
            public string MainMenu { get; set; }
            public string Menu { get; set; }
            public string Link { get; set; }
        }

        public class UserManager
        {
            [Key]
            public int ID { get; set; }
            public string Name { get; set; }
            public string Type { get; set; }
            public string ViewAll { get; set; }
            public string ViewProfile { get; set; }
            public string Read { get; set; }
            public string Edit { get; set; }
            public string Create { get; set; }
            public string Delete { get; set; }
            public string AssignRoleToUser { get; set; }
            public string RemoveUserFromRole { get; set; }
            public string ListUsersInRole { get; set; }
            public string AssignClaimToRole { get; set; }
            public string BlockUser { get; set; }
            public string UnblockUser { get; set; }
        }

        public class Listing
        {
            [Key]
            public int ID { get; set; }
            public string Name { get; set; }
            public string Type { get; set; }
            public string ViewAll { get; set; }
            public string Read { get; set; }
            public string Edit { get; set; }
            public string Create { get; set; }
            public string Delete { get; set; }
            public string Approve { get; set; }
        }

        public class Localities
        {
            [Key]
            public int ID { get; set; }
            public string Name { get; set; }
            public string Type { get; set; }
            public string ViewAll { get; set; }
            public string Read { get; set; }
            public string Edit { get; set; }
            public string Create { get; set; }
            public string Delete { get; set; }
        }

        public class Categories
        {
            [Key]
            public int ID { get; set; }
            public string Name { get; set; }
            public string Type { get; set; }
            public string ViewAll { get; set; }
            public string Read { get; set; }
            public string Edit { get; set; }
            public string Create { get; set; }
            public string Delete { get; set; }
        }

        public class Miscellaneous
        {
            [Key]
            public int ID { get; set; }
            public string Name { get; set; }
            public string Type { get; set; }
            public string ViewAll { get; set; }
            public string Read { get; set; }
            public string Edit { get; set; }
            public string Create { get; set; }
            public string Delete { get; set; }
        }

        public class Keywords
        {
            [Key]
            public int ID { get; set; }
            public string Name { get; set; }
            public string Type { get; set; }
            public string ViewAll { get; set; }
            public string Read { get; set; }
            public string Edit { get; set; }
            public string Create { get; set; }
            public string Delete { get; set; }
        }

        public class Pages
        {
            [Key]
            public int ID { get; set; }
            public string Name { get; set; }
            public string Type { get; set; }
            public string ViewAll { get; set; }
            public string Read { get; set; }
            public string Edit { get; set; }
            public string Create { get; set; }
            public string Delete { get; set; }
        }


        public class Notifications
        {
            [Key]
            public int ID { get; set; }
            public string Name { get; set; }
            public string Type { get; set; }
            public string ViewAll { get; set; }
            public string Read { get; set; }
            public string Edit { get; set; }
            public string Create { get; set; }
            public string Delete { get; set; }
            public string ReceiveSMS { get; set; }
            public string ReceiveEmail { get; set; }
        }

        public class Slideshow
        {
            [Key]
            public int ID { get; set; }
            public string Name { get; set; }
            public string Type { get; set; }
            public string ViewAll { get; set; }
            public string Read { get; set; }
            public string Edit { get; set; }
            public string Create { get; set; }
            public string Delete { get; set; }
        }

        public class HistoryAndCache
        {
            [Key]
            public int ID { get; set; }
            public string Name { get; set; }
            public string Type { get; set; }
            public string ViewAll { get; set; }
            public string Read { get; set; }
            public string Edit { get; set; }
            public string Create { get; set; }
            public string Delete { get; set; }
            public string DeleteList { get; set; }
            public string ViewCache { get; set; }
            public string ClearCache { get; set; }
        }
    }
}
