using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Linq;
using System.Xml;

namespace ExportXML
{
    [Serializable()]
    public class ProductionReport : IEquatable<ProductionReport>
    {
        [XmlAttribute("Manufacturer")]
        public string Manufacturer { get; set; }

        [XmlAttribute("Date")]
        public string Date { get; set; }

        [XmlArray(ElementName = "Factories")]
        public List<Factory> Factories { get; set; }

        public List<Car> GetProducedCarsList()
        {
            List<Car> cars = this.Factories.SelectMany(f => f.ProducedCars).ToList();
            return cars;
        }

        public ProductionReport()
        {

        }

        public ProductionReport(string manufacturer, string date, List<Factory> factories)
        {
            this.Manufacturer = manufacturer;
            this.Date = date;
            this.Factories = factories;
        }

        public bool Equals(ProductionReport other)
        {
            if (other == null)
                return false;

            bool areListsEqual = this.Factories.SequenceEqual(other.Factories);

            return this.Manufacturer.Equals(other.Manufacturer) && this.Date.Equals(other.Date) && areListsEqual;
        }

        public static ProductionReport DeserializeProductionReport(string pathToXML)
        {
            XmlReaderSettings settingsReaderXML = new XmlReaderSettings
            {
                IgnoreComments = true,
                IgnoreWhitespace = true
            };
            XmlReader readerXML = XmlReader.Create(pathToXML, settingsReaderXML);
            XmlSerializer serializerXML = new XmlSerializer(typeof(ProductionReport));
            ProductionReport productionReport = (ProductionReport)serializerXML.Deserialize(readerXML);
            readerXML.Close();
            return productionReport;
        }

        public override string ToString()
        {
            string factoriesList = string.Join("\n", Factories);
            return Manufacturer + " production report | " + Date + "\n" + factoriesList;
        }
    }   
}
