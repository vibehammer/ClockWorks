using JVH.ClockWorks.SimpleController;
using JVH.ClockWorks.SimpleController.FluentConfiguration;
using JVH.ClockWorks.SimpleController.FluentConfiguration.Schedulers;
using JVH.ClockWorks.SimpleController.Queues;
using Microsoft.Extensions.DependencyInjection;

namespace JVH.ClockWorks
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddSimpleController(this IServiceCollection services)
        {
            services.AddScoped<ISimpleConfigurator, SimpleConfigurator>();
            services.AddSingleton<ITimeBasedQueue, TimeBasedQueue>();
            services.AddSingleton<IJobTriggeringQueue, JobTriggeringQueue>();

            services.AddScoped<ITriggerConfiguration, TriggerConfiguration>();
            services.AddScoped<ISimpleController, SimpleController.SimpleController>();

            return services;
        }
    }
}
