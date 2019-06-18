namespace Sweetener.Logging
{
    /// <summary>
    /// Specifies the priority level and semantics associated with a given log entry.
    /// </summary>
    public enum LogLevel
    {
        /// <summary>
        /// Specifies highly granular information used to trace the execution of an application,
        /// typically in a development environment.
        /// </summary>
        Trace,

        /// <summary>
        /// Specifies often verbose debugging information used to diagnose potential problems.
        /// </summary>
        Debug,

        /// <summary>
        /// Specifies standard operating information used to monitor the state of an application.
        /// </summary>
        Info,

        /// <summary>
        /// Specifies problematic, but not exceptional, information.
        /// </summary>
        Warn,

        /// <summary>
        /// Specifies exceptional and unexpected infomation that does not lead to the failure of the application.
        /// </summary>
        Error,

        /// <summary>
        /// Specifies exceptional and unexpected information that leads to the overall failure of the application.
        /// </summary>
        Fatal,
    }
}
