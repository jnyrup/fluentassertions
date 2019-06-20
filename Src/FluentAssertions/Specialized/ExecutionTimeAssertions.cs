﻿using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using FluentAssertions.Common;
using FluentAssertions.Execution;

namespace FluentAssertions.Specialized
{
    /// <summary>
    /// Provides methods for asserting that the execution time of an <see cref="Action"/> satisfies certain conditions.
    /// </summary>
    public class ExecutionTimeAssertions
    {
        private readonly ExecutionTime execution;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExecutionTime"/> class.
        /// </summary>
        /// <param name="executionTime">The execution on which time must be asserted.</param>
        public ExecutionTimeAssertions(ExecutionTime executionTime)
        {
            this.execution = executionTime;
        }

        /// <summary>
        /// Checks the executing action if it satisfies a condition.
        /// If the execution runs into an exception, then this will rethrow it.
        /// </summary>
        /// <param name="condition">Condition to check on the current elapsed time.</param>
        /// <param name="expectedResult">Polling stops when condition returns the expected result.</param>
        /// <param name="rate">The rate at which the condition is re-checked.</param>
        /// <return>The elapsed time. (use this, don't measure twice)</return>
        private (bool isRunning, TimeSpan elapsed) PollUntil(Func<TimeSpan, bool> condition, bool expectedResult, TimeSpan rate)
        {
            TimeSpan elapsed = execution.ElapsedTime;
            bool isRunning = execution.IsRunning;

            while (isRunning)
            {
                if (condition(elapsed) == expectedResult)
                {
                    break;
                }

                isRunning = !execution.Wait(rate);
                elapsed = execution.ElapsedTime;
            }

            if (execution.Exception != null)
            {
                // rethrow captured exception
                throw execution.Exception;
            }

            return (isRunning, elapsed);
        }

        /// <summary>
        /// Asserts that the execution time of the operation is less or equal to a specified amount of time.
        /// </summary>
        /// <param name="maxDuration">
        /// The maximum allowed duration.
        /// </param>
        /// <param name="because">
        /// A formatted phrase explaining why the assertion should be satisfied. If the phrase does not
        /// start with the word <i>because</i>, it is prepended to the message.
        /// </param>
        /// <param name="becauseArgs">
        /// Zero or more values to use for filling in any <see cref="string.Format(string,object[])"/> compatible placeholders.
        /// </param>
        public void BeLessOrEqualTo(TimeSpan maxDuration, string because = "", params object[] becauseArgs)
        {
            bool Condition(TimeSpan duration) => duration.CompareTo(maxDuration) <= 0;
            var (isRunning, elapsed) = PollUntil(Condition, expectedResult: false, rate: maxDuration);

            Execute.Assertion
                .ForCondition(Condition(elapsed))
                .BecauseOf(because, becauseArgs)
                .FailWith("Execution of " +
                          execution.ActionDescription + " should be less or equal to {0}{reason}, but it required " +
                          (isRunning ? "more than " : "exactly ") + "{1}.",
                    maxDuration,
                    elapsed);
        }

        /// <summary>
        /// Asserts that the execution time of the operation is less than a specified amount of time.
        /// </summary>
        /// <param name="maxDuration">
        /// The maximum allowed duration.
        /// </param>
        /// <param name="because">
        /// A formatted phrase explaining why the assertion should be satisfied. If the phrase does not
        /// start with the word <i>because</i>, it is prepended to the message.
        /// </param>
        /// <param name="becauseArgs">
        /// Zero or more values to use for filling in any <see cref="string.Format(string,object[])"/> compatible placeholders.
        /// </param>
        public void BeLessThan(TimeSpan maxDuration, string because = "", params object[] becauseArgs)
        {
            bool Condition(TimeSpan duration) => duration.CompareTo(maxDuration) < 0;
            var (isRunning, elapsed) = PollUntil(Condition, expectedResult: false, rate: maxDuration);

            Execute.Assertion
                .ForCondition(Condition(execution.ElapsedTime))
                .BecauseOf(because, becauseArgs)
                .FailWith("Execution of " +
                          execution.ActionDescription + " should be less than {0}{reason}, but it required " +
                          (isRunning ? "more than " : "exactly ") + "{1}.",
                    maxDuration,
                    elapsed);
        }

