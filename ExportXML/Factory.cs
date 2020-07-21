using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace ExportXML
{
    [Serializable()]
    public class Factory : IEquatable<Factory>
    {
        [XmlArray(ElementName = "ProducedCars")]
        public List<Car> ProducedCars { get; set; }

        [XmlAttribute("Name")]
        public string FactoryName { get; set; }

        public Factory()
        {

        }

        public Factory(string factoryName, List<Car> producedCars)
        {
            this.FactoryName = factoryName;
            this.ProducedCars = producedCars;
        }

        public bool Equals(Factory other)
        {
            if (other == null)
                return false;

            bool areListsEqual = this.ProducedCars.SequenceEqual(other.ProducedCars);

            return this.FactoryName.Equals(other.FactoryName) && areListsEqual;
        }

        public override string ToString()
        {
            string carList = string.Join("\n\t", ProducedCars);
            return FactoryName + "\n\t" + carList;
        }
    }
}
