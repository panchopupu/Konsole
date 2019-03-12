using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace P3.Konsole.Commands
{
    public class CompositeCommandDefinition : ICommandDefinition
    {
        readonly IList<ICommandDefinition> _commandDefinitions;
        readonly ICommandDefinition _catchAll;

        public CompositeCommandDefinition(string name, IList<ICommandDefinition> commandDefinitions, ICommandDefinition catchAll) {
            Name = name;
            _commandDefinitions = commandDefinitions;
            _catchAll = catchAll;
        }

        public string Name { get; }

        public Task ExecuteCommandAsync(IServiceProvider serviceProvider, string[] args, CancellationToken token = default(CancellationToken)) {
            if (args != null && args.Length > 0) {
                // -- check if there is a matching child command
                var cmd = _commandDefinitions.SingleOrDefault(c => c.Name.Equals(args[0]));
                if (cmd != null)
                    return cmd.ExecuteCommandAsync(serviceProvider, args.Skip(1).ToArray(), token);
            }
            if (_catchAll != null)
                return _catchAll.ExecuteCommandAsync(serviceProvider, args, token);   // -- attempt to pass execution to catch all definition if not found
            else
                throw new InvalidCommandException();    // -- no command can be executed
        }

        public void RegisterServices(IServiceCollection serviceDescriptors) {
            // -- dont need to register any services itself but it has to give child definitions a chance to register theirs
            if (_catchAll != null)
                _catchAll.RegisterServices(serviceDescriptors);
            foreach (var cmd in _commandDefinitions)
                cmd.RegisterServices(serviceDescriptors);
        }
    }
}