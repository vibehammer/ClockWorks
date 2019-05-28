using System;
using JVH.ClockWorks.Core;
using JVH.ClockWorks.Core.TriggerDescriptions;

namespace JVH.ClockWorks.SimpleController.FluentConfiguration.TriggerDescriptions
{
    public class ExactStartTriggerDescription : TriggerDescription, IDeepCloneable<ExactStartTriggerDescription>
    {
        public virtual DateTime ExactStartTime { get; set; }

        public ExactStartTriggerDescription DeepClone() => new ExactStartTriggerDescription {ExactStartTime = this.ExactStartTime};

    }
}
