# Konsole - CLI Builder
[![Build status](https://ci.appveyor.com/api/projects/status/5hxjihsdayumq3y3/branch/master?svg=true)](https://ci.appveyor.com/project/sanisoclem/konsole/branch/master)

## Requirements
This library targets `netstandard2.0` so it should work with `net461` and `netcoreapp2.x`. However, it is only tested with `netcoreapp2.1`.

## Usage

See the [examples folder](./examples)

```csharp
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
```

## Installation
This library is available via NuGet. To install using `dotnet cli`:

```bash
dotnet package add P3.Konsole
```