using System;
using JVH.ClockWorks.Core;
using JVH.ClockWorks.Core.TriggerDescriptions;

namespace JVH.ClockWorks.SimpleController.FluentConfiguration.TriggerDescriptions
{
    public class DelayedStartTriggerDescription : TriggerDescription, IDeepCloneable<DelayedStartTriggerDescription>
    {
        public virtual TimeSpan DelayedExecution { get; set; }

        public DelayedStartTriggerDescription DeepClone() => new DelayedStartTriggerDescription {DelayedExecution = this.DelayedExecution};
    }
}
