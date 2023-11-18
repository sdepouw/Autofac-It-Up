using System.Reflection;
using Autofac;
using Autofac.Core;
using Microsoft.Extensions.Hosting;
using AutofacModule = Autofac.Module;

namespace AutofacItUp.SomeLib;

/// <summary>
/// Registers dependencies inside this class library, as well as those of the Worker
/// </summary>
public class SomeLibAutofacModule(params Type[] typesToIgnore) : AutofacModule
{
  protected override void Load(ContainerBuilder builder)
  {
    var interfacesToExclude = new[]
    {
      typeof(IHostedService), // Hosted Services (added outside Autofac)
      typeof(IModule) // Autofac Modules
    };
    Assembly thisAssembly = GetType().Assembly;
    Assembly workerAssembly = Assembly.GetEntryAssembly()!; // Worker's Assembly, confirmed working for EXE and when running as a Service
    builder.RegisterAssemblyTypes(thisAssembly, workerAssembly)
      .Where(t => !t.GetInterfaces().Any(i => interfacesToExclude.Contains(i)))
      .Where(t => !typesToIgnore.Contains(t))
      .AsImplementedInterfaces(); // Automatic registration of IFoo -> Foo etc.

    // Singleton registration + registering .NET 8's TimeProvider
    builder
      .RegisterInstance(TimeProvider.System)
      .As<TimeProvider>()
      .SingleInstance();
  }
}
