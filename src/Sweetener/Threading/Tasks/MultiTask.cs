// Copyright © William Sugarman.
// Licensed under the MIT License.

// Do not modify this file. It was automatically generated from MultiTask.tt

using System;
using System.Threading.Tasks;

namespace Sweetener.Threading.Tasks
{
    /// <summary>
    /// Provides a set of <see langword="static"/> methods for interacting with multiple <see cref="Task{T}"/> objects
    /// whose type arguments may differ.
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
        public static Task<(T1, T2)> WhenAll<T1, T2>(Task<T1> task1, Task<T2> task2)
        {
            if (task1 is null)
                throw new ArgumentNullException(nameof(task1));

            if (task2 is null)
                throw new ArgumentNullException(nameof(task2));

            return Task
                .WhenAll(task1, task2)
                .WithResultOnSuccess(
                    t => (t.Task1.Result, t.Task2.Result),
                    (Task1: task1, Task2: task2));
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
        public static Task<(T1, T2, T3)> WhenAll<T1, T2, T3>(Task<T1> task1, Task<T2> task2, Task<T3> task3)
        {
            if (task1 is null)
                throw new ArgumentNullException(nameof(task1));

            if (task2 is null)
                throw new ArgumentNullException(nameof(task2));

            if (task3 is null)
                throw new ArgumentNullException(nameof(task3));

            return Task
                .WhenAll(task1, task2, task3)
                .WithResultOnSuccess(
                    t => (t.Task1.Result, t.Task2.Result, t.Task3.Result),
                    (Task1: task1, Task2: task2, Task3: task3));
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
        public static Task<(T1, T2, T3, T4)> WhenAll<T1, T2, T3, T4>(Task<T1> task1, Task<T2> task2, Task<T3> task3, Task<T4> task4)
        {
            if (task1 is null)
                throw new ArgumentNullException(nameof(task1));

            if (task2 is null)
                throw new ArgumentNullException(nameof(task2));

            if (task3 is null)
                throw new ArgumentNullException(nameof(task3));

            if (task4 is null)
                throw new ArgumentNullException(nameof(task4));

            return Task
                .WhenAll(task1, task2, task3, task4)
                .WithResultOnSuccess(
                    t => (t.Task1.Result, t.Task2.Result, t.Task3.Result, t.Task4.Result),
                    (Task1: task1, Task2: task2, Task3: task3, Task4: task4));
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
        public static Task<(T1, T2, T3, T4, T5)> WhenAll<T1, T2, T3, T4, T5>(Task<T1> task1, Task<T2> task2, Task<T3> task3, Task<T4> task4, Task<T5> task5)
        {
            if (task1 is null)
                throw new ArgumentNullException(nameof(task1));

            if (task2 is null)
                throw new ArgumentNullException(nameof(task2));

            if (task3 is null)
                throw new ArgumentNullException(nameof(task3));

            if (task4 is null)
                throw new ArgumentNullException(nameof(task4));

            if (task5 is null)
                throw new ArgumentNullException(nameof(task5));

            return Task
                .WhenAll(task1, task2, task3, task4, task5)
                .WithResultOnSuccess(
                    t => (t.Task1.Result, t.Task2.Result, t.Task3.Result, t.Task4.Result, t.Task5.Result),
                    (Task1: task1, Task2: task2, Task3: task3, Task4: task4, Task5: task5));
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
        public static Task<(T1, T2, T3, T4, T5, T6)> WhenAll<T1, T2, T3, T4, T5, T6>(Task<T1> task1, Task<T2> task2, Task<T3> task3, Task<T4> task4, Task<T5> task5, Task<T6> task6)
        {
            if (task1 is null)
                throw new ArgumentNullException(nameof(task1));

            if (task2 is null)
                throw new ArgumentNullException(nameof(task2));

            if (task3 is null)
                throw new ArgumentNullException(nameof(task3));

            if (task4 is null)
                throw new ArgumentNullException(nameof(task4));

            if (task5 is null)
                throw new ArgumentNullException(nameof(task5));

            if (task6 is null)
                throw new ArgumentNullException(nameof(task6));

            return Task
                .WhenAll(task1, task2, task3, task4, task5, task6)
                .WithResultOnSuccess(
                    t => (t.Task1.Result, t.Task2.Result, t.Task3.Result, t.Task4.Result, t.Task5.Result, t.Task6.Result),
                    (Task1: task1, Task2: task2, Task3: task3, Task4: task4, Task5: task5, Task6: task6));
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
        public static Task<(T1, T2, T3, T4, T5, T6, T7)> WhenAll<T1, T2, T3, T4, T5, T6, T7>(Task<T1> task1, Task<T2> task2, Task<T3> task3, Task<T4> task4, Task<T5> task5, Task<T6> task6, Task<T7> task7)
        {
            if (task1 is null)
                throw new ArgumentNullException(nameof(task1));

            if (task2 is null)
                throw new ArgumentNullException(nameof(task2));

            if (task3 is null)
                throw new ArgumentNullException(nameof(task3));

            if (task4 is null)
                throw new ArgumentNullException(nameof(task4));

            if (task5 is null)
                throw new ArgumentNullException(nameof(task5));

            if (task6 is null)
                throw new ArgumentNullException(nameof(task6));

            if (task7 is null)
                throw new ArgumentNullException(nameof(task7));

            return Task
                .WhenAll(task1, task2, task3, task4, task5, task6, task7)
                .WithResultOnSuccess(
                    t => (t.Task1.Result, t.Task2.Result, t.Task3.Result, t.Task4.Result, t.Task5.Result, t.Task6.Result, t.Task7.Result),
                    (Task1: task1, Task2: task2, Task3: task3, Task4: task4, Task5: task5, Task6: task6, Task7: task7));
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
        public static Task<(T1, T2, T3, T4, T5, T6, T7, T8)> WhenAll<T1, T2, T3, T4, T5, T6, T7, T8>(Task<T1> task1, Task<T2> task2, Task<T3> task3, Task<T4> task4, Task<T5> task5, Task<T6> task6, Task<T7> task7, Task<T8> task8)
        {
            if (task1 is null)
                throw new ArgumentNullException(nameof(task1));

            if (task2 is null)
                throw new ArgumentNullException(nameof(task2));

            if (task3 is null)
                throw new ArgumentNullException(nameof(task3));

            if (task4 is null)
                throw new ArgumentNullException(nameof(task4));

            if (task5 is null)
                throw new ArgumentNullException(nameof(task5));

            if (task6 is null)
                throw new ArgumentNullException(nameof(task6));

            if (task7 is null)
                throw new ArgumentNullException(nameof(task7));

            if (task8 is null)
                throw new ArgumentNullException(nameof(task8));

            return Task
                .WhenAll(task1, task2, task3, task4, task5, task6, task7, task8)
                .WithResultOnSuccess(
                    t => (t.Task1.Result, t.Task2.Result, t.Task3.Result, t.Task4.Result, t.Task5.Result, t.Task6.Result, t.Task7.Result, t.Task8.Result),
                    (Task1: task1, Task2: task2, Task3: task3, Task4: task4, Task5: task5, Task6: task6, Task7: task7, Task8: task8));
        }
    }
}
