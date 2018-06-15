using System;
using System.Collections.Generic;
using System.Text;

namespace P3.Konsole.Parser
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class CommandParameterAttribute : Attribute
    {
        public CommandParameterAttribute(string shortName) {
            ShortName = shortName;
        }

        public string ShortName { get; }
        public string LongName { get; set; }
        public bool IsRequired { get; set; }
        public bool HasValue { get; set; } = true;
    }
}
