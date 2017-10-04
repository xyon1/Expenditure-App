using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeneralUseClasses.Contracts;

namespace GeneralUseClasses.Services
{
    public interface IRecordExpenditureData
    {
        //IExpenditureEntry ExpenditureData { get; set; }

        void RecordExpenditureData(IExpenditureEntry entry);
    }
}
