using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.Memory.Qdrant;

namespace Op.SemanticKernel.Demo.Azure.Configuration;

internal static class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureApp(this IServiceCollection services, AzureSettings settings) =>
        services.AddKernel(settings)
            .AddHostedService<Worker>();

    private static IServiceCollection AddKernel(this IServiceCollection services, AzureSettings settings)
    {
        int qdrantPort = 6333;
        var memoryStore = new QdrantMemoryStore("http://localhost", qdrantPort, vectorSize: 1536);

        // var memoryStore = new VolatileMemoryStore();
        services.AddSingleton(memoryStore);

        var kernel = Kernel.Builder
            .Configure(c => c
                .AddAzureChatCompletionService("chat", settings.ChatDeploymentName, settings.Endpoint, settings.ApiKey)
                .AddAzureTextEmbeddingGenerationService("embedding", settings.EmbeddingDeploymentName, settings.Endpoint, settings.ApiKey))
            .WithMemoryStorage(memoryStore)
            .Build();

        return services.AddSingleton<IKernel>(kernel);
    }
}