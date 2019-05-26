using JVH.ClockWorks.Core.FluentConfiguration;
using JVH.ClockWorks.SimpleController.FluentConfiguration.JobDescriptors;

namespace JVH.ClockWorks.SimpleController.FluentConfiguration.Schedulers
{
    public interface ISimpleConfigurator : IPersistedConfigurationConfigurator
    {
        ITriggerConfiguration ConfigureTrigger();
        IRepeaterConfiguration ConfigureRepetition();
        ISimpleConfigurator SetJobType<TJob>(string identifier) where TJob: IJob, new();
        SimpleJobDescription Build(); 
    }
}
