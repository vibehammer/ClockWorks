using System;

namespace JVH.ClockWorks.SimpleController.Queues
{
    public class NextExecutionIsInThePastException : Exception
    {
        public NextExecutionIsInThePastException(string message) : base(message)
        {
        }
    }
}
