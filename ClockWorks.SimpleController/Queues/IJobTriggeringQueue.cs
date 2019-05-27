using System.Collections.Generic;
using JVH.ClockWorks.Core.JobQueues;
using JVH.ClockWorks.SimpleController.FluentConfiguration.JobDescriptors;

namespace JVH.ClockWorks.SimpleController.Queues
{
    public interface IJobTriggeringQueue : IJobQueue<SimpleJobDescription, SimpleJobDescription>
    {
        IEnumerable<SimpleJobDescription> Next(string finishedJobId);

    }
}
