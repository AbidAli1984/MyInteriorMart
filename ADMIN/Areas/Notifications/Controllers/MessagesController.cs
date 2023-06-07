using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BOL.SHARED;
using DAL.SHARED;
using Microsoft.AspNetCore.Authorization;

namespace ADMIN.Areas.Notifications.Controllers
{
    [Area("Notifications")]
    [Authorize]
    public class MessagesController : Controller
    {
        private readonly SharedDbContext _context;

        public MessagesController(SharedDbContext context)
        {
            _context = context;
        }

        // GET: Notifications/Messages
        [Authorize(Policy = "Admin-Notification-ViewAll")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Messages.ToListAsync());
        }

        // GET: Notifications/Messages/Details/5
        [Authorize(Policy = "Admin-Notification-Read")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var messages = await _context.Messages
                .FirstOrDefaultAsync(m => m.MessageID == id);
            if (messages == null)
            {
                return NotFound();
            }

            return View(messages);
        }

        // GET: Notifications/Messages/Create
        [Authorize(Policy = "Admin-Notification-Create")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Notifications/Messages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Policy = "Admin-Notification-Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MessageID,Name,SmsMessage,EmailSubject,EmailMessage,Variables,Active")] Messages messages)
        {
            if (ModelState.IsValid)
            {
                _context.Add(messages);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(messages);
        }

        // GET: Notifications/Messages/Edit/5
        [Authorize(Policy = "Admin-Notification-Edit")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var messages = await _context.Messages.FindAsync(id);
            if (messages == null)
            {
                return NotFound();
            }
            return View(messages);
        }

        // POST: Notifications/Messages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Policy = "Admin-Notification-Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MessageID,Name,SmsMessage,EmailSubject,EmailMessage,Variables,Active")] Messages messages)
        {
            if (id != messages.MessageID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(messages);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MessagesExists(messages.MessageID))
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
            return View(messages);
        }

        // GET: Notifications/Messages/Delete/5
        [Authorize(Policy = "Admin-Notification-Delete")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var messages = await _context.Messages
                .FirstOrDefaultAsync(m => m.MessageID == id);
            if (messages == null)
            {
                return NotFound();
            }

            return View(messages);
        }

        // POST: Notifications/Messages/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Policy = "Admin-Notification-Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var messages = await _context.Messages.FindAsync(id);
            _context.Messages.Remove(messages);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MessagesExists(int id)
        {
            return _context.Messages.Any(e => e.MessageID == id);
        }
    }
}
