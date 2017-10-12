using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using GeneralUseClasses.Contracts;
using GeneralUseClasses.Services;
using GeneralUseClasses;
using ExpenditureAppModel;

namespace XmlClasses
{
    public class ExtractExpenditureDataXml : IProvideExpenditureData
    {
        private string xmlFilePath;

        public ExtractExpenditureDataXml(string xmlFilePath)
        {
            this.xmlFilePath = xmlFilePath;
        }

        /// <summary>
        /// extracts expenditures from xml file found at xmlFilePath based on format of an entryTemplate. There are 6 extraction
        /// types - 0: dominant tag, 1: associated tags, 2: people, 3: date, 4: expenditure, 5: xmlID
        /// </summary>
        //internal static List<IExpenditureEntry> ExtractExpendituresFromXml(IExpenditureEntry entryTemplate, string xmlFilePath,
        //    int extractionType)
        //{
        //    List<IExpenditureEntry> entries = new List<IExpenditureEntry>();
        //    XmlDocument doc = new XmlDocument();
        //    doc.Load(xmlFilePath);
        //    foreach (XmlNode node in doc.ChildNodes)
        //    {
        //        if (node.Name == "Expenditures")
        //        {
        //            foreach (XmlNode IExpenditureEntry in node.ChildNodes)
        //            {
        //                if (IExpenditureEntry.Name != "LatestID")
        //                {
        //                    switch (IExpenditureEntry.Attributes[0].Value) // For  version compatibility
        //                    {
        //                        case "1":
        //                            IExpenditureEntry entry = ReadData(IExpenditureEntry);
        //                            switch (extractionType)
        //                            {
        //                                case 0: // Base on Dominant Tag
        //                                    if (entry.dominantTag == entryTemplate.dominantTag)
        //                                    {
        //                                        entries.Add(entry);
        //                                    }
        //                                    break;
        //                                case 1: // Base on Associated Tags
        //                                    foreach (string tag in entryTemplate.associatedTags)
        //                                    {
        //                                        if (entry.associatedTags.Contains(tag))
        //                                        {
        //                                            entries.Add(entry);
        //                                        }
        //                                    }
        //                                    break;
        //                                case 2: // Base on People
        //                                    foreach (string person in entryTemplate.people)
        //                                    {
        //                                        if (entry.people.Contains(person))
        //                                        {
        //                                            entries.Add(entry);
        //                                        }
        //                                    }
        //                                    break;
        //                                case 3: // Base on Date
        //                                    if (entry.date.IsTheSameDateAs(entryTemplate.date))
        //                                    {
        //                                        entries.Add(entry);
        //                                    }
        //                                    break;
        //                                case 4: // Base on Expenditure
        //                                    break;
        //                                case 5: // Base on ID
        //                                    break;
        //                                default:
        //                                    break;
        //                            }
        //                            break;
        //                        default:
        //                            break;
        //                    }
        //                }

        //            }
        //        }
        //    }
        //    return entries;
        //}

        //internal static List<IExpenditureEntry> ExtractAllExpendituresFromXml(string xmlFilePath)
        //{
        //    List<IExpenditureEntry> entries = new List<IExpenditureEntry>();
        //    XmlDocument doc = new XmlDocument();
        //    doc.Load(xmlFilePath);
        //    foreach (XmlNode node in doc.ChildNodes)
        //    {
        //        if (node.Name == "Expenditures")
        //        {
        //            foreach (XmlNode IExpenditureEntry in node.ChildNodes)
        //            {
        //                if (IExpenditureEntry.Name != "LatestID")
        //                {
        //                    switch (IExpenditureEntry.Attributes[0].Value) // For  version compatibility
        //                    {
        //                        case "1":
        //                            IExpenditureEntry entry = ReadData(IExpenditureEntry);
        //                            entries.Add(entry);
        //                            break;
        //                        default:
        //                            break;
        //                    }
        //                }

        //            }
        //        }
        //    }
        //    return entries;
        //}

