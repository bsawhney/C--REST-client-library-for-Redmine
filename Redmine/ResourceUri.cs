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
