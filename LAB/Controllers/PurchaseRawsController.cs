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
    public class PurchaseRawsController : Controller
    {
        private readonly LABAppContext _context;

        public PurchaseRawsController(LABAppContext context)
        {
            _context = context;
        }

        // GET: PurchaseRaws
        private async Task<List<PurchaseRaw>> GetAllPurchase()
        {
            var purchase = await _context.PurchaseRaws.Include(u => u.Raw).ToListAsync();
            return purchase;
        }
        public async Task<IActionResult> Index()
        {
            var purchase = await GetAllPurchase();
            return View(purchase);
        }

        // GET: PurchaseRaws/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchaseRaw = await _context.PurchaseRaws
                .FirstOrDefaultAsync(m => m.Id == id);
            if (purchaseRaw == null)
            {
                return NotFound();
            }

            return View(purchaseRaw);
        }

        // GET: PurchaseRaws/Create
       public IActionResult Create()
        {
            List<Employee> employees = _context.Employees.ToList();
            List<Raw> raws = _context.Raws.ToList();
            PurchaseRawViewModel purchaseView = new PurchaseRawViewModel
            {
                Raws = new SelectList(raws, "Id", "NameOfRaw"),
                Employees = new SelectList(employees, "Id", "Surname"),
                errorText = ""

            };
            return View(purchaseView);
        }

        
        // POST: PurchaseRaws/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int? raw, int? emp, double? quan, double? sum)
        {
            var budget = _context.Budgets.Where(u => u.Id == 1).FirstOrDefault();                   

            if (sum <= budget.CountOfBudget)
            {
                var rawId = _context.Raws.Where(u => u.Id == raw).FirstOrDefault();
                              
                PurchaseRaw purchaseRaw = new PurchaseRaw();
                purchaseRaw.EmployeeId = (int)emp;
                purchaseRaw.RawId = (int)raw;
                purchaseRaw.Sum = (double)sum;
                purchaseRaw.Quantity = (double)quan;
                _context.Add(purchaseRaw);
                await _context.SaveChangesAsync();

                budget.CountOfBudget = budget.CountOfBudget - purchaseRaw.Sum;
                rawId.Quantity += purchaseRaw.Quantity;
                rawId.Sum += purchaseRaw.Sum;
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
             List<Employee> employees = await _context.Employees.ToListAsync();
             List<Raw> raws = await _context.Raws.ToListAsync();
             PurchaseRawViewModel purchaseView = new PurchaseRawViewModel
             {
                 Raws = new SelectList(raws, "Id", "NameOfRaw"),
                 Employees = new SelectList(employees, "Id", "Surname"),
                 SelectEmployee = emp,
                 SelectRaw = raw,
                 quan = quan,
                 sum = sum,
                 errorText = "Сумма закупа привышает бюджет!"
             };
            
            if (emp.HasValue)
            {
                var empSelect = purchaseView.Employees.FirstOrDefault(x => x.Value == emp.Value.ToString());
                empSelect.Selected = true;
            }
            if (raw.HasValue)
            {
                var rawSelect = purchaseView.Raws.FirstOrDefault(x => x.Value == raw.Value.ToString());
                rawSelect.Selected = true;
            }
           
            return View(purchaseView);
        }

        // GET: PurchaseRaws/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchaseRaw = await _context.PurchaseRaws.FindAsync(id);
            if (purchaseRaw == null)
            {
                return NotFound();
            }
            return View(purchaseRaw);
        }

        // POST: PurchaseRaws/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Quantity,Sum,Date")] PurchaseRaw purchaseRaw)
        {
            if (id != purchaseRaw.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(purchaseRaw);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PurchaseRawExists(purchaseRaw.Id))
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
            return View(purchaseRaw);
        }

        // GET: PurchaseRaws/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchaseRaw = await _context.PurchaseRaws
                .FirstOrDefaultAsync(m => m.Id == id);
            if (purchaseRaw == null)
            {
                return NotFound();
            }

            return View(purchaseRaw);
        }

        // POST: PurchaseRaws/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var purchaseRaw = await _context.PurchaseRaws.FindAsync(id);
            _context.PurchaseRaws.Remove(purchaseRaw);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PurchaseRawExists(int id)
        {
            return _context.PurchaseRaws.Any(e => e.Id == id);
        }
    }
}
