using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BOL.ComponentModels.MyAccount.ListingWizard
{
    public class UploadImages
    {
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

        public Stream OwnerImage { get; set; }
        public Stream GalleryImage { get; set; }
    }
}
