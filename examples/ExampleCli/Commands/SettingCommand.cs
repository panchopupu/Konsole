using Microsoft.Extensions.Options;
using P3.Konsole.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ExampleCli.Commands
{
    public class SettingCommand : ICommand<string[]>
    {
        readonly Setting _settings;
        public SettingCommand(IOptions<Setting> settingsAccessor) {
            _settings = settingsAccessor.Value;
        }

        public Task ExecuteAsync(string[] parameter, CancellationToken token = default(CancellationToken)) {
            Console.WriteLine($"SomeValue: {_settings.SomeValue}");
            Console.WriteLine($"SomeSetting: {_settings.SomeSetting}");

            return Task.CompletedTask;
        }
    }
}