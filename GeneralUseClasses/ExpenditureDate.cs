using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeneralUseClasses.Exceptions;

namespace GeneralUseClasses

{
    public class ExpenditureDate
    {
        public int day;
        public int month;
        public int year;

        public ExpenditureDate()
        {
            day = new int();
            month = new int();
            year = new int();
        }

        public ExpenditureDate(int day, int month, int year)
        {
            this.day = day;
            this.month = month;
            FormatYear(year);
        }

        private void FormatYear(int year)
        {
            if (year.ToString().Length == 4)
            {
                this.year = year; ;
            }
            else if (year.ToString().Length == 2 || year.ToString().Length == 1)
            {
                this.year = year + 2000;
            }
            else
            {
                throw new ExceptionForUser("Year is in unusable format");
            }
        }

        public bool IsNotEarlierThan(ExpenditureDate date)
        {
            bool isLaterThan = true;
            if (!(this.year >= date.year))
            {
                isLaterThan = false;
            }
            else if (!(this.month >= date.month) && this.year == date.year)
            {
                isLaterThan = false;
            }
            else if (!(this.day >= date.day) && this.month == date.month && this.year == date.year)
            {
                isLaterThan = false;
            }

            return isLaterThan;
        }

        public bool IsNotLaterThan(ExpenditureDate date)
        {
            bool isEarlierThan = true;
            if (!(this.year <= date.year))
            {
                isEarlierThan = false;
            }
            else if (!(this.month <= date.month) && this.year == date.year)
            {
                isEarlierThan = false;
            }
            else if (!(this.day <= date.day) && this.month == date.month && this.year == date.year)
            {
                isEarlierThan = false;
            }

            return isEarlierThan;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(this.day.ToString("D2"));
            sb.Append("/");
            sb.Append(this.month.ToString("D2"));
            sb.Append("/");
            sb.Append(this.year);
            return sb.ToString();
        }

        public bool IsTheSameDateAs(ExpenditureDate date)
        {
            bool sameDate = false;
            sameDate = date.day == this.day ? true : false;
            sameDate = (date.month == this.month) && sameDate ? true : false;
            sameDate = (date.year == this.year) && sameDate ? true : false;

            return sameDate;
        }
    }
}
