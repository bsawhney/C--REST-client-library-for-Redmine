using System;
using System.Xml.Serialization;

namespace Redmine {
    public class NameId {
        [XmlAttribute]
        public string name;
        [XmlAttribute]
        public int id;

        public int Id {
            get { return id; }
        }

        public string Name {
            get { return name; }
        }

        public NameId() : this("unknown", 0) { }

        public NameId(string name, int id) {
            this.name = name;
            this.id = id;
        }

        public override string ToString() {
            return name + " : " + id;
        }
    }
}
