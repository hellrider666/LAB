using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LAB.ViewModels
{
    public class SalaryConfViewModel
    {
        public SelectList employee { get; set; }
        public int? selectedEmp { get; set; }
        public double budget { get; set; }
        public string errorText {get; set;}
    }
}
