using Microsoft.Extensions.Logging;

namespace AutofacItUp.SomeLib;

/// <summary>
/// An interface located within <see cref="SomeLib" />
/// </summary>
public interface ISomeLibService
{
  void DoStuff();
}

public class SomeLibService(ILogger<SomeLibService> logger) : ISomeLibService
{
  public void DoStuff() => logger.LogInformation("[SomeLibService] Doing stuff!");
}
