using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using FluentAssertions.Execution;
using Xunit;
using Xunit.Sdk;

namespace FluentAssertions.Specs.Specialized
{
    public class AggregateExceptionAssertionSpecs
    {
        private class EqualityComparer<T> : IEqualityComparer<T>
        {
            public EqualityComparer(Func<T, T, bool> cmp, Func<T, int> hasher)
            {
                this.Cmp = cmp;
                this.Hasher = hasher;
            }

            public bool Equals(T x, T y)
            {
                return Cmp(x, y);
            }

            public int GetHashCode(T obj)
            {
                return Hasher(obj);
            }

            public Func<T, T, bool> Cmp { get; set; }

            public Func<T, int> Hasher { get; set; }
        }

        [Fact]
        private void Lollostein()
        {
            var shoulds = typeof(FluentAssertions.AssertionExtensions)
                .GetMembers(BindingFlags.Public | BindingFlags.Static)
                .OfType<MethodInfo>()
                .Where(e => !e.ContainsGenericParameters);

            var exceptions = new List<Exception>();

            foreach (MethodInfo should in shoulds)
            {
                ParameterInfo[] methodParameters = should.GetParameters();
                IEnumerable<MethodInfo> subAssertions = should.ReturnType
                    .GetMembers(BindingFlags.Public | BindingFlags.Instance)
                    .OfType<MethodInfo>()
                    .Where(e => e.GetParameters().Length > 0);

                foreach (IEnumerable<object> paramCombination in GetParameters(methodParameters))
                {
                    InvokeAssertion(should, subAssertions, paramCombination.ToArray());
                }
            }

            var set = new HashSet<Exception>(exceptions, new EqualityComparer<Exception>((a, b) => a.StackTrace == b.StackTrace, e => e.StackTrace.GetHashCode()));
            var sb = new System.Text.StringBuilder();
            foreach (var ex in set)
            {
                sb.AppendLine(ex.ToString());
            }

            var report = sb.ToString();
            _ = report;

            void InvokeAssertion(
                MethodInfo should,
                IEnumerable<MethodInfo> subAssertions,
                object[] paramCombination)
            {
                try
                {
                    object assertion = should.Invoke(null, paramCombination);
                    foreach (MethodInfo subAssertion in subAssertions.Where(e => !(
                        e.ContainsGenericParameters
                            && e.ReflectedType == typeof(FluentAssertions.Xml.XAttributeAssertions)
                            && (e.Name is "Be" or "NotBe" or "BeSameAs" or "NotBeSameAs")
                        )))
                    {
                        ParameterInfo[] subAssertionParams = subAssertion.GetParameters();
                        foreach (IEnumerable<object> subParams in GetParameters(subAssertionParams))
                        {
                            InvokeSubAssertion(assertion, subAssertion, subParams);
                        }
                    }
                }
                catch (TargetInvocationException tie)
                    when (tie.InnerException is NullReferenceException nre)
                {
                    exceptions.Add(nre);
                }
                catch
                {
                }
            }

            void InvokeSubAssertion(object assertion, MethodInfo subAssertion, IEnumerable<object> subParams)
            {
                try
                {
                    using var _ = new AssertionScope();
                    object[] parameters = subParams.ToArray();
                    subAssertion.Invoke(assertion, parameters);
                }
                catch (TargetInvocationException tie)
                    when (tie.InnerException is NullReferenceException innerNRE)
                {
                    exceptions.Add(innerNRE);
                }
                catch
                {
                }
            }
        }

        private IEnumerable<IEnumerable<object>> GetParameters(IEnumerable<ParameterInfo> parameters)
        {
            if (!parameters.Any())
            {
                yield return Array.Empty<object>();
                yield break;
            }

            ParameterInfo firstParam = parameters.First();
            IEnumerable<ParameterInfo> remaining = parameters.Skip(1);

            var combinations = from p in GetParameter(firstParam)
                               from ps in GetParameters(remaining)
                               select Combine(p, ps);

            foreach (var combination in combinations)
            {
                yield return combination;
            }

            IEnumerable<object> Combine(object obj, IEnumerable<object> objs)
            {
                yield return obj;
                foreach (var otherObj in objs)
                {
                    yield return otherObj;
                }
            }

            IEnumerable<object> GetParameter(ParameterInfo p)
            {
                Type type = p.ParameterType;

                if (type.IsValueType)
                {
                    yield return Activator.CreateInstance(type);
                }
                else
                {
                    yield return null;

                    if (type.IsAbstract
                        || type.IsGenericParameter
                        || type == typeof(XName)
                        || type == typeof(XAttribute))
                    {
                        yield break;
                    }

                    var ctor = type.GetConstructor(
                        BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic,
                        null, Type.EmptyTypes, null);

                    if (ctor is object)
                    {
                        object obj = null;
                        try
                        {
                            obj = ctor.Invoke(null);
                        }
                        catch
                        {
                        }

                        if (obj != null)
                        {
                            yield return obj;
                        }
                    }
                    else if (type.IsArray)
                    {
                        yield return Array.CreateInstance(type, 0);
                    }
                    else if (type.IsGenericType)
                    {
                        if (type.GetGenericTypeDefinition().Equals(typeof(IEnumerable<>)))
                        {
                            yield return Array.Empty<object>();
                        }
                    }
                    else if (type == typeof(string))
                    {
                        yield return string.Empty;
                    }
                    else if (type == typeof(Type))
                    {
                        yield return type;
                    }
                    else if (type.GetConstructors().Length > 0)
                    {
                        foreach (var constructor in type.GetConstructors())
                        {
                            foreach (var constructorParams in GetParameters(constructor.GetParameters()))
                            {
                                object a = null;
                                try
                                {
                                    a = constructor.Invoke(constructorParams.ToArray());
                                }
                                catch
                                {
                                }

                                if (a != null)
                                {
                                    yield return a;
                                }
                            }
                        }
                    }
                    else
                    {
                    }
                }
            }
        }

        [Fact]
        public void When_the_expected_exception_is_wrapped_it_should_succeed()
        {
            // Arrange
            var exception = new AggregateException(
                new InvalidOperationException("Ignored"),
                new XunitException("Background"));

            // Act
            Action act = () => throw exception;

            // Assert
            act.Should().Throw<XunitException>().WithMessage("Background");
        }

        [Fact]
        public void When_the_expected_exception_was_not_thrown_it_should_report_the_actual_exceptions()
        {
            // Arrange
            Action throwingOperation = () =>
            {
                throw new AggregateException(
                    new InvalidOperationException("You can't do this"),
                    new NullReferenceException("Found a null"));
            };

            // Act
            Action act = () => throwingOperation
                .Should().Throw<ArgumentNullException>()
                .WithMessage("Something I expected");

            // Assert
            act.Should().Throw<XunitException>()
                .WithMessage("*InvalidOperation*You can't do this*")
                .WithMessage("*NullReferenceException*Found a null*");
        }

        [Fact]
        public void When_no_exception_was_expected_it_should_report_the_actual_exceptions()
        {
            // Arrange
            Action throwingOperation = () =>
            {
                throw new AggregateException(
                    new InvalidOperationException("You can't do this"),
                    new NullReferenceException("Found a null"));
            };

            // Act
            Action act = () => throwingOperation.Should().NotThrow();

            // Assert
            act.Should().Throw<XunitException>()
                .WithMessage("*InvalidOperation*You can't do this*")
                .WithMessage("*NullReferenceException*Found a null*");
        }
    }
}
