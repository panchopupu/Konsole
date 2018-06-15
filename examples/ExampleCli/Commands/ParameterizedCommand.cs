using P3.Konsole.Commands;
using P3.Konsole.Parser;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ExampleCli.Commands
{
    public class ParameterizedCommand : ICommand<Parameters>
    {
        public Task ExecuteAsync(Parameters parameter, CancellationToken token = default(CancellationToken)) {
            Console.WriteLine($"Something: {parameter.Something}");
            Console.WriteLine($"Flag: {parameter.Flag}");
            Console.WriteLine($"Value: {parameter.Value}");

            return Task.CompletedTask;
        }
    }
    public class Parameters
    {
        [CommandParameter("s", LongName = "something", IsRequired = true)]
        public string Something { get; set; }

        [CommandParameter("f", LongName = "flag", HasValue = false)]
        public bool Flag { get; set; }

        [CommandParameter("v", LongName = "value")]
        public decimal Value { get; set; }
    }
}