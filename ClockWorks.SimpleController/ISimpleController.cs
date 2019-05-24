using JVH.ClockWorks.Core;
using JVH.ClockWorks.SimpleController.FluentConfiguration.JobDescriptors;

namespace JVH.ClockWorks.SimpleController
{
    public interface ISimpleController : IController
    {
        void ScheduleJob(SimpleJobDescription jobDescriptor);
    }
}
