using JVH.ClockWorks.Core.JobQueues;
using JVH.ClockWorks.SimpleController.FluentConfiguration.JobDescriptors;

namespace JVH.ClockWorks.SimpleController.Queues
{
    public interface ITimeBasedQueue : IJobQueue<SimpleJobDescription, SimpleJobDescription>
    {
    }
}
