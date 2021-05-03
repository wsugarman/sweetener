// Copyright © William Sugarman.
// Licensed under the MIT License.

// Do not modify this file. It was automatically generated from MultiTask.tt

using System;
using System.Threading.Tasks;

namespace Sweetener.Threading.Tasks
{
    /// <summary>
    /// Provides a set of <see langword="static"/> methods for interacting with multiple instances of <see cref="Task{T}"/> and <see cref="ValueTask{T}"/>.
    /// </summary>
    public static class MultiTask
    {
        /// <summary>
        /// Creates a task that will complete when all of the <see cref="Task{TResult}"/> objects have completed.
        /// </summary>
        /// <typeparam name="T1">The result type of the first task.</typeparam>
        /// <typeparam name="T2">The result type of the second task.</typeparam>
        /// <param name="task1">The first task to wait on for completion.</param>
        /// <param name="task2">The second task to wait on for completion.</param>
        /// <returns>A task that represents the completion of all of the supplied tasks.</returns>
        public static async Task<(T1, T2)> WhenAll<T1, T2>(Task<T1> task1, Task<T2> task2)
        {
            if (task1 == null)
                throw new ArgumentNullException(nameof(task1));

            if (task2 == null)
                throw new ArgumentNullException(nameof(task2));

            await Task.WhenAll(task1, task2).ConfigureAwait(false);
            return (task1.Result, task2.Result);
        }

        /// <summary>
        /// Creates a task that will complete when all of the <see cref="Task{TResult}"/> objects have completed.
        /// </summary>
        /// <typeparam name="T1">The result type of the first task.</typeparam>
        /// <typeparam name="T2">The result type of the second task.</typeparam>
        /// <typeparam name="T3">The result type of the third task.</typeparam>
        /// <param name="task1">The first task to wait on for completion.</param>
        /// <param name="task2">The second task to wait on for completion.</param>
        /// <param name="task3">The third task to wait on for completion.</param>
        /// <returns>A task that represents the completion of all of the supplied tasks.</returns>
        public static async Task<(T1, T2, T3)> WhenAll<T1, T2, T3>(Task<T1> task1, Task<T2> task2, Task<T3> task3)
        {
            if (task1 == null)
                throw new ArgumentNullException(nameof(task1));

            if (task2 == null)
                throw new ArgumentNullException(nameof(task2));

            if (task3 == null)
                throw new ArgumentNullException(nameof(task3));

            await Task.WhenAll(task1, task2, task3).ConfigureAwait(false);
            return (task1.Result, task2.Result, task3.Result);
        }

        /// <summary>
        /// Creates a task that will complete when all of the <see cref="Task{TResult}"/> objects have completed.
        /// </summary>
        /// <typeparam name="T1">The result type of the first task.</typeparam>
        /// <typeparam name="T2">The result type of the second task.</typeparam>
        /// <typeparam name="T3">The result type of the third task.</typeparam>
        /// <typeparam name="T4">The result type of the fourth task.</typeparam>
        /// <param name="task1">The first task to wait on for completion.</param>
        /// <param name="task2">The second task to wait on for completion.</param>
        /// <param name="task3">The third task to wait on for completion.</param>
        /// <param name="task4">The fourth task to wait on for completion.</param>
        /// <returns>A task that represents the completion of all of the supplied tasks.</returns>
        public static async Task<(T1, T2, T3, T4)> WhenAll<T1, T2, T3, T4>(Task<T1> task1, Task<T2> task2, Task<T3> task3, Task<T4> task4)
        {
            if (task1 == null)
                throw new ArgumentNullException(nameof(task1));

            if (task2 == null)
                throw new ArgumentNullException(nameof(task2));

            if (task3 == null)
                throw new ArgumentNullException(nameof(task3));

            if (task4 == null)
                throw new ArgumentNullException(nameof(task4));

            await Task.WhenAll(task1, task2, task3, task4).ConfigureAwait(false);
            return (task1.Result, task2.Result, task3.Result, task4.Result);
        }

