using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeneralUseClasses;
using GeneralUseClasses.Contracts;

namespace ExpenditureAppModel
{
    public class ExpenditureEntry : IExpenditureEntry, IComparable<ExpenditureEntry>
    {
        public bool budgetItem;
        public string dominantTag { get; set; }
        public List<string> associatedTags { get; set; }
        public List<string> people { get; set; }
        public double expenditure { get; set; }
        public ExpenditureDate date { get; set; }

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
