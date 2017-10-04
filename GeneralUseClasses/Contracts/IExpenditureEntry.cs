using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeneralUseClasses;

namespace GeneralUseClasses.Contracts
{
    public interface IExpenditureEntry
    {
        string dominantTag { get; set; }
        List<string> associatedTags { get; set; }
        List<string> people { get; set; }
        double expenditure { get; set; }
        ExpenditureDate date { get; set; }
    }
}
