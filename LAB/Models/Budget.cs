using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LAB.Models
{
    public class Budget
    {
        public int Id { get; set; }
        public double CountOfBudget { get; set; }
        public double Rate { get; set; }
        public double EmployeeRate { get; set; }
        public double Income_Tax { get; set; }
        public double Unioin_Tax { get; set; }
    }
}
