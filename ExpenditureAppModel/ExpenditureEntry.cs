using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeneralUseClasses;

namespace ExpenditureAppModel
{
    public class ExpenditureEntry : IComparable<ExpenditureEntry>
    {
        internal int xmlID;
        internal bool budgetItem;
        internal string dominantTag;
        internal List<string> associatedTags;
        internal List<string> people;
        internal double expenditure;
        internal ExpenditureDate date;

        public ExpenditureEntry()
        {
            associatedTags = new List<string>();
            people = new List<string>();
        }

        public ExpenditureEntry(bool budgetItem, string dominantTag, List<string> associatedTags, List<string> people,
            double expenditure, ExpenditureDate date)
        {
            this.budgetItem = budgetItem;
            this.dominantTag = dominantTag;
            this.associatedTags = associatedTags;
            this.people = people;
            this.expenditure = expenditure;
            this.date = date;
        }

        public ExpenditureEntry(int xmlID, bool budgetItem, string dominantTag, List<string> associatedTags, List<string> people,
    double expenditure, ExpenditureDate date)
        {
            this.xmlID = xmlID;
            this.budgetItem = budgetItem;
            this.dominantTag = dominantTag;
            this.associatedTags = associatedTags;
            this.people = people;
            this.expenditure = expenditure;
            this.date = date;
        }

        public int CompareTo(ExpenditureEntry entry)
        {
            // Compares expenditure cost value
            if (entry.expenditure > this.expenditure)
            {
                return 1;
            }
            else if (entry.expenditure == this.expenditure)
            {
                return 0;
            }
            else
            {
                return -1;
            }
        }
    }
}
