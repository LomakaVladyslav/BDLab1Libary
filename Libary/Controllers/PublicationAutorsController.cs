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
    public class PublicationAutorsController : Controller
    {
        private readonly DblibaryContext _context;

        public PublicationAutorsController(DblibaryContext context)
        {
            _context = context;
        }

        // GET: PublicationAutors
        public async Task<IActionResult> Index()
        {
            var dblibaryContext = _context.PublicationAutors.Include(p => p.Autor).Include(p => p.Publication);
            return View(await dblibaryContext.ToListAsync());
        }

        // GET: PublicationAutors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var publicationAutor = await _context.PublicationAutors
                .Include(p => p.Autor)
                .Include(p => p.Publication)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (publicationAutor == null)
            {
                return NotFound();
            }

            return View(publicationAutor);
        }

        // GET: PublicationAutors/Create
        public IActionResult Create()
        {
            ViewData["AutorId"] = new SelectList(_context.Autors, "Id", "Id");
            ViewData["PublicationId"] = new SelectList(_context.Publications, "Id", "Id");
            return View();
        }

        // POST: PublicationAutors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PublicationId,AutorId")] PublicationAutor publicationAutor)
        {
            
            {
                _context.Add(publicationAutor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AutorId"] = new SelectList(_context.Autors, "Id", "Id", publicationAutor.AutorId);
            ViewData["PublicationId"] = new SelectList(_context.Publications, "Id", "Id", publicationAutor.PublicationId);
            return View(publicationAutor);
        }

        // GET: PublicationAutors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var publicationAutor = await _context.PublicationAutors.FindAsync(id);
            if (publicationAutor == null)
            {
                return NotFound();
            }
            ViewData["AutorId"] = new SelectList(_context.Autors, "Id", "Id", publicationAutor.AutorId);
            ViewData["PublicationId"] = new SelectList(_context.Publications, "Id", "Id", publicationAutor.PublicationId);
            return View(publicationAutor);
        }

        // POST: PublicationAutors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PublicationId,AutorId")] PublicationAutor publicationAutor)
        {
            if (id != publicationAutor.Id)
            {
                return NotFound();
            }

            
            {
                try
                {
                    _context.Update(publicationAutor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PublicationAutorExists(publicationAutor.Id))
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
            ViewData["AutorId"] = new SelectList(_context.Autors, "Id", "Id", publicationAutor.AutorId);
            ViewData["PublicationId"] = new SelectList(_context.Publications, "Id", "Id", publicationAutor.PublicationId);
            return View(publicationAutor);
        }

        // GET: PublicationAutors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var publicationAutor = await _context.PublicationAutors
                .Include(p => p.Autor)
                .Include(p => p.Publication)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (publicationAutor == null)
            {
                return NotFound();
            }

            return View(publicationAutor);
        }

        // POST: PublicationAutors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var publicationAutor = await _context.PublicationAutors.FindAsync(id);
            if (publicationAutor != null)
            {
                _context.PublicationAutors.Remove(publicationAutor);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PublicationAutorExists(int id)
        {
            return _context.PublicationAutors.Any(e => e.Id == id);
        }
    }
}
