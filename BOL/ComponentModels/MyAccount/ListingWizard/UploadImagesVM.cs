using BOL.ComponentModels.Shared;
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
        public ImageDetails OwnerImageDetail { get; set; } = new ImageDetails();
        public IList<ImageDetails> OwnerImages { get; set; } = new List<ImageDetails>();

        public bool isOwnerValid()
        {
            return OwnerImage != null && !string.IsNullOrWhiteSpace(OwnerImageDetail.Designation) && 
                !string.IsNullOrWhiteSpace(OwnerImageDetail.TitleOrName);
        }

        public LWAddressVM LWAddressVM { get; set; } = new LWAddressVM();
        public ReligionsDropdownVM ReligionsDropdownVM { get; set; } = new ReligionsDropdownVM();


        public Stream GalleryImage { get; set; }
        public ImageDetails GalleryImageDetail { get; set; } = new ImageDetails();
        public IList<ImageDetails> GalleryImages { get; set; } = new List<ImageDetails>();

        public bool isGalleryValid()
        {
            return GalleryImage != null && !string.IsNullOrWhiteSpace(GalleryImageDetail.TitleOrName);
        }
    }

    public class ImageDetails
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public string Designation { get; set; }
        public string TitleOrName { get; set; }
    }
}
