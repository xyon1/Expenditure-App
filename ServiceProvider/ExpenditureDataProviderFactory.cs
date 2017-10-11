using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeneralUseClasses.Contracts;
using GeneralUseClasses.Services;
using XmlClasses;

namespace ServiceProvider
{
    public class ExpenditureDataProviderFactory : IProvideExpenditureDataProvider
    {
        private IProvideExpenditureData recorder;

        public ExpenditureDataProviderFactory()
        {
            recorder = new ExtractExpenditureDataXml();
        }

        public IProvideExpenditureData GetExpenditureDataProvider()
        {
            return recorder;
        }
    }
}
