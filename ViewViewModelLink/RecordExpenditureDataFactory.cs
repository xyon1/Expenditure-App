using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ModelViewModelLink.Services;
using ViewViewModelLink.Contracts;
using XmlClasses;

namespace ViewViewModelLink
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
