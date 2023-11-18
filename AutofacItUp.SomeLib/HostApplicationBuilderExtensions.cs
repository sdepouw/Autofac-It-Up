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
  public static HostApplicationBuilder RegisterDependencies(this HostApplicationBuilder hostAppBuilder)
  {
    hostAppBuilder.ConfigureContainer<ContainerBuilder>(
      new AutofacServiceProviderFactory(),
      containerBuilder => containerBuilder.RegisterModule<SomeLibAutofacModule>()
    );
    return hostAppBuilder;
  }
}
