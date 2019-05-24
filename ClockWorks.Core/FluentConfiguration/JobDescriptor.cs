using System;
using JVH.ClockWorks.Core.TriggerDescriptions;

namespace JVH.ClockWorks.Core.FluentConfiguration
{
    public abstract class JobDescriptor
    {
        public virtual TriggerDescription TriggerConfiguration { get; set; }
        public virtual Type Job { get; set; }
        public virtual string Identifier { get; set; }
    }
}
