using BAL.Services.Contracts;
using BOL.BANNERADS;
using DAL.Repositories.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;
using BOL.LISTING;
using BOL.ComponentModels.Pages;
using System.Linq;
using BOL.VIEWMODELS;
using System;
using BOL.ComponentModels.Listings;
using System.IO;
using BOL.LISTING.UploadImage;
using BOL.ComponentModels.MyAccount.ListingWizard;
using BOL.SHARED;

namespace BAL.Services
{
    public class ListingService : IListingService
    {
        private readonly IListingRepository _listingRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ISharedRepository _sharedRepository;
        private readonly IAuditService _auditService;
        private readonly IUserProfileRepository _userProfileRepository;
        private readonly HelperFunctions _helperFunctions;
        public ListingService(IListingRepository listingRepository, ICategoryRepository categoryRepository,
            ISharedRepository sharedRepository, IAuditService auditService, IUserProfileRepository userProfileRepository,
            HelperFunctions helperFunctions)
        {
            _listingRepository = listingRepository;
            _categoryRepository = categoryRepository;
            _sharedRepository = sharedRepository;
            _auditService = auditService;
            _userProfileRepository = userProfileRepository;
            _helperFunctions = helperFunctions;
        }

        public async Task<IList<ListingResultVM>> GetListings(string url, string level, PageVM pageVM)
        {
            IEnumerable<Categories> listCat = null;
            IList<ListingResultVM> ListingResultVMs = new List<ListingResultVM>();

            if (level == Constants.LevelFirstCategory)
            {
                var id = await _categoryRepository.GetFirstCategoryByURL(url);
                listCat = await _listingRepository.GetCategoriesByFirstCategoryId(id.FirstCategoryID);
            }
            else if (level == Constants.LevelSecondCategory)
            {
                var id = await _categoryRepository.GetSecondCategoryByURL(url);
                listCat = await _listingRepository.GetCategoriesBySecondCategoryId(id.SecondCategoryID);
            }
            else if (level == Constants.LevelThirdCategory)
            {
                var id = await _categoryRepository.GetThirdCategoryByURL(url);
                listCat = await _listingRepository.GetCategoriesByThirdCategoryId(id.ThirdCategoryID);
            }
            else if (level == Constants.LevelFourthCategory)
            {
                var id = await _categoryRepository.GetFourthCategoryByURL(url);
                listCat = await _listingRepository.GetCategoriesByFourthCategoryId(id.FourthCategoryID);
            }
            else if (level == Constants.LevelFifthCategory)
            {
                var id = await _categoryRepository.GetFifthCategoryByURL(url);
                listCat = await _listingRepository.GetCategoriesByFifthCategoryId(id.FifthCategoryID);
            }
            else if (level == Constants.LevelSixthCategory)
            {
                var id = await _categoryRepository.GetSixthCategoryByURL(url);
                listCat = await _listingRepository.GetCategoriesBySixthCategoryId(id.SixthCategoryID);
            }

            if (listCat.Count() >= pageVM.FromPageNo)
            {
                int[] listingIds = listCat.Select(x => x.ListingID).ToArray();
                var approvedlistings = await _listingRepository.GetApprovedListingsByListingIds(listingIds);
                int[] approvedListingIds = approvedlistings.Select(x => x.ListingID).ToArray();
                var addresses = await _listingRepository.GetAddressesByListingIds(approvedListingIds);
                var communications = await _listingRepository.GetCommunicationsByListingIds(approvedListingIds);

                pageVM.TotalData = approvedlistings.Count();
                approvedlistings = approvedlistings.Skip(pageVM.FromPageNo).Take(pageVM.PageSize);
                // Begin: Add result to listLrvm
                foreach (var item in approvedlistings)
                {
                    int listingId = item.ListingID;

                    var address = addresses.Where(x => x.ListingID == listingId).FirstOrDefault();
                    var assembly = await _sharedRepository.GetLocalityByLocalityId(address.AssemblyID);
                    var area = await _sharedRepository.GetAreaByAreaId(address.LocalityID);
                    var logoImage = await _listingRepository.GetLogoImageByListingId(listingId);

                    var communication = communications.Where(x => x.ListingID == listingId).FirstOrDefault();

                    var secondCatId = listCat.First().SecondCategoryID;
                    var secondCat = await _categoryRepository.GetSecondCategoryById(secondCatId);

                    var businessWorking = await _helperFunctions.IsBusinessOpen(listingId);

                    var rating = await GetRatingAsync(listingId);
                    var ratingCount = rating.Count();
                    var ratingAverage = await GetRatingAverage(listingId);

                    ListingResultVM ListingResultVM = new ListingResultVM
                    {
                        ListingId = listingId,
                        CompanyName = item.CompanyName,
                        Url = item.ListingURL,
                        //SubCategoryId = item.Categories.SecondCategoryID,
                        SubCategory = secondCat != null ? secondCat.Name : string.Empty,
                        Assembly = assembly != null ? assembly.Name : string.Empty,
                        Area = area != null ? area.Name : string.Empty,
                        Mobile = communication.Mobile,
                        Email = communication.Email,
                        BusinessYear = DateTime.Now.Year - item.YearOfEstablishment.Year,
                        BusinessWorking = businessWorking,
                        RatingAverage = ratingAverage,
                        RatingCount = ratingCount,
                        LogoImage = logoImage,
                    };

                    ListingResultVMs.Add(ListingResultVM);
                    // End: Add result to listLrvm
                }
            }
            return ListingResultVMs;
        }

