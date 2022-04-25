using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LAB.Models;

namespace LAB.Controllers
{
    public class FinishedProductsController : Controller
    {
        private readonly LABAppContext _context;

        public FinishedProductsController(LABAppContext context)
        {
            _context = context;
        }

        // GET: FinishedProducts
        public async Task<List<FinishedProducts>> GetAllMeasurement()
        {
            var AllMeas = await _context.FinishedProducts.Include(u => u.Measurement).ToListAsync();
            return AllMeas;
        }
        public async Task<IActionResult> Index()
        {
            var GetAllMeas = await GetAllMeasurement();
            return View(GetAllMeas);
        }

        // GET: FinishedProducts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var finishedProducts = await _context.FinishedProducts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (finishedProducts == null)
            {
                return NotFound();
            }

            return View(finishedProducts);
        }

        // GET: FinishedProducts/Create
        public IActionResult Create()
        {
            SelectList Measurements = new SelectList(_context.Measurements, "Id", "Measurements");
            ViewBag.Meas = Measurements;
            return View();
        }

        // POST: FinishedProducts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FinishedProducts finishedProducts)
        {
            if (ModelState.IsValid)
            {
                _context.Add(finishedProducts);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(finishedProducts);
        }

        // GET: FinishedProducts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var finishedProducts = await _context.FinishedProducts.FindAsync(id);
            if (finishedProducts == null)
            {
                return NotFound();
            }
            return View(finishedProducts);
        }

        // POST: FinishedProducts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Sum,Quantity")] FinishedProducts finishedProducts)
        {
            if (id != finishedProducts.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(finishedProducts);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FinishedProductsExists(finishedProducts.Id))
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
            return View(finishedProducts);
        }

        // GET: FinishedProducts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var finishedProducts = await _context.FinishedProducts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (finishedProducts == null)
            {
                return NotFound();
            }

            return View(finishedProducts);
        }

        // POST: FinishedProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var finishedProducts = await _context.FinishedProducts.FindAsync(id);
            _context.FinishedProducts.Remove(finishedProducts);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FinishedProductsExists(int id)
        {
            return _context.FinishedProducts.Any(e => e.Id == id);
        }
    }
}
