using System.Threading.Tasks;

namespace JVH.ClockWorks
{
    public interface IJob
    {
        Task<bool> Execute(JobData data);
    }
}
