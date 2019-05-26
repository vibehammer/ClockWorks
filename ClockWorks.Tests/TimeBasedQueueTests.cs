using System;
using System.Collections.Generic;
using System.Threading;
using Xunit;
using Microsoft.Extensions.DependencyInjection;
using FluentAssertions;
using JVH.ClockWorks;
using JVH.ClockWorks.Core.FluentConfiguration;
using JVH.ClockWorks.SimpleController.FluentConfiguration.JobDescriptors;
using JVH.ClockWorks.SimpleController.FluentConfiguration.Schedulers;
using JVH.ClockWorks.SimpleController.Queues;

namespace ClockWorks.Tests
{
    public class TimeBasedQueueTests
    {
        private readonly IServiceProvider serviceProviderMock;

        public TimeBasedQueueTests()
        {
            serviceProviderMock = ServiceProviderMock.InitializeDependencyInjection(serviceCollection =>
            {
                serviceCollection.AddClockWorks();
            });
        }

        [Fact]
        public void CanAddJob()
        {
            // Arrange
            var jobDescription = CreateJobWithExactStartTime("42");
            var queue = serviceProviderMock.GetService<ITimeBasedQueue>();

            // act
            queue.AddEntry(jobDescription);

            // Assert
        }

        [Fact]
        public void CanGetNextJobWhenEmpty()
        {
            // Arrange
            var queue = serviceProviderMock.GetService<ITimeBasedQueue>();

            // Act
            JobDescriptor job = queue.Next();

            // Assert
        }

        [Fact]
        public void CanAddTwoJobs()
        {
            // Arrange
            var job1 = CreateJobWithExactStartTime("42");
            var job2 = CreateJobWithExactStartTime("43");
            var queue = serviceProviderMock.GetService<ITimeBasedQueue>();

            // Act
            queue.AddEntry(job1);
            queue.AddEntry(job2);
            
            // Assert
        }

        [Fact]
        public void CanAddTwoJobsAndRetrieveThemInCorrectOrder()
        {
            // Arrange
            var triggerTime1 = DateTime.Now.AddMilliseconds(100);
            var triggerTime2 = DateTime.Now.AddMilliseconds(25);
            var stop1 = triggerTime1.AddMilliseconds(20);
            var stop2 = triggerTime2.AddMilliseconds(20);
            var job1 = CreateJobWithExactStartTime("42", triggerTime1);
            var job2 = CreateJobWithExactStartTime("43", triggerTime2);
            var queue = serviceProviderMock.GetService<ITimeBasedQueue>();

            queue.AddEntry(job1);
            queue.AddEntry(job2);

            // Act
            var jobs = new JobDescriptor[2] { GetNextMessage(stop2, queue, out var actualStopMessage2), GetNextMessage(stop1, queue, out var actualStopMessage1) };

            // Assert
            jobs[0].Identifier.Should().Be("43", "Job with ID = 43 is set to start before the other job");
            jobs[1].Identifier.Should().Be("42", "Job with ID = 42 should be last.");
        }

        [Fact]
        public void PeekDoesNotRemoveFromQueue()
        {
            // Arrange
            var triggerTime = DateTime.Now.AddMilliseconds(50);
            var stop = triggerTime.AddMilliseconds(20);
            var job = CreateJobWithExactStartTime("42", triggerTime);
            var queue = serviceProviderMock.GetService<ITimeBasedQueue>();

            queue.AddEntry(job);

            // Act
            var result = queue.PeekNext();
            var secondResult = queue.PeekNext();

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(secondResult);
            Assert.Equal(result, secondResult);
        }

        [Fact]
        public void WillGetJobWhenReady()
        {
            // Arrange
            var triggerTime = DateTime.Now.AddMilliseconds(50);
            var stop = triggerTime.AddMilliseconds(20);
            var job = CreateJobWithExactStartTime("42", triggerTime);
            var queue = serviceProviderMock.GetService<ITimeBasedQueue>();

            queue.AddEntry(job);

            // Act
            var result = GetNextMessage(stop, queue, out var actualStop);

            // Assert
            Assert.NotNull(result);
            actualStop.Should().BeCloseTo(triggerTime, TimeSpan.FromMilliseconds(3));
        }

        [Fact]
        public void TimeOfDayTriggerIsScheduledCorrect()
        {
            EnsureValidDateForTest();

            // Arrange
            var now = DateTime.Now;
            var triggerTime = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second).AddSeconds(1);
            var time = new TimeSpan(triggerTime.Hour, triggerTime.Minute, triggerTime.Second);
            var stop = triggerTime.AddMilliseconds(2000);
            var job = CreateJobWithTimeOfDay("42", time);
            var queue = serviceProviderMock.GetService<ITimeBasedQueue>();

            queue.AddEntry(job);

            // Act
            var result = GetNextMessage(stop, queue, out var actualStop);

            // Assert
            Assert.NotNull(result);
            actualStop.Should().BeCloseTo(triggerTime, TimeSpan.FromMilliseconds(3));
        }

        private static void EnsureValidDateForTest()
        {
            if (DateTime.Now.AddSeconds(10).Date != DateTime.Now.Date)
            {
                // Waiting for 11 seconds to move the test into tomorrow
                Thread.Sleep(11000);
            }
        }

        private static JobDescriptor GetNextMessage(DateTime stop, ITimeBasedQueue queue, out DateTime actualStop)
        {
            JobDescriptor result = null;
            actualStop = DateTime.MaxValue;
            int count = 0;
            while (DateTime.Now < stop && result == null)
            {
                result = queue.Next();
                actualStop = DateTime.Now;
                count++;
                Thread.Yield();
            }

            return result;
        }

        #region Private Helpers

        private SimpleJobDescription CreateJobWithExactStartTime(string id)
        {
            var configurator = serviceProviderMock.GetService<ISimpleConfigurator>();
            return configurator
                .SetJobType<TestJob>(id)
                .ConfigureRepetition()
                    .ExecuteInfinitly()
                    .WithIntervalInHours(1)
                .ConfigureTrigger()
                    .OnExactStartTime(DateTime.Now.AddMinutes(10))
                .Build();
        }

        private SimpleJobDescription CreateJobWithExactStartTime(string id, DateTime startTime)
        {
            var configurator = serviceProviderMock.GetService<ISimpleConfigurator>();
            return configurator
                .SetJobType<TestJob>(id)
                .ConfigureRepetition()
                    .ExecuteInfinitly()
                    .WithIntervalInHours(1)
                .ConfigureTrigger()
                    .OnExactStartTime(startTime)
                .Build();
        }

        private SimpleJobDescription CreateJobWithTimeOfDay(string id, TimeSpan timeOfDay)
        {
            var configurator = serviceProviderMock.GetService<ISimpleConfigurator>();
            return configurator
                .SetJobType<TestJob>(id)
                .ConfigureRepetition()
                .ExecuteInfinitly()
                .WithIntervalInHours(1)
                .ConfigureTrigger()
                .AtTimeOfDay(timeOfDay.Hours, timeOfDay.Minutes, timeOfDay.Seconds)
                .Build();
        }
        #endregion

    }
}
