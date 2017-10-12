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

        public ExpenditureDataRecorderFactory(Func<string> getXmlFilePath)
        {
            recorder = new RecordExpenditureDataXml(getXmlFilePath.Invoke());
        }

        public IRecordExpenditureData GetExpenditureDataRecorder()
        {
            return recorder;
        }
    }
}
