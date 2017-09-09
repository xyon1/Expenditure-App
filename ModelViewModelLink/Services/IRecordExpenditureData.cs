using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelViewModelLink.Contracts;

namespace ModelViewModelLink.Services
{
    public interface IRecordExpenditureData
    {
        //IExpenditureEntry ExpenditureData { get; set; }

        void RecordExpenditureData(IExpenditureEntry entry);
    }
}
