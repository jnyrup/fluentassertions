using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions.Common;
using FluentAssertions.Extensions;
using Xunit;
using Xunit.Sdk;

namespace FluentAssertions.Specs
{
    public class ExecutionTimeAssertionsSpecs
    {
        #region BeLessOrEqualTo
        [Fact]
        public void When_the_execution_time_of_a_member_is_not_less_or_equal_to_a_limit_it_should_throw()
        {
            //-----------------------------------------------------------------------------------------------------------
            // Arrange
            //-----------------------------------------------------------------------------------------------------------
            var clock = new FakeClock();
            var subject = new SleepingClass(clock);

            //-----------------------------------------------------------------------------------------------------------
            // Act
            //-----------------------------------------------------------------------------------------------------------
            Action act = () => subject.ExecutionTimeOf(s => s.Sleep(610), clock).Should().BeLessOrEqualTo(500.Milliseconds(),
                "we like speed");
            clock.RunsIntoTimeout();

            //-----------------------------------------------------------------------------------------------------------
            // Assert
            //-----------------------------------------------------------------------------------------------------------
            act.Should().Throw<XunitException>().WithMessage(
                "*(s.Sleep(610)) should be less or equal to 0.500s because we like speed, but it required*");
        }

        [Fact]
        public void When_the_execution_time_of_a_member_is_less_or_equal_to_a_limit_it_should_not_throw()
        {
            //-----------------------------------------------------------------------------------------------------------
            // Arrange
            //-----------------------------------------------------------------------------------------------------------
            var clock = new FakeClock();
            var subject = new SleepingClass(clock);

            //-----------------------------------------------------------------------------------------------------------
            // Act
            //-----------------------------------------------------------------------------------------------------------
            Action act = () => subject.ExecutionTimeOf(s => s.Sleep(0), clock).Should().BeLessOrEqualTo(500.Milliseconds());
            clock.CompletesBeforeTimeout();

            //-----------------------------------------------------------------------------------------------------------
            // Assert
            //-----------------------------------------------------------------------------------------------------------
            act.Should().NotThrow();
        }

        [Fact]
        public void When_the_execution_time_of_an_action_is_not_less_or_equal_to_a_limit_it_should_throw()
        {
            //-----------------------------------------------------------------------------------------------------------
            // Arrange
            //-----------------------------------------------------------------------------------------------------------
            var clock = new FakeClock();
            Action someAction = () => clock.Delay(TimeSpan.FromMilliseconds(510));

            //-----------------------------------------------------------------------------------------------------------
            // Act
            //-----------------------------------------------------------------------------------------------------------
            Action act = () => someAction.ExecutionTime(clock).Should().BeLessOrEqualTo(100.Milliseconds());
            clock.RunsIntoTimeout();

            //-----------------------------------------------------------------------------------------------------------
            // Assert
            //-----------------------------------------------------------------------------------------------------------
            act.Should().Throw<XunitException>().WithMessage(
                "*action should be less or equal to 0.100s, but it required*");
        }

        [Fact]
        public void When_the_execution_time_of_an_action_is_less_or_equal_to_a_limit_it_should_not_throw()
        {
            //-----------------------------------------------------------------------------------------------------------
            // Arrange
            //-----------------------------------------------------------------------------------------------------------
            var clock = new FakeClock();
            Action someAction = () => clock.Delay(TimeSpan.FromMilliseconds(100));

            //-----------------------------------------------------------------------------------------------------------
            // Act
            //-----------------------------------------------------------------------------------------------------------
            Action act = () => someAction.ExecutionTime(clock).Should().BeLessOrEqualTo(1.Seconds());
            clock.CompletesBeforeTimeout();

            //-----------------------------------------------------------------------------------------------------------
            // Assert
            //-----------------------------------------------------------------------------------------------------------
            act.Should().NotThrow();
        }

        [Fact]
        public void When_action_runs_indefinitely_it_should_be_stopped_and_throw_if_there_is_less_or_equal_condition()
        {
            //-----------------------------------------------------------------------------------------------------------
            // Arrange
            //-----------------------------------------------------------------------------------------------------------
            Action someAction = () =>
            {
                // lets cause a deadlock
                var semaphore = new Semaphore(0, 1); // my weapon of choice is a semaphore
                semaphore.WaitOne(); // oops
            };

            //-----------------------------------------------------------------------------------------------------------
            // Act
            //-----------------------------------------------------------------------------------------------------------
            Action act = () => someAction.ExecutionTime().Should().BeLessOrEqualTo(100.Milliseconds());

            //-----------------------------------------------------------------------------------------------------------
            // Assert
            //-----------------------------------------------------------------------------------------------------------
            act.Should().Throw<XunitException>().WithMessage(
                "*action should be less or equal to 0.100s, but it required more than*");
        }
        #endregion