        /// <summary>
        /// Creates a task that will complete when all of the <see cref="Task{TResult}"/> objects have completed.
        /// </summary>
        /// <typeparam name="T1">The result type of the first task.</typeparam>
        /// <typeparam name="T2">The result type of the second task.</typeparam>
        /// <typeparam name="T3">The result type of the third task.</typeparam>
        /// <typeparam name="T4">The result type of the fourth task.</typeparam>
        /// <typeparam name="T5">The result type of the fifth task.</typeparam>
        /// <param name="task1">The first task to wait on for completion.</param>
        /// <param name="task2">The second task to wait on for completion.</param>
        /// <param name="task3">The third task to wait on for completion.</param>
        /// <param name="task4">The fourth task to wait on for completion.</param>
        /// <param name="task5">The fifth task to wait on for completion.</param>
        /// <returns>A task that represents the completion of all of the supplied tasks.</returns>
        public static async Task<(T1, T2, T3, T4, T5)> WhenAll<T1, T2, T3, T4, T5>(Task<T1> task1, Task<T2> task2, Task<T3> task3, Task<T4> task4, Task<T5> task5)
        {
            if (task1 == null)
                throw new ArgumentNullException(nameof(task1));

            if (task2 == null)
                throw new ArgumentNullException(nameof(task2));

            if (task3 == null)
                throw new ArgumentNullException(nameof(task3));

            if (task4 == null)
                throw new ArgumentNullException(nameof(task4));

            if (task5 == null)
                throw new ArgumentNullException(nameof(task5));

            await Task.WhenAll(task1, task2, task3, task4, task5).ConfigureAwait(false);
            return (task1.Result, task2.Result, task3.Result, task4.Result, task5.Result);
        }

        /// <summary>
        /// Creates a task that will complete when all of the <see cref="Task{TResult}"/> objects have completed.
        /// </summary>
        /// <typeparam name="T1">The result type of the first task.</typeparam>
        /// <typeparam name="T2">The result type of the second task.</typeparam>
        /// <typeparam name="T3">The result type of the third task.</typeparam>
        /// <typeparam name="T4">The result type of the fourth task.</typeparam>
        /// <typeparam name="T5">The result type of the fifth task.</typeparam>
        /// <typeparam name="T6">The result type of the sixth task.</typeparam>
        /// <param name="task1">The first task to wait on for completion.</param>
        /// <param name="task2">The second task to wait on for completion.</param>
        /// <param name="task3">The third task to wait on for completion.</param>
        /// <param name="task4">The fourth task to wait on for completion.</param>
        /// <param name="task5">The fifth task to wait on for completion.</param>
        /// <param name="task6">The sixth task to wait on for completion.</param>
        /// <returns>A task that represents the completion of all of the supplied tasks.</returns>
        public static async Task<(T1, T2, T3, T4, T5, T6)> WhenAll<T1, T2, T3, T4, T5, T6>(Task<T1> task1, Task<T2> task2, Task<T3> task3, Task<T4> task4, Task<T5> task5, Task<T6> task6)
        {
            if (task1 == null)
                throw new ArgumentNullException(nameof(task1));

            if (task2 == null)
                throw new ArgumentNullException(nameof(task2));

            if (task3 == null)
                throw new ArgumentNullException(nameof(task3));

            if (task4 == null)
                throw new ArgumentNullException(nameof(task4));

            if (task5 == null)
                throw new ArgumentNullException(nameof(task5));

            if (task6 == null)
                throw new ArgumentNullException(nameof(task6));

            await Task.WhenAll(task1, task2, task3, task4, task5, task6).ConfigureAwait(false);
            return (task1.Result, task2.Result, task3.Result, task4.Result, task5.Result, task6.Result);
        }

