using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeneralUseClasses.Contracts;
using GeneralUseClasses.Services;
using XmlClasses;

namespace ServiceProvider
{
    public class ExpenditureDataRecorderFactory : IProvideExpenditureDataRecorder
    {
        private IRecordExpenditureData recorder;

        public ExpenditureDataRecorderFactory(Func<string> getXmlFilePath, Action<string, string> messageForUser)
        {
            SettingsManager.CheckForXmlFileDirectory(getXmlFilePath, messageForUser);

            recorder = new RecordExpenditureDataXml(DataStorage.Default.xmlFileDirectory);
        }

        public IRecordExpenditureData GetExpenditureDataRecorder()
        {
            return recorder;
        }
    }
}