        /// <summary>
        /// Asserts that the execution time of the operation is greater or equal to a specified amount of time.
        /// </summary>
        /// <param name="minDuration">
        /// The minimum allowed duration.
        /// </param>
        /// <param name="because">
        /// A formatted phrase explaining why the assertion should be satisfied. If the phrase does not
        /// start with the word <i>because</i>, it is prepended to the message.
        /// </param>
        /// <param name="becauseArgs">
        /// Zero or more values to use for filling in any <see cref="string.Format(string,object[])"/> compatible placeholders.
        /// </param>
        public void BeGreaterOrEqualTo(TimeSpan minDuration, string because = "", params object[] becauseArgs)
        {
            bool Condition(TimeSpan duration) => duration.CompareTo(minDuration) >= 0;
            var (isRunning, elapsed) = PollUntil(Condition, expectedResult: true, rate: minDuration);

            Execute.Assertion
                .ForCondition(Condition(elapsed))
                .BecauseOf(because, becauseArgs)
                .FailWith("Execution of " +
                          execution.ActionDescription + " should be greater or equal to {0}{reason}, but it required " +
                          (isRunning ? "more than " : "exactly ") + "{1}.",
                    minDuration,
                    elapsed);
        }

        /// <summary>
        /// Asserts that the execution time of the operation is greater than a specified amount of time.
        /// </summary>
        /// <param name="minDuration">
        /// The minimum allowed duration.
        /// </param>
        /// <param name="because">
        /// A formatted phrase explaining why the assertion should be satisfied. If the phrase does not
        /// start with the word <i>because</i>, it is prepended to the message.
        /// </param>
        /// <param name="becauseArgs">
        /// Zero or more values to use for filling in any <see cref="string.Format(string,object[])"/> compatible placeholders.
        /// </param>
        public void BeGreaterThan(TimeSpan minDuration, string because = "", params object[] becauseArgs)
        {
            bool Condition(TimeSpan duration) => duration.CompareTo(minDuration) > 0;
            var (isRunning, elapsed) = PollUntil(Condition, expectedResult: true, rate: minDuration);

            Execute.Assertion
                .ForCondition(Condition(elapsed))
                .BecauseOf(because, becauseArgs)
                .FailWith("Execution of " +
                          execution.ActionDescription + " should be greater than {0}{reason}, but it required " +
                          (isRunning ? "more than " : "exactly ") + "{1}.",
                    minDuration,
                    elapsed);
        }

        /// <summary>
        /// Asserts that the execution time of the operation is within the expected duration.
        /// by a specified precision.
        /// </summary>
        /// <param name="expectedDuration">
        /// The expected duration.
        /// </param>
        /// <param name="precision">
        /// The maximum amount of time which the execution time may differ from the expected duration.
        /// </param>
        /// <param name="because">
        /// A formatted phrase explaining why the assertion should be satisfied. If the phrase does not
        /// start with the word <i>because</i>, it is prepended to the message.
        /// </param>
        /// <param name="becauseArgs">
        /// Zero or more values to use for filling in any <see cref="string.Format(string,object[])"/> compatible placeholders.
        /// </param>
        public void BeCloseTo(TimeSpan expectedDuration, TimeSpan precision, string because = "", params object[] becauseArgs)
        {
            var minimumValue = expectedDuration - precision;
            var maximumValue = expectedDuration + precision;

            bool MaxCondition(TimeSpan duration) => duration <= maximumValue;
            bool MinCondition(TimeSpan duration) => duration >= minimumValue;

            // for polling we only use max condition, we don't want poll to stop if
            // elapsed time didn't even get to the acceptable range
            var (isRunning, elapsed) = PollUntil(MaxCondition, expectedResult: false, rate: maximumValue);

            Execute.Assertion
                .ForCondition(MinCondition(elapsed) && MaxCondition(elapsed))
                .BecauseOf(because, becauseArgs)
                .FailWith("Execution of " + execution.ActionDescription +
                          " should be within {0} from {1}{reason}, but it required " +
                          (isRunning ? "more than " : "exactly ") + "{2}.",
                    precision,
                    expectedDuration,
                    elapsed);
        }
    }

