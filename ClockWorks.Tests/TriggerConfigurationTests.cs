using System;
using JVH.ClockWorks;
using JVH.ClockWorks.SimpleController.FluentConfiguration;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace ClockWorks.Tests
{
    public class TriggerConfigurationTests
    {
        private readonly IServiceProvider serviceProviderMock;

        public TriggerConfigurationTests()
        {
            serviceProviderMock = ServiceProviderMock.InitializeDependencyInjection(serviceCollection =>
            {
                serviceCollection.AddClockWorks();
            });
        }

        [Fact]
        public void TriggerCreationThrowsStartTimeInThePastException()
        {
            // Arrange
            var sut = serviceProviderMock.GetService<ITriggerConfiguration>();

            // Act
            Assert.Throws<StartTimeInThePastException>(() => sut.OnExactStartTime(DateTime.Now.AddMilliseconds(-100)));
        }
    }
}
