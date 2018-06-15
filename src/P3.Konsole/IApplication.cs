using Microsoft.Extensions.Configuration;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace P3.Konsole
{
    public interface IApplication
    {
        Task ExecuteCommandAsync(string[] args, IServiceProvider serviceProvider = null, CancellationToken token = default(CancellationToken));

        IServiceProvider ServiceProvider { get; }
        IConfigurationRoot Configuration { get; }
    }
}
