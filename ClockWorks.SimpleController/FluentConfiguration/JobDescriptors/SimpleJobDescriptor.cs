using System;
using JVH.ClockWorks.Core.FluentConfiguration;
using JVH.ClockWorks.SimpleController.FluentConfiguration.TriggerDescriptions;

namespace JVH.ClockWorks.SimpleController.FluentConfiguration.JobDescriptors
{
    public class SimpleJobDescription : JobDescriptor
    {
        public SimpleJobDescription()
        {

        }

        public SimpleJobDescription(SimpleJobDescription cloneThis)
        {
            this.Identifier = cloneThis.Identifier;
            this.Job = cloneThis.Job;
            this.Repetition = cloneThis.Repetition;
            this.NextExecutionTime = cloneThis.NextExecutionTime;
            this.ActualExecutionCount = cloneThis.ActualExecutionCount;
            this.IntervalInMilliseconds = cloneThis.IntervalInMilliseconds;
            this.ExactStartTime = cloneThis.ExactStartTime;
            this.TriggerType = cloneThis.TriggerType;
            this.TimeOfDay = cloneThis.TimeOfDay;
            this.SucceedesJobId = cloneThis.SucceedesJobId;
            this.SucceedsOnlyOnSuccess = cloneThis.SucceedsOnlyOnSuccess;

        }

        public int Repetition { get; set; }
        public int ActualExecutionCount { get; set; }
        public DateTime NextExecutionTime { get; set; }
        public int IntervalInMilliseconds { get; internal set; }
        public TimeSpan TimeOfDay { get; set; }
        public string SucceedesJobId { get; set; }
        public bool SucceedsOnlyOnSuccess { get; set; }
    }
}
