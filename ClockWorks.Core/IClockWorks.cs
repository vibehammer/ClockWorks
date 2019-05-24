using JVH.ClockWorks.Core;

namespace JVH.ClockWorks
{
    public interface IClockWorks
    {
        IController SingleInstanceController { get; }
        //Task AddDistributedController();
        //Task AddQueueController();
        //Task AddCustomController(IJobController controller);

    }
}
