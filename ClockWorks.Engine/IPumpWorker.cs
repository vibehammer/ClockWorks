using System;
using System.Collections.Generic;
using System.Text;
using JVH.ClockWorks.Core.FluentConfiguration;
using JVH.ClockWorks.Core.JobQueues;

namespace JVH.ClockWorks.Engine
{
    public interface IPumpWorker<TQueue> where TQueue: IJobQueue<JobDescriptor, JobDescriptor>
    {
    }
}
