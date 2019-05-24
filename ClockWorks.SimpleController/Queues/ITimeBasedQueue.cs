using JVH.ClockWorks.SimpleController.FluentConfiguration.JobDescriptors;

namespace JVH.ClockWorks.SimpleController.Queues
{
    public interface ITimeBasedQueue
    {
        void AddEntry(SimpleJobDescription simpleJobDescriptor);
        SimpleJobDescription Next();
    }
}
