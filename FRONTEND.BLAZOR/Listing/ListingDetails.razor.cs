using BOL.CATEGORIES;
using BOL.LISTING;
using BOL.SHARED;
using BOL.VIEWMODELS;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FRONTEND.BLAZOR.Listing
{
    public partial class ListingDetails
    {
        [Parameter]
        public string ListingID { get; set; }

        public FreeListingViewModel listing { get; set; }

        // Bein: Open Close Properties
        public string FromTime { get; set; }
        public string ToTime { get; set; }
        public string OpenOn { get; set; }
        public bool Closed { get; set; }
        // End:

        // Begin: Rating properties
        public string ratingAverage { get; set; }
        public string rating1 { get; set; }
        public string rating2 { get; set; }
        public string rating3 { get; set; }
        public string rating4 { get; set; }
        public string rating5 { get; set; }
        // End:
        public async Task GetListing()
        {
            int listingId = Int32.Parse(ListingID);

            // Shafi: Get Listing Owner Guid
            string listingOwnerGuid = await listingContext.Listing.Where(l => l.ListingID == listingId).Select(l => l.OwnerGuid).FirstOrDefaultAsync();
            // End:

            // Shafi: Get Owner Name
            string Name = await applicationContext.UserProfile.Where(p => p.OwnerGuid == listingOwnerGuid).Select(p => p.Name).FirstOrDefaultAsync();
            // End:

            // Shafi: Get Profile ID for Profile Image
            int ProfileID = await applicationContext.UserProfile.Where(p => p.OwnerGuid == listingOwnerGuid).Select(p => p.ProfileID).FirstOrDefaultAsync();
            // End:

            // Shafi: Get Owner Designation
            string Designation = await listingContext.Listing.Where(l => l.ListingID == listingId).Select(l => l.Designation).FirstOrDefaultAsync();
            // End:

            listing = new FreeListingViewModel();

            // Begin: Address View Model
            var country = await GetCountry(listingId);
            var state = await GetState(listingId);
            var city = await GetCity(listingId);
            var assembly = await GetAssembly(listingId);
            var pincode = await GetPincode(listingId);
            var locality = await GetLocality(listingId);

            // Begin: Get Open Close
            await BusinessOpenClose(listingId);
            // End:

            ListingAddressVM lavm = new ListingAddressVM
            {
                Country = country,
                State = state,
                City = city,
                Assembly = assembly,
                Pincode = pincode,
                Locality = locality
            };
            // End:

            // Begin: Category View Model
            var firstCat = await GetFirstCat(listingId);
            var secondCat = await GetSecondCat(listingId);

            ListingCategoryVM lcvm = new ListingCategoryVM
            {
                FirstCategory = firstCat,
                SecondCategory = secondCat
            };
            // End:

            listing.Listing = await listingContext.Listing.Where(l => l.ListingID == listingId).FirstOrDefaultAsync();
            listing.Communication = await listingContext.Communication.Where(l => l.ListingID == listingId).FirstOrDefaultAsync();
            listing.Address = lavm;
            listing.Category = lcvm;
            listing.Specialisation = await listingContext.Specialisation.Where(l => l.ListingID == listingId).FirstOrDefaultAsync();
            listing.PaymentMode = await listingContext.PaymentMode.Where(l => l.ListingID == listingId).FirstOrDefaultAsync();
            listing.WorkingHour = await listingContext.WorkingHours.Where(l => l.ListingID == listingId).FirstOrDefaultAsync();
            listing.FromTime = FromTime;
            listing.ToTime = ToTime;
            listing.OpenOn = OpenOn;
            listing.Closed = Closed;

            // Shafi: Get Time Zone
            DateTime timeZoneDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
            string day = timeZoneDate.ToString("dddd");
            string time = timeZoneDate.ToString("hh:mm tt");
            DateTime currentTime = DateTime.Parse(time, System.Globalization.CultureInfo.CurrentCulture);
            // End:
        }

        // Begin: Get Country
        public async Task<Country> GetCountry(int listingId)
        {
            var add = await listingContext.Address.Where(l => l.ListingID == listingId).FirstOrDefaultAsync();

            var country = await sharedContext.Country.Where(i => i.CountryID == add.CountryID).FirstOrDefaultAsync();

            return country;

        }
        // End:

        // Begin: Get State
        public async Task<State> GetState(int listingId)
        {
            var add = await listingContext.Address.Where(l => l.ListingID == listingId).FirstOrDefaultAsync();

            var state = await sharedContext.State.Where(i => i.StateID == add.StateID).FirstOrDefaultAsync();

            return state;

        }
        // End:

        // Begin: Get State
        public async Task<City> GetCity(int listingId)
        {
            var add = await listingContext.Address.Where(l => l.ListingID == listingId).FirstOrDefaultAsync();

            var city = await sharedContext.City.Where(i => i.CityID == add.City).FirstOrDefaultAsync();

            return city;

        }
        // End:

        // Begin: Get State
        public async Task<Station> GetAssembly(int listingId)
        {
            var add = await listingContext.Address.Where(l => l.ListingID == listingId).FirstOrDefaultAsync();

            var assembly = await sharedContext.Station.Where(i => i.StationID == add.AssemblyID).FirstOrDefaultAsync();

            return assembly;

        }
        // End:

        // Begin: Get Pincode
        public async Task<Pincode> GetPincode(int listingId)
        {
            var add = await listingContext.Address.Where(l => l.ListingID == listingId).FirstOrDefaultAsync();

            var pincode = await sharedContext.Pincode.Where(i => i.PincodeID == add.PincodeID).FirstOrDefaultAsync();

            return pincode;

        }
        // End:

        // Begin: Get Pincode
        public async Task<Locality> GetLocality(int listingId)
        {
            var add = await listingContext.Address.Where(l => l.ListingID == listingId).FirstOrDefaultAsync();

            var locality = await sharedContext.Locality.Where(i => i.LocalityID == add.LocalityID).FirstOrDefaultAsync();

            return locality;

        }
        // End:

        // Begin: Get First Category
        public async Task<FirstCategory> GetFirstCat(int listingId)
        {
            var firstCat = await listingContext.Categories.Where(i => i.ListingID == listingId).FirstOrDefaultAsync();

            var category = await categoriesContext.FirstCategory.Where(i => i.FirstCategoryID == firstCat.FirstCategoryID).FirstOrDefaultAsync();

            return category;
        }
        // End:

        // Begin: Get Second Category
        public async Task<SecondCategory> GetSecondCat(int listingId)
        {
            var firstCat = await listingContext.Categories.Where(i => i.ListingID == listingId).FirstOrDefaultAsync();

            var category = await categoriesContext.SecondCategory.Where(i => i.SecondCategoryID == firstCat.SecondCategoryID).FirstOrDefaultAsync();

            return category;
        }
        // End:

        // Begin: Business Open Or Close
        public async Task BusinessOpenClose(int listingId)
        {
            // Get Listing
            var listingWh = await listingContext.WorkingHours.Where(i => i.ListingID == listingId).FirstOrDefaultAsync();
            // End:

            // Get Time Zone
            DateTime timeZoneDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
            string day = timeZoneDate.ToString("dddd");
            string time = timeZoneDate.ToString("hh:mm tt");
            DateTime currentTime = DateTime.Parse(time, System.Globalization.CultureInfo.CurrentCulture);
            // End:

            if(listing != null)
            {
                try
                {
                    if (day == "Monday")
                    {
                        // Shafi: Display ViewBag in Index view
                        FromTime = listingWh.MondayFrom.ToString("hh:mm tt");
                        string ToTime = listingWh.MondayTo.ToString("hh:mm tt");
                        OpenOn = "Tuesday";
                        // End:

                        // Shafi: Get ToTime in ("hh:mm tt") format then convert it to string then create date time from it
                        string timeString = listingWh.MondayTo.ToString("hh:mm tt");
                        DateTime cToTime = DateTime.Parse(timeString, System.Globalization.CultureInfo.CurrentCulture);
                        // End:

                        if (currentTime > cToTime)
                        {
                            Closed = true;
                        }
                        else
                        {
                            Closed = false;
                        }
                    }

                    if (day == "Tuesday")
                    {
                        // Shafi: Display ViewBag in Index view
                        FromTime = listingWh.TuesdayFrom.ToString("hh:mm tt");
                        ToTime = listingWh.TuesdayTo.ToString("hh:mm tt");
                        OpenOn = "Wednesday";
                        // End:

                        // Shafi: Get ToTime in ("hh:mm tt") format then convert it to string then create date time from it
                        string timeString = listingWh.TuesdayTo.ToString("hh:mm tt");
                        DateTime cToTime = DateTime.Parse(timeString, System.Globalization.CultureInfo.CurrentCulture);
                        // End:

                        if (currentTime > cToTime)
                        {
                            Closed = true;
                        }
                        else
                        {
                            Closed = false;
                        }
                    }

                    if (day == "Wednesday")
                    {
                        // Shafi: Display ViewBag in Index view
                        FromTime = listingWh.WednesdayFrom.ToString("hh:mm tt");
                        ToTime = listingWh.WednesdayTo.ToString("hh:mm tt");
                        OpenOn = "Thursday";
                        // End:

                        // Shafi: Get ToTime in ("hh:mm tt") format then convert it to string then create date time from it
                        string timeString = listingWh.WednesdayTo.ToString("hh:mm tt");
                        DateTime cToTime = DateTime.Parse(timeString, System.Globalization.CultureInfo.CurrentCulture);
                        // End:

                        if (currentTime > cToTime)
                        {
                            Closed = true;
                        }
                        else
                        {
                            Closed = false;
                        }
                    }

                    if (day == "Thursday")
                    {
                        // Shafi: Display ViewBag in Index view
                        FromTime = listingWh.ThursdayFrom.ToString("hh:mm tt");
                        ToTime = listingWh.ThursdayTo.ToString("hh:mm tt");
                        OpenOn = "Friday";
                        // End:

                        // Shafi: Get ToTime in ("hh:mm tt") format then convert it to string then create date time from it
                        string timeString = listingWh.ThursdayTo.ToString("hh:mm tt");
                        DateTime cToTime = DateTime.Parse(timeString, System.Globalization.CultureInfo.CurrentCulture);
                        // End:

                        if (currentTime > cToTime)
                        {
                            Closed = true;
                        }
                        else
                        {
                            Closed = false;
                        }
                    }

                    if (day == "Friday")
                    {
                        // Shafi: Display ViewBag in Index view
                        FromTime = listingWh.FridayFrom.ToString("hh:mm tt");
                        ToTime = listingWh.FridayTo.ToString("hh:mm tt");
                        OpenOn = "Saturday";
                        // End:

                        // Shafi: Get ToTime in ("hh:mm tt") format then convert it to string then create date time from it
                        string timeString = listingWh.FridayTo.ToString("hh:mm tt");
                        DateTime cToTime = DateTime.Parse(timeString, System.Globalization.CultureInfo.CurrentCulture);
                        // End:

                        if (currentTime > cToTime)
                        {
                            Closed = true;
                        }
                        else
                        {
                            Closed = false;
                        }
                    }

                    if (day == "Saturday")
                    {
                        // Shafi: Display ViewBag in Index view
                        FromTime = listingWh.SaturdayFrom.ToString("hh:mm tt");
                        ToTime = listingWh.SaturdayTo.ToString("hh:mm tt");
                        if (listingWh.SundayHoliday != true)
                        {
                            OpenOn = "Sunday";
                        }
                        // End:

                        // Shafi: Get ToTime in ("hh:mm tt") format then convert it to string then create date time from it
                        string timeString = listingWh.SaturdayTo.ToString("hh:mm tt");
                        DateTime cToTime = DateTime.Parse(timeString, System.Globalization.CultureInfo.CurrentCulture);
                        // End:

                        if (currentTime > cToTime)
                        {
                            Closed = true;
                        }
                        else
                        {
                            Closed = false;
                        }
                    }

                    if (day == "Sunday")
                    {
                        // Shafi: Display ViewBag in Index view
                        FromTime = listingWh.SundayFrom.ToString("hh:mm tt");
                        ToTime = listingWh.SundayTo.ToString("hh:mm tt");
                        OpenOn = "Monday";
                        // End:

                        // Shafi: Get ToTime in ("hh:mm tt") format then convert it to string then create date time from it
                        string timeString = listingWh.SundayTo.ToString("hh:mm tt");
                        DateTime cToTime = DateTime.Parse(timeString, System.Globalization.CultureInfo.CurrentCulture);
                        // End:

                        if (currentTime > cToTime)
                        {
                            Closed = true;
                        }
                        else
                        {
                            Closed = false;
                        }
                    }

                    if (listingWh.SaturdayHoliday == true && listingWh.SundayHoliday == true)
                    {
                        FromTime = null;
                        ToTime = null;
                        OpenOn = "Monday";
                        Closed = true;
                    }

                    if (listingWh.SaturdayHoliday == true && listingWh.SundayHoliday == false)
                    {
                        FromTime = null;
                        ToTime = null;
                        OpenOn = "Sunday";
                        Closed = true;
                    }

                    if (listingWh.SundayHoliday == true)
                    {
                        FromTime = null;
                        ToTime = null;
                        OpenOn = "Monday";
                        Closed = true;
                    }
                }
                catch(Exception exc)
                {
                }
            }
        }
        // End: Business Open Or Close



        // Begin: Get Rating Average
        //public async Task GetRatingAverage(int listingId)
        //{
        //    // Get rating
        //    var ratings = await listingContext.Rating.Where(r => r.ListingID == listingId).ToListAsync();

        //    if (ratingCount.Count() > 0)
        //    {
        //        var R1 = await listingManager.CountRatingAsync(ListingID, 1);
        //        var R2 = await listingManager.CountRatingAsync(ListingID, 2);
        //        var R3 = await listingManager.CountRatingAsync(ListingID, 3);
        //        var R4 = await listingManager.CountRatingAsync(ListingID, 4);
        //        var R5 = await listingManager.CountRatingAsync(ListingID, 5);

        //        decimal averageCount = 5 * R5 + 4 * R4 + 3 * R3 + 2 * R2 + 1 * R1;
        //        decimal weightedCount = R5 + R4 + R3 + R2 + R1;
        //        decimal ratingAverage = averageCount / weightedCount;
        //        ViewBag.RatingAverage = ratingAverage.ToString("0.0");

        //        decimal R1Total = R1 * 15;
        //        decimal R2Total = R2 * 15;
        //        decimal R3Total = R3 * 15;
        //        decimal R4Total = R4 * 15;
        //        decimal R5Total = R5 * 15;

        //        decimal SubTotalOfAllR = R1Total + R2Total + R3Total + R4Total + R5Total;

        //        decimal R1Percent = (R1Total * 100) / SubTotalOfAllR;
        //        decimal R2Percent = (R2Total * 100) / SubTotalOfAllR;
        //        decimal R3Percent = (R3Total * 100) / SubTotalOfAllR;
        //        decimal R4Percent = (R4Total * 100) / SubTotalOfAllR;
        //        decimal R5Percent = (R5Total * 100) / SubTotalOfAllR;

        //        ViewBag.R1 = R1Percent;
        //        ViewBag.R2 = R2Percent;
        //        ViewBag.R3 = R3Percent;
        //        ViewBag.R4 = R4Percent;
        //        ViewBag.R5 = R5Percent;
        //    }
        //    else
        //    {
        //        ViewBag.RatingAverage = 0;
        //    }
        //}
        // End: Get rating average

        protected async override Task OnInitializedAsync()
        {
            await GetListing();
        }
    }
}
