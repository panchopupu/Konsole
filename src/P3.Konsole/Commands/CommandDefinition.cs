using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace P3.Konsole.Commands
{
    public class CommandDefinition<TCommand, TParameter> : ICommandDefinition where TCommand : class, ICommand<TParameter>
    {
        public CommandDefinition(string name) {
            Name = name;
        }

        public string Name { get; }

        public Task ExecuteCommandAsync(IServiceProvider serviceProvider, string[] args, CancellationToken token = default(CancellationToken)) {
            var cmd = serviceProvider.GetService<TCommand>();
            if (cmd == null)
                throw new InvalidCommandException();
            return cmd.ExecuteAsync(args, token);
        }

        public void RegisterServices(IServiceCollection serviceDescriptors) {
            serviceDescriptors.AddTransient<TCommand>();
        }
    }
}