        /// <summary>
        /// Creates a task that will complete when all of the <see cref="Task{TResult}"/> objects have completed.
        /// </summary>
        /// <typeparam name="T1">The result type of the first task.</typeparam>
        /// <typeparam name="T2">The result type of the second task.</typeparam>
        /// <typeparam name="T3">The result type of the third task.</typeparam>
        /// <typeparam name="T4">The result type of the fourth task.</typeparam>
        /// <typeparam name="T5">The result type of the fifth task.</typeparam>
        /// <typeparam name="T6">The result type of the sixth task.</typeparam>
        /// <typeparam name="T7">The result type of the seventh task.</typeparam>
        /// <param name="task1">The first task to wait on for completion.</param>
        /// <param name="task2">The second task to wait on for completion.</param>
        /// <param name="task3">The third task to wait on for completion.</param>
        /// <param name="task4">The fourth task to wait on for completion.</param>
        /// <param name="task5">The fifth task to wait on for completion.</param>
        /// <param name="task6">The sixth task to wait on for completion.</param>
        /// <param name="task7">The seventh task to wait on for completion.</param>
        /// <returns>A task that represents the completion of all of the supplied tasks.</returns>
        public static async Task<(T1, T2, T3, T4, T5, T6, T7)> WhenAll<T1, T2, T3, T4, T5, T6, T7>(Task<T1> task1, Task<T2> task2, Task<T3> task3, Task<T4> task4, Task<T5> task5, Task<T6> task6, Task<T7> task7)
        {
            if (task1 == null)
                throw new ArgumentNullException(nameof(task1));

            if (task2 == null)
                throw new ArgumentNullException(nameof(task2));

            if (task3 == null)
                throw new ArgumentNullException(nameof(task3));

            if (task4 == null)
                throw new ArgumentNullException(nameof(task4));

            if (task5 == null)
                throw new ArgumentNullException(nameof(task5));

            if (task6 == null)
                throw new ArgumentNullException(nameof(task6));

            if (task7 == null)
                throw new ArgumentNullException(nameof(task7));

            await Task.WhenAll(task1, task2, task3, task4, task5, task6, task7).ConfigureAwait(false);
            return (task1.Result, task2.Result, task3.Result, task4.Result, task5.Result, task6.Result, task7.Result);
        }

        /// <summary>
        /// Creates a task that will complete when all of the <see cref="Task{TResult}"/> objects have completed.
        /// </summary>
        /// <typeparam name="T1">The result type of the first task.</typeparam>
        /// <typeparam name="T2">The result type of the second task.</typeparam>
        /// <typeparam name="T3">The result type of the third task.</typeparam>
        /// <typeparam name="T4">The result type of the fourth task.</typeparam>
        /// <typeparam name="T5">The result type of the fifth task.</typeparam>
        /// <typeparam name="T6">The result type of the sixth task.</typeparam>
        /// <typeparam name="T7">The result type of the seventh task.</typeparam>
        /// <typeparam name="T8">The result type of the eighth task.</typeparam>
        /// <param name="task1">The first task to wait on for completion.</param>
        /// <param name="task2">The second task to wait on for completion.</param>
        /// <param name="task3">The third task to wait on for completion.</param>
        /// <param name="task4">The fourth task to wait on for completion.</param>
        /// <param name="task5">The fifth task to wait on for completion.</param>
        /// <param name="task6">The sixth task to wait on for completion.</param>
        /// <param name="task7">The seventh task to wait on for completion.</param>
        /// <param name="task8">The eighth task to wait on for completion.</param>
        /// <returns>A task that represents the completion of all of the supplied tasks.</returns>
        public static async Task<(T1, T2, T3, T4, T5, T6, T7, T8)> WhenAll<T1, T2, T3, T4, T5, T6, T7, T8>(Task<T1> task1, Task<T2> task2, Task<T3> task3, Task<T4> task4, Task<T5> task5, Task<T6> task6, Task<T7> task7, Task<T8> task8)
        {
            if (task1 == null)
                throw new ArgumentNullException(nameof(task1));

            if (task2 == null)
                throw new ArgumentNullException(nameof(task2));

            if (task3 == null)
                throw new ArgumentNullException(nameof(task3));

            if (task4 == null)
                throw new ArgumentNullException(nameof(task4));

            if (task5 == null)
                throw new ArgumentNullException(nameof(task5));

            if (task6 == null)
                throw new ArgumentNullException(nameof(task6));

            if (task7 == null)
                throw new ArgumentNullException(nameof(task7));

            if (task8 == null)
                throw new ArgumentNullException(nameof(task8));

            await Task.WhenAll(task1, task2, task3, task4, task5, task6, task7, task8).ConfigureAwait(false);
            return (task1.Result, task2.Result, task3.Result, task4.Result, task5.Result, task6.Result, task7.Result, task8.Result);
        }
    }
}
