using LAB.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LAB.ViewModels
{
    public class IngredientsViewModel
    {
        public IEnumerable<Ingredients> Ingredients { get; set; }
        public SelectList FinalProducts { get; set; }
        public int? SelectedProduct { get; set; }
        public string Name { get; set; }
        
    }
}
