using BOL.ComponentModels.MyAccount.Profile;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
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
        [Parameter]
        public EventCallback UploadProfileImageEvent { get; set; }

        public async Task UploadProfileImage(InputFileChangeEventArgs e)
        {
            UserProfileVM.file = e.File.OpenReadStream();
            await UploadProfileImageEvent.InvokeAsync();
        }
    }
}
