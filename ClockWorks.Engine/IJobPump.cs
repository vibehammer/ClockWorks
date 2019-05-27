using System.Threading.Tasks;

namespace JVH.ClockWorks.Engine
{
    public interface IJobPump
    {
        void Start();
        void Stop();
    }
}
