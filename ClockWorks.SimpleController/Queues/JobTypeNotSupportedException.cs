using System;
using System.Runtime.Serialization;

namespace JVH.ClockWorks.SimpleController.Queues
{
    [Serializable]
    internal class JobTypeNotSupportedException : Exception
    {
        public JobTypeNotSupportedException()
        {
        }

        public JobTypeNotSupportedException(string message) : base(message)
        {
        }

        public JobTypeNotSupportedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected JobTypeNotSupportedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}