using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Redmine {
    public class RedmineObjectNotFoundException : RedmineException {
        public RedmineObjectNotFoundException(string message) : base(message) { }
    }
}
