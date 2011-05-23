using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Text;

namespace Redmine {
    [XmlType("issue")]
    public class Issue {
        private static string szFormat = "{0,12} : {1}";
        private static string DATE_FORMAT = "yyyy-mm-dd";

        public int id;
        public NameId parent = new NameId();
        public NameId project = new NameId();
        public NameId tracker = new NameId();
        public NameId status = new NameId();
        public NameId priority = new NameId();
        public NameId author = new NameId();
        public NameId assigned_to = new NameId();
        public NameId cateogry = new NameId();
        public NameId fixed_version = new NameId();
        public string subject;
        public string description;
        public string start_date;
        [XmlIgnore]
        public DateTime start_datetime {
            get { return DateTime.ParseExact(start_date, DATE_FORMAT, null);}
        }
        public string due_date;
        [XmlIgnore]
        public DateTime due_datetime {
            get { return DateTime.ParseExact(due_date, DATE_FORMAT, null); }
        }
        public CustomFields custom_fields = new CustomFields();
        public DateTime created_on;
        public DateTime updated_on;
        public List<Journal> journals = new List<Journal>();

        public void dump() {
            Console.WriteLine(("Issue").PadLeft(40, '-') + ("").PadRight(40, '-'));
            Console.WriteLine(szFormat, "id", id);
            Console.WriteLine(szFormat, "parent", parent.id);
            Console.WriteLine(szFormat, "project", project);
            Console.WriteLine(szFormat, "tracker", tracker);
            Console.WriteLine(szFormat, "status", status);
            Console.WriteLine(szFormat, "priority", priority);
            Console.WriteLine(szFormat, "author", author);
            Console.WriteLine(szFormat, "assigned_to", assigned_to);
            Console.WriteLine(szFormat, "category", cateogry);
            Console.WriteLine(szFormat, "version", fixed_version);
            Console.WriteLine(szFormat, "subject", subject);
            Console.WriteLine(szFormat, "description", description);
            Console.WriteLine(szFormat, "start_date", start_date);
            Console.WriteLine(szFormat, "due_date", due_date);
            foreach (CustomField cf in custom_fields.list) {
                Console.WriteLine(szFormat, cf.name, cf.value);
            }
            Console.WriteLine(szFormat, "created_on", created_on);
            Console.WriteLine(szFormat, "updated_on", updated_on);
            Console.WriteLine(szFormat, "journals", journals.Count);
            foreach (Journal j in journals) {
                j.dump();
            }
        }
    }
}
