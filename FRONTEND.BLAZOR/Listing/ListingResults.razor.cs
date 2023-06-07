using BOL.LISTING;
using BOL.VIEWMODELS;
using FRONTEND.BLAZOR.Models.ViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FRONTEND.BLAZOR.Listing
{
    public partial class ListingResults
    {
        [Parameter]
        public string url { get; set; }

        [Parameter]
        public string level { get; set; }

        public IList<ListingResultVM> listLrvm = new List<ListingResultVM>();

        public IList<FreeListingViewModel> listFLVM = new List<FreeListingViewModel>();

        // Begin: Method for Rating
        public async Task<bool> ReviewExistsAsync(int ListingID, string OwnerGuid)
        {
            var result = await listingContext.Rating.Where(r => r.ListingID == ListingID && r.OwnerGuid == OwnerGuid).FirstOrDefaultAsync();

            if (result != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<IEnumerable<Rating>> GetRatingAsync(int ListingID)
        {
            var ratings = await listingContext.Rating.Where(r => r.ListingID == ListingID).ToListAsync();
            return ratings;
        }

        public async Task<Rating> RatingDetailsAsync(int ListingID)
        {
            var rating = await listingContext.Rating.Where(r => r.ListingID == ListingID).FirstOrDefaultAsync();
            return rating;
        }

        public async Task<string> RatingAverageAsync(int ListingID)
        {
            // Shafi: Get rating average
            var allRating = await listingContext.Rating.Where(r => r.ListingID == ListingID).ToListAsync();
            var ratingCount = allRating.Count();

            if (ratingCount > 0)
            {
                var R1 = await CountRatingAsync(ListingID, 1);
                var R2 = await CountRatingAsync(ListingID, 2);
                var R3 = await CountRatingAsync(ListingID, 3);
                var R4 = await CountRatingAsync(ListingID, 4);
                var R5 = await CountRatingAsync(ListingID, 5);

                decimal averageCount = 5 * R5 + 4 * R4 + 3 * R3 + 2 * R2 + 1 * R1;
                decimal weightedCount = R5 + R4 + R3 + R2 + R1;
                decimal ratingAverage = averageCount / weightedCount;

                return ratingAverage.ToString("0.0");
            }
            else
            {
                return "0";
            }
            // End:
        }

        public async Task<int> CountRatingAsync(int ListingID, int rating)
        {
            var count = await listingContext.Rating.Where(r => r.ListingID == ListingID && r.Ratings == rating).CountAsync();
            return count;
        }

        public async Task<int> CountListingReviewsAsync(int ListingID)
        {
            var count = await listingContext.Rating.Where(r => r.ListingID == ListingID).CountAsync();
            return count;
        }
        // End: Method for Rating

        // Shafi: Check if business is open or close
        public async Task<string> BusinessOpenOrCloseAsync(int ListingID)
        {
            // Shafi: Begin: Get timing of listing
            var listingTiming = await listingContext.WorkingHours.Where(w => w.ListingID == ListingID).FirstOrDefaultAsync();
            // End:

            // Shafi: Get Time Zone
            DateTime timeZoneDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
            string day = timeZoneDate.ToString("dddd");
            string time = timeZoneDate.ToString("hh:mm tt");
            DateTime currentTime = DateTime.Parse(time, System.Globalization.CultureInfo.CurrentCulture);
            // End:

            if(listingTiming != null)
            {
                // Begin: Business Open Now & Close
                if (day == "Monday")
                {
                    // Shafi: Get ToTime in ("hh:mm tt") format then convert it to string then create date time from it
                    string timeString = listingTiming.MondayTo.ToString("hh:mm tt");
                    DateTime ToTime = DateTime.Parse(timeString, System.Globalization.CultureInfo.CurrentCulture);
                    // End:

                    if (currentTime > ToTime)
                    {
                        return "Closed Now";
                    }
                    else
                    {
                        return "Open Now";
                    }
                }
                else if (day == "Tuesday")
                {
                    // Shafi: Get ToTime in ("hh:mm tt") format then convert it to string then create date time from it
                    string timeString = listingTiming.TuesdayTo.ToString("hh:mm tt");
                    DateTime ToTime = DateTime.Parse(timeString, System.Globalization.CultureInfo.CurrentCulture);
                    // End:

                    if (currentTime > ToTime)
                    {
                        return "Closed Now";
                    }
                    else
                    {
                        return "Open Now";
                    }
                }
                else if (day == "Wednesday")
                {
                    // Shafi: Get ToTime in ("hh:mm tt") format then convert it to string then create date time from it
                    string timeString = listingTiming.WednesdayTo.ToString("hh:mm tt");
                    DateTime ToTime = DateTime.Parse(timeString, System.Globalization.CultureInfo.CurrentCulture);
                    // End:

                    if (currentTime > ToTime)
                    {
                        return "Closed Now";
                    }
                    else
                    {
                        return "Open Now";
                    }
                }
                else if (day == "Thursday")
                {
                    // Shafi: Get ToTime in ("hh:mm tt") format then convert it to string then create date time from it
                    string timeString = listingTiming.ThursdayTo.ToString("hh:mm tt");
                    DateTime ToTime = DateTime.Parse(timeString, System.Globalization.CultureInfo.CurrentCulture);
                    // End:

                    if (currentTime > ToTime)
                    {
                        return "Closed Now";
                    }
                    else
                    {
                        return "Open Now";
                    }
                }
                else if (day == "Friday")
                {
                    // Shafi: Get ToTime in ("hh:mm tt") format then convert it to string then create date time from it
                    string timeString = listingTiming.FridayTo.ToString("hh:mm tt");
                    DateTime ToTime = DateTime.Parse(timeString, System.Globalization.CultureInfo.CurrentCulture);
                    // End:

                    if (currentTime > ToTime)
                    {
                        return "Closed Now";
                    }
                    else
                    {
                        return "Open Now";
                    }
                }
                else if (day == "Saturday" && listingTiming.SaturdayHoliday == false)
                {
                    // Shafi: Get ToTime in ("hh:mm tt") format then convert it to string then create date time from it
                    string timeString = listingTiming.SaturdayTo.ToString("hh:mm tt");
                    DateTime ToTime = DateTime.Parse(timeString, System.Globalization.CultureInfo.CurrentCulture);
                    // End:

                    if (currentTime > ToTime)
                    {
                        return "Closed Now";
                    }
                    else
                    {
                        return "Open Now";
                    }
                }
                else if (day == "Saturday" && listingTiming.SaturdayHoliday != true)
                {
                    return "Closed Now";
                }
                else if (day == "Sunday" && listingTiming.SundayHoliday == false)
                {
                    // Shafi: Get ToTime in ("hh:mm tt") format then convert it to string then create date time from it
                    string timeString = listingTiming.SundayTo.ToString("hh:mm tt");
                    DateTime ToTime = DateTime.Parse(timeString, System.Globalization.CultureInfo.CurrentCulture);
                    // End:

                    if (currentTime > ToTime)
                    {
                        return "Closed Now";
                    }
                    else
                    {
                        return "Open Now";
                    }
                }
                else if (day == "Sunday" && listingTiming.SundayHoliday != true)
                {
                    return "Closed Now";
                }
                else
                {
                    return "Open Now";
                }
            }
            else
            {
                return "Open Now";
            }
        }

        public string GetDayName(int days)
        {
            DateTime date = DateTime.Now.AddDays(-days);
            return date.ToString("d ddd");
        }
        // End: Business Open Now & Close

        public async Task PopulateListFLVM()
        {
            if (level == "fc")
            {
                int id = await categoriesContext.FirstCategory.Where(c => c.URL == url).Select(c => c.FirstCategoryID).FirstOrDefaultAsync();

                var listCat = await listingContext.Categories
                    .Where(i => i.FirstCategoryID == id)
                    .ToListAsync();

                foreach (var item in listCat)
                {
                    var listing = await listingContext.Listing.FindAsync(item.ListingID);
                    var address = await listingContext.Address.FindAsync(item.ListingID);
                    var category = await listingContext.Categories.FindAsync(item.ListingID);
                    var commun = await listingContext.Communication.FindAsync(item.ListingID);
                    var spec = await listingContext.Specialisation.FindAsync(item.ListingID);
                    var workhrs = await listingContext.WorkingHours.FindAsync(item.ListingID);
                    var payment = await listingContext.PaymentMode.FindAsync(item.ListingID);

                    if (listing != null && address != null && category != null && commun != null && spec != null && workhrs != null && payment != null)
                    {
                        FreeListingViewModel flvm = new FreeListingViewModel
                        {
                            Listing = listing,
                            //Address = address,
                            //Categories = category,
                            Communication = commun,
                            Specialisation = spec,
                            WorkingHour = workhrs,
                            PaymentMode = payment
                        };

                        listFLVM.Add(flvm);
                    }
                }

                // Begin: Add result to listLrvm
                foreach (var item in listFLVM)
                {
                    // Begin: Assembly
                    string assembly = await sharedContext.Station
                        //.Where(i => i.StationID == item.Address.AssemblyID)
                        .Select(i => i.Name)
                        .FirstOrDefaultAsync();

                    // Begin: Assembly
                    string area = await sharedContext.Locality
                        //.Where(i => i.LocalityID == item.Address.LocalityID)
                        .Select(i => i.LocalityName)
                        .FirstOrDefaultAsync();

                    // Begin: Get Rating
                    var rating = await GetRatingAsync(item.Listing.ListingID);

                    var ratingCount = rating.Count();
                    var ratingAverage = await RatingAverageAsync(item.Listing.ListingID);
                    // End:

                    // Shafi: Begin business open or close
                    var openClose = await BusinessOpenOrCloseAsync(item.Listing.ListingID);
                    // End:

                    ListingResultVM lrvm = new ListingResultVM
                    {
                        ListingId = item.Listing.ListingID,
                        CompanyName = item.Listing.CompanyName,
                        Url = item.Listing.ListingURL,
                        //SubCategoryId = item.Categories.SecondCategoryID,
                        Assembly = assembly,
                        Area = area,
                        Mobile = item.Communication.Mobile,
                        Email = item.Communication.Email,
                        OpenClose = openClose,
                        RatingAverage = ratingAverage,
                        RatingCount = ratingCount
                    };

                    listLrvm.Add(lrvm);
                    // End: Add result to listLrvm
                }
            }
            else if (level == "sc")
            {
                int id = await categoriesContext.SecondCategory.Where(c => c.URL == url).Select(c => c.SecondCategoryID).FirstOrDefaultAsync();

                var listCat = await listingContext.Categories
                    .Where(i => i.SecondCategoryID == id)
                    .ToListAsync();

                foreach (var item in listCat)
                {
                    var listing = await listingContext.Listing.FindAsync(item.ListingID);
                    var address = await listingContext.Address.FindAsync(item.ListingID);
                    var category = await listingContext.Categories.FindAsync(item.ListingID);
                    var commun = await listingContext.Communication.FindAsync(item.ListingID);
                    var spec = await listingContext.Specialisation.FindAsync(item.ListingID);
                    var workhrs = await listingContext.WorkingHours.FindAsync(item.ListingID);
                    var payment = await listingContext.PaymentMode.FindAsync(item.ListingID);

                    if (listing != null && address != null && category != null && commun != null && spec != null && workhrs != null && payment != null)
                    {
                        FreeListingViewModel flvm = new FreeListingViewModel
                        {
                            Listing = listing,
                            //Address = address,
                            //Categories = category,
                            Communication = commun,
                            Specialisation = spec,
                            WorkingHour = workhrs,
                            PaymentMode = payment
                        };

                        listFLVM.Add(flvm);
                    }
                }

                // Begin: Add result to listLrvm
                foreach (var item in listFLVM)
                {
                    // Begin: Assembly
                    string assembly = await sharedContext.Station
                        //.Where(i => i.StationID == item.Address.AssemblyID)
                        .Select(i => i.Name)
                        .FirstOrDefaultAsync();

                    // Begin: Assembly
                    string area = await sharedContext.Locality
                        //.Where(i => i.LocalityID == item.Address.LocalityID)
                        .Select(i => i.LocalityName)
                        .FirstOrDefaultAsync();

                    // Begin: Get Rating
                    var rating = await GetRatingAsync(item.Listing.ListingID);

                    var ratingCount = rating.Count();
                    var ratingAverage = await RatingAverageAsync(item.Listing.ListingID);
                    // End:

                    // Shafi: Begin business open or close
                    var openClose = await BusinessOpenOrCloseAsync(item.Listing.ListingID);
                    // End:

                    ListingResultVM lrvm = new ListingResultVM
                    {
                        ListingId = item.Listing.ListingID,
                        CompanyName = item.Listing.CompanyName,
                        Url = item.Listing.ListingURL,
                        //SubCategoryId = item.Categories.SecondCategoryID,
                        Assembly = assembly,
                        Area = area,
                        Mobile = item.Communication.Mobile,
                        Email = item.Communication.Email,
                        OpenClose = openClose,
                        RatingAverage = ratingAverage,
                        RatingCount = ratingCount
                    };

                    listLrvm.Add(lrvm);
                    // End: Add result to listLrvm
                }
            }
            else if (level == "tc")
            {
                var thirdCat = await categoriesContext.ThirdCategory.Where(i => i.URL == url).FirstOrDefaultAsync();

                var listCat = await listingContext.Categories
                    .Where(i => i.SecondCategoryID == thirdCat.SecondCategoryID)
                    .ToListAsync();

                foreach(var item in listCat)
                {
                    var listing = await listingContext.Listing.FindAsync(item.ListingID);
                    var address = await listingContext.Address.FindAsync(item.ListingID);
                    var category = await listingContext.Categories.FindAsync(item.ListingID);
                    var commun = await listingContext.Communication.FindAsync(item.ListingID);
                    var spec = await listingContext.Specialisation.FindAsync(item.ListingID);
                    var workhrs = await listingContext.WorkingHours.FindAsync(item.ListingID);
                    var payment = await listingContext.PaymentMode.FindAsync(item.ListingID);

                    if(listing != null && address != null && category != null && commun != null && spec != null && workhrs != null && payment != null)
                    {
                        FreeListingViewModel flvm = new FreeListingViewModel
                        {
                            Listing = listing,
                            //Address = address,
                            //Categories = category,
                            Communication = commun,
                            Specialisation = spec,
                            WorkingHour = workhrs,
                            PaymentMode = payment
                        };

                        listFLVM.Add(flvm);
                    }
                }

                // Begin: Add result to listLrvm
                foreach (var item in listFLVM)
                {
                    // Begin: Assembly
                    string assembly = await sharedContext.Station
                        //.Where(i => i.StationID == item.Address.AssemblyID)
                        .Select(i => i.Name)
                        .FirstOrDefaultAsync();

                    // Begin: Assembly
                    string area = await sharedContext.Locality
                        //.Where(i => i.LocalityID == item.Address.LocalityID)
                        .Select(i => i.LocalityName)
                        .FirstOrDefaultAsync();

                    // Begin: Get Rating
                    var rating = await GetRatingAsync(item.Listing.ListingID);

                    var ratingCount = rating.Count();
                    var ratingAverage = await RatingAverageAsync(item.Listing.ListingID);
                    // End:

                    // Shafi: Begin business open or close
                    var openClose = await BusinessOpenOrCloseAsync(item.Listing.ListingID);
                    // End:

                    ListingResultVM lrvm = new ListingResultVM
                    {
                        ListingId = item.Listing.ListingID,
                        CompanyName = item.Listing.CompanyName,
                        Url = item.Listing.ListingURL,
                        //SubCategoryId = item.Categories.SecondCategoryID,
                        Assembly = assembly,
                        Area = area,
                        Mobile = item.Communication.Mobile,
                        Email = item.Communication.Email,
                        OpenClose = openClose,
                        RatingAverage = ratingAverage,
                        RatingCount = ratingCount
                    };

                    listLrvm.Add(lrvm);
                    // End: Add result to listLrvm
                }
            }
            else if (level == "ivc")
            {
                int id = await categoriesContext.FourthCategory.Where(c => c.URL == url).Select(c => c.FourthCategoryID).FirstOrDefaultAsync();

                var fourthCat = await categoriesContext.FourthCategory.Where(i => i.URL == url).FirstOrDefaultAsync();

                var listCat = await listingContext.Categories
                    .Where(i => i.SecondCategoryID == fourthCat.SecondCategoryID).ToListAsync();

                foreach (var item in listCat)
                {
                    var listing = await listingContext.Listing.FindAsync(item.ListingID);
                    var address = await listingContext.Address.FindAsync(item.ListingID);
                    var category = await listingContext.Categories.FindAsync(item.ListingID);
                    var commun = await listingContext.Communication.FindAsync(item.ListingID);
                    var spec = await listingContext.Specialisation.FindAsync(item.ListingID);
                    var workhrs = await listingContext.WorkingHours.FindAsync(item.ListingID);
                    var payment = await listingContext.PaymentMode.FindAsync(item.ListingID);

                    if (listing != null && address != null && category != null && commun != null && spec != null && workhrs != null && payment != null)
                    {
                        FreeListingViewModel flvm = new FreeListingViewModel
                        {
                            Listing = listing,
                            //Address = address,
                            //Categories = category,
                            Communication = commun,
                            Specialisation = spec,
                            WorkingHour = workhrs,
                            PaymentMode = payment
                        };

                        listFLVM.Add(flvm);
                    }
                }

                // Begin: Add result to listLrvm
                foreach (var item in listFLVM)
                {
                    // Begin: Assembly
                    string assembly = await sharedContext.Station
                        //.Where(i => i.StationID == item.Address.AssemblyID)
                        .Select(i => i.Name)
                        .FirstOrDefaultAsync();

                    // Begin: Assembly
                    string area = await sharedContext.Locality
                        //.Where(i => i.LocalityID == item.Address.LocalityID)
                        .Select(i => i.LocalityName)
                        .FirstOrDefaultAsync();

                    // Begin: Get Rating
                    var rating = await GetRatingAsync(item.Listing.ListingID);

                    var ratingCount = rating.Count();
                    var ratingAverage = await RatingAverageAsync(item.Listing.ListingID);
                    // End:

                    // Shafi: Begin business open or close
                    var openClose = await BusinessOpenOrCloseAsync(item.Listing.ListingID);
                    // End:

                    ListingResultVM lrvm = new ListingResultVM
                    {
                        ListingId = item.Listing.ListingID,
                        CompanyName = item.Listing.CompanyName,
                        Url = item.Listing.ListingURL,
                        //SubCategoryId = item.Categories.SecondCategoryID,
                        Assembly = assembly,
                        Area = area,
                        Mobile = item.Communication.Mobile,
                        Email = item.Communication.Email,
                        OpenClose = openClose,
                        RatingAverage = ratingAverage,
                        RatingCount = ratingCount
                    };

                    listLrvm.Add(lrvm);
                    // End: Add result to listLrvm
                }
            }
            else if (level == "vc")
            {
                int id = await categoriesContext.FifthCategory.Where(c => c.URL == url).Select(c => c.FifthCategoryID).FirstOrDefaultAsync();

                var fifthCat = await categoriesContext.FourthCategory.Where(i => i.URL == url).FirstOrDefaultAsync();

                var listCat = await listingContext.Categories
                    .Where(i => i.SecondCategoryID == fifthCat.SecondCategoryID).ToListAsync();

                foreach (var item in listCat)
                {
                    var listing = await listingContext.Listing.FindAsync(item.ListingID);
                    var address = await listingContext.Address.FindAsync(item.ListingID);
                    var category = await listingContext.Categories.FindAsync(item.ListingID);
                    var commun = await listingContext.Communication.FindAsync(item.ListingID);
                    var spec = await listingContext.Specialisation.FindAsync(item.ListingID);
                    var workhrs = await listingContext.WorkingHours.FindAsync(item.ListingID);
                    var payment = await listingContext.PaymentMode.FindAsync(item.ListingID);

                    if (listing != null && address != null && category != null && commun != null && spec != null && workhrs != null && payment != null)
                    {
                        FreeListingViewModel flvm = new FreeListingViewModel
                        {
                            Listing = listing,
                            //Address = address,
                            //Categories = category,
                            Communication = commun,
                            Specialisation = spec,
                            WorkingHour = workhrs,
                            PaymentMode = payment
                        };

                        listFLVM.Add(flvm);
                    }
                }

                // Begin: Add result to listLrvm
                foreach (var item in listFLVM)
                {
                    // Begin: Assembly
                    string assembly = await sharedContext.Station
                        //.Where(i => i.StationID == item.Address.AssemblyID)
                        .Select(i => i.Name)
                        .FirstOrDefaultAsync();

                    // Begin: Assembly
                    string area = await sharedContext.Locality
                        //.Where(i => i.LocalityID == item.Address.LocalityID)
                        .Select(i => i.LocalityName)
                        .FirstOrDefaultAsync();

                    // Begin: Get Rating
                    var rating = await GetRatingAsync(item.Listing.ListingID);

                    var ratingCount = rating.Count();
                    var ratingAverage = await RatingAverageAsync(item.Listing.ListingID);
                    // End:

                    // Shafi: Begin business open or close
                    var openClose = await BusinessOpenOrCloseAsync(item.Listing.ListingID);
                    // End:

                    ListingResultVM lrvm = new ListingResultVM
                    {
                        ListingId = item.Listing.ListingID,
                        CompanyName = item.Listing.CompanyName,
                        Url = item.Listing.ListingURL,
                        //SubCategoryId = item.Categories.SecondCategoryID,
                        Assembly = assembly,
                        Area = area,
                        Mobile = item.Communication.Mobile,
                        Email = item.Communication.Email,
                        OpenClose = openClose,
                        RatingAverage = ratingAverage,
                        RatingCount = ratingCount
                    };

                    listLrvm.Add(lrvm);
                    // End: Add result to listLrvm
                }
            }
            else if (level == "vic")
            {
                int id = await categoriesContext.SixthCategory.Where(c => c.URL == url).Select(c => c.SixthCategoryID).FirstOrDefaultAsync();

                var sixthCat = await categoriesContext.FourthCategory.Where(i => i.URL == url).FirstOrDefaultAsync();

                var listCat = await listingContext.Categories
                    .Where(i => i.SecondCategoryID == sixthCat.SecondCategoryID).ToListAsync();

                foreach (var item in listCat)
                {
                    var listing = await listingContext.Listing.FindAsync(item.ListingID);
                    var address = await listingContext.Address.FindAsync(item.ListingID);
                    var category = await listingContext.Categories.FindAsync(item.ListingID);
                    var commun = await listingContext.Communication.FindAsync(item.ListingID);
                    var spec = await listingContext.Specialisation.FindAsync(item.ListingID);
                    var workhrs = await listingContext.WorkingHours.FindAsync(item.ListingID);
                    var payment = await listingContext.PaymentMode.FindAsync(item.ListingID);

                    if (listing != null && address != null && category != null && commun != null && spec != null && workhrs != null && payment != null)
                    {
                        FreeListingViewModel flvm = new FreeListingViewModel
                        {
                            Listing = listing,
                            //Address = address,
                            //Categories = category,
                            Communication = commun,
                            Specialisation = spec,
                            WorkingHour = workhrs,
                            PaymentMode = payment
                        };

                        listFLVM.Add(flvm);
                    }
                }

                // Begin: Add result to listLrvm
                foreach (var item in listFLVM)
                {
                    // Begin: Assembly
                    string assembly = await sharedContext.Station
                        //.Where(i => i.StationID == item.Address.AssemblyID)
                        .Select(i => i.Name)
                        .FirstOrDefaultAsync();

                    // Begin: Assembly
                    string area = await sharedContext.Locality
                        //.Where(i => i.LocalityID == item.Address.LocalityID)
                        .Select(i => i.LocalityName)
                        .FirstOrDefaultAsync();

                    // Begin: Get Rating
                    var rating = await GetRatingAsync(item.Listing.ListingID);

                    var ratingCount = rating.Count();
                    var ratingAverage = await RatingAverageAsync(item.Listing.ListingID);
                    // End:

                    // Shafi: Begin business open or close
                    var openClose = await BusinessOpenOrCloseAsync(item.Listing.ListingID);
                    // End:

                    ListingResultVM lrvm = new ListingResultVM
                    {
                        ListingId = item.Listing.ListingID,
                        CompanyName = item.Listing.CompanyName,
                        Url = item.Listing.ListingURL,
                        //SubCategoryId = item.Categories.SecondCategoryID,
                        Assembly = assembly,
                        Area = area,
                        Mobile = item.Communication.Mobile,
                        Email = item.Communication.Email,
                        OpenClose = openClose,
                        RatingAverage = ratingAverage,
                        RatingCount = ratingCount
                    };

                    listLrvm.Add(lrvm);
                    // End: Add result to listLrvm
                }
            }
            await Task.Delay(50);
        }

        protected async override Task OnInitializedAsync()
        {
            await PopulateListFLVM();
        }
    }
}