        #region BeLessThan
        [Fact]
        public void When_the_execution_time_of_a_member_is_not_less_than_a_limit_it_should_throw()
        {
            //-----------------------------------------------------------------------------------------------------------
            // Arrange
            //-----------------------------------------------------------------------------------------------------------
            var clock = new FakeClock();
            var subject = new SleepingClass(clock);

            //-----------------------------------------------------------------------------------------------------------
            // Act
            //-----------------------------------------------------------------------------------------------------------
            Action act = () => subject.ExecutionTimeOf(s => s.Sleep(610), clock).Should().BeLessThan(500.Milliseconds(),
                "we like speed");
            clock.RunsIntoTimeout();

            //-----------------------------------------------------------------------------------------------------------
            // Assert
            //-----------------------------------------------------------------------------------------------------------
            act.Should().Throw<XunitException>().WithMessage(
                "*(s.Sleep(610)) should be less than 0.500s because we like speed, but it required*");
        }

        [Fact]
        public void When_the_execution_time_of_a_member_is_less_than_a_limit_it_should_not_throw()
        {
            //-----------------------------------------------------------------------------------------------------------
            // Arrange
            //-----------------------------------------------------------------------------------------------------------
            var clock = new FakeClock();
            var subject = new SleepingClass(clock);

            //-----------------------------------------------------------------------------------------------------------
            // Act
            //-----------------------------------------------------------------------------------------------------------
            Action act = () => subject.ExecutionTimeOf(s => s.Sleep(0), clock).Should().BeLessThan(500.Milliseconds());
            clock.CompletesBeforeTimeout();

            //-----------------------------------------------------------------------------------------------------------
            // Assert
            //-----------------------------------------------------------------------------------------------------------
            act.Should().NotThrow();
        }

        [Fact]
        public void When_the_execution_time_of_an_action_is_not_less_than_a_limit__it_should_throw()
        {
            //-----------------------------------------------------------------------------------------------------------
            // Arrange
            //-----------------------------------------------------------------------------------------------------------
            var clock = new FakeClock();
            Action someAction = () => clock.Delay(TimeSpan.FromMilliseconds(510));

            //-----------------------------------------------------------------------------------------------------------
            // Act
            //-----------------------------------------------------------------------------------------------------------
            Action act = () => someAction.ExecutionTime(clock).Should().BeLessThan(100.Milliseconds());
            clock.RunsIntoTimeout();

            //-----------------------------------------------------------------------------------------------------------
            // Assert
            //-----------------------------------------------------------------------------------------------------------
            act.Should().Throw<XunitException>().WithMessage(
                "*action should be less than 0.100s, but it required*");
        }

        [Fact]
        public void When_the_execution_time_of_an_async_action_is_not_less_than_a_limit__it_should_throw()
        {
            //-----------------------------------------------------------------------------------------------------------
            // Arrange
            //-----------------------------------------------------------------------------------------------------------
            var timer = new FakeClock();
            var taskFactory = new TaskCompletionSource<bool>();

            Func<Task> someAction = () => taskFactory.Task;

            //-----------------------------------------------------------------------------------------------------------
            // Act
            //-----------------------------------------------------------------------------------------------------------
            Action act = () => someAction.ExecutionTime(timer).Should().BeLessThan(100.Milliseconds());
            timer.RunsIntoTimeout();

            //-----------------------------------------------------------------------------------------------------------
            // Assert
            //-----------------------------------------------------------------------------------------------------------
            act.Should().Throw<XunitException>().WithMessage(
                "*action should be less than 0.100s, but it required*");
        }

        [Fact]
        public void When_the_execution_time_of_an_action_is_less_than_a_limit_it_should_not_throw()
        {
            //-----------------------------------------------------------------------------------------------------------
            // Arrange
            //-----------------------------------------------------------------------------------------------------------
            var clock = new FakeClock();
            Action someAction = () => clock.Delay(TimeSpan.FromMilliseconds(100));

            //-----------------------------------------------------------------------------------------------------------
            // Act
            //-----------------------------------------------------------------------------------------------------------
            Action act = () => someAction.ExecutionTime(clock).Should().BeLessThan(2.Seconds());
            clock.CompletesBeforeTimeout();

            //-----------------------------------------------------------------------------------------------------------
            // Assert
            //-----------------------------------------------------------------------------------------------------------
            act.Should().NotThrow();
        }

