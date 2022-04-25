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
    public class RawsController : Controller
    {
        private readonly LABAppContext _context;

        public RawsController(LABAppContext context)
        {
            _context = context;
        }

        // GET: Raws
        private async Task<List<Raw>> GetAllMeasurements()
        {
            var AllMeas = await _context.Raws.Include(u => u.Measurement).ToListAsync();
            return AllMeas;
        }
        public async Task<IActionResult> Index()
        {
           var GetAllMeas = await GetAllMeasurements();
            return View(GetAllMeas);
        }

        // GET: Raws/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var raw = await _context.Raws
                .FirstOrDefaultAsync(m => m.Id == id);
            if (raw == null)
            {
                return NotFound();
            }

            return View(raw);
        }

        // GET: Raws/Create
        public IActionResult Create()
        {
            SelectList Measurements = new SelectList(_context.Measurements, "Id", "Measurements");
            ViewBag.Meas = Measurements;
            return View();
        }

        // POST: Raws/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Raw raw)
        {
            if (ModelState.IsValid)
            {
                _context.Add(raw);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(raw);
        }

        // GET: Raws/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var raw = await _context.Raws.FindAsync(id);
            if (raw == null)
            {
                return NotFound();
            }
            return View(raw);
        }

        // POST: Raws/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NameOfRaw,Sum,Quantity")] Raw raw)
        {
            if (id != raw.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(raw);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RawExists(raw.Id))
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
            return View(raw);
        }

        // GET: Raws/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var raw = await _context.Raws
                .FirstOrDefaultAsync(m => m.Id == id);
            if (raw == null)
            {
                return NotFound();
            }

            return View(raw);
        }

        // POST: Raws/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var raw = await _context.Raws.FindAsync(id);
            _context.Raws.Remove(raw);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RawExists(int id)
        {
            return _context.Raws.Any(e => e.Id == id);
        }
    }
}
