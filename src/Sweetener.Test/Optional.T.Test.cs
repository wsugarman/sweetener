// Copyright © 2021 William Sugarman. All Rights Reserved.
// Licensed under the MIT License.

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sweetener.Test
{
    [TestClass]
    public class OptionalTest
    {
        [TestMethod]
        public void Value()
        {
            Optional<double> optionalNumber;

            // Has Value
            optionalNumber = new Optional<double>(3.14D);
            Assert.AreEqual(3.14D, optionalNumber.Value);

            // Undefined
            optionalNumber = default;
            Assert.ThrowsException<InvalidOperationException>(() => optionalNumber.Value);
        }

        [TestMethod]
        public void Undefined()
        {
            Optional<byte> optionalNumber = Optional<byte>.Undefined;
            Assert.IsFalse(optionalNumber.HasValue);
            Assert.AreEqual((byte)0, optionalNumber.GetValueOrDefault());

            Optional<string> optionalString = Optional<string>.Undefined;
            Assert.IsFalse(optionalString.HasValue);
            Assert.AreEqual(null, optionalString.GetValueOrDefault());
        }

        [TestMethod]
        public void Ctor()
        {
            Optional<int> optionalNumber;

            // Default ctor
            optionalNumber = new Optional<int>();

            Assert.IsFalse(optionalNumber.HasValue);
            Assert.AreEqual(default, optionalNumber.GetValueOrDefault());

            // Default value
            optionalNumber = default;

            Assert.IsFalse(optionalNumber.HasValue);
            Assert.AreEqual(default, optionalNumber.GetValueOrDefault());
        }

        [TestMethod]
        public void Ctor_Value()
        {
            Optional<int> optionalNumber = new Optional<int>(42);

            Assert.IsTrue(optionalNumber.HasValue);
            Assert.AreEqual(42, optionalNumber.GetValueOrDefault());
        }

        [TestMethod]
        public void GetValueOrDefault()
        {
            Optional<string> optionalString;

            // Has Value
            optionalString = new Optional<string>("Foo");
            Assert.AreEqual("Foo", optionalString.GetValueOrDefault());

            // Undefined
            optionalString = default;
            Assert.AreEqual(default, optionalString.GetValueOrDefault());
        }

        [TestMethod]
        public void GetValueOrDefault_Value()
        {
            Optional<string> optionalString;

            // Has Value
            optionalString = new Optional<string>("Bar");
            Assert.AreEqual("Bar", optionalString.GetValueOrDefault("Baz"));

            // Undefined
            optionalString = default;
            Assert.AreEqual("Baz", optionalString.GetValueOrDefault("Baz"));
        }

        [TestMethod]
        public void TryGetValue()
        {
            Optional<DateTime> optionalDate;

            // Has Value
            optionalDate = new Optional<DateTime>(new DateTime(1234, 5, 6, 7, 8, 9));
            Assert.IsTrue(optionalDate.TryGetValue(out DateTime value));
            Assert.AreEqual(new DateTime(1234, 5, 6, 7, 8, 9), value);

            // Undefined
            optionalDate = default;
            Assert.IsFalse(optionalDate.TryGetValue(out value));
            Assert.AreEqual(default, value);
        }

        [TestMethod]
        public void Equals()
        {
            Optional<TimeSpan> optionalTime;
            Optional<string?> optionalString;

            #region Has Value

            optionalTime = new Optional<TimeSpan>(new TimeSpan(10, 11, 12));

            Assert.IsTrue (optionalTime.Equals(new Optional<TimeSpan>(new TimeSpan(10, 11, 12)))); // Match
            Assert.IsTrue (optionalTime.Equals(                       new TimeSpan(10, 11, 12 ))); // Match underlying
            Assert.IsFalse(optionalTime.Equals(new Optional<TimeSpan>(new TimeSpan(13, 14, 15)))); // No match
            Assert.IsFalse(optionalTime.Equals(new Optional<float>(16.1718F)));                    // No match (wrong type)
            Assert.IsFalse(optionalTime.Equals(new object()));                                     // No match (wrong type again)
            Assert.IsFalse(optionalTime.Equals(null));                                             // No match (null)

            optionalString = new Optional<string?>("Hello World");

            Assert.IsTrue (optionalString.Equals(new Optional<string>("Hello World"  ))); // Match
            Assert.IsTrue (optionalString.Equals(                     "Hello World"   )); // Match underlying
            Assert.IsFalse(optionalString.Equals(new Optional<string>("Goodbye World"))); // No match
            Assert.IsFalse(optionalString.Equals(new Optional<Guid>  (Guid.NewGuid() ))); // No match (wrong type)
            Assert.IsFalse(optionalString.Equals(new object()));                          // No match (wrong type again)
            Assert.IsFalse(optionalString.Equals(null));                                  // No match (null)

            #endregion

            #region Has (Default) Value

            optionalTime = new Optional<TimeSpan>(default);

            Assert.IsTrue (optionalTime.Equals(new Optional<TimeSpan>(default)));                  // Match
            Assert.IsTrue (optionalTime.Equals(new TimeSpan()));                                   // Match underlying
            Assert.IsFalse(optionalTime.Equals(new Optional<TimeSpan>(new TimeSpan(13, 14, 15)))); // No match
            Assert.IsFalse(optionalTime.Equals(new Optional<float>()));                            // No match (wrong type)
            Assert.IsFalse(optionalTime.Equals(new object()));                                     // No match (wrong type again)
            Assert.IsFalse(optionalTime.Equals(null));                                             // No match (null)

            optionalString = new Optional<string?>(default);

            Assert.IsTrue (optionalString.Equals(new Optional<string?>(default)));        // Match
            Assert.IsTrue (optionalString.Equals(null));                                  // Match underlying
            Assert.IsFalse(optionalString.Equals(new Optional<string>("Goodbye World"))); // No match
            Assert.IsFalse(optionalString.Equals(new Optional<Guid>(Guid.NewGuid())));    // No match (wrong type)
            Assert.IsFalse(optionalString.Equals(new object()));                          // No match (wrong type again)

            #endregion

            #region Undefined

            optionalTime = default;

            Assert.IsTrue (optionalTime.Equals(new Optional<TimeSpan>()));                         // Match
            Assert.IsFalse(optionalTime.Equals(new Optional<TimeSpan>(new TimeSpan(13, 14, 15)))); // No match
            Assert.IsFalse(optionalTime.Equals(new TimeSpan()));                                   // No match (despite matching value)
            Assert.IsFalse(optionalTime.Equals(new Optional<float>()));                            // No match (wrong type)
            Assert.IsFalse(optionalTime.Equals(new object()));                                     // No match (wrong type again)
            Assert.IsFalse(optionalTime.Equals(null));                                             // No match (null)

            optionalString = default;

            Assert.IsTrue (optionalString.Equals(new Optional<string>()));                // Match
            Assert.IsFalse(optionalString.Equals(new Optional<string>("Goodbye World"))); // No match
            Assert.IsFalse(optionalString.Equals(null));                                  // No match (despite matching value)
            Assert.IsFalse(optionalString.Equals(new Optional<Guid>(Guid.NewGuid())));    // No match (wrong type)
            Assert.IsFalse(optionalString.Equals(new object()));                          // No match (wrong type again)

            #endregion
        }

        [TestMethod]
        public void GetHashCodeTest()
        {
            Optional<TimeSpan> optionalTime;
            Optional<string?> optionalString;

            #region Has Value

            optionalTime = new Optional<TimeSpan>(new TimeSpan(10, 11, 12));
            Assert.AreEqual(new TimeSpan(10, 11, 12).GetHashCode(), optionalTime.GetHashCode());

            optionalString = new Optional<string?>("Hello World");
            Assert.AreEqual("Hello World".GetHashCode(), optionalString.GetHashCode());

            #endregion

            #region Has (Default) Value

            // It happens that default(TimeSpan).GetHashCode() == 0
            optionalTime = new Optional<TimeSpan>(default);
            Assert.AreEqual(new TimeSpan().GetHashCode(), optionalTime.GetHashCode());

            optionalString = new Optional<string?>(default);
            Assert.AreEqual(0, optionalString.GetHashCode());

            #endregion

            #region Undefined

            optionalTime = default;
            Assert.AreEqual(0, optionalTime.GetHashCode());

            optionalString = default;
            Assert.AreEqual(0, optionalString.GetHashCode());

            #endregion
        }

        [TestMethod]
        public void ToStringTest()
        {
            Optional<TimeSpan> optionalTime;
            Optional<string?> optionalString;

            #region Has Value

            optionalTime = new Optional<TimeSpan>(new TimeSpan(10, 11, 12));
            Assert.AreEqual(new TimeSpan(10, 11, 12).ToString(), optionalTime.ToString());

            optionalString = new Optional<string?>("Hello World");
            Assert.AreEqual("Hello World", optionalString.ToString());

            #endregion

            #region Has (Default) Value

            optionalTime = new Optional<TimeSpan>(default);
            Assert.AreEqual(new TimeSpan().ToString(), optionalTime.ToString());

            optionalString = new Optional<string?>(default);
            Assert.AreEqual(string.Empty, optionalString.ToString());

            #endregion

            #region Undefined

            optionalTime = default;
            Assert.AreEqual(string.Empty, optionalTime.ToString());

            optionalString = default;
            Assert.AreEqual(string.Empty, optionalString.ToString());

            #endregion
        }

        [TestMethod]
        public void OptionalCastOperator()
        {
            Optional<int> optionalNumber = 42;

            Assert.AreEqual(true, optionalNumber.HasValue);
            Assert.AreEqual(42  , optionalNumber.Value   );
        }

        [TestMethod]
        public void ValueCastOperator()
        {
            Optional<double> optionalNumber;

            // Has Value
            optionalNumber = new Optional<double>(3.14D);
            Assert.AreEqual(3.14D, (double)optionalNumber);

            // Undefined
            optionalNumber = default;
            Assert.ThrowsException<InvalidOperationException>(() => (double)optionalNumber);
        }
    }
}
