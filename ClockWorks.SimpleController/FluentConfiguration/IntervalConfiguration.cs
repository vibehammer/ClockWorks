using JVH.ClockWorks.SimpleController.FluentConfiguration.Schedulers;

namespace JVH.ClockWorks.SimpleController.FluentConfiguration
{
    internal class IntervalConfiguration : IIntervalConfiguration
    {
        private SimpleConfigurator inMemoryScheduler;
        public int Interval { get; set; }

        public IntervalConfiguration(SimpleConfigurator inMemoryScheduler)
        {
            this.inMemoryScheduler = inMemoryScheduler;
        }

        public ISimpleConfigurator WithIntervalInDays(int interval)
        {
            Interval = interval * 24 * 60 * 60 * 1000;
            return inMemoryScheduler;
        }

        public ISimpleConfigurator WithIntervalInHours(int interval)
        {
            Interval = interval * 60 * 60 * 1000;
            return inMemoryScheduler;
        }

        public ISimpleConfigurator WithIntervalInMilliseconds(int interval)
        {
            Interval = interval;
            return inMemoryScheduler;
        }

        public ISimpleConfigurator WithIntervalInMinutes(int interval)
        {
            Interval = interval * 60 * 1000;
            return inMemoryScheduler;
        }

        public ISimpleConfigurator WithIntervalInSeoncds(int interval)
        {
            Interval = interval * 1000;
            return inMemoryScheduler;
        }
    }
}