        public async Task<ListingDetailVM> GetListingDetailByListingId(int listingId, string currentUserId)
        {
            ListingDetailVM listingDetailVM = new ListingDetailVM()
            {
                CurrentUserId = currentUserId,
                LogoUrl = GetListingLogoUrlByListingId(listingId),
            };
            listingDetailVM.Listing = await _listingRepository.GetApprovedListingByListingId(listingId);
            if (listingDetailVM.Listing == null)
            {
                listingDetailVM.Listing = new Listing();
                return null;
            }

            listingDetailVM.ListingId = listingId;
            listingDetailVM.LogoImage = await GetLogoImageByListingId(listingId);
            listingDetailVM.GalleryImages = await _listingRepository.GetGalleryImagesByListingId(listingId);
            listingDetailVM.Communication = await _listingRepository.GetCommunicationByListingId(listingId);

            var ownerImages = await _listingRepository.GetOwnerImagesByListingId(listingId);
            listingDetailVM.OwnerImagesVM = ownerImages.Select(x => new OwnerImageVM
            {
                CasteId = x.CasteId,
                Designation = x.Designation,
                Id = x.Id,
                ImageUrl = x.ImagePath,
                Name = x.OwnerName,
               StateId = x.StateID

            }).ToList();

            foreach(var owner in listingDetailVM.OwnerImagesVM)
            {
                var caste = await _sharedRepository.GetCasteByCasteId(owner.CasteId);
                var state = await _sharedRepository.GetStateByStateId(owner.StateId);

                if(caste != null)
                {
                    owner.Caste = caste.Name;
                    owner.Religion = caste.Religion == null ? string.Empty : caste.Religion.Name;
                }

                if (state != null)
                {
                    owner.State = state.Name;
                    owner.Country = state.Country == null ? string.Empty : state.Country.Name;
                }
            }

            listingDetailVM.BannerImageDetail = await GetBannerDetailByListingId(listingId);

            var address = await _listingRepository.GetAddressByListingId(listingId);
            if (address != null)
            {
                var country = await _sharedRepository.GetCountryByCountryId(address.CountryID);
                var state = await _sharedRepository.GetStateByStateId(address.StateID);
                var city = await _sharedRepository.GetCityByCityId(address.City);
                var assembly = await _sharedRepository.GetLocalityByLocalityId(address.AssemblyID);
                var pincode = await _sharedRepository.GetPincodeByPincodeId(address.PincodeID);
                var locality = await _sharedRepository.GetAreaByAreaId(address.LocalityID);

                listingDetailVM.Address.LocalAddress = address.LocalAddress;
                listingDetailVM.Address.Country = country.Name;
                listingDetailVM.Address.State = state.Name;
                listingDetailVM.Address.City = city.Name;
                listingDetailVM.Address.Assembly = assembly.Name;
                listingDetailVM.Address.Pincode = pincode.PincodeNumber;
                listingDetailVM.Address.Locality = locality.Name;
            }

            listingDetailVM.BusinessWorkingHour = await _helperFunctions.IsBusinessOpen(listingId);

            var category = await _listingRepository.GetCategoryByListingId(listingId);

            var firstCategory = await _categoryRepository.GetFirstCategoryById(category.FirstCategoryID);
            var secondCategory = await _categoryRepository.GetSecondCategoryById(category.SecondCategoryID);
            listingDetailVM.FirstCategory = firstCategory.Name;
            listingDetailVM.SecondCategory = secondCategory.Name;

            listingDetailVM.Specialisation = await _listingRepository.GetSpecialisationByListingId(listingId);
            listingDetailVM.PaymentMode = await _listingRepository.GetPaymentModeByListingId(listingId);
            listingDetailVM.WorkingHour = await _listingRepository.GetWorkingHoursByListingId(listingId);

            var rating = await GetRatingAsync(listingId);
            listingDetailVM.RatingCount = rating.Count();
            listingDetailVM.RatingAverage = await GetRatingAverage(listingId);

            if (!string.IsNullOrWhiteSpace(currentUserId))
            {
                listingDetailVM.IsBookmarked = await _auditService.CheckIfUserBookmarkedListing(listingId, currentUserId); ;
                listingDetailVM.IsSubscribe = await _auditService.CheckIfUserSubscribedToListing(listingId, currentUserId);
                listingDetailVM.IsLiked = await _auditService.CheckIfUserLikedListing(listingId, currentUserId); ;
                listingDetailVM.CurrentUserRating = await _listingRepository.GetRatingByListingIdAndOwnerId(listingId, currentUserId);
            }

            listingDetailVM.listReviews = await GetReviewsAsync(listingId);

            return listingDetailVM;
        }

