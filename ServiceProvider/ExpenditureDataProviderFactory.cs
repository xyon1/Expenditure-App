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

        public ExpenditureDataProviderFactory(Func<string> getXmlFilePath)
        {
            recorder = new ExtractExpenditureDataXml(getXmlFilePath.Invoke());
        }

        public IProvideExpenditureData GetExpenditureDataProvider()
        {
            return recorder;
        }
    }
}
