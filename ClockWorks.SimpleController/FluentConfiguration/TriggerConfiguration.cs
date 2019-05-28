using System;
using JVH.ClockWorks.Core.TriggerDescriptions;
using JVH.ClockWorks.SimpleController.FluentConfiguration.JobDescriptors;
using JVH.ClockWorks.SimpleController.FluentConfiguration.Schedulers;
using JVH.ClockWorks.SimpleController.FluentConfiguration.TriggerDescriptions;

namespace JVH.ClockWorks.SimpleController.FluentConfiguration
{
    public class TriggerConfiguration : ITriggerConfiguration
    {
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
        public TimeSpan TimeOfDay { get; set; }

        public ISimpleConfigurator AtTimeOfDay(int hour, int minute, int second)
        {
            TimeOfDay = new TimeSpan(hour, minute, second);
            triggerType = TriggerType.TimeOfDay;
            return simpleConfigurator;
        }

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

        public void GetDescription(SimpleJobDescription jobDescription)
        {
            jobDescription.TriggerType = triggerType;
            switch (triggerType)
            {
                case TriggerType.Delayed:
                    jobDescription.ExactStartTime = DateTime.Now.Add(this.DelayedExecution);
                    jobDescription.TimeOfDay = default;
                    jobDescription.SucceedesJobId = string.Empty;
                    break;
                case TriggerType.ExactTime:
                    jobDescription.ExactStartTime = ExactStartTime;
                    jobDescription.TimeOfDay = default;
                    jobDescription.SucceedesJobId = string.Empty;
                    break;
                case TriggerType.JobFinished:
                    jobDescription.ExactStartTime = default;
                    jobDescription.TimeOfDay = default;
                    jobDescription.SucceedesJobId = JobId;
                    jobDescription.SucceedsOnlyOnSuccess = OnlyIfSuccessfull;
                    break;
                case TriggerType.TimeOfDay:
                    jobDescription.ExactStartTime = default;
                    jobDescription.TimeOfDay = TimeOfDay;
                    jobDescription.SucceedesJobId = string.Empty;
                    break;
                default:
                    throw new Exception("Unknown trigger type specified");
            }
        }
    }
}
