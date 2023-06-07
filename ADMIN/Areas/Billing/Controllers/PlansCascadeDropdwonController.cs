using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.BILLING;
using DAL.LISTING;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ADMIN.Areas.Billing.Controllers
{
    public class PlansCascadeDropdwonController : Controller
    {
        private readonly BillingDbContext listingManager;

        public PlansCascadeDropdwonController(BillingDbContext listingManager)
        {
            this.listingManager = listingManager;
        }

        // Begin: Cascade Dropdown For Plans
        public JsonResult fetchPlans(string PlanType)
        {
            if(PlanType == "Listing Plans")
            {
                var selPlans = listingManager.Plan
                .OrderBy(s => s.Priority)
                .Select(s => new { value = s.PlanID, text = s.Name });
                return Json(new SelectList(selPlans, "value", "text"));
            }
            else if(PlanType == "Banner Plans")
            {
                var selPlans = listingManager.BannerPlan
                .OrderBy(s => s.Priority)
                .Select(s => new { value = s.PlanID, text = s.Name });
                return Json(new SelectList(selPlans, "value", "text"));
            }
            else if (PlanType == "Advertisement Plans")
            {
                var selPlans = listingManager.AdvertisementPlan
                .OrderBy(s => s.Priority)
                .Select(s => new { value = s.PlanID, text = s.Name });
                return Json(new SelectList(selPlans, "value", "text"));
            }
            else if (PlanType == "Data Plans")
            {
                var selPlans = listingManager.DataPlan
                .OrderBy(s => s.Priority)
                .Select(s => new { value = s.PlanID, text = s.Name });
                return Json(new SelectList(selPlans, "value", "text"));
            }
            else if (PlanType == "SMS Plans")
            {
                var selPlans = listingManager.SMSPlans
                .OrderBy(s => s.Priority)
                .Select(s => new { value = s.PlanID, text = s.Name });
                return Json(new SelectList(selPlans, "value", "text"));
            }
            else if (PlanType == "Email Plans")
            {
                var selPlans = listingManager.EmailPlans
                .OrderBy(s => s.Priority)
                .Select(s => new { value = s.PlanID, text = s.Name });
                return Json(new SelectList(selPlans, "value", "text"));
            }
            else
            {
                var selPlans = listingManager.MagazinePlan
                .OrderBy(s => s.Priority)
                .Select(s => new { value = s.PlanID, text = s.Name });
                return Json(new SelectList(selPlans, "value", "text"));
            }
        }
        // End:
    }
}
