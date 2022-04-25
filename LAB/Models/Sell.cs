using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace LAB.Models
{
    public class Sell
    {
        public int Id { get; set; }
       
        public int FinishedProductsId { get; set; }
        public FinishedProducts FinishedProducts { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public double Quantity { get; set; }
        public double Sum { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}
