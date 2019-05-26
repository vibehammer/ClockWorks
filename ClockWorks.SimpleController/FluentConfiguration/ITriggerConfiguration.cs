using System;
using JVH.ClockWorks.SimpleController.FluentConfiguration.Schedulers;

namespace JVH.ClockWorks.SimpleController.FluentConfiguration
{
    public interface ITriggerConfiguration
    {
        ISimpleConfigurator OnExactStartTime(DateTime startTime);
        ISimpleConfigurator AtTimeOfDay(int hour, int minute, int second);
        ISimpleConfigurator WithDelayedStart(TimeSpan timeToStart);
        ISimpleConfigurator OnSpecificJobCompletion(string jobId, bool onlyIfSuccessfull);
    }
}