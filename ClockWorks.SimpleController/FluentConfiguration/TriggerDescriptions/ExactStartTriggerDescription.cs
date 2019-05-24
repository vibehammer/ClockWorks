using System;
using JVH.ClockWorks.Core.TriggerDescriptions;

namespace JVH.ClockWorks.SimpleController.FluentConfiguration.TriggerDescriptions
{
    public class ExactStartTriggerDescription : TriggerDescription
    {
        public virtual DateTime ExactStartTime { get; set; }
    }
}
