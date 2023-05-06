namespace Op.SemanticKernel.Demo.Azure.Configuration;

internal sealed record AzureSettings(string ApiKey, string Endpoint, string ChatDeploymentName, string EmbeddingDeploymentName);