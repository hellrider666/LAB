using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LAB.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LAB.ViewModels
{
    public class SalaryViewModel
    {
        public IEnumerable<Salary> _salaries { get; set; }
        public SelectList Month { get; set; }
        public int? SelectMonth { get; set; }  
        public SelectList Confrirmed { get; set; }        
        public bool? confirmedIndex { get; set; }
    }
}
