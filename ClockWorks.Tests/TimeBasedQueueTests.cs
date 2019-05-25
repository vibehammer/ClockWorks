using System;
using System.Collections.Generic;
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
            var jobDescription = CreateJob("42");
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
            var job1 = CreateJob("42");
            var job2 = CreateJob("43");
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
            var job1 = CreateJob("42", triggerTime1);
            var job2 = CreateJob("43", triggerTime2);
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
            var job = CreateJob("42", triggerTime);
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
            var job = CreateJob("42", triggerTime);
            var queue = serviceProviderMock.GetService<ITimeBasedQueue>();

            queue.AddEntry(job);

            // Act
            var result = GetNextMessage(stop, queue, out var actualStop);

            // Assert
            Assert.NotNull(result);
            actualStop.Should().BeCloseTo(triggerTime, TimeSpan.FromMilliseconds(3));
        }

        private static JobDescriptor GetNextMessage(DateTime stop, ITimeBasedQueue queue, out DateTime actualStop)
        {
            JobDescriptor result = null;
            actualStop = DateTime.MaxValue;
            while (DateTime.Now < stop && result == null)
            {
                result = queue.Next();
                actualStop = DateTime.Now;
            }

            return result;
        }

        #region Private Helpers

        private SimpleJobDescription CreateJob(string id)
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

        private SimpleJobDescription CreateJob(string id, DateTime startTime)
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

        #endregion

    }
}
