using BOL.ComponentModels.Listings;
using DAL.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BAL
{
    public class HelperFunctions
    {
        private readonly IListingRepository _listingRepository;
        public HelperFunctions(IListingRepository listingRepository)
        {
            _listingRepository = listingRepository;
        }

        public async Task<BusinessWorkingHour> IsBusinessOpen(int ListingID)
        {
            var workingTime = await _listingRepository.GetWorkingHoursByListingId(ListingID);
            BusinessWorkingHour businessWorking = new BusinessWorkingHour();

            if (workingTime == null)
            {
                businessWorking.IsBusinessOpen = true;
                return businessWorking;
            }

            DateTime timeZoneDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
            string day = timeZoneDate.ToString("dddd");

            if ((day == Constants.Saturday && workingTime.SaturdayHoliday) || (day == Constants.Sunday && workingTime.SundayHoliday))
            {
                return businessWorking;
            }

            string time = timeZoneDate.ToString("hh:mm tt");
            DateTime currentTime = DateTime.Parse(time, System.Globalization.CultureInfo.CurrentCulture);
            DateTime FromTime;
            DateTime ToTime;

            if (day == Constants.Monday)
            {
                FromTime = workingTime.MondayFrom;
                ToTime = workingTime.MondayTo;
            }
            else if (day == Constants.Tuesday)
            {
                FromTime = workingTime.TuesdayFrom;
                ToTime = workingTime.TuesdayTo;
            }
            else if (day == Constants.Wednesday)
            {
                FromTime = workingTime.WednesdayFrom;
                ToTime = workingTime.WednesdayTo;
            }
            else if (day == Constants.Thursday)
            {
                FromTime = workingTime.ThursdayFrom;
                ToTime = workingTime.ThursdayTo;
            }
            else if (day == Constants.Friday)
            {
                FromTime = workingTime.FridayFrom;
                ToTime = workingTime.FridayTo;
            }
            else if (day == Constants.Saturday)
            {
                FromTime = workingTime.SaturdayFrom;
                ToTime = workingTime.SaturdayTo;
            }
            else
            {
                FromTime = workingTime.SundayFrom;
                ToTime = workingTime.SundayTo;
            }

            businessWorking.FromTime = FromTime.ToString("hh:mm tt");
            businessWorking.ToTime = ToTime.ToString("hh:mm tt");

            DateTime openTime = DateTime.Parse(businessWorking.FromTime, System.Globalization.CultureInfo.CurrentCulture);
            DateTime closeTime = DateTime.Parse(businessWorking.ToTime, System.Globalization.CultureInfo.CurrentCulture);
            businessWorking.IsBusinessOpen = currentTime > openTime && currentTime < closeTime;
            return businessWorking;
        }
    }
}
