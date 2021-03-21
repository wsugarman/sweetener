// Copyright © 2021 William Sugarman. All Rights Reserved.
// Licensed under the MIT License.

using System;
using System.Runtime.Serialization;

namespace Sweetener
{
    /// <summary>
    /// The exception that is thrown when the value of an argument is less than zero.
    /// </summary>
    [Serializable]
    public class ArgumentNegativeException : ArgumentOutOfRangeException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentNegativeException"/> class.
        /// </summary>
        /// <remarks>
        /// This constructor initializes the <see cref="Exception.Message"/> property of
        /// the new instance to a system-supplied message that describes the error,
        /// such as "Nonnegative number required." This message takes into account the current
        /// system culture.
        /// </remarks>
        public ArgumentNegativeException()
            : this(paramName: null)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentNegativeException"/> class
        /// with the name of the parameter that causes this exception.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This constructor initializes the <see cref="Exception.Message"/> property of the
        /// new instance to a system-supplied message that describes the error, such as
        /// "Nonnegative number required." This message takes into account the current system culture.
        /// </para>
        /// <para>
        /// This constructor initializes the <see cref="ArgumentException.ParamName"/> property
        /// of the new instance using the <paramref name="paramName"/> parameter. The content of
        /// <paramref name="paramName"/> is intended to be understood by humans.
        /// </para>
        /// </remarks>
        /// <param name="paramName">The name of the parameter that causes this exception.</param>
        public ArgumentNegativeException(string? paramName)
            : base(paramName, SR.ArgumentNegativeMessage)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentNegativeException"/> class
        /// with a specified error message and the exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for this exception.</param>
        /// <param name="innerException">
        /// The exception that is the cause of the current exception, or <see langword="null"/>
        /// if no inner exception is specified.
        /// </param>
        public ArgumentNegativeException(string? message, Exception? innerException)
            : base(message, innerException)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentNegativeException"/> class
        /// with the name of the parameter that causes this exception and a specified error message.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This constructor initializes the <see cref="Exception.Message"/> property of the new
        /// instance using the value of the <paramref name="message"/> parameter. The content of
        /// the <paramref name="message"/> parameter is intended to be understood by humans.
        /// The caller of this constructor is required to ensure that this string has been
        /// localized for the current system culture.
        /// </para>
        /// <para>
        /// This constructor initializes the <see cref="ArgumentException.ParamName"/> property
        /// of the new instance using the <paramref name="paramName"/> parameter. The content of
        /// <paramref name="paramName"/> is intended to be understood by humans.
        /// </para>
        /// </remarks>
        /// <param name="paramName">The name of the parameter that caused the exception.</param>
        /// <param name="message">The message that describes the error.</param>
        public ArgumentNegativeException(string? paramName, string? message)
            : base(paramName, message)
        { }

        // TODO: Should ctors that accept the actual value specify numeric types instead of object? Or is that too limiting?

        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentNegativeException"/> class
        /// with the parameter name, the value of the argument, and a specified error message.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This constructor initializes the <see cref="ArgumentException.ParamName"/> property
        /// of the new instance using the <paramref name="paramName"/> parameter, the
        /// <see cref="ArgumentOutOfRangeException.ActualValue"/> property using the
        /// <paramref name="actualValue"/> parameter, and the <see cref="Exception.Message"/>
        /// property using the <paramref name="message"/> parameter. The content of the
        /// <paramref name="paramName"/> and <paramref name="message"/> parameters is intended
        /// to be understood by humans. The caller of this constructor is required to ensure
        /// that these strings have been localized for the current system culture.
        /// </para>
        /// <para>
        /// The <paramref name="actualValue"/> parameter contains an invalid value that is passed
        /// to the method and causes this exception to be thrown. This value is stored in the
        /// <see cref="ArgumentOutOfRangeException.ActualValue"/> property and its string
        /// representation is appended to the message string held in the
        /// <see cref="Exception.Message"/> property.
        /// </para>
        /// </remarks>
        /// <param name="paramName">The name of the parameter that caused the exception.</param>
        /// <param name="actualValue">The value of the argument that causes this exception.</param>
        /// <param name="message">The message that describes the error.</param>
        public ArgumentNegativeException(string? paramName, object? actualValue, string? message)
            : base(paramName, actualValue, message)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentNegativeException"/> class
        /// with serialized data.
        /// </summary>
        /// <remarks>
        /// This constructor is called during deserialization to reconstitute the exception
        /// object transmitted over a stream. For more information, see
        /// <a href="https://docs.microsoft.com/en-us/dotnet/standard/serialization/xml-and-soap-serialization">XML and SOAP Serialization</a>.
        /// </remarks>
        /// <param name="info">The object that holds the serialized object data.</param>
        /// <param name="context">An object that describes the source or destination of the serialized data.</param>
        protected ArgumentNegativeException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }
    }
}
