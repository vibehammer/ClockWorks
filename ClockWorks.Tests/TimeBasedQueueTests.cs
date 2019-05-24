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
        public void CanAddTwoJobsAndRetrieveThem()
        {
            // Arrange
            var job1 = CreateJob("42");
            var job2 = CreateJob("43");
            var queue = serviceProviderMock.GetService<ITimeBasedQueue>();

            queue.AddEntry(job1);
            queue.AddEntry(job2);

            // Act
            var jobs = new List<JobDescriptor> { queue.Next(), queue.Next() };

            // Assert
            jobs.Count.Should().Be(2, "There should be exactly two jobs");
            jobs.Should().Contain(x => x.Identifier == "42", "The job with id = 42 is missing");
            jobs.Should().Contain(x => x.Identifier == "43", "The job with id = 43 is missing");
        }

        [Fact]
        public void CanAddTwoJobsAndRetrieveThemInCorrectOrder()
        {
            // Arrange
            var job1 = CreateJob("42", DateTime.Now.AddMinutes(10));
            var job2 = CreateJob("43", DateTime.Now.AddMinutes(1));
            var queue = serviceProviderMock.GetService<ITimeBasedQueue>();

            queue.AddEntry(job1);
            queue.AddEntry(job2);

            // Act
            var jobs = new JobDescriptor[2] { queue.Next(), queue.Next() };

            // Assert
            jobs[0].Identifier.Should().Be("43", "Job with ID = 43 is set to start before the other job");
            jobs[1].Identifier.Should().Be("42", "Job with ID = 42 should be last.");
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
