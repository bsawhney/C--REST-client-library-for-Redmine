using System;

namespace Redmine {
    public class RedmineException : Exception {
        public RedmineException(string message) : base(message) { }
    }
}
