using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Op.SemanticKernel.Demo.Azure.Configuration;

var settings = new ConfigurationBuilder()
    .AddUserSecrets<Program>()
    .Build()
    .Get<AzureSettings>()!;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((ctx, services) =>
    {
        services.ConfigureApp(settings);
    })
    .Build();

await host.RunAsync();