// Copyright © William Sugarman.
// Licensed under the MIT License.

using System;
using System.Runtime.Serialization;

namespace Sweetener;

/// <summary>
/// The exception that is thrown when the value of an argument is empty
/// or consists only of white-space characters.
/// </summary>
[Serializable]
public class ArgumentWhiteSpaceException : ArgumentException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ArgumentWhiteSpaceException"/> class.
    /// </summary>
    /// <remarks>
    /// This constructor initializes the <see cref="Exception.Message"/> property of
    /// the new instance to a system-supplied message that describes the error,
    /// such as "Value cannot be empty or consist only of white-space characters."
    /// This message takes into account the current system culture.
    /// </remarks>
    public ArgumentWhiteSpaceException()
        : this(paramName: null)
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="ArgumentWhiteSpaceException"/> class
    /// with the name of the parameter that causes this exception.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This constructor initializes the <see cref="Exception.Message"/> property of the
    /// new instance to a system-supplied message that describes the error, such as
    /// "Value cannot be empty or consist only of white-space characters."
    /// This message takes into account the current system culture.
    /// </para>
    /// <para>
    /// This constructor initializes the <see cref="ArgumentException.ParamName"/> property
    /// of the new instance using the <paramref name="paramName"/> parameter. The content of
    /// <paramref name="paramName"/> is intended to be understood by humans.
    /// </para>
    /// </remarks>
    /// <param name="paramName">The name of the parameter that causes this exception.</param>
    public ArgumentWhiteSpaceException(string? paramName)
        : base(SR.ArgumentWhiteSpaceMessage, paramName)
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="ArgumentWhiteSpaceException"/> class
    /// with a specified error message and the exception that is the cause of this exception.
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
    /// An exception that is thrown as a direct result of a previous exception should include
    /// a reference to the previous exception in the <see cref="Exception.InnerException"/>
    /// property. The <see cref="Exception.InnerException"/> property returns the same value
    /// that is passed into the constructor, or <see langword="null"/> if the
    /// <see cref="Exception.InnerException"/> property does not supply the inner exception
    /// value to the constructor.
    /// </para>
    /// </remarks>
    /// <param name="message">The error message that explains the reason for this exception.</param>
    /// <param name="innerException">
    /// The exception that is the cause of the current exception, or <see langword="null"/>
    /// if no inner exception is specified.
    /// </param>
    public ArgumentWhiteSpaceException(string? message, Exception? innerException)
        : base(message, innerException)
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="ArgumentWhiteSpaceException"/> class
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
    public ArgumentWhiteSpaceException(string? paramName, string? message)
        : base(message, paramName)
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="ArgumentWhiteSpaceException"/> class
    /// with serialized data.
    /// </summary>
    /// <remarks>
    /// This constructor is called during deserialization to reconstitute the exception
    /// object transmitted over a stream. For more information, see
    /// <a href="https://docs.microsoft.com/en-us/dotnet/standard/serialization/xml-and-soap-serialization">XML and SOAP Serialization</a>.
    /// </remarks>
    /// <param name="info">The object that holds the serialized object data.</param>
    /// <param name="context">An object that describes the source or destination of the serialized data.</param>
    protected ArgumentWhiteSpaceException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    { }
}
