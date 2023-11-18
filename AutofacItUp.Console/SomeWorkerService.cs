using AutofacItUp.SomeLib;

namespace AutofacItUp.Console;

/// <summary>
/// An interface specific to this worker service
/// </summary>
public interface ISomeWorkerService
{
  long GetTimeTicks();
}

public class SomeWorkerService(TimeProvider timeProvider, ISomeLibService someLibService) : ISomeWorkerService
{
  public long GetTimeTicks()
  {
    someLibService.DoStuff();
    return timeProvider.GetUtcNow().Ticks;
  }
}
