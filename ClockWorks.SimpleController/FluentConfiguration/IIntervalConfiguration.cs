using JVH.ClockWorks.SimpleController.FluentConfiguration.Schedulers;

namespace JVH.ClockWorks.SimpleController.FluentConfiguration
{
    public interface IIntervalConfiguration
    {
        ISimpleConfigurator WithIntervalInMilliseconds(int interval);
        ISimpleConfigurator WithIntervalInSeoncds(int interval);
        ISimpleConfigurator WithIntervalInMinutes(int interval);
        ISimpleConfigurator WithIntervalInHours(int interval);
        ISimpleConfigurator WithIntervalInDays(int interval);
    }
}