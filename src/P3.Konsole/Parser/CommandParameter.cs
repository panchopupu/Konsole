using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace P3.Konsole.Parser
{
    public class CommandParameter
    {
        public string ShortName { get; set; }
        public string PropertyName { get; set; }
        public string LongName { get; set; }
        public bool IsRequired { get; set; }
        public bool HasValue { get; set; }

        public static IList<CommandParameter> GetParameters<T>() {
            return typeof(T).GetProperties(BindingFlags.FlattenHierarchy | BindingFlags.Public | BindingFlags.Instance)
                .Select(p => new { Property = p, Attribute = p.GetCustomAttributes(typeof(CommandParameterAttribute)).Cast<CommandParameterAttribute>().SingleOrDefault() })
                .Where(a => a.Attribute != null)
                .Select(a => new CommandParameter() { ShortName = "-" + a.Attribute.ShortName, PropertyName = a.Property.Name, LongName = "--" + a.Attribute.LongName, HasValue = a.Attribute.HasValue, IsRequired = a.Attribute.IsRequired })
                .ToList();
        }
    }
}
