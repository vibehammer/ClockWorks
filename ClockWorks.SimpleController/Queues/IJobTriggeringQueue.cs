using System.Collections.Generic;
using JVH.ClockWorks.SimpleController.FluentConfiguration.JobDescriptors;

namespace JVH.ClockWorks.SimpleController.Queues
{
    public interface IJobTriggeringQueue
    {
        void AddJob(SimpleJobDescription jobDescription);
        void RemoveJob(SimpleJobDescription jobDescription);
        IEnumerable<SimpleJobDescription> Next(string finishedJobId);

    }
}
