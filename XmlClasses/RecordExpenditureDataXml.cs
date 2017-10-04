﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using GeneralUseClasses.Services;
using GeneralUseClasses.Contracts;

namespace XmlClasses
{
    public class RecordExpenditureDataXml : IRecordExpenditureData
    {
        private string xmlFilePath;
        private static int latestID = 0;

        public RecordExpenditureDataXml(string directory)
        {
            xmlFilePath = directory + @"\Expenditure.xml";
        }

        public void RecordExpenditureData(IExpenditureEntry expenditureEntry)
        {
            if (File.Exists(xmlFilePath))
            {
                UpdateXmlFile(expenditureEntry, xmlFilePath);
            }
            else
            {
                XmlWriterSettings settings = new XmlWriterSettings();
                settings.Indent = true;
                settings.CheckCharacters = true;
                CreateXmlFile(expenditureEntry, settings, xmlFilePath);
            }
        }

        internal static void CreateXmlFile(IExpenditureEntry expenditureEntry, XmlWriterSettings settings, string xmlFilePath)
        {
            using (XmlWriter writer = XmlWriter.Create(xmlFilePath, settings))
            {
                writer.WriteStartDocument();

                writer.WriteStartElement("Expenditures");
                writer.WriteStartElement("LatestID");
                writer.WriteElementString("LatestID", "0");
                writer.WriteEndElement();

                writer.WriteEndDocument();
            }
            UpdateXmlFile(expenditureEntry, xmlFilePath);
        }

        internal static void UpdateXmlFile(IExpenditureEntry expenditureEntry, string xmlFilePath)
        {
            string currentVersion = "1"; //Change when change xml!
            XmlDocument doc = new XmlDocument();
            doc.Load(xmlFilePath);
            //int ID = XmlDataExtractor.GetLatestID(xmlFilePath) + 1;
            int ID = latestID + 1;

            foreach (XmlNode node in doc.ChildNodes)
            {
                if (node.Name == "Expenditures")
                {
                    createNewEntry(doc, node, currentVersion, ID, expenditureEntry);

                    node.ChildNodes[0].InnerText = ID.ToString();
                }
            }

            doc.Save(xmlFilePath);
        }

        private static void createNewEntry(XmlDocument doc, XmlNode node, string currentVersion, int ID, IExpenditureEntry entry)
        {
            XmlElement newExpenditureEntry = doc.CreateElement("ExpenditureEntry");
            XmlAttribute versionNumber = doc.CreateAttribute("version");
            versionNumber.Value = currentVersion;
            newExpenditureEntry.Attributes.Append(versionNumber);

            XmlElement iDElement = doc.CreateElement("ID");
            iDElement.InnerText = ID.ToString();

            XmlElement dominantTag = doc.CreateElement("DominantTag");
            dominantTag.InnerText = entry.dominantTag;

            XmlElement associatedTags = doc.CreateElement("AssociatedTags");
            foreach (string tag in entry.associatedTags)
            {
                XmlElement asTag = doc.CreateElement("Tag");
                asTag.InnerText = tag;
                associatedTags.AppendChild(asTag);
            }

            XmlElement people = doc.CreateElement("People");
            foreach (string person in entry.people)
            {
                XmlElement p = doc.CreateElement("Person");
                p.InnerText = person;
                people.AppendChild(p);
            }

            XmlElement expenditure = doc.CreateElement("Expenditure");
            expenditure.InnerText = entry.expenditure.ToString("0.00");

            XmlElement newExpenditureDate = doc.CreateElement("Date");
            XmlElement newExpenditureDay = doc.CreateElement("Day");
            newExpenditureDay.InnerText = entry.date.day.ToString("00");
            XmlElement newExpenditureMonth = doc.CreateElement("Month");
            newExpenditureMonth.InnerText = entry.date.month.ToString("00");
            XmlElement newExpenditureYear = doc.CreateElement("Year");
            newExpenditureYear.InnerText = entry.date.year.ToString();
            newExpenditureDate.AppendChild(newExpenditureDay);
            newExpenditureDate.AppendChild(newExpenditureMonth);
            newExpenditureDate.AppendChild(newExpenditureYear);


            newExpenditureEntry.AppendChild(iDElement);
            newExpenditureEntry.AppendChild(dominantTag);
            newExpenditureEntry.AppendChild(associatedTags);
            newExpenditureEntry.AppendChild(people);
            newExpenditureEntry.AppendChild(expenditure);
            newExpenditureEntry.AppendChild(newExpenditureDate);

            node.AppendChild(newExpenditureEntry);
        }

        internal static void RemoveExpenditureEntry(int removalID, string xmlFilePath)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(xmlFilePath);
            XmlNode childToRemove = doc.CreateElement("h"); // To satisfy declaration - never used in this form
            bool removeChild = false;
            foreach (XmlNode node in doc.ChildNodes)
            {
                if (node.Name == "Expenditures")
                {
                    foreach (XmlNode entry in node.ChildNodes)
                    {
                        if (entry.Name != "LatestID")
                        {
                            foreach (XmlNode info in entry.ChildNodes)
                            {
                                if (info.Name == "ID")
                                {
                                    int ID = int.Parse(info.InnerText);
                                    if (ID == removalID)
                                    {
                                        childToRemove = entry;
                                        removeChild = true;

                                    }
                                    else if (ID > removalID)
                                    {
                                        info.InnerText = (ID - 1).ToString();
                                    }
                                }
                            }
                        }
                    }
                    var oldID = int.Parse(node.ChildNodes.Item(0).InnerText);
                    var newID = (oldID - 1).ToString();
                    node.ChildNodes[0].InnerText = newID;
                }
                if (removeChild)
                {
                    node.RemoveChild(childToRemove);
                }
            }
            doc.Save(xmlFilePath);
        }

        private static string RemoveWhiteSpace(string tag)
        {
            char[] tagCharArray = tag.ToCharArray();
            List<char> newCharList = new List<char>();
            foreach (char character in tagCharArray)
            {
                if (character != ' ')
                {
                    newCharList.Add(character);
                }
            }
            StringBuilder sb = new StringBuilder();
            foreach (char character in newCharList)
            {
                sb.Append(character);
            }
            return sb.ToString();
        }
    }
}
