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
  /// <param name="hostBuilder">The host application builder instance</param>
  /// <param name="typesToIgnore">Any types that should not be registered by Autofac</param>
  public static HostApplicationBuilder RegisterDependencies(this HostApplicationBuilder hostBuilder, params Type[] typesToIgnore)
  {
    hostBuilder.ConfigureContainer(new AutofacServiceProviderFactory(), ContainerConfiguration(typesToIgnore));
    return hostBuilder;
  }

  /// <summary>
  /// Uses Autofac to register dependencies of SomeLib and of whatever application
  /// is calling this
  /// </summary>
  /// <remarks>This variant is for .NET 7 which uses <see cref="IHostBuilder"/> out of the box</remarks>
  /// <param name="hostBuilder">The host application builder instance</param>
  /// <param name="typesToIgnore">Any types that should not be registered by Autofac</param>
  public static IHostBuilder RegisterDependencies(this IHostBuilder hostBuilder, params Type[] typesToIgnore)
  {
    return hostBuilder
      .UseServiceProviderFactory(new AutofacServiceProviderFactory())
      .ConfigureContainer(ContainerConfiguration(typesToIgnore));
  }

  private static Action<ContainerBuilder> ContainerConfiguration(Type[] typesToIgnore) =>
    containerBuilder => containerBuilder.RegisterModule(new SomeLibAutofacModule(typesToIgnore));
}
