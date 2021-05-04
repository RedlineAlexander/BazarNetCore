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
    public class SalersController : Controller
    {
        private readonly ApplicationContex _context;

        public SalersController(ApplicationContex context)
        {
            _context = context;
        }

        // GET: Salers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Salers.ToListAsync());
        }

        // GET: Salers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salers = await _context.Salers
                .FirstOrDefaultAsync(m => m.id == id);
            if (salers == null)
            {
                return NotFound();
            }

            return View(salers);
        }

        // GET: Salers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Salers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id, Name")] Salers salers)
        {
            if (ModelState.IsValid)
            {
                _context.Add(salers);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(salers);
        }

        // GET: Salers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salers = await _context.Salers.FindAsync(id);
            if (salers == null)
            {
                return NotFound();
            }
            return View(salers);
        }

        // POST: Salers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id, Name")] Salers salers)
        {
            if (id != salers.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(salers);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SalersExists(salers.id))
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
            return View(salers);
        }

        // GET: Salers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salers = await _context.Salers
                .FirstOrDefaultAsync(m => m.id == id);
            if (salers == null)
            {
                return NotFound();
            }

            return View(salers);
        }

        // POST: Salers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var salers = await _context.Salers.FindAsync(id);
            _context.Salers.Remove(salers);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SalersExists(int id)
        {
            return _context.Salers.Any(e => e.id == id);
        }
    }
}
