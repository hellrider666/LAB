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
    public class BudgetsController : Controller
    {
        private readonly LABAppContext _context;

        public BudgetsController(LABAppContext context)
        {
            _context = context;
        }
        public async Task<List<Budget>> GetBudget()
        {
            var budget = await _context.Budgets.ToListAsync();
            return budget;
        }
        // GET: Budgets
        public async Task<IActionResult> Index()
        {
            var budget = await GetBudget();
            return View(budget);
        }

       
    }
}
