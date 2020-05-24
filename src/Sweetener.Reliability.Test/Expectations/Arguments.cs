// Generated from Arguments.tt
using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sweetener.Reliability.Test
{
    internal static class Arguments
    {
        public static void Validate(CancellationToken token)
        {
            Assert.AreNotEqual(CancellationToken.None, token);
        }

        public static void Validate(int arg)
        {
            Assert.AreEqual(42, arg);
        }

        public static void Validate(int arg, CancellationToken token)
        {
            Validate(arg);
            Assert.AreNotEqual(CancellationToken.None, token);
        }

        public static void Validate(int arg1, string arg2)
        {
            Assert.AreEqual(42   , arg1);
            Assert.AreEqual("foo", arg2);
        }

        public static void Validate(int arg1, string arg2, CancellationToken token)
        {
            Validate(arg1, arg2);
            Assert.AreNotEqual(CancellationToken.None, token);
        }

        public static void Validate(int arg1, string arg2, double arg3)
        {
            Assert.AreEqual(42   , arg1);
            Assert.AreEqual("foo", arg2);
            Assert.AreEqual(3.14D, arg3);
        }

        public static void Validate(int arg1, string arg2, double arg3, CancellationToken token)
        {
            Validate(arg1, arg2, arg3);
            Assert.AreNotEqual(CancellationToken.None, token);
        }

        public static void Validate(int arg1, string arg2, double arg3, long arg4)
        {
            Assert.AreEqual(42   , arg1);
            Assert.AreEqual("foo", arg2);
            Assert.AreEqual(3.14D, arg3);
            Assert.AreEqual(1000L, arg4);
        }

        public static void Validate(int arg1, string arg2, double arg3, long arg4, CancellationToken token)
        {
            Validate(arg1, arg2, arg3, arg4);
            Assert.AreNotEqual(CancellationToken.None, token);
        }

        public static void Validate(int arg1, string arg2, double arg3, long arg4, ushort arg5)
        {
            Assert.AreEqual(42       , arg1);
            Assert.AreEqual("foo"    , arg2);
            Assert.AreEqual(3.14D    , arg3);
            Assert.AreEqual(1000L    , arg4);
            Assert.AreEqual((ushort)1, arg5);
        }

        public static void Validate(int arg1, string arg2, double arg3, long arg4, ushort arg5, CancellationToken token)
        {
            Validate(arg1, arg2, arg3, arg4, arg5);
            Assert.AreNotEqual(CancellationToken.None, token);
        }

        public static void Validate(int arg1, string arg2, double arg3, long arg4, ushort arg5, byte arg6)
        {
            Assert.AreEqual(42       , arg1);
            Assert.AreEqual("foo"    , arg2);
            Assert.AreEqual(3.14D    , arg3);
            Assert.AreEqual(1000L    , arg4);
            Assert.AreEqual((ushort)1, arg5);
            Assert.AreEqual((byte)255, arg6);
        }

        public static void Validate(int arg1, string arg2, double arg3, long arg4, ushort arg5, byte arg6, CancellationToken token)
        {
            Validate(arg1, arg2, arg3, arg4, arg5, arg6);
            Assert.AreNotEqual(CancellationToken.None, token);
        }

        public static void Validate(int arg1, string arg2, double arg3, long arg4, ushort arg5, byte arg6, TimeSpan arg7)
        {
            Assert.AreEqual(42                   , arg1);
            Assert.AreEqual("foo"                , arg2);
            Assert.AreEqual(3.14D                , arg3);
            Assert.AreEqual(1000L                , arg4);
            Assert.AreEqual((ushort)1            , arg5);
            Assert.AreEqual((byte)255            , arg6);
            Assert.AreEqual(TimeSpan.FromDays(30), arg7);
        }

        public static void Validate(int arg1, string arg2, double arg3, long arg4, ushort arg5, byte arg6, TimeSpan arg7, CancellationToken token)
        {
            Validate(arg1, arg2, arg3, arg4, arg5, arg6, arg7);
            Assert.AreNotEqual(CancellationToken.None, token);
        }

        public static void Validate(int arg1, string arg2, double arg3, long arg4, ushort arg5, byte arg6, TimeSpan arg7, uint arg8)
        {
            Assert.AreEqual(42                   , arg1);
            Assert.AreEqual("foo"                , arg2);
            Assert.AreEqual(3.14D                , arg3);
            Assert.AreEqual(1000L                , arg4);
            Assert.AreEqual((ushort)1            , arg5);
            Assert.AreEqual((byte)255            , arg6);
            Assert.AreEqual(TimeSpan.FromDays(30), arg7);
            Assert.AreEqual(112U                 , arg8);
        }

        public static void Validate(int arg1, string arg2, double arg3, long arg4, ushort arg5, byte arg6, TimeSpan arg7, uint arg8, CancellationToken token)
        {
            Validate(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
            Assert.AreNotEqual(CancellationToken.None, token);
        }

    }
}
