using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace LAB.Models
{
    public class FinishedProducts
    {
        public FinishedProducts()
        {
            Ingredients = new HashSet<Ingredients>();
        }
        public int Id { get; set; }
        public string Name { get; set; }

        public Measurement Measurement { get; set; }
        public int MeasurementId { get; set; }
        public double Sum { get; set; }
        public double Quantity { get; set; }
       
        public IEnumerable<Ingredients> Ingredients { get; set; }
        public  IEnumerable<Sell> Sells { get; set; }
        public  IEnumerable<Production> productions { get; set; }
    }
}
