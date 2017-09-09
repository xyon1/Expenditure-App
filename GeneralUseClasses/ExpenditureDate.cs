using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralUseClasses

{
    public class ExpenditureDate
    {
        public int day { get; set; }
        public int month { get; set; }
        public int year { get; set; }

        public ExpenditureDate(int day, int month, int year)
        {
            this.day = day;
            this.month = month;
            this.year = year;
        }
    }
}
