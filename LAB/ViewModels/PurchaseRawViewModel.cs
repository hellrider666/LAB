using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LAB.ViewModels
{
    public class PurchaseRawViewModel
    {
        public SelectList Raws { get; set; }
        public SelectList Employees { get; set; }
        public int? SelectRaw { get; set; }
        public int? SelectEmployee { get; set; }
        [Required]
        public double? quan { get; set; }
        [Required]
        public double? sum { get; set; }
        public string errorText { get; set; }
    }
}
