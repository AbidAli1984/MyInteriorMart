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
            DateTime OpenTime;
            DateTime CloseTime;

            if (day == Constants.Monday)
            {
                OpenTime = workingTime.MondayFrom;
                CloseTime = workingTime.MondayTo;
                businessWorking.OpenOn = Constants.Tuesday;
            }
            else if (day == Constants.Tuesday)
            {
                OpenTime = workingTime.TuesdayFrom;
                CloseTime = workingTime.TuesdayTo;
                businessWorking.OpenOn = Constants.Wednesday;
            }
            else if (day == Constants.Wednesday)
            {
                OpenTime = workingTime.WednesdayFrom;
                CloseTime = workingTime.WednesdayTo;
                businessWorking.OpenOn = Constants.Thursday;
            }
            else if (day == Constants.Thursday)
            {
                OpenTime = workingTime.ThursdayFrom;
                CloseTime = workingTime.ThursdayTo;
                businessWorking.OpenOn = Constants.Friday;
            }
            else if (day == Constants.Friday)
            {
                OpenTime = workingTime.FridayFrom;
                CloseTime = workingTime.FridayTo;
                if (workingTime.SaturdayHoliday)
                    businessWorking.OpenOn = workingTime.SundayHoliday ? Constants.Monday : Constants.Sunday;
                else
                    businessWorking.OpenOn = Constants.Saturday;
            }
            else if (day == Constants.Saturday)
            {
                OpenTime = workingTime.SaturdayFrom;
                CloseTime = workingTime.SaturdayTo;
                businessWorking.OpenOn = workingTime.SundayHoliday ? Constants.Monday : Constants.Sunday;
            }
            else
            {
                OpenTime = workingTime.SundayFrom;
                CloseTime = workingTime.SundayTo;
                businessWorking.OpenOn = Constants.Monday;
            }

            businessWorking.OpenTime = OpenTime.ToString("hh:mm tt");
            businessWorking.CloseTime = CloseTime.ToString("hh:mm tt");

            DateTime openTime = DateTime.Parse(businessWorking.OpenTime, System.Globalization.CultureInfo.CurrentCulture);
            DateTime closeTime = DateTime.Parse(businessWorking.CloseTime, System.Globalization.CultureInfo.CurrentCulture);
            businessWorking.IsBusinessOpen = currentTime > openTime && currentTime < closeTime;
            return businessWorking;
        }
    }
}
