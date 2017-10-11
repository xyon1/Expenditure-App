using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeneralUseClasses.Services;

namespace GeneralUseClasses.Contracts
{
    public interface IProvideExpenditureDataProvider
    {
        IProvideExpenditureData GetExpenditureDataProvider();
    }
}