        [Fact]
        public void When_the_execution_time_of_an_async_action_is_less_than_a_limit_it_should_not_throw()
        {
            //-----------------------------------------------------------------------------------------------------------
            // Arrange
            //-----------------------------------------------------------------------------------------------------------
            var timer = new FakeClock();
            var taskFactory = new TaskCompletionSource<bool>();
            Func<Task> someAction = () => taskFactory.Task;

            //-----------------------------------------------------------------------------------------------------------
            // Act
            //-----------------------------------------------------------------------------------------------------------
            Action act = () => someAction.ExecutionTime(timer).Should().BeLessThan(2.Seconds());
            taskFactory.SetResult(true);
            timer.CompletesBeforeTimeout();

            //-----------------------------------------------------------------------------------------------------------
            // Assert
            //-----------------------------------------------------------------------------------------------------------
            act.Should().NotThrow();
        }

        [Fact]
        public void When_action_runs_indefinitely_it_should_be_stopped_and_throw_if_there_is_less_than_condition()
        {
            //-----------------------------------------------------------------------------------------------------------
            // Arrange
            //-----------------------------------------------------------------------------------------------------------
            Action someAction = () =>
            {
                // lets cause a deadlock
                var semaphore = new Semaphore(0, 1); // my weapon of choice is a semaphore
                semaphore.WaitOne(); // oops
            };

            //-----------------------------------------------------------------------------------------------------------
            // Act
            //-----------------------------------------------------------------------------------------------------------
            Action act = () => someAction.ExecutionTime().Should().BeLessThan(100.Milliseconds());

            //-----------------------------------------------------------------------------------------------------------
            // Assert
            //-----------------------------------------------------------------------------------------------------------
            act.Should().Throw<XunitException>().WithMessage(
                "*action should be less than 0.100s, but it required more than*");
        }
        #endregion

        #region BeGreaterOrEqualTo
        [Fact]
        public void When_the_execution_time_of_a_member_is_not_greater_or_equal_to_a_limit_it_should_throw()
        {
            //-----------------------------------------------------------------------------------------------------------
            // Arrange
            //-----------------------------------------------------------------------------------------------------------
            var clock = new FakeClock();
            var subject = new SleepingClass(clock);

            //-----------------------------------------------------------------------------------------------------------
            // Act
            //-----------------------------------------------------------------------------------------------------------
            Action act = () => subject.ExecutionTimeOf(s => s.Sleep(100), clock).Should().BeGreaterOrEqualTo(1.Seconds(),
                "we like speed");
            clock.RunsIntoTimeout();

            //-----------------------------------------------------------------------------------------------------------
            // Assert
            //-----------------------------------------------------------------------------------------------------------
            act.Should().Throw<XunitException>().WithMessage(
                "*(s.Sleep(100)) should be greater or equal to 1s because we like speed, but it required*");
        }

        [Fact]
        public void When_the_execution_time_of_a_member_is_greater_or_equal_to_a_limit_it_should_not_throw()
        {
            //-----------------------------------------------------------------------------------------------------------
            // Arrange
            //-----------------------------------------------------------------------------------------------------------
            var clock = new FakeClock();
            var subject = new SleepingClass(clock);

            //-----------------------------------------------------------------------------------------------------------
            // Act
            //-----------------------------------------------------------------------------------------------------------
            Action act = () => subject.ExecutionTimeOf(s => s.Sleep(100), clock).Should().BeGreaterOrEqualTo(50.Milliseconds());
            clock.CompletesBeforeTimeout();

            //-----------------------------------------------------------------------------------------------------------
            // Assert
            //-----------------------------------------------------------------------------------------------------------
            act.Should().NotThrow();
        }

        [Fact]
        public void When_the_execution_time_of_an_action_is_not_greater_or_equal_to_a_limit_it_should_throw()
        {
            //-----------------------------------------------------------------------------------------------------------
            // Arrange
            //-----------------------------------------------------------------------------------------------------------
            var clock = new FakeClock();
            Action someAction = () => clock.Delay(TimeSpan.FromMilliseconds(100));

            //-----------------------------------------------------------------------------------------------------------
            // Act
            //-----------------------------------------------------------------------------------------------------------
            Action act = () => someAction.ExecutionTime(clock).Should().BeGreaterOrEqualTo(1.Seconds());
            clock.RunsIntoTimeout();

            //-----------------------------------------------------------------------------------------------------------
            // Assert
            //-----------------------------------------------------------------------------------------------------------
            act.Should().Throw<XunitException>().WithMessage(
                "*action should be greater or equal to 1s, but it required*");
        }

