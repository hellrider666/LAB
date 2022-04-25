using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LAB.Models;
using LAB.ViewModels;
using Newtonsoft.Json;

namespace LAB.Controllers
{
    public class IngredientsController : Controller
    {
        private readonly LABAppContext _context;

        public IngredientsController(LABAppContext context)
        {
            _context = context;
        }

        // GET: Ingredients


        public async Task<IActionResult> Index(int? finprod, string name)
        {

            IQueryable<Ingredients> ingredients = _context.Ingredients.Include(p => p.FinishedProducts).Include(u => u.Raws);
            if (finprod != null && finprod != 0)
            {
                ingredients = ingredients.Where(p => p.FinishedProductsId == finprod);
            }

            List<FinishedProducts> finishedProducts = await _context.FinishedProducts.ToListAsync();

            finishedProducts.Insert(0, new FinishedProducts { Name = "Все", Id = 0 });

            IngredientsViewModel ingridientsViewModel = new IngredientsViewModel
            {
                Ingredients = ingredients,
                FinalProducts = new SelectList(finishedProducts, "Id", "Name"),
                Name = name,
                SelectedProduct = finprod
            };
            if (finprod.HasValue)
            {
                var itemToSelect = ingridientsViewModel.FinalProducts.FirstOrDefault(x => x.Value == finprod.Value.ToString());
                itemToSelect.Selected = true;
            }

            return View(ingridientsViewModel);
        }

        [HttpGet]
        public async Task<JsonResult> GetByProduct(int productId)
        {
            var ingridients = await _context.Ingredients.Include(x => x.Raws).Where(x => x.FinishedProductsId == productId).Select(x => new {
                Name = x.Raws.NameOfRaw,
                Quantity = x.Quantity
            }).ToListAsync();
            var resultJson = JsonConvert.SerializeObject(ingridients);
            return new JsonResult(resultJson);
        }

        // GET: Ingredients/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ingredients = await _context.Ingredients
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ingredients == null)
            {
                return NotFound();
            }

            return View(ingredients);
        }

        // GET: Ingredients/Create
        public IActionResult Create()
        {
            SelectList RawItems = new SelectList(_context.Raws, "Id", "NameOfRaw");
            SelectList finishProductsItems = new SelectList(_context.FinishedProducts, "Id", "Name");
            ViewBag.Raw = RawItems;
            ViewBag.Products = finishProductsItems;
            return View();
        }

        // POST: Ingredients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Ingredients ingredients)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ingredients);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ingredients);
        }

        // GET: Ingredients/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ingredients = await _context.Ingredients.FindAsync(id);
            if (ingredients == null)
            {
                return NotFound();
            }
            return View(ingredients);
        }

        // POST: Ingredients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Quantity")] Ingredients ingredients)
        {
            if (id != ingredients.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ingredients);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IngredientsExists(ingredients.Id))
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
            return View(ingredients);
        }

        // GET: Ingredients/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ingredients = await _context.Ingredients
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ingredients == null)
            {
                return NotFound();
            }

            return View(ingredients);
        }

        // POST: Ingredients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ingredients = await _context.Ingredients.FindAsync(id);
            _context.Ingredients.Remove(ingredients);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IngredientsExists(int id)
        {
            return _context.Ingredients.Any(e => e.Id == id);
        }
    }
}
