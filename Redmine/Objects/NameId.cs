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
    public class NameId {
        [XmlAttribute]
        public string name;
        [XmlAttribute]
        public int id;

        public int Id {
            get { return id; }
        }

        public string Name {
            get { return name; }
        }

        public NameId() : this("unknown", 0) { }

        public NameId(string name, int id) {
            this.name = name;
            this.id = id;
        }

        public override string ToString() {
            return name + " : " + id;
        }
    }
}
