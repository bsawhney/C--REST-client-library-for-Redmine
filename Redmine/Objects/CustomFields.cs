using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Redmine {
    [XmlType("custom_fields")]
    public class CustomFields {
        [XmlAttribute]
        public string type;
        [XmlElement(ElementName = "custom_field")]
        public List<CustomField> list = new List<CustomField>();
    }
}