        public string GetListingLogoUrlByListingId(int listingId)
        {
            string logoUrl = "/FileManager/ListingLogo/" + listingId + ".jpg";
            var file = Constants.WebRoot + logoUrl.Replace("/", "\\");

            return File.Exists(file) ? logoUrl : string.Empty;
        }

        public async Task<IList<ReviewListingViewModel>> GetReviewsAsync(int listingId)
        {
            IList<ReviewListingViewModel> listReviews = new List<ReviewListingViewModel>();
            var listingAllReviews = await GetRatingsByListingId(listingId);

            foreach (var i in listingAllReviews)
            {
                var profile = await _userProfileRepository.GetProfileByOwnerGuid(i.OwnerGuid);
                var reviewListingViewModel = new ReviewListingViewModel
                {
                    ReviewID = i.RatingID,
                    OwnerGuid = i.OwnerGuid,
                    Comment = i.Comment,
                    Date = i.Date,
                    VisitTime = i.Time.ToString(),
                    Ratings = i.Ratings
                };

                if (profile != null)
                {
                    reviewListingViewModel.Name = profile.Name;
                    reviewListingViewModel.UserImage = string.IsNullOrWhiteSpace(profile.ImageUrl) ? "resources/img/icon/profile.svg" : profile.ImageUrl;
                }

                listReviews.Add(reviewListingViewModel);
            }
            return listReviews;
        }

        public async Task<decimal> GetRatingAverage(int ListingID)
        {
            var ratings = await _listingRepository.GetRatingsByListingId(ListingID);

            if (ratings.Count() <= 0)
                return 0;

            decimal totalRatings = ratings.Sum(x => x.Ratings);
            return totalRatings / ratings.Count();
        }

        public async Task<int> CountRatingAsync(int ListingID, int rating)
        {
            return await _listingRepository.CountRatingAsync(ListingID, rating);
        }

        public async Task<IEnumerable<Rating>> GetRatingAsync(int ListingID)
        {
            return await _listingRepository.GetRatingsByListingId(ListingID);
        }

        public async Task<Listing> GetListingByOwnerId(string ownerId)
        {
            return await _listingRepository.GetListingByOwnerId(ownerId);
        }

        public async Task UpdateListingStepByOwnerId(string ownerId, int currentPageStep)
        {
            var listing = await GetListingByOwnerId(ownerId);
            if (listing.Steps < currentPageStep)
            {
                listing.Steps = currentPageStep;
                await UpdateAsync(listing);
            }
        }

        public async Task<Categories> GetCategoryByListingId(int listingId)
        {
            return await _listingRepository.GetCategoryByListingId(listingId);
        }


        public async Task<Communication> GetCommunicationByListingId(int listingId)
        {
            return await _listingRepository.GetCommunicationByListingId(listingId);
        }

