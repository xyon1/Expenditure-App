using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeneralUseClasses.Contracts;
using GeneralUseClasses;

namespace ExpenditureAppViewModels
{
    public class ExpenditureEntry : IExpenditureEntry
    {
        public string dominantTag { get; set; }
        public List<string> associatedTags { get; set; }
        public List<string> people { get; set; }
        public double expenditure { get; set; }
        public ExpenditureDate date { get; set; }

        public ExpenditureEntry(string dominantTag, List<string> associatedTags, List<string> people, double expenditure, ExpenditureDate date)
        {
            this.dominantTag = dominantTag;
            this.associatedTags = associatedTags;
            this.people = people;
            this.expenditure = expenditure;
            this.date = date;
        }
    }
}
