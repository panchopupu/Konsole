using P3.Konsole.Parser;
using System;
using System.Collections.Generic;
using System.Text;

namespace P3.Konsole.Tests.Assets
{
    public class TestParameters
    {
        [CommandParameter("s", LongName = "string", IsRequired = true)]
        public string String { get; set; }

        [CommandParameter("f", LongName = "flag", HasValue = false)]
        public bool Flag { get; set; }

        [CommandParameter("v", LongName = "value")]
        public decimal Value { get; set; }

        [CommandParameter("d", LongName = "date")]
        public DateTime DateTime { get; set; }
    }
}
