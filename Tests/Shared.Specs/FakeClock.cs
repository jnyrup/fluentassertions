﻿using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions.Common;

namespace FluentAssertions.Specs
{
    /// <summary>
    /// Implementation of <see cref="IClock"/> for testing purposes only.
    /// </summary>
    /// <remarks>
    /// It allows you to control the "current" time.
    /// </remarks>
    internal class FakeClock : IClock
    {
        private TimeSpan elapsedTime = TimeSpan.Zero;

        private readonly TaskCompletionSource<bool> delayTask = new TaskCompletionSource<bool>();

        Task IClock.DelayAsync(TimeSpan delay, CancellationToken cancellationToken)
        {
            elapsedTime += delay;
            return delayTask.Task;
        }

        bool IClock.Wait(Task task, TimeSpan timeout)
        {
            elapsedTime += timeout;
            delayTask.Task.GetAwaiter().GetResult();
            return delayTask.Task.Result;
        }

        public ITimer StartTimer() => new TestTimer(() => elapsedTime);

        public void Delay(TimeSpan timeToDelay)
        {
            elapsedTime += timeToDelay;
        }

        public void CompletesBeforeTimeout()
        {
            delayTask.SetResult(true);
        }

        public void RunsIntoTimeout()
        {
            delayTask.SetResult(false);
        }
    }

    internal class TestTimer : ITimer
    {
        private readonly Func<TimeSpan> getElapsed;

        public TestTimer(Func<TimeSpan> getElapsed)
        {
            this.getElapsed = getElapsed;
        }

        public TimeSpan Elapsed => getElapsed();

        public void Stop()
        {
        }
    }
}