        [Fact]
        public void When_the_execution_time_of_an_action_is_greater_or_equal_to_a_limit_it_should_not_throw()
        {
            //-----------------------------------------------------------------------------------------------------------
            // Arrange
            //-----------------------------------------------------------------------------------------------------------
            var clock = new FakeClock();
            Action someAction = () => clock.Delay(TimeSpan.FromMilliseconds(100));

            //-----------------------------------------------------------------------------------------------------------
            // Act
            //-----------------------------------------------------------------------------------------------------------
            Action act = () => someAction.ExecutionTime(clock).Should().BeGreaterOrEqualTo(50.Milliseconds());
            clock.CompletesBeforeTimeout();

            //-----------------------------------------------------------------------------------------------------------
            // Assert
            //-----------------------------------------------------------------------------------------------------------
            act.Should().NotThrow();
        }

        [Fact]
        public void When_action_runs_indefinitely_it_should_be_stopped_and_not_throw_if_there_is_greater_or_equal_condition()
        {
            //-----------------------------------------------------------------------------------------------------------
            // Arrange
            //-----------------------------------------------------------------------------------------------------------
            Action someAction = () =>
            {
                // lets cause a deadlock
                var semaphore = new Semaphore(0, 1); // my weapon of choice is a semaphore
                semaphore.WaitOne(); // oops
            };

            //-----------------------------------------------------------------------------------------------------------
            // Act
            //-----------------------------------------------------------------------------------------------------------
            Action act = () => someAction.ExecutionTime().Should().BeGreaterOrEqualTo(100.Milliseconds());

            //-----------------------------------------------------------------------------------------------------------
            // Assert
            //-----------------------------------------------------------------------------------------------------------
            act.Should().NotThrow<XunitException>();
        }
        #endregion

        #region BeGreaterThan
        [Fact]
        public void When_the_execution_time_of_a_member_is_not_greater_than_a_limit_it_should_throw()
        {
            //-----------------------------------------------------------------------------------------------------------
            // Arrange
            //-----------------------------------------------------------------------------------------------------------
            var clock = new FakeClock();
            var subject = new SleepingClass(clock);

            //-----------------------------------------------------------------------------------------------------------
            // Act
            //-----------------------------------------------------------------------------------------------------------
            Action act = () => subject.ExecutionTimeOf(s => s.Sleep(100), clock).Should().BeGreaterThan(1.Seconds(),
                "we like speed");
            clock.RunsIntoTimeout();

            //-----------------------------------------------------------------------------------------------------------
            // Assert
            //-----------------------------------------------------------------------------------------------------------
            act.Should().Throw<XunitException>().WithMessage(
                "*(s.Sleep(100)) should be greater than 1s because we like speed, but it required*");
        }

        [Fact]
        public void When_the_execution_time_of_a_member_is_greater_than_a_limit_it_should_not_throw()
        {
            //-----------------------------------------------------------------------------------------------------------
            // Arrange
            //-----------------------------------------------------------------------------------------------------------
            var clock = new FakeClock();
            var subject = new SleepingClass(clock);

            //-----------------------------------------------------------------------------------------------------------
            // Act
            //-----------------------------------------------------------------------------------------------------------
            Action act = () => subject.ExecutionTimeOf(s => s.Sleep(200), clock).Should().BeGreaterThan(100.Milliseconds());
            clock.CompletesBeforeTimeout();

            //-----------------------------------------------------------------------------------------------------------
            // Assert
            //-----------------------------------------------------------------------------------------------------------
            act.Should().NotThrow();
        }

        [Fact]
        public void When_the_execution_time_of_an_action_is_not_greater_than_a_limit__it_should_throw()
        {
            //-----------------------------------------------------------------------------------------------------------
            // Arrange
            //-----------------------------------------------------------------------------------------------------------
            var clock = new FakeClock();
            Action someAction = () => clock.Delay(TimeSpan.FromMilliseconds(100));

            //-----------------------------------------------------------------------------------------------------------
            // Act
            //-----------------------------------------------------------------------------------------------------------
            Action act = () => someAction.ExecutionTime(clock).Should().BeGreaterThan(1.Seconds());
            clock.RunsIntoTimeout();

            //-----------------------------------------------------------------------------------------------------------
            // Assert
            //-----------------------------------------------------------------------------------------------------------
            act.Should().Throw<XunitException>().WithMessage(
                "*action should be greater than 1s, but it required*");
        }

