using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LAB.OtherClasses
{
    public class Confirmed
    {
        public string Name { get; set; }
        public bool? conf { get; set; }

        public Confirmed(string  Name, bool? conf)
        {
            this.Name = Name;
            this.conf = conf;
        }
    }
}
