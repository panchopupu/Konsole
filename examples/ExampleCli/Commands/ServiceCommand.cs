using P3.Konsole.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ExampleCli.Commands
{
    public class ServiceCommand : ICommand<string[]>
    {
        readonly IService _service;
        public ServiceCommand(IService service) => _service = service;

        public Task ExecuteAsync(string[] parameter, CancellationToken token = default(CancellationToken)) {
            Console.WriteLine(_service.DoSomething());
            return Task.CompletedTask;
        }
    }
}
