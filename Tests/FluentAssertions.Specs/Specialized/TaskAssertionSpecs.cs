﻿using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions.Extensions;
using Xunit;
using Xunit.Sdk;

namespace FluentAssertions.Specs.Specialized;

public static class TaskAssertionSpecs
{
    public class Extension
    {
        [Fact]
        public void When_getting_the_subject_it_should_remain_unchanged()
        {
            // Arrange
            Func<Task<int>> subject = () => Task.FromResult(42);

            // Act
            Action action = () => subject.Should().Subject.As<object>().Should().BeSameAs(subject);

            // Assert
            action.Should().NotThrow("the Subject should remain the same");
        }
    }

    public class CompleteWithinAsync
    {
        [Fact]
        public async Task When_subject_is_null_it_should_throw()
        {
            // Arrange
            var timeSpan = 0.Milliseconds();
            Func<Task> action = null;

            // Act
            Func<Task> testAction = () => action.Should().CompleteWithinAsync(
                timeSpan, "because we want to test the failure {0}", "message");

            // Assert
            await testAction.Should().ThrowAsync<XunitException>()
                .WithMessage("*because we want to test the failure message*found <null>*");
        }

        [Fact]
        public async Task When_task_completes_fast_t_should_succeed()
        {
            // Arrange
            var timer = new FakeClock();
            var taskFactory = new TaskCompletionSource<bool>();

            // Act
            Func<Task> action = () =>
                taskFactory.Awaiting(t => (Task)t.Task).Should(timer).CompleteWithinAsync(100.Milliseconds());
            taskFactory.SetResult(true);
            timer.Complete();

            // Assert
            await action.Should().NotThrowAsync();
        }

        [Fact]
        public async Task When_task_consumes_time_in_sync_portion_it_should_fail()
        {
            // Arrange
            var timer = new FakeClock();
            var taskFactory = new TaskCompletionSource<bool>();

            // Act
            Func<Task> action = () => taskFactory
                .Awaiting(t =>
                {
                    // simulate sync work longer than accepted time
                    timer.Delay(101.Milliseconds());
                    return (Task)t.Task;
                })
                .Should(timer)
                .CompleteWithinAsync(100.Milliseconds());

            taskFactory.SetResult(true);
            timer.Complete();

            // Assert
            await action.Should().ThrowAsync<XunitException>();
        }

        [Fact]
        public async Task When_task_completes_late_it_should_fail()
        {
            // Arrange
            var timer = new FakeClock();
            var taskFactory = new TaskCompletionSource<bool>();

            // Act
            Func<Task> action = () => taskFactory.Awaiting(t => (Task)t.Task).Should(timer).CompleteWithinAsync(100.Milliseconds());
            timer.Complete();

            // Assert
            await action.Should().ThrowAsync<XunitException>();
        }
    }

    public class NotCompleteWithinAsync
    {
        [Fact]
        public async Task When_subject_is_null_it_should_throw()
        {
            // Arrange
            var timeSpan = 0.Milliseconds();
            Func<Task> action = null;

            // Act
            Func<Task> testAction = () => action.Should().NotCompleteWithinAsync(
                timeSpan, "because we want to test the failure {0}", "message");

            // Assert
            await testAction.Should().ThrowAsync<XunitException>()
                .WithMessage("*because we want to test the failure message*found <null>*");
        }

        [Fact]
        public async Task When_task_completes_fast_it_should_throw()
        {
            // Arrange
            var timer = new FakeClock();
            var taskFactory = new TaskCompletionSource<bool>();

            // Act
            Func<Task> action = () => taskFactory
                .Awaiting(t => (Task)t.Task).Should(timer)
                .NotCompleteWithinAsync(100.Milliseconds());
            taskFactory.SetResult(true);
            timer.Complete();

            // Assert
            await action.Should().ThrowAsync<XunitException>();
        }

        [Fact]
        public async Task When_task_consumes_time_in_sync_portion_it_should_succeed()
        {
            // Arrange
            var timer = new FakeClock();
            var taskFactory = new TaskCompletionSource<bool>();

            // Act
            Func<Task> action = () => taskFactory
                .Awaiting(t =>
                {
                    // simulate sync work longer than accepted time
                    timer.Delay(101.Milliseconds());
                    return (Task)t.Task;
                })
                .Should(timer)
                .NotCompleteWithinAsync(100.Milliseconds());

            taskFactory.SetResult(true);
            timer.Complete();

            // Assert
            await action.Should().NotThrowAsync();
        }

        [Fact]
        public async Task When_task_completes_late_it_should_succeed()
        {
            // Arrange
            var timer = new FakeClock();
            var taskFactory = new TaskCompletionSource<bool>();

            // Act
            Func<Task> action = () => taskFactory
                .Awaiting(t => (Task)t.Task).Should(timer)
                .NotCompleteWithinAsync(100.Milliseconds());
            timer.Complete();

            // Assert
            await action.Should().NotThrowAsync();
        }
    }

