﻿using System;

namespace Sweetener.Reliability.Test
{
    public class ReliableDelegateTest<T>
    {
        protected static readonly Func<ReliableDelegate<T>, ResultPolicy<T>      > s_getResultPolicy    = DynamicGetter.ForField<ReliableDelegate<T>, ResultPolicy<T>      >("_validate");
        protected static readonly Func<ReliableDelegate<T>, ExceptionPolicy      > s_getExceptionPolicy = DynamicGetter.ForField<ReliableDelegate<T>, ExceptionPolicy      >("_canRetry");
        protected static readonly Func<ReliableDelegate<T>, ComplexDelayPolicy<T>> s_getDelayPolicy     = DynamicGetter.ForField<ReliableDelegate<T>, ComplexDelayPolicy<T>>("_getDelay");
    }
}
