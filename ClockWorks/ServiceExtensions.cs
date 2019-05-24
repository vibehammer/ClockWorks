using Microsoft.Extensions.DependencyInjection;

namespace JVH.ClockWorks
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddClockWorks(this IServiceCollection services)
        {
            services.AddScoped<IClockWorks, ClockWorks>();

            services.AddSimpleController();
            return services;
        }
    }
}
