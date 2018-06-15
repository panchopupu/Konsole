using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using P3.Konsole.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace P3.Konsole
{
    public class KonsoleApplicationBuilder : IApplicationBuilder<KonsoleApplication>
    {
        readonly List<Action<IConfigurationBuilder>> _configActions = new List<Action<IConfigurationBuilder>>();
        readonly List<Action<IServiceCollection, IConfiguration>> _serviceActions = new List<Action<IServiceCollection, IConfiguration>>();
        readonly List<Action<ICompositeCommandBuilder, IConfiguration>> _commandActions = new List<Action<ICompositeCommandBuilder, IConfiguration>>();

        public KonsoleApplication Build() {
            var configBuilder = new ConfigurationBuilder();
            var serviceCollection = new ServiceCollection();
            var rootCmdBuilder = new CompositeCommandBuilder();

            // -- running all actions on the builder allow consumers to manipulate the builder (in order)
            foreach (var action in _configActions)
                action(configBuilder);

            // -- build config so we can use it in the config service phase
            var config = configBuilder.Build();

            foreach (var action in _serviceActions)
                action(serviceCollection, config);

            foreach (var action in _commandActions)
                action(rootCmdBuilder, config);

            var rootCommand = rootCmdBuilder.Build();

            rootCommand.RegisterServices(serviceCollection);

            return new KonsoleApplication(serviceCollection, config, rootCommand);
        }

        public IApplicationBuilder<KonsoleApplication> Configure(Action<IConfigurationBuilder> configBuilder) {
            _configActions.Add(configBuilder);
            return this;
        }

        public IApplicationBuilder<KonsoleApplication> ConfigureCommands(Action<ICompositeCommandBuilder, IConfiguration> commandBuilder) {
            _commandActions.Add(commandBuilder);
            return this;
        }

        public IApplicationBuilder<KonsoleApplication> ConfigureServices(Action<IServiceCollection, IConfiguration> serviceBuilder) {
            _serviceActions.Add(serviceBuilder);
            return this;
        }
    }
}