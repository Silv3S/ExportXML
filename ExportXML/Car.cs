using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace ExportXML
{
    [Serializable()]
    public class Car  : IEquatable<Car>
    {
        [XmlAttribute("VIN")]
        public string VIN { get; set; }
        
        [XmlElement("ProductionYear")]
        public string ProductionYear{ get; set; }

        [XmlElement("Model")]
        public string Model { get; set; }

        [XmlArray(ElementName = "Features")]
        public List<Feature> Features { get; set; }

        public Car()
        {

        }

        public Car(string vin, string productionYear, string model, List<Feature> features)
        {
            this.VIN = vin;
            this.ProductionYear = productionYear;
            this.Model = model;
            this.Features = features;
        }

        public bool Equals(Car other)
        {
            if (other == null)
                return false;

            bool areListsEqual = this.Features.SequenceEqual(other.Features);

            return this.VIN.Equals(other.VIN) && this.Model.Equals(other.Model) && this.ProductionYear.Equals(other.ProductionYear) && areListsEqual;
        }

        public static string Serialize(List<Car> cars, string saveDirectory, bool saveToXML)
        {
            XmlRootAttribute rootAttributeXML = new XmlRootAttribute("Cars");
            XmlSerializerNamespaces namespacesSerializerXML = new XmlSerializerNamespaces();
            namespacesSerializerXML.Add("", "");
            XmlSerializer serializerXML = new XmlSerializer(typeof(List<Car>), rootAttributeXML);

            if (saveToXML)
            {
                using (Stream stream = File.Create(saveDirectory))
                {
                    serializerXML.Serialize(stream, cars, namespacesSerializerXML);
                }
            }

            using (StringWriter stringWriter = new StringWriter())
            {
                serializerXML.Serialize(stringWriter, cars, namespacesSerializerXML);
                return stringWriter.ToString();
            }
        }

        public override string ToString()
        {
            string featuresList = string.Join("\n\t\t", Features);
            return "VIN " + VIN + " | " + Model + " wyprodukowana w " + ProductionYear+" roku\n\t\t"+featuresList ;
        }
    }
}
