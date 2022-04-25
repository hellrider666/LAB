using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LAB.Models
{
    public class Position
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<Employee> employees { get; set; }
    }
}
