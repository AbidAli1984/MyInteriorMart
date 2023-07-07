using BOL.ComponentModels.MyAccount.Profile;
using Microsoft.AspNetCore.Components;

namespace Components.MyAccount.Profile
{
    public partial class EditProfileForm
    {
        [Parameter]
        public EditProfileVM EditProfile { get; set; }
    }
}