        [Fact]
        public void When_the_execution_time_of_an_action_is_greater_than_a_limit_it_should_not_throw()
        {
            //-----------------------------------------------------------------------------------------------------------
            // Arrange
            //-----------------------------------------------------------------------------------------------------------
            var clock = new FakeClock();
            Action someAction = () => clock.Delay(TimeSpan.FromMilliseconds(200));

            //-----------------------------------------------------------------------------------------------------------
            // Act
            //-----------------------------------------------------------------------------------------------------------
            Action act = () => someAction.ExecutionTime(clock).Should().BeGreaterThan(100.Milliseconds());
            clock.CompletesBeforeTimeout();

            //-----------------------------------------------------------------------------------------------------------
            // Assert
            //-----------------------------------------------------------------------------------------------------------
            act.Should().NotThrow();
        }

        [Fact]
        public void When_action_runs_indefinitely_it_should_be_stopped_and_not_throw_if_there_is_greater_than_condition()
        {
            //-----------------------------------------------------------------------------------------------------------
            // Arrange
            //-----------------------------------------------------------------------------------------------------------
            Action someAction = () =>
            {
                // lets cause a deadlock
                var semaphore = new Semaphore(0, 1); // my weapon of choice is a semaphore
                semaphore.WaitOne(); // oops
            };

            //-----------------------------------------------------------------------------------------------------------
            // Act
            //-----------------------------------------------------------------------------------------------------------
            Action act = () => someAction.ExecutionTime().Should().BeGreaterThan(100.Milliseconds());

            //-----------------------------------------------------------------------------------------------------------
            // Assert
            //-----------------------------------------------------------------------------------------------------------
            act.Should().NotThrow<XunitException>();
        }
        #endregion

        #region BeCloseTo
        [Fact]
        public void When_the_execution_time_of_a_member_is_not_close_to_a_limit_it_should_throw()
        {
            //-----------------------------------------------------------------------------------------------------------
            // Arrange
            //-----------------------------------------------------------------------------------------------------------
            var clock = new FakeClock();
            var subject = new SleepingClass(clock);

            //-----------------------------------------------------------------------------------------------------------
            // Act
            //-----------------------------------------------------------------------------------------------------------
            Action act = () => subject.ExecutionTimeOf(s => s.Sleep(200), clock).Should().BeCloseTo(100.Milliseconds(),
                50.Milliseconds(),
                "we like speed");
            clock.RunsIntoTimeout();

            //-----------------------------------------------------------------------------------------------------------
            // Assert
            //-----------------------------------------------------------------------------------------------------------
            act.Should().Throw<XunitException>().WithMessage(
                "*(s.Sleep(200)) should be within 0.050s from 0.100s because we like speed, but it required*");
        }

        [Fact]
        public void When_the_execution_time_of_a_member_is_close_to_a_limit_it_should_not_throw()
        {
            //-----------------------------------------------------------------------------------------------------------
            // Arrange
            //-----------------------------------------------------------------------------------------------------------
            var clock = new FakeClock();
            var subject = new SleepingClass(clock);

            //-----------------------------------------------------------------------------------------------------------
            // Act
            //-----------------------------------------------------------------------------------------------------------
            Action act = () => subject.ExecutionTimeOf(s => s.Sleep(210), clock).Should().BeCloseTo(200.Milliseconds(),
                150.Milliseconds());
            clock.CompletesBeforeTimeout();

            //-----------------------------------------------------------------------------------------------------------
            // Assert
            //-----------------------------------------------------------------------------------------------------------
            act.Should().NotThrow();
        }

