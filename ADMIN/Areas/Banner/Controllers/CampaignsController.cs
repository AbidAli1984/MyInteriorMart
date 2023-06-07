using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BOL.BANNER;
using DAL.BANNER;

namespace ADMIN.Areas.Banner.Controllers
{
    [Area("Banner")]
    public class CampaignsController : Controller
    {
        private readonly BannerDbContext _context;

        public CampaignsController(BannerDbContext context)
        {
            _context = context;
        }

        // GET: Banner/Campaigns
        public async Task<IActionResult> Index()
        {
            var bannerDbContext = _context.Campaign.Include(c => c.BannerPage).Include(c => c.BannerSpace).Include(c => c.BannerType);
            return View(await bannerDbContext.ToListAsync());
        }

        // GET: Banner/Campaigns/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var campaign = await _context.Campaign
                .Include(c => c.BannerPage)
                .Include(c => c.BannerSpace)
                .Include(c => c.BannerType)
                .FirstOrDefaultAsync(m => m.CampaignID == id);
            if (campaign == null)
            {
                return NotFound();
            }

            return View(campaign);
        }

        // GET: Banner/Campaigns/Create
        public IActionResult Create()
        {
            ViewData["BannerPageID"] = new SelectList(_context.BannerPage, "BannerPageID", "PageName");
            ViewData["BannerSpaceID"] = new SelectList(_context.BannerSpace, "BannerSpaceID", "SpaceName");
            ViewData["BannerTypeID"] = new SelectList(_context.BannerType, "BannerTypeID", "Type");
            return View();
        }

        // POST: Banner/Campaigns/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CampaignID,ListingID,OwnerGUID,CampaignName,DateCreated,StartDate,EndDate,ImageAltText,DestinationURL,VideoURL,HTML5BannerAdScript,BannerTypeID,BannerPageID,BannerSpaceID,ImpressionsCount,ClicksCount")] Campaign campaign)
        {
            if (ModelState.IsValid)
            {
                _context.Add(campaign);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BannerPageID"] = new SelectList(_context.BannerPage, "BannerPageID", "PageName", campaign.BannerPageID);
            ViewData["BannerSpaceID"] = new SelectList(_context.BannerSpace, "BannerSpaceID", "SpaceName", campaign.BannerSpaceID);
            ViewData["BannerTypeID"] = new SelectList(_context.BannerType, "BannerTypeID", "Type", campaign.BannerTypeID);
            return View(campaign);
        }

        // GET: Banner/Campaigns/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var campaign = await _context.Campaign.FindAsync(id);
            if (campaign == null)
            {
                return NotFound();
            }
            ViewData["BannerPageID"] = new SelectList(_context.BannerPage, "BannerPageID", "PageName", campaign.BannerPageID);
            ViewData["BannerSpaceID"] = new SelectList(_context.BannerSpace, "BannerSpaceID", "SpaceName", campaign.BannerSpaceID);
            ViewData["BannerTypeID"] = new SelectList(_context.BannerType, "BannerTypeID", "Type", campaign.BannerTypeID);
            return View(campaign);
        }

        // POST: Banner/Campaigns/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CampaignID,ListingID,OwnerGUID,CampaignName,DateCreated,StartDate,EndDate,ImageAltText,DestinationURL,VideoURL,HTML5BannerAdScript,BannerTypeID,BannerPageID,BannerSpaceID,ImpressionsCount,ClicksCount")] Campaign campaign)
        {
            if (id != campaign.CampaignID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(campaign);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CampaignExists(campaign.CampaignID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["BannerPageID"] = new SelectList(_context.BannerPage, "BannerPageID", "PageName", campaign.BannerPageID);
            ViewData["BannerSpaceID"] = new SelectList(_context.BannerSpace, "BannerSpaceID", "SpaceName", campaign.BannerSpaceID);
            ViewData["BannerTypeID"] = new SelectList(_context.BannerType, "BannerTypeID", "Type", campaign.BannerTypeID);
            return View(campaign);
        }

        // GET: Banner/Campaigns/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var campaign = await _context.Campaign
                .Include(c => c.BannerPage)
                .Include(c => c.BannerSpace)
                .Include(c => c.BannerType)
                .FirstOrDefaultAsync(m => m.CampaignID == id);
            if (campaign == null)
            {
                return NotFound();
            }

            return View(campaign);
        }

        // POST: Banner/Campaigns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var campaign = await _context.Campaign.FindAsync(id);
            _context.Campaign.Remove(campaign);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CampaignExists(int id)
        {
            return _context.Campaign.Any(e => e.CampaignID == id);
        }
    }
}
