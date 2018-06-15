using P3.Konsole;
using P3.Konsole.Commands;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using ExampleCli.Commands;
using System.Threading;

namespace ExampleCli
{
    class Program
    {
        static async Task MainAsync(string[] args) {
            var app = new KonsoleApplicationBuilder()
                .Configure(builder => builder
                    .AddEnvironmentVariables("EXAMPLE_")
                    .AddJsonFile("config.json", true)
                    .AddUserSecrets("EXAMPLE_"))
                .ConfigureServices((col, config) => col
                    .AddOptions()

                    .Configure<Setting>(config.GetSection("settings"))

                    .AddExampleServices())
                .ConfigureCommands((builder, config) => builder
                    .DefineCommand<ParameterizedCommand, Parameters>("param")
                    .DefineCommand<PrintCommand>("print")
                    .DefineCommand<SettingCommand>("setting")
                    .DefineCompositeCommand("nested", builder2 => builder2
                         .DefineCommand<NestedCommand>("n1")
                         .DefineCommand<ServiceCommand>("svc")
                         .DefineCommand<NestedCommand2>("n2")
                         .DefineCommand<NestedCommand3>("n3")
                         .DefineCommand<NestedCommand4>("n4")
                         .DefineCatchAllCommand<NestedCatchAllCommand>()))
                .Build();

            try {
                var cancellation = new CancellationTokenSource();


                Console.CancelKeyPress += (sender, e) => {
                    e.Cancel = true;
                    cancellation.Cancel();
                };

                // -- execute command in child scope
                using (var scope = app.ServiceProvider.CreateScope()) {
                    await app.ExecuteCommandAsync(args, scope.ServiceProvider, cancellation.Token);
                }
            }
            catch (InvalidCommandException) {
                // PRINT USAGE
                Console.Write("Invalid command");
                Environment.Exit(-1);
            }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                Environment.Exit(-2);
            }
        }

        private static void Console_CancelKeyPress(object sender, ConsoleCancelEventArgs e) {
            throw new NotImplementedException();
        }

        static void Main(string[] args) {
            Task.Run(() => MainAsync(args)).Wait();
        }
    }


    public static class Extensions
    {
        public static IServiceCollection AddExampleServices(this IServiceCollection col)
            => col.AddScoped<IService, Service>();
    }
}