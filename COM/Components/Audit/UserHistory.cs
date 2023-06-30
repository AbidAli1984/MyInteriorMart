using BAL.Audit;
using DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using SendGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace COM.Components.Audit
{
    public class UserHistory : ViewComponent
    {

        readonly private IHistoryAudit historyAudit;
        readonly private UserManager<ApplicationUser> userManager;
        public UserHistory(IHistoryAudit historyAudit, UserManager<ApplicationUser> userManager)
        {
            this.historyAudit = historyAudit;
            this.userManager = userManager;
        }
        public async Task<IViewComponentResult> InvokeAsync(string activity)
        {
            // Shafi: Browser header
            string ipAddress = this.HttpContext.Connection.RemoteIpAddress.ToString();
            string userAgent = this.HttpContext.Request.Headers["User-Agent"];
            string referUrl = this.HttpContext.Request.Headers["Referer"];
            string visitedURL = this.HttpContext.Request.Headers["Host"];
            // End:

            var user = await userManager.FindByNameAsync(User.Identity.Name);

            return View();
        }
    }
}