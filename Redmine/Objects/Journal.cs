/*
   Copyright 2011 Bobby Sawhney bobbysawhney@gmail.com

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
 */
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
