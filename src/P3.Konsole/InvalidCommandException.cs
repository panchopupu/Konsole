using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace P3.Konsole
{
    public class InvalidCommandException : Exception
    {
        public InvalidCommandException() {
        }

        public InvalidCommandException(string message) : base(message) {
        }

        public InvalidCommandException(string message, Exception innerException) : base(message, innerException) {
        }

        protected InvalidCommandException(SerializationInfo info, StreamingContext context) : base(info, context) {
        }
    }
}