        public async Task<PaymentMode> GetPaymentModeByListingId(int listingId)
        {
            return await _listingRepository.GetPaymentModeByListingId(listingId);
        }

        public async Task<WorkingHours> GetWorkingHoursByListingId(int listingId)
        {
            return await _listingRepository.GetWorkingHoursByListingId(listingId);
        }

        public async Task<Address> GetAddressByListingId(int listingId)
        {
            return await _listingRepository.GetAddressByListingId(listingId);
        }

        public async Task<Specialisation> GetSpecialisationByListingId(int listingId)
        {
            return await _listingRepository.GetSpecialisationByListingId(listingId);
        }

        public async Task<IEnumerable<Rating>> GetRatingsByListingId(int listingId)
        {
            return await _listingRepository.GetRatingsByListingId(listingId);
        }

        public async Task<Rating> GetRatingByListingIdAndOwnerId(int listingId, string ownerId)
        {
            return await _listingRepository.GetRatingByListingIdAndOwnerId(listingId, ownerId);
        }

        public async Task AddAsync(object data)
        {
            await _listingRepository.AddAsync(data);
        }

        public async Task UpdateAsync(object data)
        {
            await _listingRepository.UpdateAsync(data);
        }

        public async Task<IList<SearchResultViewModel>> GetSearchListings()
        {
            var listings = await _listingRepository.GetApprovedListings();
            return listings.Select((x) => new SearchResultViewModel
            {
                label = x.CompanyName,
                value = Convert.ToString(x.ListingID)
            }).ToList();
        }

        #region Social Network
        public async Task<SocialNetwork> GetSocialNetworkByListingId(int listingId)
        {
            return await _listingRepository.GetSocialNetworkByListingId(listingId);
        }
        #endregion

        #region Keyword
        public async Task<IList<SearchResultViewModel>> GetKeywords()
        {
            var keywords = await _listingRepository.GetKeywords();
            return keywords.Select((x) => new SearchResultViewModel
            {
                label = x.SeoKeyword,
                value = x.SeoKeyword
            }).Distinct().ToList();
        }
        public async Task<List<BOL.LISTING.Keyword>> GetKeywordsByListingId(int listingId)
        {
            return await _listingRepository.GetKeywordsByListingId(listingId);
        }

        public async Task<IList<BOL.LISTING.Keyword>> AddKeywordsAsync(IList<BOL.LISTING.Keyword> keywords)
        {
            return await _listingRepository.AddKeywordsAsync(keywords);
        }

        public async Task DeleteKeywordsByListingId(IList<BOL.LISTING.Keyword> keywords)
        {
            await _listingRepository.DeleteKeywordsByListingId(keywords);
        }
        #endregion

        #region Banner
        public async Task<IndexVM> GetHomeBannerList()
        {
            var HomeBannerList = await _listingRepository.GetHomeBannerList();
            IndexVM indexVM = new IndexVM()
            {
                HomeBannerTop = HomeBannerList.Where(i => i.Placement == "HomeTop").ToList(),
                HomeBannerMiddle1 = HomeBannerList.Where(i => i.Placement == "HomeMiddle1").ToList(),
                HomeBannerMiddle2 = HomeBannerList.Where(i => i.Placement == "HomeMiddle2").ToList(),
                HomeBannerBottom = HomeBannerList.Where(i => i.Placement == "HomeBottom").ToList()
            };
            return indexVM;
        }

        public async Task<ListingResultBannerVM> GetListingResultBannersByUrl(string url)
        {
            var thirdCategory = await _categoryRepository.GetThirdCategoryByURL(url);
            var CategoryBanners = await _listingRepository.GetCategoryBannersByThirtCategoryId(thirdCategory.ThirdCategoryID);

            ListingResultBannerVM indexVM = new ListingResultBannerVM()
            {
                CategoryBannersTop = CategoryBanners.Where(i => i.Placement == "banner-1").ToList(),
                CategoryBannersLeftTop = CategoryBanners.Where(i => i.Placement == "banner-2").ToList(),
                CategoryBannersLeftBottom = CategoryBanners.Where(i => i.Placement == "banner-3").ToList()
            };
            return indexVM;
        }
        #endregion

