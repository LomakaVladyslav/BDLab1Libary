using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Libary.Data;
using Libary.Services;

namespace Libary.Controllers
{
    public class AutorsController : Controller
    {
        private readonly DblibaryContext _context;

        public AutorsController(DblibaryContext context)
        {
            _context = context;
        }

        // GET: Autors
        public async Task<IActionResult> Index()
        {
            var dblibaryContext = _context.Autors.Include(a => a.Region);
            return View(await dblibaryContext.ToListAsync());
        }

        // GET: Autors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var autor = await _context.Autors
                .Include(a => a.Region)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (autor == null)
            {
                return NotFound();
            }

            return View(autor);
        }

        // GET: Autors/Create
        public IActionResult Create()
        {
            ViewData["RegionId"] = new SelectList(_context.Regions, "Id", "RegionName");
            return View();
        }

        // POST: Autors/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,RegionId,AutorName,Pseudonym")] Autor autor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(autor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RegionId"] = new SelectList(_context.Regions, "Id", "RegionName", autor.RegionId);
            return View(autor);
        }

        // GET: Autors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var autor = await _context.Autors.FindAsync(id);
            if (autor == null)
            {
                return NotFound();
            }
            ViewData["RegionId"] = new SelectList(_context.Regions, "Id", "RegionName", autor.RegionId);
            return View(autor);
        }

        // POST: Autors/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,RegionId,AutorName,Pseudonym")] Autor autor)
        {
            if (id != autor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(autor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AutorExists(autor.Id))
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
            ViewData["RegionId"] = new SelectList(_context.Regions, "Id", "RegionName", autor.RegionId);
            return View(autor);
        }

        // GET: Autors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var autor = await _context.Autors
                .Include(a => a.Region)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (autor == null)
            {
                return NotFound();
            }

            return View(autor);
        }

        // POST: Autors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var autor = await _context.Autors.FindAsync(id);

            // Перевірка наявності книг, написаних автором
            bool isAutorInUse = await _context.PublicationAutors.AnyAsync(b => b.AutorId == id);
            if (isAutorInUse)
            {
                // Додати повідомлення про помилку
                TempData["ErrorMessage"] = "Неможливо видалити автора, оскільки він використовується в інших записах.";
                return RedirectToAction(nameof(Delete), new { id });
            }

            if (autor != null)
            {
                _context.Autors.Remove(autor);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool AutorExists(int id)
        {
            return _context.Autors.Any(e => e.Id == id);
        }

        [HttpGet]
        public IActionResult Import()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Import(IFormFile fileExcel, CancellationToken cancellationToken)
        {
            var factory = new AutorDataPortServiceFactory(_context);
            var importService = factory.GetImportService(fileExcel.ContentType);

            using var stream = fileExcel.OpenReadStream();
            await importService.ImportFromStreamAsync(stream, cancellationToken);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Export([FromQuery] string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                    CancellationToken cancellationToken = default)
        {
            var factory = new AutorDataPortServiceFactory(_context);
            var exportService = factory.GetExportService(contentType);

            var memoryStream = new MemoryStream();

            await exportService.WriteToAsync(memoryStream, cancellationToken);

            await memoryStream.FlushAsync(cancellationToken);
            memoryStream.Position = 0;

            return new FileStreamResult(memoryStream, contentType)
            {
                FileDownloadName = $"autors_{DateTime.UtcNow.ToShortDateString()}.xlsx"
            };
        }
    }
}
