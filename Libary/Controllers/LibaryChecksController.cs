using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Libary.Data;

namespace Libary.Controllers
{
    public class LibaryChecksController : Controller
    {
        private readonly DblibaryContext _context;

        public LibaryChecksController(DblibaryContext context)
        {
            _context = context;
        }

        // GET: LibaryChecks
        public async Task<IActionResult> Index()
        {
            var dblibaryContext = _context.LibaryChecks.Include(l => l.Delivery).Include(l => l.Publication);
            return View(await dblibaryContext.ToListAsync());
        }

        // GET: LibaryChecks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var libaryCheck = await _context.LibaryChecks
                .Include(l => l.Delivery)
                .Include(l => l.Publication)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (libaryCheck == null)
            {
                return NotFound();
            }

            return View(libaryCheck);
        }

        // GET: LibaryChecks/Create
        public IActionResult Create()
        {
            ViewData["DeliveryId"] = new SelectList(_context.Deliveries, "Id", "PostOfficeAdress");
            ViewData["PublicationId"] = new SelectList(_context.Publications, "Id", "BookName");
            return View();
        }

        // POST: LibaryChecks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PublicationId,DeliveryId,DateTime")] LibaryCheck libaryCheck)
        {
            
            {
                _context.Add(libaryCheck);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DeliveryId"] = new SelectList(_context.Deliveries, "Id", "PostOfficeAdress", libaryCheck.DeliveryId);
            ViewData["PublicationId"] = new SelectList(_context.Publications, "Id", "BookName", libaryCheck.PublicationId);
            return View(libaryCheck);
        }

        // GET: LibaryChecks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var libaryCheck = await _context.LibaryChecks.FindAsync(id);
            if (libaryCheck == null)
            {
                return NotFound();
            }
            ViewData["DeliveryId"] = new SelectList(_context.Deliveries, "Id", "PostOfficeAdress", libaryCheck.DeliveryId);
            ViewData["PublicationId"] = new SelectList(_context.Publications, "Id", "BookName", libaryCheck.PublicationId);
            return View(libaryCheck);
        }

        // POST: LibaryChecks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PublicationId,DeliveryId,DateTime")] LibaryCheck libaryCheck)
        {
            if (id != libaryCheck.Id)
            {
                return NotFound();
            }

            
            {
                try
                {
                    _context.Update(libaryCheck);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LibaryCheckExists(libaryCheck.Id))
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
            ViewData["DeliveryId"] = new SelectList(_context.Deliveries, "Id", "PostOfficeAdress", libaryCheck.DeliveryId);
            ViewData["PublicationId"] = new SelectList(_context.Publications, "Id", "BookName", libaryCheck.PublicationId);
            return View(libaryCheck);
        }

        // GET: LibaryChecks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var libaryCheck = await _context.LibaryChecks
                .Include(l => l.Delivery)
                .Include(l => l.Publication)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (libaryCheck == null)
            {
                return NotFound();
            }

            return View(libaryCheck);
        }

        // POST: LibaryChecks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var libaryCheck = await _context.LibaryChecks.FindAsync(id);
            if (libaryCheck != null)
            {
                _context.LibaryChecks.Remove(libaryCheck);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LibaryCheckExists(int id)
        {
            return _context.LibaryChecks.Any(e => e.Id == id);
        }
    }
}