        #region Upload Images
        #region logo
        public async Task<LogoImage> GetLogoImageByListingId(int listingId)
        {
            return await _listingRepository.GetLogoImageByListingId(listingId);
        }
        public async Task<bool> AddOrUpdateLogoImage(UploadImagesVM uploadImagesVM)
        {
            try
            {
                var logoImage = await _listingRepository.GetLogoImageByListingId(uploadImagesVM.ListingId);
                bool recordNotFound = logoImage == null;

                if (recordNotFound)
                    logoImage = new LogoImage();

                logoImage.ImagePath = uploadImagesVM.LogoImageUrl;

                if (recordNotFound)
                {
                    logoImage.ListingID = uploadImagesVM.ListingId;
                    logoImage.OwnerGuid = uploadImagesVM.OwnerId;
                    logoImage.CreatedDate = DateTime.Now;

                    await _listingRepository.AddAsync(logoImage);
                }
                else
                {
                    logoImage.UpdateDate = DateTime.Now;
                    await _listingRepository.UpdateAsync(logoImage);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region owner
        public async Task<IList<ImageDetails>> GetOwnerImagesByListingId(int listingId)
        {
            var ownerImages = await _listingRepository.GetOwnerImagesByListingId(listingId);

            return ownerImages.Select(x => new ImageDetails
            {
                Id = x.Id,
                Designation = x.Designation,
                ImageUrl = x.ImagePath,
                TitleOrName = x.OwnerName
            }).ToList();
        }

        public async Task<bool> AddOwnerImage(UploadImagesVM uploadImagesVM)
        {
            try
            {
                var ownerImage = new OwnerImage
                {
                    ListingID = uploadImagesVM.ListingId,
                    OwnerGuid = uploadImagesVM.OwnerId,
                    CreatedDate = DateTime.Now,
                    ImagePath = uploadImagesVM.OwnerImageDetail.ImageUrl,
                    Designation = uploadImagesVM.OwnerImageDetail.Designation,
                    OwnerName = uploadImagesVM.OwnerImageDetail.TitleOrName,
                    CountryID = uploadImagesVM.LWAddressVM.CountryId,
                    StateID = uploadImagesVM.LWAddressVM.StateId,
                    ReligionId = uploadImagesVM.ReligionsDropdownVM.SelectedReligionId,
                    CasteId = uploadImagesVM.ReligionsDropdownVM.SelectedCasteId,
                };
                await _listingRepository.AddAsync(ownerImage);

                ownerImage.ImagePath += $"{ownerImage.Id}.jpg";
                await _listingRepository.UpdateAsync(ownerImage);

                uploadImagesVM.OwnerImageDetail.ImageUrl = ownerImage.ImagePath;
                uploadImagesVM.OwnerImageDetail.Id = ownerImage.Id;
                uploadImagesVM.OwnerImages.Add(uploadImagesVM.OwnerImageDetail);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteOwnerImage(int id)
        {
            try
            {
                await _listingRepository.DeleteOwnerImage(id);
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region gallery
        public async Task<IList<ImageDetails>> GetGalleryImagesByListingId(int listingId)
        {
            var ownerImages = await _listingRepository.GetGalleryImagesByListingId(listingId);

            return ownerImages.Select(x => new ImageDetails
            {
                Id = x.Id,
                ImageUrl = x.ImagePath,
                TitleOrName = x.ImageTitle
            }).ToList();
        }

        public async Task<bool> AddGalleryImage(UploadImagesVM uploadImagesVM)
        {
            try
            {
                var galleryImage = new GalleryImage
                {
                    ListingID = uploadImagesVM.ListingId,
                    OwnerGuid = uploadImagesVM.OwnerId,
                    CreatedDate = DateTime.Now,
                    ImagePath = uploadImagesVM.GalleryImageDetail.ImageUrl,
                    ImageTitle = uploadImagesVM.GalleryImageDetail.TitleOrName,
                };
                await _listingRepository.AddAsync(galleryImage);

                galleryImage.ImagePath += $"{galleryImage.Id}.jpg";
                await _listingRepository.UpdateAsync(galleryImage);

                uploadImagesVM.GalleryImageDetail.ImageUrl = galleryImage.ImagePath;
                uploadImagesVM.GalleryImageDetail.Id = galleryImage.Id;
                uploadImagesVM.GalleryImages.Add(uploadImagesVM.GalleryImageDetail);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteGalleryImage(int id)
        {
            try
            {
                await _listingRepository.DeleteGalleryImage(id);
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region Banner
        public async Task<BannerDetail> GetBannerDetailByListingId(int listingId)
        {
            return await _listingRepository.GetBannerDetailByListingId(listingId);
        }
        public async Task<BannerDetail> AddOrUpdateBannerImage(UploadImagesVM uploadImagesVM)
        {
            try
            {
                var bannerDetail = await _listingRepository.GetBannerDetailByListingId(uploadImagesVM.ListingId);
                bool recordNotFound = bannerDetail == null;

                if (recordNotFound)
                    bannerDetail = new BannerDetail();

                bannerDetail.ImagePath = uploadImagesVM.BannerImageDet.ImageUrl;
                bannerDetail.ImageTitle = uploadImagesVM.BannerImageDet.TitleOrName;

                if (recordNotFound)
                {
                    bannerDetail.ListingID = uploadImagesVM.ListingId;
                    bannerDetail.OwnerGuid = uploadImagesVM.OwnerId;
                    bannerDetail.CreatedDate = DateTime.Now;

                    await _listingRepository.AddAsync(bannerDetail);
                }
                else
                {
                    bannerDetail.UpdateDate = DateTime.Now;
                    await _listingRepository.UpdateAsync(bannerDetail);
                }
                bannerDetail.ImagePath += "?DummyId=" + DateTime.Now.Ticks;
                return bannerDetail;
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region certificate
        public async Task<IList<ImageDetails>> GetCertificateDetailsByListingId(int listingId)
        {
            var certificate = await _listingRepository.GetCertificationDetailsByListingId(listingId);

            return certificate.Select(x => new ImageDetails
            {
                Id = x.Id,
                ImageUrl = x.ImagePath,
                TitleOrName = x.ImageTitle
            }).ToList();
        }

        public async Task<bool> AddCertificateDetail(UploadImagesVM uploadImagesVM)
        {
            try
            {
                var certificate = new CertificationDetail
                {
                    ListingID = uploadImagesVM.ListingId,
                    OwnerGuid = uploadImagesVM.OwnerId,
                    CreatedDate = DateTime.Now,
                    ImagePath = uploadImagesVM.CertificateImageDetail.ImageUrl,
                    ImageTitle = uploadImagesVM.CertificateImageDetail.TitleOrName,
                };
                await _listingRepository.AddAsync(certificate);

                certificate.ImagePath += $"{certificate.Id}.jpg";
                await _listingRepository.UpdateAsync(certificate);

                uploadImagesVM.CertificateImageDetail.ImageUrl = certificate.ImagePath;
                uploadImagesVM.CertificateImageDetail.Id = certificate.Id;
                uploadImagesVM.CertificateImages.Add(uploadImagesVM.CertificateImageDetail);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteCertificateDetail(int id)
        {
            try
            {
                await _listingRepository.DeleteCertificationDetail(id);
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region client
        public async Task<IList<ImageDetails>> GetClientDetailsByListingId(int listingId)
        {
            var client = await _listingRepository.GetClientDetailsByListingId(listingId);

            return client.Select(x => new ImageDetails
            {
                Id = x.Id,
                ImageUrl = x.ImagePath,
                TitleOrName = x.ImageTitle
            }).ToList();
        }

        public async Task<bool> AddClientDetail(UploadImagesVM uploadImagesVM)
        {
            try
            {
                var client = new ClientDetail
                {
                    ListingID = uploadImagesVM.ListingId,
                    OwnerGuid = uploadImagesVM.OwnerId,
                    CreatedDate = DateTime.Now,
                    ImagePath = uploadImagesVM.ClientImageDetail.ImageUrl,
                    ImageTitle = uploadImagesVM.ClientImageDetail.TitleOrName,
                };
                await _listingRepository.AddAsync(client);

                client.ImagePath += $"{client.Id}.jpg";
                await _listingRepository.UpdateAsync(client);

                uploadImagesVM.ClientImageDetail.ImageUrl = client.ImagePath;
                uploadImagesVM.ClientImageDetail.Id = client.Id;
                uploadImagesVM.ClientImages.Add(uploadImagesVM.ClientImageDetail);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteClientDetail(int id)
        {
            try
            {
                await _listingRepository.DeleteClientDetail(id);
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion
        #endregion
    }
}
