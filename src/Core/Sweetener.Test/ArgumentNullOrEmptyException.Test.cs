// Copyright © William Sugarman.
// Licensed under the MIT License.

using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sweetener.Test;

[TestClass]
public class ArgumentNullOrEmptyExceptionTest
{
    [TestMethod]
    public void Ctor()
    {
        ArgumentNullOrEmptyException exception = new ArgumentNullOrEmptyException();

        Assert.AreEqual(null                         , exception.InnerException);
        Assert.AreEqual(SR.ArgumentNullOrEmptyMessage, exception.Message       );
        Assert.AreEqual(null                         , exception.ParamName     );
    }

    [TestMethod]
    public void Ctor_ParameterName()
    {
        ArgumentNullOrEmptyException exception = new ArgumentNullOrEmptyException("parameter1");

        Assert.AreEqual(null        , exception.InnerException);
        Assert.AreEqual("parameter1", exception.ParamName     );

        // There will be an additional line concerning the parameter name
        Assert.IsTrue(exception.Message.StartsWith(SR.ArgumentNullOrEmptyMessage, StringComparison.Ordinal));
    }

    [TestMethod]
    public void Ctor_Message_InnerException()
    {
        Exception innerException = new FormatException();
        ArgumentNullOrEmptyException exception = new ArgumentNullOrEmptyException("Hello World", innerException);

        Assert.AreSame (innerException, exception.InnerException);
        Assert.AreEqual("Hello World" , exception.Message       );
        Assert.AreEqual(null          , exception.ParamName     );
    }

    [TestMethod]
    public void Ctor_ParameterName_Message()
    {
        ArgumentNullOrEmptyException exception = new ArgumentNullOrEmptyException("parameter1", "Hello World");

        Assert.AreEqual(null        , exception.InnerException);
        Assert.AreEqual("parameter1", exception.ParamName     );

        // Message will contain both the value from the ctor and a statement about the parameter
        Assert.IsTrue(exception.Message.StartsWith("Hello World", StringComparison.Ordinal));
        Assert.IsTrue(exception.Message.Contains  ("parameter1" , StringComparison.Ordinal));
    }

    [TestMethod]
    public void Ctor_SerializationInfo_StreamingContext()
    {
        ArgumentNullOrEmptyException? after;
        ArgumentNullOrEmptyException before = new ArgumentNullOrEmptyException("parameter1", "Hello World");

        using (MemoryStream buffer = new MemoryStream())
        {
#pragma warning disable CA2300, CA2301, SYSLIB0011 // BinaryFormatter is obsolete and insecure
            BinaryFormatter formatter = new BinaryFormatter { Binder = null };
            formatter.Serialize(buffer, before);

            Assert.IsTrue(buffer.Position > 0L);

            buffer.Seek(0, SeekOrigin.Begin);

            after = formatter.Deserialize(buffer) as ArgumentNullOrEmptyException;
#pragma warning restore CA2300, CA2301, SYSLIB0011
        }

        Assert.IsNotNull(after);
        Assert.AreEqual(null        , after.InnerException);
        Assert.AreEqual("parameter1", after.ParamName     );

        // Message will contain the value from the ctor, a statement about the parameter, and the actual value
        Assert.IsTrue(after.Message.StartsWith("Hello World", StringComparison.Ordinal));
        Assert.IsTrue(after.Message.Contains  ("parameter1" , StringComparison.Ordinal));
    }
}
