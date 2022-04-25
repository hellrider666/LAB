using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LAB.ViewModels
{
    public class SellsViewModel
    {
        public SelectList Employees { get; set; }
        public int? SelectedEmp { get; set; }
        public SelectList FinProducts { get; set; }
        public int? SelectedProd { get; set; }
        [Required]
        public double? Quantity { get; set; }
        public string errorText { get; set; }
    }
}
