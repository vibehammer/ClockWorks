using System.Threading.Tasks;
using JVH.ClockWorks.Core.FluentConfiguration;

namespace JVH.ClockWorks.Core
{
    public interface IController
    {
        Task<bool> Start();

        Task<bool> Stop();

        void ScheduleJob(JobDescriptor jobDescriptor);
    }
}
