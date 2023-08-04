using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BOL.ComponentModels.MyAccount.ListingWizard
{
    public class UploadImagesVM
    {
        public string OwnerId { get; set; }
        public int ListingId { get; set; }
        public Stream LogoImage { get; set; }
        private string logoImgUrl;
        public string LogoImageUrl
        {
            get
            {
                return string.IsNullOrEmpty(logoImgUrl) ? "/resources/img/furniture-design1.jpg" : (logoImgUrl + "?DummyId=" + DateTime.Now.Ticks);
            }
            set { logoImgUrl = value; }
        }

        public bool isLogoValid()
        {
            return LogoImage != null;
        }

        public Stream OwnerImage { get; set; }
        public Stream GalleryImage { get; set; }
    }
}
