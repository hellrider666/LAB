using LAB.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LAB.ViewModels
{
    public class productionViewModel
    {
        public SelectList Products { get; set; }
        public SelectList Employees { get; set; }
        public int? SelectProduct { get; set; }
        public int? SelectEmployee { get; set; }
        [Required]
        public double? quan { get; set; }
        [Required]
        public string errorText { get; set; }
        public DateTime Date { get; set; }
    }
}
