using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AutofacItUp.SomeLib;

public static class HostExtensions
{
  /// <summary>
  /// Temporarily enables Serilog debugging, so we can catch errors that occur
  /// when attempting to write to logs with Serilog, so that the application
  /// cannot run unless Serilog configuration is valid
  /// </summary>
  /// <exception cref="ApplicationException">Thrown when Serilog throws an error</exception>
  public static void ValidateSerilog(this IHost host)
  {
    Serilog.Debugging.SelfLog.Enable(message =>
    {
      string appIdentifier = Assembly.GetExecutingAssembly().FullName?.Split(",").FirstOrDefault() ?? "Program";
      var logPath = Path.Join(AppContext.BaseDirectory, $"[{DateTime.UtcNow:yyyy-MM-dd HH.mm.ss}]-[{appIdentifier}]-SerilogError.txt");
      var messageToLog = $"[Serilog Validation Error] {message}";
      File.AppendAllText(logPath, messageToLog);
      throw new ApplicationException($"[Serilog Validation Error - Check [{logPath}] for errors");
    });
    var logger = host.Services.GetRequiredService<ILogger<SomeLibService>>();
    logger.LogInformation("Hi"); // File write errors occur here.
    Serilog.Debugging.SelfLog.Disable();
  }
}
