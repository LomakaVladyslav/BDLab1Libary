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
    public class PublicationsController : Controller
    {
        private readonly DblibaryContext _context;

        public PublicationsController(DblibaryContext context)
        {
            _context = context;
        }

        // GET: Publications
        public async Task<IActionResult> Index()
        {
            var dblibaryContext = _context.Publications.Include(p => p.Epoch).Include(p => p.Genre);
            return View(await dblibaryContext.ToListAsync());
        }

        // GET: Publications/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var publication = await _context.Publications
                .Include(p => p.Epoch)
                .Include(p => p.Genre)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (publication == null)
            {
                return NotFound();
            }

            return View(publication);
        }

        // GET: Publications/Create
        public IActionResult Create()
        {
            ViewData["EpochId"] = new SelectList(_context.Epoches, "Id", "EpochName");
            ViewData["GenreId"] = new SelectList(_context.Genres, "Id", "GenreName");
            return View();
        }

        // POST: Publications/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,GenreId,EpochId,BookName,Annotation,PageCout,Price,Year")] Publication publication)
        {
            if (ModelState.IsValid)
            {
                _context.Add(publication);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EpochId"] = new SelectList(_context.Epoches, "Id", "EpochName", publication.EpochId);
            ViewData["GenreId"] = new SelectList(_context.Genres, "Id", "GenreName", publication.GenreId);
            return View(publication);
        }

        // GET: Publications/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var publication = await _context.Publications.FindAsync(id);
            if (publication == null)
            {
                return NotFound();
            }
            ViewData["EpochId"] = new SelectList(_context.Epoches, "Id", "EpochName", publication.EpochId);
            ViewData["GenreId"] = new SelectList(_context.Genres, "Id", "GenreName", publication.GenreId);
            return View(publication);
        }

        // POST: Publications/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,GenreId,EpochId,BookName,Annotation,PageCout,Price,Year")] Publication publication)
        {
            if (id != publication.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(publication);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PublicationExists(publication.Id))
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
            ViewData["EpochId"] = new SelectList(_context.Epoches, "Id", "EpochName", publication.EpochId);
            ViewData["GenreId"] = new SelectList(_context.Genres, "Id", "GenreName", publication.GenreId);
            return View(publication);
        }

        // GET: Publications/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var publication = await _context.Publications
                .Include(p => p.Epoch)
                .Include(p => p.Genre)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (publication == null)
            {
                return NotFound();
            }

            return View(publication);
        }

        // POST: Publications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var publication = await _context.Publications.FindAsync(id);

            // Перевірка наявності публікацій у LibaryCheck
            bool isPublicationInUse = await _context.LibaryChecks.AnyAsync(l => l.PublicationId == id);
            if (isPublicationInUse)
            {
                // Додати повідомлення про помилку
                TempData["ErrorMessage"] = "Неможливо видалити публікацію, оскільки вона використовується в перевірках бібліотеки.";
                return RedirectToAction(nameof(Delete), new { id });
            }

            if (publication != null)
            {
                _context.Publications.Remove(publication);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool PublicationExists(int id)
        {
            return _context.Publications.Any(e => e.Id == id);
        }
    }
}
