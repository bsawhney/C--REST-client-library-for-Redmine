using System;

namespace Redmine {
    public class ResourceUri {
        private static string PROJECTS_RESOURCE = "projects.xml";
        private static string PROJECT_RESOURCE = "projects/{0}.xml";
        private static string ISSUES_RESOURCE = "issues.xml";
        private static string ISSUE_RESOURCE = "issues/{0}.xml";
        private static string USERS_RESOURCE = "users.xml";

        public static string Projects() {
            return PROJECTS_RESOURCE;
        }

        public static string Project(int id) {
            return string.Format(PROJECT_RESOURCE, id);
        }

        public static string Issues() {
            return ISSUES_RESOURCE;
        }

        public static string Issue(int id) {
            return string.Format(ISSUE_RESOURCE, id);
        }

        public static string Users() {
            return USERS_RESOURCE;
        }
    }
}
