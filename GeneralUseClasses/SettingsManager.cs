using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralUseClasses
{
    public static class SettingsManager
    {
        public static void CheckForXmlFileDirectory(Func<string> getXmlFilePath, Action<string, string> messageForUser)
        {
            if (string.IsNullOrEmpty(DataStorage.Default.xmlFileDirectory))
            {
                DataStorage.Default.xmlFileDirectory = getXmlFilePath.Invoke();
                DataStorage.Default.Save();
            }
        }
    }
}
