//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;

//namespace Sweetener.Reliability
//{
//    internal class ReliableFunc<T, TResult>
//    {
//        private readonly Func<T, TResult> _func;

//        public IRetryPolicy<TResult> RetryPolicy { get; set; }

//        public IOutputPolicy<TResult> OutputPolicy { get; set; }

//        public ReliableFunc(Func<T, TResult> func)
//        {
//            _func = func;
//        }

//        public ExecutionResult<TResult> Invoke(T arg)
//        {
//            TResult result;
//            int attempt = 0;
//            do
//            {
//                attempt++;
//                try
//                {
//                    result = _func(arg);
//                }
//                catch (Exception e)
//                {
//                    switch (OutputPolicy.GetExceptionBehavior(e))
//                    {
//                        case ExceptionBehavior.Retry:
//                            if (RetryPolicy.ShouldRetry())
//                        case ExceptionBehavior.Abort:
//                            return ExecutionResult<TResult>.Aborted(e, attempt);
//                        default:
//                            throw new UnknownBehaviorException();
//                    }
//                }

//                // Validate the result of the function
//                switch (OutputPolicy.GetResultBehavior(result))
//                {
//                    case ResultBehavior.Return:
//                        return ExecutionResult<TResult>.Succeeded(result, attempt);
//                    case ResultBehavior.Retry:
//                        continue;
//                    case ResultBehavior.Fail:
//                        return ExecutionResult<TResult>.Aborted(result, attempt);
//                    default:
//                        throw new UnknownBehaviorException();
//                }
//            } while (RetryPolicy.ShouldRetry(attempt) && RetryPolicy.GetResultDelay();

//            return ExecutionResult<TResult>.ExceededRetries(result, attempt);
//        }
//    }
//}
