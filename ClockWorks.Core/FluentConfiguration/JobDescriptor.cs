using System;
using JVH.ClockWorks.Core.TriggerDescriptions;

namespace JVH.ClockWorks.Core.FluentConfiguration
{
    public abstract class JobDescriptor 
    {
        public virtual TriggerType TriggerType { get; set; }
        public DateTime ExactStartTime { get; set; }
        public virtual Type Job { get; set; }
        public virtual string Identifier { get; set; }
    }
}
