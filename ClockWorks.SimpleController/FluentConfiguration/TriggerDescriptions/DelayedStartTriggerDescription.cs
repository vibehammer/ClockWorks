using System;
using JVH.ClockWorks.Core.TriggerDescriptions;

namespace JVH.ClockWorks.SimpleController.FluentConfiguration.TriggerDescriptions
{
    public class DelayedStartTriggerDescription : TriggerDescription
    {
        public virtual TimeSpan DelayedExecution { get; set; }
    }
}
