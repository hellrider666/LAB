using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LAB.Models
{
    public class Salary
    {
        public int Id { get; set; }
        public Employee Employee { get; set; }
        public int employeeId { get; set; }
        public int CountOfWork { get; set; }
        public double FinishSalary { get; set; }
        public int Month { get; set; }
        public bool Confirm { get; set; }
    }
}
