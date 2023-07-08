using BOL.ComponentModels.MyAccount.Profile;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Components.MyAccount.Profile
{
    public partial class UserProfileForm
    {
        [Parameter]
        public UserProfileVM UserProfileVM { get; set; }
    }
}
