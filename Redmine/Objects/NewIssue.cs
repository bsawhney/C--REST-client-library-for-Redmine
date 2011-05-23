using System;
using System.Xml.Serialization;

namespace Redmine {
    [XmlRoot]
    [XmlType("issue")]
    public class NewIssue {
        public int project_id;
        public int tracker_id;
        public string subject;
        public string description;
        public int? assigned_to_id;
        public int? parent_issue_id;

        [XmlIgnore]
        public bool assigned_to_idSpecified { get { return this.assigned_to_id != null; } }
        [XmlIgnore]
        public bool parent_issue_idSpecified { get { return this.parent_issue_id != null; } }

        public NewIssue() { }
        public NewIssue(Project project, Tracker tracker, string subject, string description) :
            this(project.Id, tracker.id, subject, description, null, null) { }

        public NewIssue(Project project, Tracker tracker, string subject, string description, User assignedToUser) :
            this(project.Id, tracker.id, subject, description, assignedToUser.id, null) { }

        public NewIssue(Project project, Tracker tracker, string subject, string description, User assignedToUser, int? parent_issue_id) :
            this(project.Id, tracker.id, subject, description, assignedToUser.id, parent_issue_id) { }

        public NewIssue(int project_id, int tracker_id, string subject, string description) :
            this(project_id, tracker_id, subject, description, null, null) { }

        public NewIssue(int project_id, int tracker_id, string subject, string description, int? assigned_to_user_id, int? parent_issue_id) {
            this.project_id = project_id;
            this.tracker_id = tracker_id;
            this.subject = subject;
            this.description = description;
            this.assigned_to_id = assigned_to_user_id;
            this.parent_issue_id = parent_issue_id;
        }
    }
}
