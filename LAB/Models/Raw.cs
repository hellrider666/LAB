using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LAB.Models
{
    public class Raw
    {
        public Raw()
        {
            Ingredients = new HashSet<Ingredients>();
        }
        public int Id { get; set; }
        [Required]
        public string NameOfRaw { get; set; }


        public Measurement Measurement { get; set; }
        [Required]
        public int MeasurementId { get; set; }

        public double Sum { get; set; }
        
        public double Quantity { get; set; }

       public IEnumerable<Ingredients> Ingredients { get; set; }
        
       public IEnumerable<PurchaseRaw> PurchaseRaws { get; set; }
       
    }
}
