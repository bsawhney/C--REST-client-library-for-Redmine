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
    [XmlType("custom_field")]
    public class CustomField {
        [XmlAttribute]
        public string name;
        [XmlAttribute]
        public int id;
        public string value;

        public CustomField() : this(null, 0, null) { }
        public CustomField(string name, int id, string value) {
            this.name = name;
            this.id = id;
            this.value = value;
        }
    }
}
