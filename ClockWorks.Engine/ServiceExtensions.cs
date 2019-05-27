using JVH.ClockWorks.Engine;
using Microsoft.Extensions.DependencyInjection;

namespace JVH.ClockWorks
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddSimpleController(this IServiceCollection services)
        {
            services.AddSingleton<IJobPump, JobPump>();

            return services;
        }
    }
}
