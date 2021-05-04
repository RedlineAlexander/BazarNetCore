using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BazarNetCore.Data;
using BazarNetCore.Models;

namespace BazarNetCore.Controllers
{
    public class ProdactTypesController : Controller
    {
        private readonly ApplicationContex _context;

        public ProdactTypesController(ApplicationContex context)
        {
            _context = context;
        }

        // GET: ProdactTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.ProdactTypes.ToListAsync());
        }

        // GET: ProdactTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prodactType = await _context.ProdactTypes
                .FirstOrDefaultAsync(m => m.id == id);
            if (prodactType == null)
            {
                return NotFound();
            }

            return View(prodactType);
        }

        // GET: ProdactTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProdactTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,Name")] ProdactType prodactType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(prodactType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(prodactType);
        }

        // GET: ProdactTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prodactType = await _context.ProdactTypes.FindAsync(id);
            if (prodactType == null)
            {
                return NotFound();
            }
            return View(prodactType);
        }

        // POST: ProdactTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,Name")] ProdactType prodactType)
        {
            if (id != prodactType.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(prodactType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProdactTypeExists(prodactType.id))
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
            return View(prodactType);
        }

        // GET: ProdactTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prodactType = await _context.ProdactTypes
                .FirstOrDefaultAsync(m => m.id == id);
            if (prodactType == null)
            {
                return NotFound();
            }

            return View(prodactType);
        }

        // POST: ProdactTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var prodactType = await _context.ProdactTypes.FindAsync(id);
            _context.ProdactTypes.Remove(prodactType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProdactTypeExists(int id)
        {
            return _context.ProdactTypes.Any(e => e.id == id);
        }
    }
}
