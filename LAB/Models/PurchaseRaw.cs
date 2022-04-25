using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LAB.Models
{
    public class PurchaseRaw
    {
        public int Id { get; set; }      
        public double Quantity { get; set; }      
        public double Sum { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public Employee Employee { get; set; }
        public int EmployeeId { get; set; }
        public Raw Raw { get; set; }
        public int RawId { get; set; }
    }
}
