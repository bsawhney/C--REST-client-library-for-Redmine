using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Redmine {
    [XmlType("journal")]
    public class Journal {
        private static string szFormat = "{0,12} : {1}";

        [XmlAttribute]
        public int id;
        public NameId user = new NameId();
        public string notes;
        public DateTime created_on;
        public List<Detail> details = new List<Detail>();

        public void dump() {
            Console.WriteLine(("Journal").PadLeft(40, '-') + ("").PadRight(20, '-'));
            Console.WriteLine(szFormat, "journal_id", id);
            Console.WriteLine(szFormat, "user", user);
            Console.WriteLine(szFormat, "notes", notes);
            Console.WriteLine(szFormat, "created_on", created_on);
            foreach (Detail d in details) {
                Console.WriteLine("  " + szFormat, "property", d.property);
                Console.WriteLine("  " + szFormat, "name", d.name);
                Console.WriteLine("  " + szFormat, "old_value", d.old_value);
                Console.WriteLine("  " + szFormat, "new_value", d.new_value);
            }
        }
    }

    [XmlType("detail")]
    public class Detail {
        [XmlAttribute]
        public string property;
        [XmlAttribute]
        public string name;
        public string old_value;
        public string new_value;
    }
}
