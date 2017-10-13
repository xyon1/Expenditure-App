using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceProvider
{
    public static class SettingsManager
    {
        internal static void CheckForXmlFileDirectory(Func<string> getXmlFilePath, Action<string, string> messageForUser)
        {
            if (string.IsNullOrEmpty(DataStorage.Default.xmlFileDirectory))
            {
                DataStorage.Default.xmlFileDirectory = getXmlFilePath.Invoke();
                DataStorage.Default.Save();
            }
        }
    }
}
