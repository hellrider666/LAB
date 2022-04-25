using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LAB.Models
{
    public class Measurement
    {
        public int Id { get; set; }
        public string Measurements { get; set; }
        public  IEnumerable<FinishedProducts> FinishedProducts { get; set; }
        public  IEnumerable<Raw> Raws { get; set; }
        public Measurement()
        {
            FinishedProducts = new HashSet<FinishedProducts>();
            Raws = new HashSet<Raw>();
        }
    }
}

