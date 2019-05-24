using System;

namespace JVH.ClockWorks.SimpleController.FluentConfiguration
{
    public class StartTimeInThePastException : Exception
    {
        public StartTimeInThePastException(string message) : base(message)
        {
        }
    }
}
