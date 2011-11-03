using System;
using System.Xml.Serialization;

namespace Redmine {
    [XmlType("tracker")]
    public class Tracker {
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

        public Tracker() : this("unknown", 0) { }

        public Tracker(string name, int id) {
            this.name = name;
            this.id = id;
        }

        public override string ToString() {
            return name + " : " + id;
        }
    }
}
