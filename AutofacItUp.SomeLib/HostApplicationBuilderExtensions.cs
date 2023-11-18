using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AutofacItUp.SomeLib;

public static class HostApplicationBuilderExtensions
{
  /// <summary>
  /// Uses Autofac to register dependencies of SomeLib and of whatever application
  /// is calling this
  /// </summary>
  /// <param name="hostAppBuilder">The host application builder instance</param>
  /// <param name="typesToIgnore">Any types that should not be registered by Autofac</param>
  public static HostApplicationBuilder RegisterDependencies(this HostApplicationBuilder hostAppBuilder, params Type[] typesToIgnore)
  {
    hostAppBuilder.ConfigureContainer<ContainerBuilder>(
      new AutofacServiceProviderFactory(),
      containerBuilder => containerBuilder.RegisterModule(new SomeLibAutofacModule(typesToIgnore))
    );
    return hostAppBuilder;
  }
}
