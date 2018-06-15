using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using P3.Konsole.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace P3.Konsole
{
    public class KonsoleApplication : IApplication
    {
        readonly IServiceCollection _serviceDescriptors;
        readonly ICommandDefinition _rootCommand;

        internal KonsoleApplication(IServiceCollection serviceDescriptors, IConfigurationRoot configuration, ICommandDefinition rootCommand) {
            _serviceDescriptors = serviceDescriptors;
            Configuration = configuration;
            ServiceProvider = serviceDescriptors.BuildServiceProvider();
            _rootCommand = rootCommand;
        }

        public IServiceProvider ServiceProvider { get; }

        public IConfigurationRoot Configuration { get; }

        public Task ExecuteCommandAsync(string[] args, IServiceProvider serviceProvider = null, CancellationToken token = default(CancellationToken)) {
            return _rootCommand.ExecuteCommandAsync(serviceProvider ?? serviceProvider, args, token);
        }
    }
}