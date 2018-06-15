using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace P3.Konsole.Commands
{
    public interface ICommand<T>
    {
        Task ExecuteAsync(T parameter, CancellationToken token = default(CancellationToken));
    }
    public interface ICommandDefinition
    {
        string Name { get; }
        /// <summary>
        /// Resolves the command to execute using args and retrieves it from the service provider then executes it.
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="args"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task ExecuteCommandAsync(IServiceProvider serviceProvider, string[] args, CancellationToken token = default(CancellationToken));


        /// <summary>
        /// Called after the command definition is built to give it a chance to register services 
        /// </summary>
        /// <param name="serviceDescriptors"></param>
        void RegisterServices(IServiceCollection serviceDescriptors);
    }
}