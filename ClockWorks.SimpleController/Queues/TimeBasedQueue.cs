using System;
using System.Collections.Generic;
using System.Linq;
using JVH.ClockWorks.SimpleController.FluentConfiguration.JobDescriptors;
using JVH.ClockWorks.SimpleController.FluentConfiguration.TriggerDescriptions;

namespace JVH.ClockWorks.SimpleController.Queues
{
    internal class TimeBasedQueue : ITimeBasedQueue
    {
        private SortedList<DateTime, SimpleJobDescription> jobs = new SortedList<DateTime, SimpleJobDescription>();

        public void AddEntry(SimpleJobDescription jobDescription)
        {
            if (jobDescription == null)
            {
                throw new ArgumentNullException(nameof(jobDescription));
            }

            CalculateNextExecutionTime(jobDescription);

            VerifyValidExecutionTime(jobDescription);

            AddJob(jobDescription);
        }

        private static void VerifyValidExecutionTime(SimpleJobDescription jobDescription)
        {
            if (jobDescription.NextExecutionTime < DateTime.Now)
            {
                throw new NextExecutionIsInThePastException("Next execution time is in the past");
            }
        }

        private static void CalculateNextExecutionTime(SimpleJobDescription jobDescription)
        {
            var trigger = jobDescription.TriggerConfiguration as ExactStartTriggerDescription;

            if (trigger == null)
            {
                throw new InvalidTriggerException("Only ExactStartTimeTriggerDescription types are allowed for time based queue");
            }

            jobDescription.NextExecutionTime = trigger.ExactStartTime.AddMilliseconds(jobDescription.ActualExecutionCount * jobDescription.IntervalInMilliseconds);
        }

        public SimpleJobDescription Next()
        {
            var job = jobs.FirstOrDefault();
            jobs.Remove(job.Key);
            return job.Value;
        }

        private void AddJob(SimpleJobDescription jobDescription) => jobs.Add(jobDescription.NextExecutionTime, jobDescription);
    }
}
