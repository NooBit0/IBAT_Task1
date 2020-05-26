using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows;
using System.Xml.Linq;
using Task1.Model;

namespace Task1.FileExtensions
{
    public class FileExtension
    {
        public static List<User> GetFromFile(string path)
        {
            try
            {
                using var reader = new StreamReader(path);
                using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
                csv.Configuration.HasHeaderRecord = false;
                csv.Configuration.Delimiter = ";";
                csv.Configuration.RegisterClassMap<CollectionMap>();
                return csv.GetRecords<User>().ToList();
            }
            catch (Exception)
            {
                MessageBox.Show("Error open file! File is already open.");
                return null;
            }
        }

        public static void SaveToCsvFile(string path, List<User> users)
        {
            try
            {
                using StreamWriter streamReader = new StreamWriter(path);
                using CsvWriter csvReader = new CsvWriter(streamReader, CultureInfo.InvariantCulture);
                csvReader.Configuration.HasHeaderRecord = false;
                csvReader.Configuration.Delimiter = ";";
                csvReader.Configuration.RegisterClassMap<CollectionMap>();
                csvReader.WriteRecords(users);
                MessageBox.Show("Report saved!");
            }
            catch (Exception)
            {
                MessageBox.Show("Error save to file! File is already open.");
            }
        }

        public static void SaveToXmlFile(string path, List<User> users)
        {
            try
            {
                XElement elements = new XElement("TestProgram",
                users.Select(item => new XElement("Record",
                    new XAttribute("id", item.UserId),
                        new XElement("Date", item.Date),
                        new XElement("FirstName", item.FirstName),
                        new XElement("LastName", item.LastName),
                        new XElement("SurName", item.SurName),
                        new XElement("City", item.City),
                        new XElement("Country", item.Country))));
                elements.Save(path);
                MessageBox.Show("Report saved!");
            }
            catch (Exception)
            {
                MessageBox.Show("Error save to file! File is already open.");
            }
}
    }
}
