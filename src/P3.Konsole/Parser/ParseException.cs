using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace P3.Konsole.Parser
{
    public class ParseException : Exception
    {
        public ParseException() {
        }

        public ParseException(string message) : base(message) {
        }

        public ParseException(string message, Exception innerException) : base(message, innerException) {
        }

        protected ParseException(SerializationInfo info, StreamingContext context) : base(info, context) {
        }
    }

    public class UnknownParameterParseException : Exception
    {
        public UnknownParameterParseException() {
        }

        public UnknownParameterParseException(string message) : base(message) {
        }

        public UnknownParameterParseException(string message, Exception innerException) : base(message, innerException) {
        }

        protected UnknownParameterParseException(SerializationInfo info, StreamingContext context) : base(info, context) {
        }
    }

    public class MissingParameterParseException : ParseException
    {
        public MissingParameterParseException() {
        }

        public MissingParameterParseException(string message) : base(message) {
        }

        public MissingParameterParseException(string message, Exception innerException) : base(message, innerException) {
        }

        protected MissingParameterParseException(SerializationInfo info, StreamingContext context) : base(info, context) {
        }
    }

    public class MissingValueParseException : ParseException
    {
        public MissingValueParseException() {
        }

        public MissingValueParseException(string message) : base(message) {
        }

        public MissingValueParseException(string message, Exception innerException) : base(message, innerException) {
        }

        protected MissingValueParseException(SerializationInfo info, StreamingContext context) : base(info, context) {
        }
    }
}