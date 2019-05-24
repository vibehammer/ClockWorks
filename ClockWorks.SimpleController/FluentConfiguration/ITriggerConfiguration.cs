using System;
using JVH.ClockWorks.SimpleController.FluentConfiguration.Schedulers;

namespace JVH.ClockWorks.SimpleController.FluentConfiguration
{
    public interface ITriggerConfiguration
    {
        ISimpleConfigurator OnExactStartTime(DateTime startTime);
        ISimpleConfigurator WithDelayedStart(TimeSpan timeToStart);
        ISimpleConfigurator OnSpecificJobCompletion(string jobId, bool onlyIfSuccessfull);
    }
}