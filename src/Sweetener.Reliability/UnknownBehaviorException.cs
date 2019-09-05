using System;
using System.Runtime.Serialization;

namespace Sweetener.Reliability
{
    public class UnknownBehaviorException : Exception
    {
        public UnknownBehaviorException()
            : this("Desired behavior is an unknown value.")
        { }

        public UnknownBehaviorException(string message)
            : base(message)
        { }

        public UnknownBehaviorException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UnknownBehaviorException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
