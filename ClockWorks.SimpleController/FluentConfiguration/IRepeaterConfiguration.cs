using JVH.ClockWorks.SimpleController.FluentConfiguration.Schedulers;

namespace JVH.ClockWorks.SimpleController.FluentConfiguration
{
    public interface IRepeaterConfiguration
    {
        IIntervalConfiguration ExecuteTwice();
        IIntervalConfiguration ExeucteThisManyTimes(int repetitions);
        IIntervalConfiguration ExecuteInfinitly();
        ISimpleConfigurator ExecuteOnce();
    }
}