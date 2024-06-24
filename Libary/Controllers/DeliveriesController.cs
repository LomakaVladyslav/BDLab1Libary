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
    public class DeliveriesController : Controller
    {
        private readonly DblibaryContext _context;

        public DeliveriesController(DblibaryContext context)
        {
            _context = context;
        }

        // GET: Deliveries
        public async Task<IActionResult> Index()
        {
            var dblibaryContext = _context.Deliveries.Include(d => d.Reader);
            return View(await dblibaryContext.ToListAsync());
        }

        // GET: Deliveries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var delivery = await _context.Deliveries
                .Include(d => d.Reader)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (delivery == null)
            {
                return NotFound();
            }

            return View(delivery);
        }

        // GET: Deliveries/Create
        public IActionResult Create()
        {
            ViewData["ReaderId"] = new SelectList(_context.Readers, "Id", "NickName");
            return View();
        }

        // POST: Deliveries/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PostOfficeNumber,PostOfficeAdress,ReaderId")] Delivery delivery)
        {
            if (ModelState.IsValid)
            {
                _context.Add(delivery);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ReaderId"] = new SelectList(_context.Readers, "Id", "NickName", delivery.ReaderId);
            return View(delivery);
        }

        // GET: Deliveries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var delivery = await _context.Deliveries.FindAsync(id);
            if (delivery == null)
            {
                return NotFound();
            }
            ViewData["ReaderId"] = new SelectList(_context.Readers, "Id", "NickName", delivery.ReaderId);
            return View(delivery);
        }

        // POST: Deliveries/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PostOfficeNumber,PostOfficeAdress,ReaderId")] Delivery delivery)
        {
            if (id != delivery.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(delivery);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DeliveryExists(delivery.Id))
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
            ViewData["ReaderId"] = new SelectList(_context.Readers, "Id", "NickName", delivery.ReaderId);
            return View(delivery);
        }

        // GET: Deliveries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var delivery = await _context.Deliveries
                .Include(d => d.Reader)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (delivery == null)
            {
                return NotFound();
            }

            return View(delivery);
        }

        // POST: Deliveries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var delivery = await _context.Deliveries.FindAsync(id);

            // Перевірка наявності доставки в LibaryCheck
            bool isDeliveryInUse = await _context.LibaryChecks.AnyAsync(lc => lc.DeliveryId == id);
            if (isDeliveryInUse)
            {
                // Додати повідомлення про помилку
                TempData["ErrorMessage"] = "Неможливо видалити доставку, оскільки вона використовується в LibaryCheck.";
                return RedirectToAction(nameof(Delete), new { id });
            }

            if (delivery != null)
            {
                _context.Deliveries.Remove(delivery);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool DeliveryExists(int id)
        {
            return _context.Deliveries.Any(e => e.Id == id);
        }
    }
}
