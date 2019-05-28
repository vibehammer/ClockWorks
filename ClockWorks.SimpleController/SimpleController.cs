using System;
using System.Threading.Tasks;
using JVH.ClockWorks.Core.FluentConfiguration;
using JVH.ClockWorks.Core.TriggerDescriptions;
using JVH.ClockWorks.SimpleController.FluentConfiguration;
using JVH.ClockWorks.SimpleController.FluentConfiguration.JobDescriptors;
using JVH.ClockWorks.SimpleController.FluentConfiguration.TriggerDescriptions;
using JVH.ClockWorks.SimpleController.Queues;

namespace JVH.ClockWorks.SimpleController
{
    internal class SimpleController : ISimpleController
    {
        private readonly ITimeBasedQueue timeBasedQueue;
        private readonly IJobTriggeringQueue jobTriggeringQueue;

        public SimpleController(ITimeBasedQueue timeBasedQueue, IJobTriggeringQueue jobTriggeringQueue)
        {
            this.timeBasedQueue = timeBasedQueue ?? throw new ArgumentNullException(nameof(timeBasedQueue));
            this.jobTriggeringQueue = jobTriggeringQueue ?? throw new ArgumentNullException(nameof(jobTriggeringQueue));
        }

        public Task<bool> Start()
        {
            throw new NotImplementedException();
        }

        public Task<bool> Stop()
        {
            throw new NotImplementedException();
        }

        public void ScheduleJob(JobDescriptor jobDescriptor) => ScheduleJob(jobDescriptor as SimpleJobDescription);

        public void ScheduleJob(SimpleJobDescription jobDescriptor)
        {
            if (jobDescriptor.TriggerType == TriggerType.ExactTime)
            {
                timeBasedQueue.AddJob(jobDescriptor);
            }
            else if (jobDescriptor.TriggerType == TriggerType.JobFinished)
            {
                jobTriggeringQueue.AddJob(jobDescriptor);
            }
            else
            {
                throw new JobTypeNotSupportedException($"The job type {jobDescriptor.GetType().Name} is not supported in the SimpleController");
            }
        }
    }
}
