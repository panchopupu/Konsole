using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using P3.Konsole.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace P3.Konsole
{
    public interface IApplicationBuilder<T> where T : IApplication
    {
        IApplicationBuilder<T> Configure(Action<IConfigurationBuilder> configBuilder);

        IApplicationBuilder<T> ConfigureServices(Action<IServiceCollection, IConfiguration> serviceBuilder);

        IApplicationBuilder<T> ConfigureCommands(Action<ICompositeCommandBuilder, IConfiguration> commandBuilder);

        T Build();
    }
}