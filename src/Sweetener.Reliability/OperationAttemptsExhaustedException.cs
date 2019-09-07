using System;
using System.Runtime.Serialization;

namespace Sweetener.Reliability
{
    [Serializable]
    public class RetriesExhaustedException : Exception
    {
        public int Retries { get; }

        private const int E_ABORT = unchecked((int)0x80004004);

        public RetriesExhaustedException()
            : base("The operation cannot be retried anymore.")
        {
            HResult = E_ABORT;
        }

        public RetriesExhaustedException(string message)
            : base(message)
        {
            HResult = E_ABORT;
        }

        public RetriesExhaustedException(string message, Exception innerException)
            : base(message, innerException)
        {
            HResult = E_ABORT;
        }

        public RetriesExhaustedException(int retries)
            : this()
        {
            Retries = retries;
        }

        public RetriesExhaustedException(string message, int retries)
            : this(message)
        {
            Retries = retries;
        }

        public RetriesExhaustedException(string message, Exception innerException, int retries)
            : this(message, innerException)
        {
            Retries = retries;
        }

        protected RetriesExhaustedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            Retries = info.GetInt32(nameof(Retries));
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue(nameof(Retries), Retries);
        }
    }
}
