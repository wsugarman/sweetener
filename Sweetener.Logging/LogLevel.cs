namespace Sweetener.Logging
{
    /// <summary>
    /// Specifies a priority level and semantics associated with a log entry.
    /// </summary>
    public enum LogLevel
    {
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
