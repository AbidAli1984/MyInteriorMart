using BOL.LISTING;
using System;

namespace BOL.ComponentModels.MyAccount.ListingWizard
{
    public class WorkingHoursVM
    {
        public DateTime? MondayFrom { get; set; }
        public DateTime? MondayTo { get; set; }
        public DateTime? TuesdayFrom { get; set; }
        public DateTime? TuesdayTo { get; set; }
        public DateTime? WednesdayFrom { get; set; }
        public DateTime? WednesdayTo { get; set; }
        public DateTime? ThursdayFrom { get; set; }
        public DateTime? ThursdayTo { get; set; }
        public DateTime? FridayFrom { get; set; }
        public DateTime? FridayTo { get; set; }
        public bool SaturdayHoliday { get; set; }
        public DateTime? SaturdayFrom { get; set; }
        public DateTime? SaturdayTo { get; set; }
        public bool SundayHoliday { get; set; }
        public DateTime? SundayFrom { get; set; }
        public DateTime? SundayTo { get; set; }

        public void SetViewModel(WorkingHours workingHour)
        {
            MondayFrom = workingHour.MondayFrom;
            MondayTo = workingHour.MondayTo;
            TuesdayFrom = workingHour.TuesdayFrom;
            TuesdayTo = workingHour.TuesdayTo;
            WednesdayFrom = workingHour.WednesdayFrom;
            WednesdayTo = workingHour.WednesdayTo;
            ThursdayFrom = workingHour.ThursdayFrom;
            ThursdayTo = workingHour.ThursdayTo;
            FridayFrom = workingHour.FridayFrom;
            FridayTo = workingHour.FridayTo;
            SaturdayHoliday = workingHour.SaturdayHoliday;
            SaturdayFrom = workingHour.SaturdayFrom;
            SaturdayTo = workingHour.SaturdayTo;
            SundayHoliday = workingHour.SundayHoliday;
            SundayFrom = workingHour.SundayFrom;
            SundayTo = workingHour.SundayTo;
        }

        public void SetContextModel(WorkingHours workingHours)
        {
            workingHours.MondayFrom = MondayFrom.Value;
            workingHours.MondayTo = MondayTo.Value;
            workingHours.TuesdayFrom = TuesdayFrom.Value;
            workingHours.TuesdayTo = TuesdayTo.Value;
            workingHours.WednesdayFrom = WednesdayFrom.Value;
            workingHours.WednesdayTo = WednesdayTo.Value;
            workingHours.ThursdayFrom = ThursdayFrom.Value;
            workingHours.ThursdayTo = ThursdayTo.Value;
            workingHours.FridayFrom = FridayFrom.Value;
            workingHours.FridayTo = FridayTo.Value;
            workingHours.SaturdayHoliday = SaturdayHoliday;
            workingHours.SaturdayFrom = SaturdayFrom.Value;
            workingHours.SaturdayTo = SaturdayTo.Value;
            workingHours.SundayHoliday = SundayHoliday;
            workingHours.SundayFrom = SundayFrom.Value;
            workingHours.SundayTo = SundayTo.Value;
        }

        public bool IsCopiedToAll()
        {
            if (MondayFrom != null && MondayTo != null)
            {
                TuesdayFrom = MondayFrom;
                TuesdayTo = MondayTo;

                WednesdayFrom = MondayFrom;
                WednesdayTo = MondayTo;

                ThursdayFrom = MondayFrom;
                ThursdayTo = MondayTo;

                FridayFrom = MondayFrom;
                FridayTo = MondayTo;

                SaturdayFrom = MondayFrom;
                SaturdayTo = MondayTo;

                SundayFrom = MondayFrom;
                SundayTo = MondayTo;
                return true;
            }
            return false;
        }

        public bool isValid()
        {
            return MondayFrom != null && MondayTo != null && TuesdayFrom != null && TuesdayTo != null && 
                WednesdayFrom != null & WednesdayTo != null && ThursdayFrom != null && ThursdayTo != null && 
                FridayFrom != null && FridayTo != null;
        }

        public void ClearAll()
        {
            MondayFrom = null;
            MondayTo = null;

            TuesdayFrom = null;
            TuesdayTo = null;

            WednesdayFrom = null;
            WednesdayTo = null;

            ThursdayFrom = null;
            ThursdayTo = null;

            FridayFrom = null;
            FridayTo = null;

            SaturdayFrom = null;
            SaturdayTo = null;

            SundayFrom = null;
            SundayTo = null;
        }
    }
}
