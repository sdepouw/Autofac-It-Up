using Autofac;
using FluentAssertions;

namespace AutofacItUp.SomeLib.Tests.SomeLibAutofacModuleTests;

public static class AutofacContainerAssertionExtensions
{
  /// <summary>
  /// Potential method to extract to test that a provided type registers to an expected type, and
  /// has a singleton lifetime instance; not perfect
  /// </summary>
  /// <param name="autofacContainer">The container to check the registrations of</param>
  /// <param name="expectedSingletonType">The exact type that is expected dto resolve</param>
  /// <typeparam name="TTypeToAssert">The type being tested</typeparam>
  public static void AssertTypeRegisteredAsSingleton<TTypeToAssert>(this IContainer autofacContainer, Type expectedSingletonType)
    where TTypeToAssert : notnull
  {
    autofacContainer.IsRegistered<TTypeToAssert>().Should().BeTrue();
    var timeProvider = autofacContainer.Resolve<TTypeToAssert>();
    timeProvider.GetType().Should().Be(expectedSingletonType);
    var secondTimeProvider = autofacContainer.Resolve<TTypeToAssert>();
    timeProvider.Should().BeSameAs(secondTimeProvider);
  }
}
