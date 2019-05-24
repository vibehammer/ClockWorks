using System;
using System.Runtime.Serialization;

namespace JVH.ClockWorks.SimpleController.Queues
{
    [Serializable]
    internal class InvalidTriggerException : Exception
    {
        public InvalidTriggerException()
        {
        }

        public InvalidTriggerException(string message) : base(message)
        {
        }

        public InvalidTriggerException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidTriggerException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}