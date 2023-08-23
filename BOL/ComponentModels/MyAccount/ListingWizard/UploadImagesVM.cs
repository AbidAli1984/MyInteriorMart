using BOL.ComponentModels.Shared;
using BOL.LISTING.UploadImage;
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
        public LWAddressVM LWAddressVM { get; set; } = new LWAddressVM();
        public ReligionsDropdownVM ReligionsDropdownVM { get; set; } = new ReligionsDropdownVM();

        #region Logo
        public Stream LogoImage { get; set; }
        private string logoImgUrl { get; set; }
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
        #endregion

        #region Owner
        public Stream OwnerImage { get; set; }
        public ImageDetails OwnerImageDetail { get; set; } = new ImageDetails();
        public IList<ImageDetails> OwnerImages { get; set; } = new List<ImageDetails>();

        public bool isOwnerValid()
        {
            return OwnerImage != null && !string.IsNullOrWhiteSpace(OwnerImageDetail.Designation) && 
                !string.IsNullOrWhiteSpace(OwnerImageDetail.TitleOrName);
        }
        #endregion

        #region Gallery
        public Stream GalleryImage { get; set; }
        public ImageDetails GalleryImageDetail { get; set; } = new ImageDetails();
        public IList<ImageDetails> GalleryImages { get; set; } = new List<ImageDetails>();

        public bool isGalleryValid()
        {
            return GalleryImage != null && !string.IsNullOrWhiteSpace(GalleryImageDetail.TitleOrName);
        }
        #endregion

        #region Banner
        public Stream BannerImage { get; set; }
        public ImageDetails BannerImageDet { get; set; } = new ImageDetails();
        public BannerDetail BannerImageDetail { get; set; } = new BannerDetail();

        public bool isBannerValid()
        {
            return BannerImage != null && !string.IsNullOrWhiteSpace(BannerImageDet.TitleOrName);
        }
        #endregion

        #region Certificate
        public Stream CertificateImage { get; set; }
        public ImageDetails CertificateImageDetail { get; set; } = new ImageDetails();
        public IList<ImageDetails> CertificateImages { get; set; } = new List<ImageDetails>();

        public bool isCertificateValid()
        {
            return CertificateImage != null && !string.IsNullOrWhiteSpace(CertificateImageDetail.TitleOrName);
        }
        #endregion

        #region Client
        public Stream ClientImage { get; set; }
        public ImageDetails ClientImageDetail { get; set; } = new ImageDetails();
        public IList<ImageDetails> ClientImages { get; set; } = new List<ImageDetails>();

        public bool isClientValid()
        {
            return ClientImage != null && !string.IsNullOrWhiteSpace(ClientImageDetail.TitleOrName);
        }
        #endregion
    }

    public class ImageDetails
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public string Designation { get; set; }
        public string TitleOrName { get; set; }
    }
}
