using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
namespace LAB.Models
{
    public class Ingredients
    {
        public int Id { get; set; }
        public double Quantity { get; set; }
        public Raw Raws { get; set; }
        public int? RawsId { get; set; }
        public FinishedProducts FinishedProducts { get; set; }
        public int? FinishedProductsId { get; set; }

    }
}
