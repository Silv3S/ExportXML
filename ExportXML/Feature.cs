using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace ExportXML
{
    public class Feature : IEquatable<Feature>
    {
        [XmlAttribute("Code")]
        public string Code{ get; set; }

        [XmlText]
        public string Description { get; set; }

        internal Feature()
        {
        
        }

        public Feature(string code, string description)
        {
            this.Code = code;
            this.Description = description;
        }

        public bool Equals(Feature other)
        {
            if (other == null)
                return false;           

            return
                this.Code.Equals(other.Code)  && this.Description.Equals(other.Description);
        }

        public override string ToString()
        {
            return Code + " - " + Description;
        }
    }
}
