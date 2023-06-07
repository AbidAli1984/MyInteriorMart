﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BOL.IDENTITY.ViewModels
{
    public class ShowUsersByRoleViewModel
    {
        public ShowUsersByRoleViewModel()
        {
            UserName = new List<string>();
        }

        public string RoleId { get; set; }

        public string RoleName { get; set; }

        public List<string> UserName { get; set; }
    }
}
