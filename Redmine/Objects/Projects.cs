using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Redmine {
    [XmlRoot]
    [XmlType("projects")]
    public class Projects {
        private static string szFormat = "{0,12} : {1}";

        [XmlAttribute]
        public int total_count;
        [XmlAttribute]
        public string type;
        [XmlAttribute]
        public int limit;
        [XmlAttribute]
        public int offset;
        [XmlElement(ElementName = "project")]
        public List<Project> list = new List<Project>();

        public Project getProjectByID(int ID) {
            Project project = null;
            foreach (Project o in list) {
                if (o.Id == ID) {
                    project = o;
                    break;
                }
            }
            return project;
        }

        public Project getProjectByName(string name) {
            Project project = null;
            foreach (Project o in list) {
                if (System.String.Compare(o.name, name, true) == 0) {
                    project = o;
                    break;
                }
            }
            return project;
        }

        public Project getProjectByIdentifier(string identifier) {
            Project project = null;
            foreach (Project o in list) {
                if (System.String.Compare(o.identifier, identifier, true) == 0) {
                    project = o;
                    break;
                }
            }
            return project;
        }

        public int Count {
            get { return list.Count; }
        }

        public void dump(string label) {
            Console.WriteLine((label + " start").PadLeft(40, '-') + ("").PadRight(40, '-'));
            foreach (Project o in list) {
                o.dump();
            }
            Console.WriteLine((label + " stats").PadLeft(40, '-') + ("").PadRight(40, '-'));
            Console.WriteLine(szFormat, "total_count", total_count);
            Console.WriteLine(szFormat, "type", type);
            Console.WriteLine(szFormat, "limit", limit);
            Console.WriteLine(szFormat, "offset", offset);
            Console.WriteLine((label + " end").PadLeft(40, '-') + ("").PadRight(40, '-'));
        }
    }
}

// <projects total_count="11" type="array" limit="25" offset="0">