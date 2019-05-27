using System;
using System.Collections.Generic;
using System.Linq;
using JVH.ClockWorks.Core.JobQueues;
using JVH.ClockWorks.SimpleController.FluentConfiguration.JobDescriptors;
using JVH.ClockWorks.SimpleController.FluentConfiguration.TriggerDescriptions;

namespace JVH.ClockWorks.SimpleController.Queues
{
    internal class TimeBasedQueue : ITimeBasedQueue
    {
        private SortedList<DateTime, SimpleJobDescription> jobs = new SortedList<DateTime, SimpleJobDescription>();

        public void AddJob(SimpleJobDescription jobDescription)
        {
            if (jobDescription == null)
            {
                throw new ArgumentNullException(nameof(jobDescription));
            }

            CalculateNextExecutionTime(jobDescription);

            VerifyValidExecutionTime(jobDescription);

            AddEntry(jobDescription);
        }

        public void RemoveJob(SimpleJobDescription jobDescription)
        {
            throw new NotImplementedException();
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
            if (jobDescription.TriggerConfiguration is ExactStartTriggerDescription)
            {
                var trigger = jobDescription.TriggerConfiguration as ExactStartTriggerDescription;
                jobDescription.NextExecutionTime = trigger.ExactStartTime.AddMilliseconds(jobDescription.ActualExecutionCount * jobDescription.IntervalInMilliseconds);
            }
            else if (jobDescription.TriggerConfiguration is TimeOfDayTriggerDescription)
            {
                var now = DateTime.Now;
                var trigger = jobDescription.TriggerConfiguration as TimeOfDayTriggerDescription;
                if (now.Date.Add(trigger.TimeOfDay) < now)
                {
                    jobDescription.NextExecutionTime = now.Date.AddDays(1).Add(trigger.TimeOfDay);
                }
                else
                {
                    jobDescription.NextExecutionTime = now.Date.Add(trigger.TimeOfDay);
                }
            }
            else
            {
                throw new InvalidTriggerException("Only ExactStartTimeTriggerDescription types are allowed for time based queue");
            }
        }

        public SimpleJobDescription Next()
        {
            if (jobs.Count == 0)
            {
                return null;
            }

            var job = jobs.FirstOrDefault();
            if (job.Key <= DateTime.Now)
            {
                jobs.Remove(job.Key);
                return job.Value;
            }

            return null;
        }

        public SimpleJobDescription PeekNext()
        {
            if (jobs.Count == 0)
            {
                return null;
            }

            return jobs.FirstOrDefault().Value;
        }

        private void AddEntry(SimpleJobDescription jobDescription) => jobs.Add(jobDescription.NextExecutionTime, jobDescription);
    }
}
