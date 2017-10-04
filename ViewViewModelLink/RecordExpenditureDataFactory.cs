﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeneralUseClasses.Contracts;
using GeneralUseClasses.Services;


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