    public class ThrowAsync
    {
        [Fact]
        public async Task When_task_throws_it_should_succeed()
        {
            // Arrange
            var timer = new FakeClock();
            var taskFactory = new TaskCompletionSource<bool>();

            // Act
            Func<Task> action = () =>
            {
                return taskFactory
                    .Awaiting(t => (Task)t.Task)
                    .Should(timer).ThrowAsync<InvalidOperationException>();
            };
            taskFactory.SetException(new InvalidOperationException("foo"));

            // Assert
            await action.Should().CompleteWithinAsync(1.Seconds());
        }

        [Fact]
        public async Task When_task_completes_without_exception_it_should_fail()
        {
            // Arrange
            var timer = new FakeClock();
            var taskFactory = new TaskCompletionSource<bool>();

            // Act
            Func<Task> action = () =>
            {
                return taskFactory
                    .Awaiting(t => (Task)t.Task)
                    .Should(timer).ThrowAsync<InvalidOperationException>();
            };
            taskFactory.SetResult(true);
            timer.Complete();

            // Assert
            await action.Should().ThrowAsync<XunitException>().WithMessage(
                "Expected a <System.InvalidOperationException> to be thrown, but no exception was thrown.");
        }
    }

    public class ThrowWithinAsync
    {
        [Fact]
        public async Task When_task_throws_fast_it_should_succeed()
        {
            // Arrange
            var timer = new FakeClock();
            var taskFactory = new TaskCompletionSource<bool>();

            // Act
            Func<Task> action = () =>
            {
                return taskFactory
                    .Awaiting(t => (Task)t.Task)
                    .Should(timer).ThrowWithinAsync<InvalidOperationException>(100.Milliseconds());
            };
            taskFactory.SetException(new InvalidOperationException("foo"));

            // Assert
            await action.Should().CompleteWithinAsync(1.Seconds());
        }

        [Fact]
        public async Task When_task_not_completes_it_should_fail()
        {
            // Arrange
            var timer = new FakeClock();
            var taskFactory = new TaskCompletionSource<bool>();

            // Act
            Func<Task> action = () =>
            {
                return taskFactory
                    .Awaiting(t => (Task)t.Task)
                    .Should(timer).ThrowWithinAsync<InvalidOperationException>(100.Milliseconds());
            };
            timer.Delay(200.Milliseconds());

            // Assert
            await action.Should().ThrowAsync<XunitException>().WithMessage(
                "Expected a <System.InvalidOperationException> to be thrown within 100ms, but no exception was thrown.");
        }

        [Fact]
        public async Task When_task_completes_without_exception_it_should_fail()
        {
            // Arrange
            var timer = new FakeClock();
            var taskFactory = new TaskCompletionSource<bool>();

            // Act
            Func<Task> action = () =>
            {
                return taskFactory
                    .Awaiting(t => (Task)t.Task)
                    .Should(timer).ThrowWithinAsync<InvalidOperationException>(100.Milliseconds());
            };
            taskFactory.SetResult(true);
            timer.Complete();

            // Assert
            await action.Should().ThrowAsync<XunitException>().WithMessage(
                "Expected a <System.InvalidOperationException> to be thrown within 100ms, but no exception was thrown.");
        }
    }

    [Collection("UIFacts")]
    public class CompleteWithinAsyncUIFacts
    {
        [UIFact]
        public async Task When_task_completes_fast_it_should_succeed()
        {
            // Arrange
            var timer = new FakeClock();
            var taskFactory = new TaskCompletionSource<bool>();

            // Act
            Func<Task> action = () =>
                taskFactory.Awaiting(t => (Task)t.Task).Should(timer).CompleteWithinAsync(100.Milliseconds());
            taskFactory.SetResult(true);
            timer.Complete();

            // Assert
            await action.Should().NotThrowAsync();
        }

        [UIFact]
        public async Task When_task_completes_late_it_should_fail()
        {
            // Arrange
            var timer = new FakeClock();
            var taskFactory = new TaskCompletionSource<bool>();

            // Act
            Func<Task> action = () =>
                taskFactory.Awaiting(t => (Task)t.Task).Should(timer).CompleteWithinAsync(100.Milliseconds());
            timer.Complete();

            // Assert
            await action.Should().ThrowAsync<XunitException>();
        }

        [UIFact]
        public async Task When_task_is_checking_synchronization_context_it_should_succeed()
        {
            // Arrange
            Func<Task> task = CheckContextAsync;

            // Act
            Func<Task> action = () => this.Awaiting(_ => task()).Should().CompleteWithinAsync(1.Seconds());

            // Assert
            await action.Should().NotThrowAsync();

            async Task CheckContextAsync()
            {
                await Task.Delay(1);
                SynchronizationContext.Current.Should().NotBeNull();
            }
        }
    }
}
