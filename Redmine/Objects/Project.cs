using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Redmine {
    /// <summary>
    /// Redmine project object that can be serialized and deserialized to XML.
    /// The Id and Name fields are exposed as properties so that they this
    /// object can be used as a datasource for UI elements as well.
    /// </summary>
    [XmlType("project")]
    public class Project {
        private static string szFormat = "{0,12} : {1}";

        public int id;
        public string name;
        public string identifier;
        public string description;
        public DateTime created_on;
        public DateTime updated_on;
        public List<Tracker> trackers = new List<Tracker>();

        public int Id {
            get { return id; }
        }

        public string Name {
            get { return name; }
        }

        public Tracker getTrackerByName(string name) {
            Tracker tracker = null;
            foreach (Tracker o in trackers) {
                if (System.String.Compare(o.name, name, true) == 0) {
                    tracker = o;
                    break;
                }
            }
            return tracker;
        }

        public void dump() {
            Console.WriteLine(("Project").PadLeft(40, '-') + ("").PadRight(40, '-'));
            Console.WriteLine(szFormat, "id", id);
            Console.WriteLine(szFormat, "identifier", identifier);
            Console.WriteLine(szFormat, "name", name);
            Console.WriteLine(szFormat, "description", description);
            Console.WriteLine(szFormat, "created on", created_on);
            Console.WriteLine(szFormat, "trackers", trackers.Count);
            foreach (Tracker tracker in trackers) {
                Console.WriteLine("\t" + szFormat, "tracker", tracker);
            }
        }
    }
}
