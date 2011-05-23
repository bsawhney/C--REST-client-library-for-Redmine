using System;
using System.Xml.Serialization;

namespace Redmine {
    [XmlType("user")]
    public class User {
        private static string szFormat = "{0,15} : {1}";

        public int id;
        public string login;
        public string firstname;
        public string lastname;
        public string mail;
        public string created_on;
        [XmlIgnore]
        public DateTime created_ontime {
            get { return DateTime.Parse(created_on); }
        }
        public string last_login_on;
        [XmlIgnore]
        public DateTime last_login_ontime {
            get { return DateTime.Parse(last_login_on); }
        }

        public void dump() {
            Console.WriteLine(("User").PadLeft(40, '-') + ("").PadRight(40, '-'));
            Console.WriteLine(szFormat, "id", id);
            Console.WriteLine(szFormat, "login", login);
            Console.WriteLine(szFormat, "firstname", firstname);
            Console.WriteLine(szFormat, "lastname", lastname);
            Console.WriteLine(szFormat, "mail", mail);
            Console.WriteLine(szFormat, "created on", created_on);
            Console.WriteLine(szFormat, "last login on", last_login_on);
        }
    }
}
/*
<user>
    <id>1</id>
    <login>admin</login>
    <firstname>Redmine</firstname>
    <lastname>Admin</lastname>
    <mail>redmine.admin@redmine.org</mail>
    <created_on>2011-03-11T18:57:20-08:00</created_on>
    <last_login_on>2011-03-29T13:44:00-07:00</last_login_on>
</user>
*/
