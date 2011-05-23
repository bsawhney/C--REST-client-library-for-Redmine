using System;
using System.Xml.Serialization;

namespace Redmine {
    [XmlRoot]
    [XmlType("issue")]
    public class UpdateIssue {
        [XmlIgnore]
        public int id;
        public string notes;
        public int? assigned_to_id;

        [XmlIgnore]
        public bool assigned_to_idSpecified { get { return this.assigned_to_id != null; } }

        public UpdateIssue() { }
        public UpdateIssue(Issue issue, string notes) : this(issue, notes, null) { }

        public UpdateIssue(Issue issue, string notes, int? assigned_to_user_id) {
            this.id = issue.id;
            this.notes = notes;
            this.assigned_to_id = assigned_to_user_id;
        }
    }
}

