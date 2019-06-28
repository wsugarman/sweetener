namespace Sweetener.Logging
{
    internal sealed class NullLogger<T> : Logger<T>
    {
        public override bool IsSynchronized => true;

        protected override void Dispose(bool disposing)
        {
            // There is nothing to dispose and we don't want users to be able to
            // dispose of a logger used statically!
            //
            // Furthermore, repeated calls to GC.SuppressFinalize(this) are just fine
            // in Logger<T>.Dispose() since there is no finalizer
        }

        protected internal override void Log(LogEntry<T> logEntry)
        { }
    }
}
