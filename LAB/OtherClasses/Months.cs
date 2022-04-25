using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LAB.OtherClasses
{
    public class Months
    {
       public string NameOfMonth { get; set; }
       public int NumberOfMonth { get; set; }

        public Months(string NameOfMonth, int NumberOfMonth)
        {
            this.NameOfMonth = NameOfMonth;
            this.NumberOfMonth = NumberOfMonth;
        }
    }
}
