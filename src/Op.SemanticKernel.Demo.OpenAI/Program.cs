using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Op.SemanticKernel.Demo.OpenAI.Configuration;

var settings = new ConfigurationBuilder()
    .AddUserSecrets<Program>()
    .Build()
    .Get<OpenAISettings>()!;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((ctx, services) =>
    {
        services.ConfigureApp(settings);
    })
    .Build();

await host.RunAsync();