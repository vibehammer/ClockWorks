using Microsoft.Extensions.DependencyInjection;
using System;

namespace ClockWorks.Tests
{
    public class ServiceProviderMock
    {
        public static IServiceProvider InitializeDependencyInjection(Action<IServiceCollection> callback)
        {
            var serviceCollection = new ServiceCollection();
            callback?.Invoke(serviceCollection);
            return serviceCollection.BuildServiceProvider();
        }
    }
}
