namespace AutofacItUp.Console;

public class Worker(ILogger<Worker> logger, ISomeWorkerService someWorkerService) : MySuperCoolBaseType
{
  private readonly Guid _guid = Guid.NewGuid();

  protected override async Task ExecuteAsync(CancellationToken stoppingToken)
  {
    await Task.Delay(100, stoppingToken); // Wait for log messages
    while (!stoppingToken.IsCancellationRequested)
    {
      logger.LogInformation("[{Guid}] What's the time? {TimeTicks}", _guid, someWorkerService.GetTimeTicks());
      await Task.Delay(5000, stoppingToken);
    }
  }
}

/// <summary>
/// Simulate a Worker having a base type that isn't directly <see cref="BackgroundService" />
/// </summary>
public abstract class MySuperCoolBaseType : BackgroundService;