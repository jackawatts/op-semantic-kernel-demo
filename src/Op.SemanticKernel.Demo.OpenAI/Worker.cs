using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel;
using Op.SemanticKernel.Demo.Examples;

namespace Op.SemanticKernel.Demo.OpenAI;

internal class Worker : BackgroundService
{
    private readonly IKernel _kernel;
    private readonly ILogger<Worker> _logger;

    public Worker(IKernel kernel, ILogger<Worker> logger)
    {
        (_kernel, _logger) = (kernel, logger);
    }

    public override async Task StopAsync(CancellationToken stoppingToken)
    {
        _logger.LogWarning("{Worker} is stopping", nameof(Worker));
        await base.StopAsync(stoppingToken);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("{Worker} is starting", nameof(Worker));

        var example = new ProjectHistoryExample(_kernel, _logger);
        await example.RunAsync();
    }
}