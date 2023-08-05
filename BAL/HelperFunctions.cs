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

            if ((day == Constants.Saturday && workingTime.SaturdayHoliday))
            {
                businessWorking.OpenDay = workingTime.SundayHoliday ? Constants.Monday : Constants.Sunday;
                businessWorking.OpenTime = (workingTime.SundayHoliday ? workingTime.MondayFrom : workingTime.SundayFrom).ToString("hh:mm tt");
                return businessWorking;
            }
            else if((day == Constants.Sunday && workingTime.SundayHoliday))
            {
                businessWorking.OpenDay = Constants.Monday;
                businessWorking.OpenTime = workingTime.MondayFrom.ToString("hh:mm tt");
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
                businessWorking.OpenDay = Constants.Tuesday;
            }
            else if (day == Constants.Tuesday)
            {
                OpenTime = workingTime.TuesdayFrom;
                CloseTime = workingTime.TuesdayTo;
                businessWorking.OpenDay = Constants.Wednesday;
            }
            else if (day == Constants.Wednesday)
            {
                OpenTime = workingTime.WednesdayFrom;
                CloseTime = workingTime.WednesdayTo;
                businessWorking.OpenDay = Constants.Thursday;
            }
            else if (day == Constants.Thursday)
            {
                OpenTime = workingTime.ThursdayFrom;
                CloseTime = workingTime.ThursdayTo;
                businessWorking.OpenDay = Constants.Friday;
            }
            else if (day == Constants.Friday)
            {
                OpenTime = workingTime.FridayFrom;
                CloseTime = workingTime.FridayTo;
                if (workingTime.SaturdayHoliday)
                    businessWorking.OpenDay = workingTime.SundayHoliday ? Constants.Monday : Constants.Sunday;
                else
                    businessWorking.OpenDay = Constants.Saturday;
            }
            else if (day == Constants.Saturday)
            {
                OpenTime = workingTime.SaturdayFrom;
                CloseTime = workingTime.SaturdayTo;
                businessWorking.OpenDay = workingTime.SundayHoliday ? Constants.Monday : Constants.Sunday;
            }
            else
            {
                OpenTime = workingTime.SundayFrom;
                CloseTime = workingTime.SundayTo;
                businessWorking.OpenDay = Constants.Monday;
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
