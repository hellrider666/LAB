using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LAB.Models;
using LAB.OtherClasses;
using LAB.ViewModels;

namespace LAB.Controllers
{
   
    public class SalariesController : Controller
    {
        public static double finSalary { get; set; }

        private readonly LABAppContext _context;

        public SalariesController(LABAppContext context)
        {
            _context = context;
        }

        public List<Months> SetMonths()
        {
            List<Months> allMonth = new List<Months>();
            allMonth.Add(new Months("Все", 0));
            allMonth.Add(new Months("Январь", 1));
            allMonth.Add(new Months("Февраль", 2));
            allMonth.Add(new Months("Март", 3));
            allMonth.Add(new Months("Апрель", 4));
            allMonth.Add(new Months("Май", 5));
            allMonth.Add(new Months("Июнь", 6));
            allMonth.Add(new Months("Июль", 7));
            allMonth.Add(new Months("Август", 8));
            allMonth.Add(new Months("Сентябрь", 9));
            allMonth.Add(new Months("Октябрь", 10));
            allMonth.Add(new Months("Ноябрь", 11));
            allMonth.Add(new Months("Декабрь", 12));
           
            return allMonth;
        }
        public List<MonthWeekend> SetMonthsWeekends()
        {
            List<MonthWeekend> allMonth = new List<MonthWeekend>();
            allMonth.Add(new MonthWeekend(1, 20, 1));
            if (DateTime.IsLeapYear(DateTime.Now.Year) == true)
            {
                allMonth.Add(new MonthWeekend(2, 21, 1));
            }
            else
            {
                allMonth.Add(new MonthWeekend(2, 20,1));
            }
            allMonth.Add(new MonthWeekend(3, 23, 2));
            allMonth.Add(new MonthWeekend(4, 21, 1));
            allMonth.Add(new MonthWeekend(5, 22, 1));
            allMonth.Add(new MonthWeekend(6, 22, 0));
            allMonth.Add(new MonthWeekend(7, 21, 0));
            allMonth.Add(new MonthWeekend(8, 23, 1));
            allMonth.Add(new MonthWeekend(9, 22, 0));
            allMonth.Add(new MonthWeekend(10, 21, 0));
            allMonth.Add(new MonthWeekend(11, 22, 2));
            allMonth.Add(new MonthWeekend(12, 22, 0));
            
            return allMonth;
        }

        public List<Confirmed> SetConf()
        {
            List<Confirmed> allConf = new List<Confirmed>();
            allConf.Add(new Confirmed("Все", null));
            allConf.Add(new Confirmed("В Госдепе кончились деньги", false));
            allConf.Add(new Confirmed("Проплачено Госдепом", true));

            return allConf;
        }

        // GET: Salaries
        public IActionResult Index(int? monthNumber, bool? conf)
        {                                
            IQueryable<Salary> salaries = _context.Salaries.Include(p => p.Employee);
            if (monthNumber != null && monthNumber != 0)
            {
                salaries = salaries.Where(p => p.Month == monthNumber);
            }
            if(conf != null)
            {
                salaries = salaries.Where(u => u.Confirm == conf);
            }

            List<Months> allMonth = SetMonths();
            List<Confirmed> allConf = SetConf();

            SalaryViewModel salaryViewModel = new SalaryViewModel
            {
                _salaries = salaries,
                Month = new SelectList(allMonth, "NumberOfMonth", "NameOfMonth"),
                Confrirmed = new SelectList(allConf, "conf", "Name"),
                SelectMonth = monthNumber,
                confirmedIndex = conf,
              
            };
            if (monthNumber.HasValue)
            {
                var itemToSelect = salaryViewModel.Month.FirstOrDefault(x => x.Value == monthNumber.Value.ToString());
                itemToSelect.Selected = true;
            }
            if (conf.HasValue)
            {
                var itemToSelect = salaryViewModel.Confrirmed.FirstOrDefault(x => x.Value == conf.Value.ToString());
                itemToSelect.Selected = true;
            }
            return View(salaryViewModel);
        }

        // GET: Salaries/Details/5
       
             
        // GET: Salaries/Create
        
        public IActionResult Create(int? id, string text)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salary =  _context.Salaries
                .Include(s => s.Employee)
                .FirstOrDefault(m => m.Id == id);
            if (salary == null)
            {
                return NotFound();
            }
            var budget = _context.Budgets.Where(u => u.Id == 1).FirstOrDefault();
            var employee = _context.Employees.Where(u => u.Id == id).FirstOrDefault();
            var months = SetMonthsWeekends();
            var month = months.Where(u => u.NumberOfMonth == salary.Month).FirstOrDefault();

            double SalaryFromOneDay = employee.Salary/month.MonthWorkDayCount;
            int CountOfWorkDays = month.MonthWorkDayCount - month.WeekendsCount;

            double finishSalary = (SalaryFromOneDay * CountOfWorkDays) * 1 - (budget.Income_Tax + budget.Unioin_Tax);

            finSalary = finishSalary;

            ViewData["FinalSalary"] = finishSalary.ToString();
            ViewData["CountOfWorkDays"] = CountOfWorkDays.ToString();

            ViewBag.text = text;
            return View(salary);
        }


        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateConfirmed(int id)
        {
            var salary = await _context.Salaries.FindAsync(id);
            var budget = await _context.Budgets.Where(u => u.Id == 1).FirstOrDefaultAsync();
            if(salary.FinishSalary <= budget.CountOfBudget)
            {
                salary.Confirm = true;
                budget.CountOfBudget -= salary.FinishSalary;
                salary.FinishSalary = finSalary;
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            string text = "Ну как там, с деньгами?";            
            return Create(id, text);
        }      

        // GET: Salaries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salary = await _context.Salaries
                .Include(s => s.Employee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (salary == null)
            {
                return NotFound();
            }

            return View(salary);
        }

        // POST: Salaries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var salary = await _context.Salaries.FindAsync(id);
            _context.Salaries.Remove(salary);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SalaryExists(int id)
        {
            return _context.Salaries.Any(e => e.Id == id);
        }
    }
}
