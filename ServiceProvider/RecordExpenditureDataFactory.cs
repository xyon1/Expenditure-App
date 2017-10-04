using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeneralUseClasses.Contracts;
using GeneralUseClasses.Services;
using XmlClasses;

namespace ServiceProvider
{
    public class RecordExpenditureDataFactory : IRecordExpenditureDataFactory
    {
        private IRecordExpenditureData recorder;

        public RecordExpenditureDataFactory()
        {
            recorder = new RecordExpenditureDataXml();
        }

        public IRecordExpenditureData GetExpenditureDataRecorder()
        {
            return recorder;
        }
    }
}
