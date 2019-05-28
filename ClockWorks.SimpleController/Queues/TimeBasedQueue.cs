using System;
using System.Collections.Generic;
using System.Linq;
using JVH.ClockWorks.Core.JobQueues;
using JVH.ClockWorks.Core.TriggerDescriptions;
using JVH.ClockWorks.SimpleController.FluentConfiguration.JobDescriptors;
using JVH.ClockWorks.SimpleController.FluentConfiguration.TriggerDescriptions;

namespace JVH.ClockWorks.SimpleController.Queues
{
    internal class TimeBasedQueue : ITimeBasedQueue
    {
        private SortedList<DateTime, SimpleJobDescription> jobs = new SortedList<DateTime, SimpleJobDescription>();
        private object lockOnThis = new object();

        public void AddJob(SimpleJobDescription jobDescription)
        {
            if (jobDescription == null)
            {
                throw new ArgumentNullException(nameof(jobDescription));
            }

            CalculateFirstExecution(jobDescription);

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

        private static void CalculateFirstExecution(SimpleJobDescription jobDescription)
        {
            if (jobDescription.TriggerType == TriggerType.ExactTime)
            {
                jobDescription.NextExecutionTime = jobDescription.ExactStartTime.AddMilliseconds(jobDescription.ActualExecutionCount * jobDescription.IntervalInMilliseconds);
            }
            else if (jobDescription.TriggerType == TriggerType.TimeOfDay)
            {
                var now = DateTime.Now;
                if (now.Date.Add(jobDescription.TimeOfDay) < now)
                {
                    jobDescription.NextExecutionTime = now.Date.AddDays(1).Add(jobDescription.TimeOfDay);
                }
                else
                {
                    jobDescription.NextExecutionTime = now.Date.Add(jobDescription.TimeOfDay);
                }
            }
            else
            {
                throw new InvalidTriggerException("Only ExactStartTimeTriggerDescription types are allowed for time based queue");
            }
        }

        private static void CalculateNextExecutionTime(SimpleJobDescription jobDescription)
        {
            if (jobDescription.TriggerType == TriggerType.ExactTime)
            {
                jobDescription.NextExecutionTime = jobDescription.NextExecutionTime.AddMilliseconds(jobDescription.IntervalInMilliseconds);
            }
            else if (jobDescription.TriggerType == TriggerType.TimeOfDay)
            {
                var now = DateTime.Now;
                if (now.Date.Add(jobDescription.TimeOfDay) < now)
                {
                    jobDescription.NextExecutionTime = now.Date.AddDays(1).Add(jobDescription.TimeOfDay);
                }
                else
                {
                    jobDescription.NextExecutionTime = now.Date.Add(jobDescription.TimeOfDay);
                }
            }
            else
            {
                throw new InvalidTriggerException("Only ExactStartTimeTriggerDescription types are allowed for time based queue");
            }
        }

        public SimpleJobDescription Next()
        {
            lock (lockOnThis)
            {
                if (jobs.Count == 0)
                {
                    return null;
                }

                var job = jobs.FirstOrDefault();
                if (job.Key <= DateTime.Now)
                {
                    var nextJob = new SimpleJobDescription(job.Value);
                    CalculateNextExecutionTime(nextJob);
                    jobs.Remove(job.Key);
                    jobs.Add(nextJob.NextExecutionTime, nextJob);
                    return job.Value;
                }

                return null;
            }
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
