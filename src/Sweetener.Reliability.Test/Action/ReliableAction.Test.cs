// Generated from ReliableAction.Test.tt
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sweetener.Reliability.Test
{
        #region ReliableAction

        [TestClass]
        public class ReliableAction_0_Test
        {
            [TestMethod]
            public void Ctor()
            {
                // DelayPolicy
                Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction(null, Retry.Infinite, ExceptionPolicies.Fatal, DelayPolicies.None));
                Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ReliableAction(() => Console.WriteLine("Hello World"), -2            , ExceptionPolicies.Fatal, DelayPolicies.None));
                Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction(() => Console.WriteLine("Hello World"), Retry.Infinite, null                   , DelayPolicies.None));
                Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction(() => Console.WriteLine("Hello World"), Retry.Infinite, ExceptionPolicies.Fatal, (DelayPolicy)null ));

                // ComplexDelayPolicy
                Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction(null, Retry.Infinite, ExceptionPolicies.Fatal, (i, e) => TimeSpan.Zero));
                Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ReliableAction(() => Console.WriteLine("Hello World"), -2            , ExceptionPolicies.Fatal, (i, e) => TimeSpan.Zero ));
                Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction(() => Console.WriteLine("Hello World"), Retry.Infinite, null                   , (i, e) => TimeSpan.Zero ));
                Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction(() => Console.WriteLine("Hello World"), Retry.Infinite, ExceptionPolicies.Fatal, (ComplexDelayPolicy)null));
            }
        }

        #endregion

        #region ReliableAction<T>

        [TestClass]
        public class ReliableAction_1_Test
        {
            [TestMethod]
            public void Ctor()
            {
                // DelayPolicy
                Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int>(null, Retry.Infinite, ExceptionPolicies.Fatal, DelayPolicies.None));
                Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ReliableAction<int>((arg) => Console.WriteLine("Hello World"), -2            , ExceptionPolicies.Fatal, DelayPolicies.None));
                Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int>((arg) => Console.WriteLine("Hello World"), Retry.Infinite, null                   , DelayPolicies.None));
                Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int>((arg) => Console.WriteLine("Hello World"), Retry.Infinite, ExceptionPolicies.Fatal, (DelayPolicy)null ));

                // ComplexDelayPolicy
                Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int>(null, Retry.Infinite, ExceptionPolicies.Fatal, (i, e) => TimeSpan.Zero));
                Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ReliableAction<int>((arg) => Console.WriteLine("Hello World"), -2            , ExceptionPolicies.Fatal, (i, e) => TimeSpan.Zero ));
                Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int>((arg) => Console.WriteLine("Hello World"), Retry.Infinite, null                   , (i, e) => TimeSpan.Zero ));
                Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int>((arg) => Console.WriteLine("Hello World"), Retry.Infinite, ExceptionPolicies.Fatal, (ComplexDelayPolicy)null));
            }
        }

        #endregion

        #region ReliableAction<T1, T2>

        [TestClass]
        public class ReliableAction_2_Test
        {
            [TestMethod]
            public void Ctor()
            {
                // DelayPolicy
                Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string>(null, Retry.Infinite, ExceptionPolicies.Fatal, DelayPolicies.None));
                Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ReliableAction<int, string>((arg1, arg2) => Console.WriteLine("Hello World"), -2            , ExceptionPolicies.Fatal, DelayPolicies.None));
                Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string>((arg1, arg2) => Console.WriteLine("Hello World"), Retry.Infinite, null                   , DelayPolicies.None));
                Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string>((arg1, arg2) => Console.WriteLine("Hello World"), Retry.Infinite, ExceptionPolicies.Fatal, (DelayPolicy)null ));

                // ComplexDelayPolicy
                Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string>(null, Retry.Infinite, ExceptionPolicies.Fatal, (i, e) => TimeSpan.Zero));
                Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ReliableAction<int, string>((arg1, arg2) => Console.WriteLine("Hello World"), -2            , ExceptionPolicies.Fatal, (i, e) => TimeSpan.Zero ));
                Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string>((arg1, arg2) => Console.WriteLine("Hello World"), Retry.Infinite, null                   , (i, e) => TimeSpan.Zero ));
                Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string>((arg1, arg2) => Console.WriteLine("Hello World"), Retry.Infinite, ExceptionPolicies.Fatal, (ComplexDelayPolicy)null));
            }
        }

        #endregion

        #region ReliableAction<T1, T2, T3>

        [TestClass]
        public class ReliableAction_3_Test
        {
            [TestMethod]
            public void Ctor()
            {
                // DelayPolicy
                Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double>(null, Retry.Infinite, ExceptionPolicies.Fatal, DelayPolicies.None));
                Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ReliableAction<int, string, double>((arg1, arg2, arg3) => Console.WriteLine("Hello World"), -2            , ExceptionPolicies.Fatal, DelayPolicies.None));
                Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double>((arg1, arg2, arg3) => Console.WriteLine("Hello World"), Retry.Infinite, null                   , DelayPolicies.None));
                Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double>((arg1, arg2, arg3) => Console.WriteLine("Hello World"), Retry.Infinite, ExceptionPolicies.Fatal, (DelayPolicy)null ));

                // ComplexDelayPolicy
                Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double>(null, Retry.Infinite, ExceptionPolicies.Fatal, (i, e) => TimeSpan.Zero));
                Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ReliableAction<int, string, double>((arg1, arg2, arg3) => Console.WriteLine("Hello World"), -2            , ExceptionPolicies.Fatal, (i, e) => TimeSpan.Zero ));
                Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double>((arg1, arg2, arg3) => Console.WriteLine("Hello World"), Retry.Infinite, null                   , (i, e) => TimeSpan.Zero ));
                Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double>((arg1, arg2, arg3) => Console.WriteLine("Hello World"), Retry.Infinite, ExceptionPolicies.Fatal, (ComplexDelayPolicy)null));
            }
        }

        #endregion

        #region ReliableAction<T1, T2, T3, T4>

        [TestClass]
        public class ReliableAction_4_Test
        {
            [TestMethod]
            public void Ctor()
            {
                // DelayPolicy
                Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long>(null, Retry.Infinite, ExceptionPolicies.Fatal, DelayPolicies.None));
                Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ReliableAction<int, string, double, long>((arg1, arg2, arg3, arg4) => Console.WriteLine("Hello World"), -2            , ExceptionPolicies.Fatal, DelayPolicies.None));
                Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long>((arg1, arg2, arg3, arg4) => Console.WriteLine("Hello World"), Retry.Infinite, null                   , DelayPolicies.None));
                Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long>((arg1, arg2, arg3, arg4) => Console.WriteLine("Hello World"), Retry.Infinite, ExceptionPolicies.Fatal, (DelayPolicy)null ));

                // ComplexDelayPolicy
                Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long>(null, Retry.Infinite, ExceptionPolicies.Fatal, (i, e) => TimeSpan.Zero));
                Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ReliableAction<int, string, double, long>((arg1, arg2, arg3, arg4) => Console.WriteLine("Hello World"), -2            , ExceptionPolicies.Fatal, (i, e) => TimeSpan.Zero ));
                Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long>((arg1, arg2, arg3, arg4) => Console.WriteLine("Hello World"), Retry.Infinite, null                   , (i, e) => TimeSpan.Zero ));
                Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long>((arg1, arg2, arg3, arg4) => Console.WriteLine("Hello World"), Retry.Infinite, ExceptionPolicies.Fatal, (ComplexDelayPolicy)null));
            }
        }

        #endregion

        #region ReliableAction<T1, T2, T3, T4, T5>

        [TestClass]
        public class ReliableAction_5_Test
        {
            [TestMethod]
            public void Ctor()
            {
                // DelayPolicy
                Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort>(null, Retry.Infinite, ExceptionPolicies.Fatal, DelayPolicies.None));
                Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ReliableAction<int, string, double, long, ushort>((arg1, arg2, arg3, arg4, arg5) => Console.WriteLine("Hello World"), -2            , ExceptionPolicies.Fatal, DelayPolicies.None));
                Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort>((arg1, arg2, arg3, arg4, arg5) => Console.WriteLine("Hello World"), Retry.Infinite, null                   , DelayPolicies.None));
                Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort>((arg1, arg2, arg3, arg4, arg5) => Console.WriteLine("Hello World"), Retry.Infinite, ExceptionPolicies.Fatal, (DelayPolicy)null ));

                // ComplexDelayPolicy
                Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort>(null, Retry.Infinite, ExceptionPolicies.Fatal, (i, e) => TimeSpan.Zero));
                Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ReliableAction<int, string, double, long, ushort>((arg1, arg2, arg3, arg4, arg5) => Console.WriteLine("Hello World"), -2            , ExceptionPolicies.Fatal, (i, e) => TimeSpan.Zero ));
                Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort>((arg1, arg2, arg3, arg4, arg5) => Console.WriteLine("Hello World"), Retry.Infinite, null                   , (i, e) => TimeSpan.Zero ));
                Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort>((arg1, arg2, arg3, arg4, arg5) => Console.WriteLine("Hello World"), Retry.Infinite, ExceptionPolicies.Fatal, (ComplexDelayPolicy)null));
            }
        }

        #endregion

        #region ReliableAction<T1, T2, T3, T4, T5, T6>

        [TestClass]
        public class ReliableAction_6_Test
        {
            [TestMethod]
            public void Ctor()
            {
                // DelayPolicy
                Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort, byte>(null, Retry.Infinite, ExceptionPolicies.Fatal, DelayPolicies.None));
                Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ReliableAction<int, string, double, long, ushort, byte>((arg1, arg2, arg3, arg4, arg5, arg6) => Console.WriteLine("Hello World"), -2            , ExceptionPolicies.Fatal, DelayPolicies.None));
                Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort, byte>((arg1, arg2, arg3, arg4, arg5, arg6) => Console.WriteLine("Hello World"), Retry.Infinite, null                   , DelayPolicies.None));
                Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort, byte>((arg1, arg2, arg3, arg4, arg5, arg6) => Console.WriteLine("Hello World"), Retry.Infinite, ExceptionPolicies.Fatal, (DelayPolicy)null ));

                // ComplexDelayPolicy
                Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort, byte>(null, Retry.Infinite, ExceptionPolicies.Fatal, (i, e) => TimeSpan.Zero));
                Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ReliableAction<int, string, double, long, ushort, byte>((arg1, arg2, arg3, arg4, arg5, arg6) => Console.WriteLine("Hello World"), -2            , ExceptionPolicies.Fatal, (i, e) => TimeSpan.Zero ));
                Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort, byte>((arg1, arg2, arg3, arg4, arg5, arg6) => Console.WriteLine("Hello World"), Retry.Infinite, null                   , (i, e) => TimeSpan.Zero ));
                Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort, byte>((arg1, arg2, arg3, arg4, arg5, arg6) => Console.WriteLine("Hello World"), Retry.Infinite, ExceptionPolicies.Fatal, (ComplexDelayPolicy)null));
            }
        }

        #endregion

        #region ReliableAction<T1, T2, T3, T4, T5, T6, T7>

        [TestClass]
        public class ReliableAction_7_Test
        {
            [TestMethod]
            public void Ctor()
            {
                // DelayPolicy
                Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan>(null, Retry.Infinite, ExceptionPolicies.Fatal, DelayPolicies.None));
                Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan>((arg1, arg2, arg3, arg4, arg5, arg6, arg7) => Console.WriteLine("Hello World"), -2            , ExceptionPolicies.Fatal, DelayPolicies.None));
                Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan>((arg1, arg2, arg3, arg4, arg5, arg6, arg7) => Console.WriteLine("Hello World"), Retry.Infinite, null                   , DelayPolicies.None));
                Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan>((arg1, arg2, arg3, arg4, arg5, arg6, arg7) => Console.WriteLine("Hello World"), Retry.Infinite, ExceptionPolicies.Fatal, (DelayPolicy)null ));

                // ComplexDelayPolicy
                Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan>(null, Retry.Infinite, ExceptionPolicies.Fatal, (i, e) => TimeSpan.Zero));
                Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan>((arg1, arg2, arg3, arg4, arg5, arg6, arg7) => Console.WriteLine("Hello World"), -2            , ExceptionPolicies.Fatal, (i, e) => TimeSpan.Zero ));
                Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan>((arg1, arg2, arg3, arg4, arg5, arg6, arg7) => Console.WriteLine("Hello World"), Retry.Infinite, null                   , (i, e) => TimeSpan.Zero ));
                Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan>((arg1, arg2, arg3, arg4, arg5, arg6, arg7) => Console.WriteLine("Hello World"), Retry.Infinite, ExceptionPolicies.Fatal, (ComplexDelayPolicy)null));
            }
        }

        #endregion

        #region ReliableAction<T1, T2, T3, T4, T5, T6, T7, T8>

        [TestClass]
        public class ReliableAction_8_Test
        {
            [TestMethod]
            public void Ctor()
            {
                // DelayPolicy
                Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint>(null, Retry.Infinite, ExceptionPolicies.Fatal, DelayPolicies.None));
                Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint>((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8) => Console.WriteLine("Hello World"), -2            , ExceptionPolicies.Fatal, DelayPolicies.None));
                Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint>((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8) => Console.WriteLine("Hello World"), Retry.Infinite, null                   , DelayPolicies.None));
                Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint>((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8) => Console.WriteLine("Hello World"), Retry.Infinite, ExceptionPolicies.Fatal, (DelayPolicy)null ));

                // ComplexDelayPolicy
                Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint>(null, Retry.Infinite, ExceptionPolicies.Fatal, (i, e) => TimeSpan.Zero));
                Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint>((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8) => Console.WriteLine("Hello World"), -2            , ExceptionPolicies.Fatal, (i, e) => TimeSpan.Zero ));
                Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint>((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8) => Console.WriteLine("Hello World"), Retry.Infinite, null                   , (i, e) => TimeSpan.Zero ));
                Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint>((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8) => Console.WriteLine("Hello World"), Retry.Infinite, ExceptionPolicies.Fatal, (ComplexDelayPolicy)null));
            }
        }

        #endregion

        #region ReliableAction<T1, T2, T3, T4, T5, T6, T7, T8, T9>

        [TestClass]
        public class ReliableAction_9_Test
        {
            [TestMethod]
            public void Ctor()
            {
                // DelayPolicy
                Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, object>(null, Retry.Infinite, ExceptionPolicies.Fatal, DelayPolicies.None));
                Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, object>((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9) => Console.WriteLine("Hello World"), -2            , ExceptionPolicies.Fatal, DelayPolicies.None));
                Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, object>((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9) => Console.WriteLine("Hello World"), Retry.Infinite, null                   , DelayPolicies.None));
                Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, object>((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9) => Console.WriteLine("Hello World"), Retry.Infinite, ExceptionPolicies.Fatal, (DelayPolicy)null ));

                // ComplexDelayPolicy
                Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, object>(null, Retry.Infinite, ExceptionPolicies.Fatal, (i, e) => TimeSpan.Zero));
                Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, object>((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9) => Console.WriteLine("Hello World"), -2            , ExceptionPolicies.Fatal, (i, e) => TimeSpan.Zero ));
                Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, object>((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9) => Console.WriteLine("Hello World"), Retry.Infinite, null                   , (i, e) => TimeSpan.Zero ));
                Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, object>((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9) => Console.WriteLine("Hello World"), Retry.Infinite, ExceptionPolicies.Fatal, (ComplexDelayPolicy)null));
            }
        }

        #endregion

        #region ReliableAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>

        [TestClass]
        public class ReliableAction_10_Test
        {
            [TestMethod]
            public void Ctor()
            {
                // DelayPolicy
                Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, object, DateTime>(null, Retry.Infinite, ExceptionPolicies.Fatal, DelayPolicies.None));
                Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, object, DateTime>((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10) => Console.WriteLine("Hello World"), -2            , ExceptionPolicies.Fatal, DelayPolicies.None));
                Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, object, DateTime>((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10) => Console.WriteLine("Hello World"), Retry.Infinite, null                   , DelayPolicies.None));
                Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, object, DateTime>((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10) => Console.WriteLine("Hello World"), Retry.Infinite, ExceptionPolicies.Fatal, (DelayPolicy)null ));

                // ComplexDelayPolicy
                Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, object, DateTime>(null, Retry.Infinite, ExceptionPolicies.Fatal, (i, e) => TimeSpan.Zero));
                Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, object, DateTime>((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10) => Console.WriteLine("Hello World"), -2            , ExceptionPolicies.Fatal, (i, e) => TimeSpan.Zero ));
                Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, object, DateTime>((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10) => Console.WriteLine("Hello World"), Retry.Infinite, null                   , (i, e) => TimeSpan.Zero ));
                Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, object, DateTime>((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10) => Console.WriteLine("Hello World"), Retry.Infinite, ExceptionPolicies.Fatal, (ComplexDelayPolicy)null));
            }
        }

        #endregion

        #region ReliableAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>

        [TestClass]
        public class ReliableAction_11_Test
        {
            [TestMethod]
            public void Ctor()
            {
                // DelayPolicy
                Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, object, DateTime, ulong>(null, Retry.Infinite, ExceptionPolicies.Fatal, DelayPolicies.None));
                Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, object, DateTime, ulong>((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11) => Console.WriteLine("Hello World"), -2            , ExceptionPolicies.Fatal, DelayPolicies.None));
                Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, object, DateTime, ulong>((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11) => Console.WriteLine("Hello World"), Retry.Infinite, null                   , DelayPolicies.None));
                Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, object, DateTime, ulong>((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11) => Console.WriteLine("Hello World"), Retry.Infinite, ExceptionPolicies.Fatal, (DelayPolicy)null ));

                // ComplexDelayPolicy
                Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, object, DateTime, ulong>(null, Retry.Infinite, ExceptionPolicies.Fatal, (i, e) => TimeSpan.Zero));
                Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, object, DateTime, ulong>((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11) => Console.WriteLine("Hello World"), -2            , ExceptionPolicies.Fatal, (i, e) => TimeSpan.Zero ));
                Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, object, DateTime, ulong>((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11) => Console.WriteLine("Hello World"), Retry.Infinite, null                   , (i, e) => TimeSpan.Zero ));
                Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, object, DateTime, ulong>((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11) => Console.WriteLine("Hello World"), Retry.Infinite, ExceptionPolicies.Fatal, (ComplexDelayPolicy)null));
            }
        }

        #endregion

        #region ReliableAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>

        [TestClass]
        public class ReliableAction_12_Test
        {
            [TestMethod]
            public void Ctor()
            {
                // DelayPolicy
                Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, object, DateTime, ulong, sbyte>(null, Retry.Infinite, ExceptionPolicies.Fatal, DelayPolicies.None));
                Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, object, DateTime, ulong, sbyte>((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12) => Console.WriteLine("Hello World"), -2            , ExceptionPolicies.Fatal, DelayPolicies.None));
                Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, object, DateTime, ulong, sbyte>((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12) => Console.WriteLine("Hello World"), Retry.Infinite, null                   , DelayPolicies.None));
                Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, object, DateTime, ulong, sbyte>((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12) => Console.WriteLine("Hello World"), Retry.Infinite, ExceptionPolicies.Fatal, (DelayPolicy)null ));

                // ComplexDelayPolicy
                Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, object, DateTime, ulong, sbyte>(null, Retry.Infinite, ExceptionPolicies.Fatal, (i, e) => TimeSpan.Zero));
                Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, object, DateTime, ulong, sbyte>((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12) => Console.WriteLine("Hello World"), -2            , ExceptionPolicies.Fatal, (i, e) => TimeSpan.Zero ));
                Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, object, DateTime, ulong, sbyte>((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12) => Console.WriteLine("Hello World"), Retry.Infinite, null                   , (i, e) => TimeSpan.Zero ));
                Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, object, DateTime, ulong, sbyte>((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12) => Console.WriteLine("Hello World"), Retry.Infinite, ExceptionPolicies.Fatal, (ComplexDelayPolicy)null));
            }
        }

        #endregion

        #region ReliableAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>

        [TestClass]
        public class ReliableAction_13_Test
        {
            [TestMethod]
            public void Ctor()
            {
                // DelayPolicy
                Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, object, DateTime, ulong, sbyte, decimal>(null, Retry.Infinite, ExceptionPolicies.Fatal, DelayPolicies.None));
                Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, object, DateTime, ulong, sbyte, decimal>((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13) => Console.WriteLine("Hello World"), -2            , ExceptionPolicies.Fatal, DelayPolicies.None));
                Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, object, DateTime, ulong, sbyte, decimal>((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13) => Console.WriteLine("Hello World"), Retry.Infinite, null                   , DelayPolicies.None));
                Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, object, DateTime, ulong, sbyte, decimal>((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13) => Console.WriteLine("Hello World"), Retry.Infinite, ExceptionPolicies.Fatal, (DelayPolicy)null ));

                // ComplexDelayPolicy
                Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, object, DateTime, ulong, sbyte, decimal>(null, Retry.Infinite, ExceptionPolicies.Fatal, (i, e) => TimeSpan.Zero));
                Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, object, DateTime, ulong, sbyte, decimal>((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13) => Console.WriteLine("Hello World"), -2            , ExceptionPolicies.Fatal, (i, e) => TimeSpan.Zero ));
                Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, object, DateTime, ulong, sbyte, decimal>((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13) => Console.WriteLine("Hello World"), Retry.Infinite, null                   , (i, e) => TimeSpan.Zero ));
                Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, object, DateTime, ulong, sbyte, decimal>((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13) => Console.WriteLine("Hello World"), Retry.Infinite, ExceptionPolicies.Fatal, (ComplexDelayPolicy)null));
            }
        }

        #endregion

        #region ReliableAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>

        [TestClass]
        public class ReliableAction_14_Test
        {
            [TestMethod]
            public void Ctor()
            {
                // DelayPolicy
                Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, object, DateTime, ulong, sbyte, decimal, char>(null, Retry.Infinite, ExceptionPolicies.Fatal, DelayPolicies.None));
                Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, object, DateTime, ulong, sbyte, decimal, char>((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14) => Console.WriteLine("Hello World"), -2            , ExceptionPolicies.Fatal, DelayPolicies.None));
                Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, object, DateTime, ulong, sbyte, decimal, char>((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14) => Console.WriteLine("Hello World"), Retry.Infinite, null                   , DelayPolicies.None));
                Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, object, DateTime, ulong, sbyte, decimal, char>((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14) => Console.WriteLine("Hello World"), Retry.Infinite, ExceptionPolicies.Fatal, (DelayPolicy)null ));

                // ComplexDelayPolicy
                Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, object, DateTime, ulong, sbyte, decimal, char>(null, Retry.Infinite, ExceptionPolicies.Fatal, (i, e) => TimeSpan.Zero));
                Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, object, DateTime, ulong, sbyte, decimal, char>((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14) => Console.WriteLine("Hello World"), -2            , ExceptionPolicies.Fatal, (i, e) => TimeSpan.Zero ));
                Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, object, DateTime, ulong, sbyte, decimal, char>((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14) => Console.WriteLine("Hello World"), Retry.Infinite, null                   , (i, e) => TimeSpan.Zero ));
                Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, object, DateTime, ulong, sbyte, decimal, char>((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14) => Console.WriteLine("Hello World"), Retry.Infinite, ExceptionPolicies.Fatal, (ComplexDelayPolicy)null));
            }
        }

        #endregion

        #region ReliableAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>

        [TestClass]
        public class ReliableAction_15_Test
        {
            [TestMethod]
            public void Ctor()
            {
                // DelayPolicy
                Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, object, DateTime, ulong, sbyte, decimal, char, float>(null, Retry.Infinite, ExceptionPolicies.Fatal, DelayPolicies.None));
                Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, object, DateTime, ulong, sbyte, decimal, char, float>((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15) => Console.WriteLine("Hello World"), -2            , ExceptionPolicies.Fatal, DelayPolicies.None));
                Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, object, DateTime, ulong, sbyte, decimal, char, float>((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15) => Console.WriteLine("Hello World"), Retry.Infinite, null                   , DelayPolicies.None));
                Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, object, DateTime, ulong, sbyte, decimal, char, float>((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15) => Console.WriteLine("Hello World"), Retry.Infinite, ExceptionPolicies.Fatal, (DelayPolicy)null ));

                // ComplexDelayPolicy
                Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, object, DateTime, ulong, sbyte, decimal, char, float>(null, Retry.Infinite, ExceptionPolicies.Fatal, (i, e) => TimeSpan.Zero));
                Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, object, DateTime, ulong, sbyte, decimal, char, float>((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15) => Console.WriteLine("Hello World"), -2            , ExceptionPolicies.Fatal, (i, e) => TimeSpan.Zero ));
                Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, object, DateTime, ulong, sbyte, decimal, char, float>((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15) => Console.WriteLine("Hello World"), Retry.Infinite, null                   , (i, e) => TimeSpan.Zero ));
                Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, object, DateTime, ulong, sbyte, decimal, char, float>((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15) => Console.WriteLine("Hello World"), Retry.Infinite, ExceptionPolicies.Fatal, (ComplexDelayPolicy)null));
            }
        }

        #endregion

        #region ReliableAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>

        [TestClass]
        public class ReliableAction_16_Test
        {
            [TestMethod]
            public void Ctor()
            {
                // DelayPolicy
                Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, object, DateTime, ulong, sbyte, decimal, char, float, Guid>(null, Retry.Infinite, ExceptionPolicies.Fatal, DelayPolicies.None));
                Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, object, DateTime, ulong, sbyte, decimal, char, float, Guid>((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16) => Console.WriteLine("Hello World"), -2            , ExceptionPolicies.Fatal, DelayPolicies.None));
                Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, object, DateTime, ulong, sbyte, decimal, char, float, Guid>((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16) => Console.WriteLine("Hello World"), Retry.Infinite, null                   , DelayPolicies.None));
                Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, object, DateTime, ulong, sbyte, decimal, char, float, Guid>((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16) => Console.WriteLine("Hello World"), Retry.Infinite, ExceptionPolicies.Fatal, (DelayPolicy)null ));

                // ComplexDelayPolicy
                Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, object, DateTime, ulong, sbyte, decimal, char, float, Guid>(null, Retry.Infinite, ExceptionPolicies.Fatal, (i, e) => TimeSpan.Zero));
                Assert.ThrowsException<ArgumentOutOfRangeException>(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, object, DateTime, ulong, sbyte, decimal, char, float, Guid>((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16) => Console.WriteLine("Hello World"), -2            , ExceptionPolicies.Fatal, (i, e) => TimeSpan.Zero ));
                Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, object, DateTime, ulong, sbyte, decimal, char, float, Guid>((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16) => Console.WriteLine("Hello World"), Retry.Infinite, null                   , (i, e) => TimeSpan.Zero ));
                Assert.ThrowsException<ArgumentNullException      >(() => new ReliableAction<int, string, double, long, ushort, byte, TimeSpan, uint, object, DateTime, ulong, sbyte, decimal, char, float, Guid>((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16) => Console.WriteLine("Hello World"), Retry.Infinite, ExceptionPolicies.Fatal, (ComplexDelayPolicy)null));
            }
        }

        #endregion

}
