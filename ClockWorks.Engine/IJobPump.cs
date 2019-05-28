using System.Threading.Tasks;
using JVH.ClockWorks.Core.FluentConfiguration;
using JVH.ClockWorks.Core.JobQueues;

namespace JVH.ClockWorks.Engine
{
    public interface IJobPump
    {
        void Start();
        void Stop();
    }
}
