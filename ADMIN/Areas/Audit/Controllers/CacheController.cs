using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace ADMIN.Areas.Audit.Controllers
{
    [Area("Audit")]
    public class CacheController : Controller
    {
        private readonly IMemoryCache memoryCache;

        public CacheController(IMemoryCache memoryCache)
        {
            this.memoryCache = memoryCache;
        }

        [Authorize(Policy = "Admin-Suggestion-ViewCache")]
        public IActionResult Index()
        {

            ViewBag.CacheRemove = ViewData["CacheRemove"];
            return View();
        }

        [HttpGet]
        [Authorize(Policy = "Admin-Suggestion-ClearCache")]
        public IActionResult RemoveCache(string cacheKey, string cacheName)
        {
            memoryCache.Get(cacheKey);
            memoryCache.Remove(cacheKey);
            ViewData["CacheRemove"] = "Cache for " + cacheName + " removed successfully.";
            return View("Index");
        }
    }
}
