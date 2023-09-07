using AntDesign;
using BAL;
using BAL.Services.Contracts;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.IO;
using System.Threading.Tasks;

namespace FRONTEND.BLAZOR.MyAccount.Profile
{
    public partial class Complaint
    {
        [Inject] public IAuditService auditService { get; set; }
        [Inject] public IUserService userService { get; set; }
        [Inject] public IUserProfileService userProfileService { get; set; }
        [Inject] AuthenticationStateProvider authenticationState { get; set; }
        [Inject] NotificationService _notification { get; set; }
        [Inject] Helper helper { get; set; }
        [Inject] HelperFunctions helperFunction { get; set; }

        public BOL.AUDITTRAIL.Complaint complaint { get; set; } = new BOL.AUDITTRAIL.Complaint();

        private string _inputFileId = Guid.NewGuid().ToString();
        public Stream ComplaintImage { get; set; }
        public bool buttonBusy { get; set; }
        public bool isVendor { get; set; } = false;

        protected async override Task OnInitializedAsync()
        {
            try
            {
                var authstate = await authenticationState.GetAuthenticationStateAsync();
                var user = authstate.User;
                if (user.Identity.IsAuthenticated)
                {
                    var applicationUser = await userService.GetUserByUserName(user.Identity.Name);
                    var userProfile = await userProfileService.GetProfileByOwnerGuid(applicationUser.Id);
                    isVendor = applicationUser.IsVendor;

                    complaint.OwnerGuid = applicationUser.Id;
                    complaint.Mobile = applicationUser.PhoneNumber;
                    complaint.Email = applicationUser.Email;

                    if (userProfile != null)
                        complaint.Name = userProfile.Name;
                }
            }
            catch (Exception exc)
            {

            }
        }

        public void SetComplaintImage(InputFileChangeEventArgs e)
        {
            ComplaintImage = e.File.OpenReadStream();
        }

        private async Task AddComplaint()
        {
            if (string.IsNullOrEmpty(complaint.Title) || string.IsNullOrEmpty(complaint.Description) || ComplaintImage == null)
            {
                helper.ShowNotification(_notification, NotificationType.Info, NotificationPlacement.BottomRight, "Information", "All fields are compulsory.");
                return;
            }

            try
            {
                buttonBusy = true;
                complaint.Date = DateTime.Now;
                complaint.ImagePath = await helperFunction.UploadComplaintImage(ComplaintImage, complaint.OwnerGuid);
                await auditService.AddAsync(complaint);
                ResetComplaint();
                helper.ShowNotification(_notification, NotificationType.Success, NotificationPlacement.BottomRight, "Success", "Your complaint submitted successfully!");
            }
            catch (Exception exc)
            {
                helper.ShowNotification(_notification, NotificationType.Error, NotificationPlacement.BottomRight, "Error", exc.Message);
            }
            finally
            {
                buttonBusy = false;
            }
        }

        private void ResetComplaint()
        {
            complaint.Id = 0;
            complaint.Title = string.Empty;
            complaint.Description = string.Empty;
            ComplaintImage = null;
            _inputFileId = Guid.NewGuid().ToString();
        }
    }
}
