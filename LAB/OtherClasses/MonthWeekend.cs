using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LAB.OtherClasses
{
    public class MonthWeekend
    {      

        
        public int NumberOfMonth { get; set; }
        public int MonthWorkDayCount { get; set; }
        public int WeekendsCount { get; set; }

        public MonthWeekend(int NumberOfMonth, int MonthWorkDayCount, int WeekendsCount)
        {
            this.NumberOfMonth = NumberOfMonth;
            this.MonthWorkDayCount = MonthWorkDayCount;
            this.WeekendsCount = WeekendsCount;
        }
    }
}
