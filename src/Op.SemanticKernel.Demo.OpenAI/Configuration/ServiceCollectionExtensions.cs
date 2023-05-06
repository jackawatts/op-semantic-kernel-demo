using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.Memory.Qdrant;

namespace Op.SemanticKernel.Demo.OpenAI.Configuration;

internal static class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureApp(this IServiceCollection services, OpenAISettings settings) =>
        services.AddKernel(settings)
            .AddHostedService<Worker>();

    private static IServiceCollection AddKernel(this IServiceCollection services, OpenAISettings settings)
    {
        int qdrantPort = 6333;
        var memoryStore = new QdrantMemoryStore("http://localhost", qdrantPort, vectorSize: 1536);

        // var memoryStore = new VolatileMemoryStore();
        services.AddSingleton(memoryStore);

        var kernel = Kernel.Builder
            .Configure(c => c
                .AddOpenAIChatCompletionService("chat", "gpt-3.5-turbo", settings.ApiKey)
                .AddOpenAITextEmbeddingGenerationService("embedding", "text-embedding-ada-002", settings.ApiKey))
            .WithMemoryStorage(memoryStore)
            .Build();

        return services.AddSingleton<IKernel>(kernel);
    }
}