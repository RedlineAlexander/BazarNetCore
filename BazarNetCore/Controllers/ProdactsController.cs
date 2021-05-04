using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BazarNetCore.Data;
using BazarNetCore.Models;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace BazarNetCore.Controllers
{
    public class ProdactsController : Controller
    {
        private readonly ApplicationContex _context;

        public ProdactsController(ApplicationContex context)
        {
            _context = context;
        }


        public async Task<IActionResult> AddImage(int? id, IFormFile uploads)
        {
            if (id != null)
                if (uploads != null)
                {
                    Prodacts prodacts = await _context.Prodacts.Where(x => x.id == id).FirstOrDefaultAsync();
                    byte[] p1 = null;
                    using (var fs1 = uploads.OpenReadStream())
                    using (var ms1 = new MemoryStream())
                    {
                        fs1.CopyTo(ms1);
                        p1 = ms1.ToArray();
                    }
                    prodacts.ImageMimeTypeOfData = uploads.ContentType;
                    prodacts.Image = p1;
                    _context.Prodacts.Update(prodacts);
                    _context.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult AddImageForm(int? id)
        {
            Prodacts prodacts;
            if (id == null)
            {
                return NotFound();
            }

            prodacts = _context.Prodacts.Where(x => x.id == id).FirstOrDefault();
            if (prodacts == null)
            {
                return NotFound();
            }
            return View(prodacts);
        }

        public FileContentResult GetImage(int id)
        {
            Prodacts prodacts = _context.Prodacts
                .FirstOrDefault(g => g.id == id);

            if (prodacts != null)
            {
                var file = File(prodacts.Image, prodacts.ImageMimeTypeOfData);
                return file;
            }
            else
            {
                return null;
            }
        }
        // GET: Prodacts
        public async Task<IActionResult> Index()
        {
            return View(await _context.Prodacts.ToListAsync());
        }

        // GET: Prodacts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prodacts = await _context.Prodacts
                .FirstOrDefaultAsync(m => m.id == id);
            if (prodacts == null)
            {
                return NotFound();
            }

            return View(prodacts);
        }

        // GET: Prodacts/Create
        public IActionResult Create()
        {
            SelectList salers = new SelectList(_context.Salers.Select(x => x.Name).ToList());
            ViewBag.salers = salers;
            SelectList types = new SelectList(_context.ProdactTypes.Select(x => x.Name).ToList());
            ViewBag.types = types;
            return View();
        }

        // POST: Prodacts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,Name,Salers,ProdactType, Weight, Count, Price")] Prodacts prodacts)
        {
            if (ModelState.IsValid)
            {
                _context.Add(prodacts);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(prodacts);
        }

        // GET: Prodacts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prodacts = await _context.Prodacts.FindAsync(id);
            if (prodacts == null)
            {
                return NotFound();
            }
            SelectList salers = new SelectList(_context.Salers.Select(x => x.Name).ToList());
            ViewBag.salers = salers;
            SelectList types = new SelectList(_context.ProdactTypes.Select(x => x.Name).ToList());
            ViewBag.types = types;
            return View(prodacts);
        }

        // POST: Prodacts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,Name,Salers,ProdactType, Weight, Count, Price")] Prodacts prodacts)
        {
            if (id != prodacts.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(prodacts);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProdactsExists(prodacts.id))
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
            return View(prodacts);
        }

        // GET: Prodacts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prodacts = await _context.Prodacts
                .FirstOrDefaultAsync(m => m.id == id);
            if (prodacts == null)
            {
                return NotFound();
            }

            return View(prodacts);
        }

        // POST: Prodacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var prodacts = await _context.Prodacts.FindAsync(id);
            _context.Prodacts.Remove(prodacts);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProdactsExists(int id)
        {
            return _context.Prodacts.Any(e => e.id == id);
        }
    }
}
