using JVH.ClockWorks.Core;
using JVH.ClockWorks.SimpleController;

namespace JVH.ClockWorks
{
    internal class ClockWorks : IClockWorks
    {
        private readonly ISimpleController simpleController;

        public ClockWorks(ISimpleController simpleController)
        {
            this.simpleController = simpleController ?? throw new System.ArgumentNullException(nameof(simpleController));
        }

        public IController SingleInstanceController { get => simpleController; }
    }
}
