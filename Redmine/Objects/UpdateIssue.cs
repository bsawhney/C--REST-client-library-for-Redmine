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