        private static ExpenditureEntry ReadData(XmlNode IExpenditureEntry)
        {
            int day = 0;
            int month = 0;
            int year = 0;
            int xmlID = 0;
            bool budgetItem = false;
            string dominantTag = "";
            List<string> associatedTags = new List<string>();
            List<string> people = new List<string>();
            double expenditure = 0;
            ExpenditureDate expenditureDate = new ExpenditureDate();
            foreach (XmlNode expenditureData in IExpenditureEntry.ChildNodes)
            {
                switch (expenditureData.Name)
                {
                    case "ID":
                        xmlID = int.Parse(expenditureData.InnerText);
                        break;
                    case "BudgetItem":
                        budgetItem = expenditureData.InnerText == "true" ? true : false;
                        break;
                    case "DominantTag":
                        dominantTag = expenditureData.InnerText;
                        break;
                    case "AssociatedTags":
                        foreach (XmlNode asTag in expenditureData.ChildNodes)
                        {
                            associatedTags.Add(asTag.InnerText);
                        }
                        break;
                    case "People":
                        foreach (XmlNode p in expenditureData.ChildNodes)
                        {
                            people.Add(p.InnerText);
                        }
                        break;
                    case "Expenditure":
                        expenditure = double.Parse(expenditureData.InnerText);
                        break;
                    case "Date":
                        foreach (XmlNode item in expenditureData.ChildNodes)
                        {
                            switch (item.Name)
                            {
                                case "Day":
                                    day = int.Parse(item.InnerText);
                                    break;
                                case "Month":
                                    month = int.Parse(item.InnerText);
                                    break;
                                case "Year":
                                    year = int.Parse(item.InnerText);
                                    break;
                                default:
                                    break;
                            }
                            expenditureDate = new ExpenditureDate(day, month, year);
                        }
                        break;
                    default:
                        break;
                }
            }
            return new ExpenditureEntry(xmlID, budgetItem, dominantTag, associatedTags, people, expenditure, expenditureDate);
        }


        private List<string> ExtractTagsIntoList(int extractionType)
        {
            List<string> tagsList = new List<string>();
            if (File.Exists(xmlFilePath))
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(xmlFilePath);
                foreach (XmlNode node in doc.ChildNodes)
                {
                    if (node.Name == "Expenditures")
                    {
                        foreach (XmlNode ExpenditureEntry in node.ChildNodes)
                        {
                            if (ExpenditureEntry.Name != "LatestID")
                            {
                                ExpenditureEntry entry = ReadData(ExpenditureEntry);
                                switch (extractionType)
                                {
                                    case 0: // extract Dominant Tags
                                        if (!(tagsList.Contains(entry.dominantTag)))
                                        {
                                            tagsList.Add(entry.dominantTag);
                                        }
                                        break;
                                    case 1: // extract Associated Tags
                                        foreach (string associatedTag in entry.associatedTags)
                                        {
                                            if (!(tagsList.Contains(associatedTag)))
                                            {
                                                tagsList.Add(associatedTag);
                                            }
                                        }
                                        break;
                                    case 2: // extract people
                                        foreach (string person in entry.people)
                                        {
                                            if (!(tagsList.Contains(person)))
                                            {
                                                tagsList.Add(person);
                                            }
                                        }
                                        break;
                                }
                            }
                        }
                    }
                }
            }
            return tagsList;
        }

        internal static int GetLatestID(string path)
        {
            int ID = 0;
            if (File.Exists(path))
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(path);
                foreach (XmlNode node in doc.ChildNodes)
                {
                    if (node.Name == "Expenditures")
                    {
                        var IDnode = node.ChildNodes[0];
                        ID = int.Parse(IDnode.InnerText);
                        break;
                    }
                }
            }
            return ID;
        }

        public IEnumerable<string> GetDominantTags()
        {
            return ExtractTagsIntoList(0);
        }

        public IEnumerable<string> GetAssociatedTags()
        {
            return ExtractTagsIntoList(1);
        }

        public IEnumerable<string> GetPeople()
        {
            return ExtractTagsIntoList(2);
        }

        //internal static List<IExpenditureEntry> GetLastFiveEntries(string xmlFilePath)
        //{
        //    if (File.Exists(xmlFilePath))
        //    {
        //        List<IExpenditureEntry> lastFiveEntries = new List<IExpenditureEntry>();
        //        int LatestID = GetLatestID(xmlFilePath);
        //        XmlDocument doc = new XmlDocument();
        //        doc.Load(xmlFilePath);
        //        foreach (XmlNode node in doc.ChildNodes)
        //        {
        //            if (node.Name == "Expenditures")
        //            {
        //                foreach (XmlNode IExpenditureEntry in node.ChildNodes)
        //                {
        //                    if (IExpenditureEntry.Name != "LatestID")
        //                    {
        //                        IExpenditureEntry entry = ReadData(IExpenditureEntry);
        //                        bool inTheLastFive = false;
        //                        inTheLastFive = (entry.xmlID > LatestID - 5) ? true : false;
        //                        if (inTheLastFive)
        //                        {
        //                            lastFiveEntries.Add(entry);
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //        return lastFiveEntries;
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}

    }
}
