using System;
using System.Collections.Generic;
using JVH.ClockWorks.SimpleController.FluentConfiguration.JobDescriptors;

namespace JVH.ClockWorks.SimpleController.Queues
{
    internal class JobTriggeringQueue : IJobTriggeringQueue
    {
        private readonly Dictionary<string, List<SimpleJobDescription>> triggers = new Dictionary<string, List<SimpleJobDescription>>();

        public JobTriggeringQueue()
        {
        }

        public void AddJob(SimpleJobDescription jobDescription)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SimpleJobDescription> Next(string finishedJobId)
        {
            throw new NotImplementedException();
        }

        public void RemoveJob(SimpleJobDescription jobDescription)
        {
            throw new NotImplementedException();
        }
    }
}
