using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LAB.Models;
using LAB.ViewModels;

namespace LAB.Controllers
{
    public class ProductionsController : Controller
    {
        private readonly LABAppContext _context;

        public ProductionsController(LABAppContext context)
        {
            _context = context;
        }

        // GET: Productions
        public async Task<IActionResult> Index()
        {
           return View(await _context.Productions.ToListAsync());
        }

        // GET: Productions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var production = await _context.Productions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (production == null)
            {
                return NotFound();
            }

            return View(production);
        }

        // GET: Productions/Create

        public IActionResult Create(int? selectedProd)
        {
            List<Employee> employees = _context.Employees.ToList();
            List<FinishedProducts> products = _context.FinishedProducts.ToList();
            productionViewModel productionView = new productionViewModel
            {
                Products = new SelectList(products, "Id", "Name"),
                Employees = new SelectList(employees, "Id", "Name"),
                errorText = ""

            };
            return View(productionView);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int? prod, int? emp, double? quan)
        {
            var budget = _context.Budgets.Where(u => u.Id == 1).FirstOrDefault();
            var product = _context.FinishedProducts.Where(u => u.Id == prod).FirstOrDefault();
            var present_ingredient = false;
            var ingredients = _context.Ingredients.Where(u => u.FinishedProductsId == prod).ToList();
            foreach (var item in ingredients)
            {
                var material = _context.Raws.Where(u => u.Id == item.RawsId).FirstOrDefault();
                if (material.Quantity < item.Quantity * quan)
                {
                    present_ingredient = true;
                }
                if (present_ingredient)
                {
                    break;
                }

            }

            if (!present_ingredient)
            {
                Production productionProduct = new Production();
                productionProduct.EmployeeId = (int)emp;
                productionProduct.FinishedProductsId = (int)prod;
                productionProduct.Quantity = (double)quan;               
                _context.Add(productionProduct);
                await _context.SaveChangesAsync();
                double sum = 0;
                double count = 0;
                foreach (var item in ingredients)
                {
                    var material = _context.Raws.Where(u => u.Id == item.RawsId).FirstOrDefault();
                    sum += material.Sum / material.Quantity * (double)quan * item.Quantity;
                    material.Sum -= material.Sum / material.Quantity * item.Quantity * (double)quan;
                    material.Quantity -= item.Quantity * (double)quan;
                    count += 1;


                }
                product.Quantity += (int)quan;
                product.Sum += (int)sum;
                await _context.SaveChangesAsync();

                int monthNow = DateTime.Now.Month;
                var employee = _context.Employees.Where(u => u.Id == emp).FirstOrDefault();
                var salary = _context.Salaries.Where(u => u.employeeId == emp).Where(p => p.Month == monthNow).FirstOrDefault();
                

                if (salary != null && salary.Month == monthNow)
                {
                    salary.CountOfWork += 1;
                    salary.FinishSalary += (double)sum * (budget.EmployeeRate / 100);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    Salary sal = new Salary();
                    sal.employeeId = (int)emp;
                    sal.Month = monthNow;
                    sal.FinishSalary = employee.Salary + ((double)sum * (budget.EmployeeRate / 100));
                    sal.CountOfWork += 1;
                    sal.Confirm = false;

                    _context.Add(sal);
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction(nameof(Index));
            }
            List<Employee> employees =  _context.Employees.ToList();
            List<FinishedProducts> products =  _context.FinishedProducts.ToList();
            productionViewModel productionView = new productionViewModel
            {
                Products = new SelectList(products, "Id", "Name"),
                Employees = new SelectList(employees, "Id", "Name"),
                SelectEmployee = emp,
                SelectProduct = prod,
                quan = quan,
                errorText = "Недостаточное количество материалов!"
            };

            if (emp.HasValue)
            {
                var empSelect = productionView.Employees.FirstOrDefault(x => x.Value == emp.Value.ToString());
                empSelect.Selected = true;
            }
            if (prod.HasValue)
            {
                var rawSelect = productionView.Products.FirstOrDefault(x => x.Value == prod.Value.ToString());
                rawSelect.Selected = true;
            }


            return View(productionView);

        }
        
        // POST: Productions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        

        // GET: Productions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var production = await _context.Productions.FindAsync(id);
            if (production == null)
            {
                return NotFound();
            }
            return View(production);
        }

        // POST: Productions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Date,Quantity")] Production production)
        {
            if (id != production.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(production);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductionExists(production.Id))
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
            return View(production);
        }

        // GET: Productions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var production = await _context.Productions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (production == null)
            {
                return NotFound();
            }

            return View(production);
        }

        // POST: Productions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var production = await _context.Productions.FindAsync(id);
            _context.Productions.Remove(production);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductionExists(int id)
        {
            return _context.Productions.Any(e => e.Id == id);
        }
    }
}
