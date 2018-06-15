using P3.Konsole.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ExampleCli.Commands
{
    public class PrintCommand : ICommand<string[]>
    {
        public Task ExecuteAsync(string[] parameter, CancellationToken token = default(CancellationToken)) {
            Console.WriteLine(string.Join(",", parameter));

            return Task.CompletedTask;
        }
    }
}