    public class ExecutionTime
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExecutionTime"/> class.
        /// </summary>
        /// <param name="action">The action of which the execution time must be asserted.</param>
        public ExecutionTime(Action action)
            : this(action, new Clock())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExecutionTime"/> class.
        /// </summary>
        /// <param name="action">The action of which the execution time must be asserted.</param>
        public ExecutionTime(Action action, IClock clock)
            : this(action, "the action", clock)
        {
        }

        public ExecutionTime(Func<Task> action)
            : this(action, new Clock())
        {
        }

        public ExecutionTime(Func<Task> action, IClock clock)
            : this(action, "the action", clock)
        {
        }

        protected ExecutionTime(Action action, string actionDescription)
            : this(action, actionDescription, new Clock())
        {
        }

        protected ExecutionTime(Action action, string actionDescription, IClock clock)
        {
            ActionDescription = actionDescription;
            this.clock = clock;
            IsRunning = true;
            task = Task.Run(() =>
            {
                // move stopwatch as close to action start as possible
                // so that we have to get correct time readings
                try
                {
                    timer = clock.StartTimer();
                    action();
                }
                catch (Exception exception)
                {
                    Exception = exception;
                }
                finally
                {
                    // ensures that we stop the stopwatch even on exceptions
                    timer.Stop();
                    IsRunning = false;
                }
            });
        }

        protected ExecutionTime(Func<Task> action, string actionDescription)
            : this(action, actionDescription, new Clock())
        {
        }

        /// <remarks>
        /// This constructor is almost exact copy of the one accepting <see cref="Action"/>.
        /// The original constructor shall stay in place in order to keep backward-compatibility
        /// and to avoid unnecessary wrapping action in <see cref="task"/>.
        /// </remarks>
        protected ExecutionTime(Func<Task> action, string actionDescription, IClock clock)
        {
            ActionDescription = actionDescription;
            this.clock = clock;
            IsRunning = true;
            task = Task.Run(async () =>
            {
                // move stopwatch as close to action start as possible
                // so that we have to get correct time readings
                try
                {
                    timer = clock.StartTimer();
                    await action();
                }
                catch (Exception exception)
                {
                    Exception = exception;
                }
                finally
                {
                    // ensures that we stop the stopwatch even on exceptions
                    timer.Stop();
                    IsRunning = false;
                }
            });
        }

        internal TimeSpan ElapsedTime => timer?.Elapsed ?? TimeSpan.Zero;

        internal bool IsRunning { get; private set; }

        internal string ActionDescription { get; }

        internal Exception Exception { get; private set; }

        internal bool Wait(TimeSpan timeout) => clock.Wait(task, timeout);

        private readonly IClock clock;

        private readonly Task task;

        private ITimer timer;
    }

    public class MemberExecutionTime<T> : ExecutionTime
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MemberExecutionTime&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="subject">The object that exposes the method or property.</param>
        /// <param name="action">A reference to the method or property to measure the execution time of.</param>
        public MemberExecutionTime(T subject, Expression<Action<T>> action)
            : this(subject, action, new Clock())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MemberExecutionTime&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="subject">The object that exposes the method or property.</param>
        /// <param name="action">A reference to the method or property to measure the execution time of.</param>
        public MemberExecutionTime(T subject, Expression<Action<T>> action, IClock clock)
            : base(() => action.Compile()(subject), "(" + action.Body + ")", clock)
        {
        }
    }
}