        [Fact]
        public void When_the_execution_time_of_an_action_is_not_close_to_a_limit__it_should_throw()
        {
            //-----------------------------------------------------------------------------------------------------------
            // Arrange
            //-----------------------------------------------------------------------------------------------------------
            var clock = new FakeClock();
            Action someAction = () => clock.Delay(TimeSpan.FromMilliseconds(200));

            //-----------------------------------------------------------------------------------------------------------
            // Act
            //-----------------------------------------------------------------------------------------------------------
            Action act = () => someAction.ExecutionTime(clock).Should().BeCloseTo(100.Milliseconds(), 50.Milliseconds());
            clock.RunsIntoTimeout();

            //-----------------------------------------------------------------------------------------------------------
            // Assert
            //-----------------------------------------------------------------------------------------------------------
            act.Should().Throw<XunitException>().WithMessage(
                "*action should be within 0.050s from 0.100s, but it required*");
        }

        [Fact]
        public void When_the_execution_time_of_an_action_is_close_to_a_limit_it_should_not_throw()
        {
            //-----------------------------------------------------------------------------------------------------------
            // Arrange
            //-----------------------------------------------------------------------------------------------------------
            var clock = new FakeClock();
            Action someAction = () => clock.Delay(TimeSpan.FromMilliseconds(210));

            //-----------------------------------------------------------------------------------------------------------
            // Act
            //-----------------------------------------------------------------------------------------------------------
            Action act = () => someAction.ExecutionTime(clock).Should().BeCloseTo(200.Milliseconds(), 150.Milliseconds());
            clock.CompletesBeforeTimeout();

            //-----------------------------------------------------------------------------------------------------------
            // Assert
            //-----------------------------------------------------------------------------------------------------------
            act.Should().NotThrow();
        }

        [Fact]
        public void When_action_runs_indefinitely_it_should_be_stopped_and_throw_if_there_is_be_close_to_condition()
        {
            //-----------------------------------------------------------------------------------------------------------
            // Arrange
            //-----------------------------------------------------------------------------------------------------------
            Action someAction = () =>
            {
                // lets cause a deadlock
                var semaphore = new Semaphore(0, 1); // my weapon of choice is a semaphore
                semaphore.WaitOne(); // oops
            };

            //-----------------------------------------------------------------------------------------------------------
            // Act
            //-----------------------------------------------------------------------------------------------------------
            Action act = () => someAction.ExecutionTime().Should().BeCloseTo(100.Milliseconds(), 50.Milliseconds());

            //-----------------------------------------------------------------------------------------------------------
            // Assert
            //-----------------------------------------------------------------------------------------------------------
            act.Should().Throw<XunitException>().WithMessage(
                "*action should be within 0.050s from 0.100s, but it required*");
        }
        #endregion

        #region ExecutionTime
        [Fact]
        public void When_action_runs_inside_execution_time_exceptions_are_captured_and_rethrown()
        {
            //-----------------------------------------------------------------------------------------------------------
            // Arrange
            //-----------------------------------------------------------------------------------------------------------
            Action someAction = () => throw new ArgumentException("Let's say somebody called the wrong method.");

            //-----------------------------------------------------------------------------------------------------------
            // Act
            //-----------------------------------------------------------------------------------------------------------
            Action act = () => someAction.ExecutionTime().Should().BeLessThan(200.Milliseconds());

            //-----------------------------------------------------------------------------------------------------------
            // Assert
            //-----------------------------------------------------------------------------------------------------------
            act.Should().Throw<ArgumentException>().WithMessage("Let's say somebody called the wrong method.");
        }

        [Fact]
        public void Stopwatch_is_not_stopped_after_first_execution_time_assertion()
        {
            //-----------------------------------------------------------------------------------------------------------
            // Arrange
            //-----------------------------------------------------------------------------------------------------------
            var clock = new FakeClock();
            Action someAction = () => clock.Delay(TimeSpan.FromMilliseconds(300));

            //-----------------------------------------------------------------------------------------------------------
            // Act
            //-----------------------------------------------------------------------------------------------------------
            Action act = () =>
            {
                // I know it's not meant to be used like this,
                // but since you can, it should still give consistent results
                Specialized.ExecutionTime time = someAction.ExecutionTime(clock);
                clock.RunsIntoTimeout();
                time.Should().BeGreaterThan(100.Milliseconds());
                time.Should().BeGreaterThan(200.Milliseconds());
            };

            //-----------------------------------------------------------------------------------------------------------
            // Assert
            //-----------------------------------------------------------------------------------------------------------
            act.Should().NotThrow();
        }
        #endregion

        internal class SleepingClass
        {
            private readonly IClock clock;

            public SleepingClass(IClock clock)
            {
                this.clock = clock;
            }

            public void Sleep(int milliseconds)
                => clock.Delay(TimeSpan.FromMilliseconds(milliseconds));
        }
    }
}
