using System;
using JVH.ClockWorks.Core.TriggerDescriptions;
using JVH.ClockWorks.SimpleController.FluentConfiguration.Schedulers;
using JVH.ClockWorks.SimpleController.FluentConfiguration.TriggerDescriptions;

namespace JVH.ClockWorks.SimpleController.FluentConfiguration
{
    internal class TriggerConfiguration : ITriggerConfiguration
    {
        private enum TriggerType
        {
            Unknown = 0,
            Delayed,
            JobFinished,
            ExactTime
        }

        private readonly ISimpleConfigurator simpleConfigurator;
        private TriggerType triggerType;

        public TriggerConfiguration(ISimpleConfigurator simpleConfigurator)
        {
            this.simpleConfigurator = simpleConfigurator;
        }

        public string JobId { get; set; }
        public bool OnlyIfSuccessfull { get; set; }
        public DateTime ExactStartTime { get; set; }
        public TimeSpan DelayedExecution { get; set; }

        public ISimpleConfigurator WithDelayedStart(TimeSpan timeToStart)
        {
            DelayedExecution = timeToStart;
            triggerType = TriggerType.Delayed;
            return simpleConfigurator;
        }

        public ISimpleConfigurator OnExactStartTime(DateTime startTime)
        {
            if (startTime < DateTime.Now)
            {
                throw new StartTimeInThePastException("The specified start time is in the past");
            }

            ExactStartTime = startTime;
            triggerType = TriggerType.ExactTime;
            return simpleConfigurator;
        }

        public ISimpleConfigurator OnSpecificJobCompletion(string jobId, bool onlyIfSuccessfull)
        {
            JobId = jobId;
            OnlyIfSuccessfull = onlyIfSuccessfull;
            triggerType = TriggerType.JobFinished;
            return simpleConfigurator;
        }

        public TriggerDescription GetDescription()
        {
            switch (triggerType)
            {
                case TriggerType.Delayed:
                    return new ExactStartTriggerDescription
                    {
                        ExactStartTime = DateTime.Now.Add(this.DelayedExecution)
                    };
                case TriggerType.ExactTime:
                    return new ExactStartTriggerDescription
                    {
                        ExactStartTime = this.ExactStartTime
                    };
                case TriggerType.JobFinished:
                    return new JobFinishedTriggerDescription
                    {
                        JobId = this.JobId,
                        OnlyIfSuccessfull = this.OnlyIfSuccessfull
                    };
                default:
                    throw new Exception("Unknown trigger type specified");
            }
        }
    }
}