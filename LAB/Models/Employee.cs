using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LAB.Models
{
    public class Employee
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public Position Position { get; set; }

        public int? PositionId { get; set; }
        public double Salary { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }

        public IEnumerable<Production> productions { get; set; }
        public IEnumerable<PurchaseRaw> PurchaseRaws { get; set; }
        public IEnumerable<Sell> Sells { get; set; }
        public IEnumerable<Salary> salaries { get; set; }
    }
}
