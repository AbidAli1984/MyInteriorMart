using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BAL.Services.Contracts;
using BOL.LABOURNAKA;
using DAL.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FRONTEND.BLAZOR.MyAccount.LabourNakaWizard.Create
{
    public partial class DocumentEdit
    {
        [Inject]
        private IHttpContextAccessor httpConAccess { get; set; }
        [Inject]
        public IWebHostEnvironment hostEnv { get; set; }
        [Inject]
        public IUserService userService { get; set; }

        public string CurrentUserGuid { get; set; }
        public string ErrorMessage { get; set; }
        public bool userAuthenticated { get; set; } = false;
        public string IpAddress { get; set; }
        public ApplicationUser iUser { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime CreatedTime { get; set; }
        public bool Busy { get; set; }

        // Begin: Check Record Exist
        public bool Disable { get; set; } = true;
        public bool Edit { get; set; }

        public Document Doc = new Document();
        public async Task CheckRecordExistAsync()
        {
            // Get Existing Record
            Doc = await listingContext.Document
                .Where(i => i.UserGuid == CurrentUserGuid)
                .FirstOrDefaultAsync();
        }
        // End: Check Record Exist

        // Begin: Edit Record
        public void ToggleEdit()
        {
            Disable = !Disable;
            Edit = !Edit;
        }
        // Begin: Edit Record

        // Begin: Save Record
        public async Task SaveRecordAsync()
        {
            try
            {
                listingContext.Update(Doc);
                await listingContext.SaveChangesAsync();

                // Change Disable Value
                Disable = true;

                // Change Edit Value
                Edit = false;

                // Redirect to Page
                navManager.NavigateTo("/MyAccount/LabourNakaWizard/CategoryCreate");
            }
            catch (Exception exc)
            {
                ErrorMessage = exc.InnerException.ToString();
                Disable = false;
            }
        }
        // Begin: Save Record

        // Begin: Upload Aadhar Card
        public async Task UploadAadharCardAsync(InputFileChangeEventArgs e)
        {
            try
            {
                var docId = Doc.DocumentId;

                var dir = hostEnv.ContentRootPath + "\\FileManager\\LabourNaka\\" + docId + "\\";

                // If directory does not exists then create a new directory
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }

                // get final path with file type
                var fileType = e.File.ContentType;
                var extension = fileType.Substring(fileType.LastIndexOf('/') + 1);

                var fileName = "aadharcard." + extension;
                var filePath = dir + fileName;

                

                // Delete exisiting file if any
                if (File.Exists(filePath) == true)
                {
                    File.Delete(filePath);
                }

                
                await using FileStream fs = new(filePath, FileMode.Create);
                await e.File.OpenReadStream().CopyToAsync(fs);
                
            }
            catch (Exception exc)
            {
                ErrorMessage = exc.Message;
            }
            await Task.Delay(10);
        }
        // End: Upload Aadhar Card

        // Begin: Upload Pan Card
        public async Task UploadPanCardAsync(InputFileChangeEventArgs e)
        {
            try
            {
                var docId = Doc.DocumentId;

                var dir = hostEnv.ContentRootPath + "\\FileManager\\LabourNaka\\" + docId + "\\";

                // If directory does not exists then create a new directory
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }

                // get final path with file type
                var fileType = e.File.ContentType;
                var extension = fileType.Substring(fileType.LastIndexOf('/') + 1);

                var fileName = "pancard." + extension;
                var filePath = dir + fileName;



                // Delete exisiting file if any
                if (File.Exists(filePath) == true)
                {
                    File.Delete(filePath);
                }


                await using FileStream fs = new(filePath, FileMode.Create);
                await e.File.OpenReadStream().CopyToAsync(fs);

            }
            catch (Exception exc)
            {
                ErrorMessage = exc.Message;
            }
            await Task.Delay(10);
        }
        // End: Upload Pan Card

        // Begin: Upload Driving License
        public async Task UploadDrivingLicensedAsync(InputFileChangeEventArgs e)
        {
            try
            {
                var docId = Doc.DocumentId;

                var dir = hostEnv.ContentRootPath + "\\FileManager\\LabourNaka\\" + docId + "\\";

                // If directory does not exists then create a new directory
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }

                // get final path with file type
                var fileType = e.File.ContentType;
                var extension = fileType.Substring(fileType.LastIndexOf('/') + 1);

                var fileName = "drivinglicense." + extension;
                var filePath = dir + fileName;



                // Delete exisiting file if any
                if (File.Exists(filePath) == true)
                {
                    File.Delete(filePath);
                }


                await using FileStream fs = new(filePath, FileMode.Create);
                await e.File.OpenReadStream().CopyToAsync(fs);

            }
            catch (Exception exc)
            {
                ErrorMessage = exc.Message;
            }
            await Task.Delay(10);
        }
        // End: Upload Driving License

        // Begin: Upload Voter ID
        public async Task UploadVoterIDAsync(InputFileChangeEventArgs e)
        {
            try
            {
                var docId = Doc.DocumentId;

                var dir = hostEnv.ContentRootPath + "\\FileManager\\LabourNaka\\" + docId + "\\";

                // If directory does not exists then create a new directory
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }

                // get final path with file type
                var fileType = e.File.ContentType;
                var extension = fileType.Substring(fileType.LastIndexOf('/') + 1);

                var fileName = "voterid." + extension;
                var filePath = dir + fileName;



                // Delete exisiting file if any
                if (File.Exists(filePath) == true)
                {
                    File.Delete(filePath);
                }


                await using FileStream fs = new(filePath, FileMode.Create);
                await e.File.OpenReadStream().CopyToAsync(fs);

            }
            catch (Exception exc)
            {
                ErrorMessage = exc.Message;
            }
            await Task.Delay(10);
        }
        // End: Upload Voter ID

        // Begin: Upload Electricity Bill
        public async Task UploadElectricityBillAsync(InputFileChangeEventArgs e)
        {
            try
            {
                var docId = Doc.DocumentId;

                var dir = hostEnv.ContentRootPath + "\\FileManager\\LabourNaka\\" + docId + "\\";

                // If directory does not exists then create a new directory
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }

                // get final path with file type
                var fileType = e.File.ContentType;
                var extension = fileType.Substring(fileType.LastIndexOf('/') + 1);

                var fileName = "electricitybill." + extension;
                var filePath = dir + fileName;



                // Delete exisiting file if any
                if (File.Exists(filePath) == true)
                {
                    File.Delete(filePath);
                }


                await using FileStream fs = new(filePath, FileMode.Create);
                await e.File.OpenReadStream().CopyToAsync(fs);

            }
            catch (Exception exc)
            {
                ErrorMessage = exc.Message;
            }
            await Task.Delay(10);
        }
        // End: Upload Electricity Bill

        protected async override Task OnInitializedAsync()
        {
            try
            {
                // Get User Name
                var authstate = await authenticationState.GetAuthenticationStateAsync();
                var user = authstate.User;
                if (user.Identity.IsAuthenticated)
                {
                    // Shafi: Assign Time Zone to CreatedDate & Created Time
                    DateTime timeZoneDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
                    IpAddress = httpConAccess.HttpContext.Connection.RemoteIpAddress.ToString();
                    CreatedDate = timeZoneDate;
                    CreatedTime = timeZoneDate;
                    // End:

                    iUser = await userService.GetUserByUserNameOrEmail(user.Identity.Name);
                    CurrentUserGuid = iUser.Id;
                }

                // Execute Method
                await CheckRecordExistAsync();
            }
            catch (Exception exc)
            {
                ErrorMessage = exc.Message;
            }
        }
    }
}