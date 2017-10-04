using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelViewModelLink.Services;

namespace GeneralUseClasses.Contracts
{
    public interface IRecordExpenditureDataFactory
    {
        IRecordExpenditureData GetExpenditureDataRecorder();
    }
}
