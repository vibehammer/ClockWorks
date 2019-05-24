using JVH.ClockWorks.SimpleController.FluentConfiguration.Schedulers;

namespace JVH.ClockWorks.SimpleController.FluentConfiguration
{
    internal class RepetionConfiguration : IRepeaterConfiguration
    {
        private SimpleConfigurator simpleConfigurator;
        public IntervalConfiguration IntervalConfiguration { get; set; }

        public RepetionConfiguration(SimpleConfigurator inMemoryScheduler)
        {
            IntervalConfiguration = new IntervalConfiguration(inMemoryScheduler);
            this.simpleConfigurator = inMemoryScheduler;
        }

        public int Repeat { get; set; }

        public IIntervalConfiguration ExecuteInfinitly()
        {
            Repeat = int.MaxValue;
            return IntervalConfiguration;
        }


        public ISimpleConfigurator ExecuteOnce()
        {
            Repeat = 0;
            return simpleConfigurator;
        }

        public IIntervalConfiguration ExecuteTwice()
        {
            Repeat = 1;
            return IntervalConfiguration;
        }

        public IIntervalConfiguration ExeucteThisManyTimes(int repetitions)
        {
            Repeat = repetitions;
            return IntervalConfiguration;
        }
    }
}