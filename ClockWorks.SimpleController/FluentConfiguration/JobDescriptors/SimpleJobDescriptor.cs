using System;
using JVH.ClockWorks.Core.FluentConfiguration;

namespace JVH.ClockWorks.SimpleController.FluentConfiguration.JobDescriptors
{
    public class SimpleJobDescription : JobDescriptor
    {
        public virtual int Repetition { get; set; }
        public virtual int ActualExecutionCount { get; set; }
        public virtual DateTime NextExecutionTime { get; set; }
        public int IntervalInMilliseconds { get; internal set; }
    }
}
