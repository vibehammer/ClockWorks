using System;
using System.Collections.Generic;
using JVH.ClockWorks.Core.FluentConfiguration;
using JVH.ClockWorks.SimpleController.FluentConfiguration.JobDescriptors;

namespace JVH.ClockWorks.SimpleController.FluentConfiguration.Schedulers
{
    internal class SimpleConfigurator : ISimpleConfigurator
    {
        private Type Job { get; set; }
        private string Identifier { get; set; }
        private RepetionConfiguration Repetition { get; set; }
        private TriggerConfiguration TriggerConfiguration { get; set; }

        public SimpleJobDescription Build()
        {
            var description = new SimpleJobDescription
            {
                Identifier = this.Identifier,
                Job = this.Job,
                Repetition = this.Repetition.Repeat,
                IntervalInMilliseconds = this.Repetition.IntervalConfiguration.Interval
            };
            TriggerConfiguration.GetDescription(description);
            return description;
        }

        public IRepeaterConfiguration ConfigureRepetition()
        {
            Repetition = new RepetionConfiguration(this);
            return Repetition;
        }

        public ITriggerConfiguration ConfigureTrigger()
        {
            TriggerConfiguration = new TriggerConfiguration(this);
            return TriggerConfiguration;
        }

        public ISimpleConfigurator SetJobType<TJob>(string identifier) where TJob : IJob, new()
        {
            Job = typeof(TJob);
            this.Identifier = identifier;
            return this;
        }

        public List<JobDescriptor> Configure(string configurationFile)
        {
            throw new NotImplementedException();
        }
    }
}
