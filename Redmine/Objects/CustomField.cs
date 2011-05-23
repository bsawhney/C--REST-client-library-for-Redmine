using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Redmine {
    [XmlType("custom_field")]
    public class CustomField {
        [XmlAttribute]
        public string name;
        [XmlAttribute]
        public int id;
        public string value;

        public CustomField() : this(null, 0, null) { }
        public CustomField(string name, int id, string value) {
            this.name = name;
            this.id = id;
            this.value = value;
        }
    }
}
