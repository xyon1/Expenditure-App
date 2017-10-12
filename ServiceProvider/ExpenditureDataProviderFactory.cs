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

        public ExpenditureDataProviderFactory(Func<string> getXmlFilePath, Action<string, string> messageForUser)
        {
            SettingsManager.CheckForXmlFileDirectory(getXmlFilePath, messageForUser);

            recorder = new ExtractExpenditureDataXml(DataStorage.Default.xmlFileDirectory);
        }

        public IProvideExpenditureData GetExpenditureDataProvider()
        {
            return recorder;
        }
    }
}
