using Autofac;
using FluentAssertions;

namespace AutofacItUp.SomeLib.Tests.SomeLibAutofacModuleTests;

public class Registrations
{
  [Fact]
  public void RegistersSingletonSystemTimeProvider()
  {
    Type expectedTimeProviderType = TimeProvider.System.GetType();
    IContainer autofacContainer = BuildContainerWithAutofacModule();

    autofacContainer.AssertTypeRegisteredAsSingleton<TimeProvider>(expectedTimeProviderType);
  }

  [Fact]
  public void RegistersSomeLibService()
  {
    IContainer autofacContainer = BuildContainerWithAutofacModule();

    bool isRegistered = autofacContainer.IsRegistered<ISomeLibService>();

    isRegistered.Should().BeTrue();
  }

  [Fact]
  public void DoesNotRegisterServiceWhenPassedAsTypeToIgnore()
  {
    IContainer autofacContainer = BuildContainerWithAutofacModule(typeof(SomeLibService));

    bool isRegistered = autofacContainer.IsRegistered<ISomeLibService>();

    isRegistered.Should().BeFalse();
  }

  private static IContainer BuildContainerWithAutofacModule(params Type[] typesToIgnore)
  {
    var builder = new ContainerBuilder();
    builder.RegisterModule(new SomeLibAutofacModule(typesToIgnore));
    return builder.Build();
  }
}
