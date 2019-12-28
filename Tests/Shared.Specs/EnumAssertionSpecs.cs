using System;
using Xunit;
using Xunit.Sdk;

namespace FluentAssertions.Specs
{
    public enum EnumULong : ulong
    {
        Int64Max = long.MaxValue,
        UInt64LessOne = ulong.MaxValue - 1,
        UInt64Max = ulong.MaxValue
    }

    public enum EnumLong : long
    {
        Int64Max = long.MaxValue,
        Int64LessOne = long.MaxValue - 1
    }

    public class EnumAssertionSpecs
    {
        [Fact]
        public void When_both_enums_are_equal_and_greater_than_max_long_it_should_not_throw()
        {
            // Arrange
            var enumOne = EnumULong.UInt64Max;
            var enumTwo = EnumULong.UInt64Max;

            // Act
            Action act = () => enumOne.Should().BeEquivalentTo(enumTwo);

            // Assert
            act.Should().NotThrow();
        }

        [Fact]
        public void When_both_enums_are_equal_and_of_different_underlying_types_it_should_not_throw()
        {
            // Arrange
            var enumOne = EnumLong.Int64Max;
            var enumTwo = EnumULong.Int64Max;

            // Act
            Action act = () => enumOne.Should().BeEquivalentTo(enumTwo);

            // Assert
            act.Should().NotThrow();
        }

        [Fact]
        public void When_both_enums_are_large_and_not_equal_it_should_throw()
        {
            // Arrange
            var subjectEnum = EnumLong.Int64LessOne;
            var expectedEnum = EnumULong.UInt64Max;

            // Act
            Action act = () => subjectEnum.Should().BeEquivalentTo(expectedEnum, "comparing enums should throw");

            // Assert
            act.Should().Throw<XunitException>()
#if NETCOREAPP1_1
                .WithMessage($"Expected enum to equal EnumULong.UInt64Max({(UInt64)EnumULong.UInt64Max}) by value because comparing enums should throw, but found EnumLong.Int64LessOne({(Int64)EnumLong.Int64LessOne})*");
#else
                .WithMessage($"Expected subjectEnum to equal EnumULong.UInt64Max({(ulong)EnumULong.UInt64Max}) by value because comparing enums should throw, but found EnumLong.Int64LessOne({(long)EnumLong.Int64LessOne})*");
#endif
        }

        [Fact]
        public void When_subject_is_null_and_enum_has_some_value_it_should_throw()
        {
            // Arrange
            object subject = null;
            object expectedEnum = EnumULong.UInt64Max;

            // Act
            // ReSharper disable once ExpressionIsAlwaysNull
            Action act = () => subject.Should().BeEquivalentTo(expectedEnum, x => x.ComparingEnumsByName(), "comparing enums should throw");

            // Assert
            act.Should().Throw<XunitException>()
                .WithMessage($"Expected*to equal EnumULong.UInt64Max({(ulong)EnumULong.UInt64Max}) by name because comparing enums should throw, but found null*");
        }

        [Fact]
        public void When_expectation_is_null_and_subject_enum_has_some_value_it_should_throw_with_a_useful_message()
        {
            // Arrange
            object subjectEnum = EnumULong.UInt64Max;
            object expected = null;

            // Act
            // ReSharper disable once ExpressionIsAlwaysNull
            Action act = () => subjectEnum.Should().BeEquivalentTo(expected, x => x.ComparingEnumsByName(), "comparing enums should throw");

            // Assert
            act.Should().Throw<XunitException>()
                .WithMessage("Expected*to equal null by name because comparing enums should throw, but found EnumULong.UInt64Max*");
        }

        [Fact]
        public void When_comparing_an_enum_and_a_numeric_for_equality_it_should_fail()
        {
            // Arrange
            MyEnum subject = MyEnum.One;
            object expected = 1;

            // Act
            Action act = () => subject.Should().Be(expected);

            // Assert
            act.Should().Throw<XunitException>();
        }

        [Fact]
        public void When_comparing_a_numeric_and_an_enum_for_equality_it_should_fail()
        {
            // Arrange
            object subject = 1;
            MyEnum expected = MyEnum.One;

            // Act
            Action act = () => subject.Should().Be(expected);

            // Assert
            act.Should().Throw<XunitException>();
        }

        [Fact]
        public void When_comparing_an_enum_and_a_numeric_for_equivalency_it_should_succeed()
        {
            // Arrange
            MyEnum subject = MyEnum.One;
            object expected = 1;

            // Act
            Action act = () => subject.Should().BeEquivalentTo(expected);

            // Assert
            act.Should().NotThrow();
        }

        [Fact]
        public void When_comparing_a_numeric_and_an_enum_for_equivalency_it_should_succeed()
        {
            // Arrange
            object subject = 1;
            MyEnum expected = MyEnum.One;

            // Act
            Action act = () => subject.Should().BeEquivalentTo(expected);

            // Assert
            act.Should().NotThrow();
        }

        [Fact]
        public void When_comparing_an_enum_and_a_string_for_equivalency_by_value_it_should_fail()
        {
            // Arrange
            MyEnum subject = MyEnum.One;
            object expected = "one";

            // Act
            Action act = () => subject.Should().BeEquivalentTo(expected, opt => opt.ComparingEnumsByValue());

            // Assert
            act.Should().Throw<XunitException>();
        }

        [Fact]
        public void When_comparing_a_string_and_an_enum_for_equivalency_by_value_it_should_fail()
        {
            // Arrange
            object subject = "one";
            MyEnum expected = MyEnum.One;

            // Act
            Action act = () => subject.Should().BeEquivalentTo(expected, opt => opt.ComparingEnumsByValue());

            // Assert
            act.Should().Throw<XunitException>();
        }

        [Fact]
        public void When_comparing_an_enum_and_a_string_for_equivalency_by_name_it_should_succeed()
        {
            // Arrange
            MyEnum subject = MyEnum.One;
            object expected = "one";

            // Act
            Action act = () => subject.Should().BeEquivalentTo(expected, opt => opt.ComparingEnumsByName());

            // Assert
            act.Should().NotThrow();
        }

        [Fact]
        public void When_comparing_a_string_and_an_enum_for_equivalency_by_name_it_should_succeed()
        {
            // Arrange
            object subject = "one";
            MyEnum expected = MyEnum.One;

            // Act
            Action act = () => subject.Should().BeEquivalentTo(expected, opt => opt.ComparingEnumsByName());

            // Assert
            act.Should().NotThrow();
        }

        [Fact]
        public void When_comparing_an_enum_and_an_invalid_string_for_equivalency_by_name_it_should_fail()
        {
            // Arrange
            MyEnum subject = MyEnum.One;
            object expected = "one111";

            // Act
            Action act = () => subject.Should().BeEquivalentTo(expected, opt => opt.ComparingEnumsByName());

            // Assert
            act.Should().Throw<XunitException>();
        }

        [Fact]
        public void When_comparing_an_invalid_string_and_an_enum_for_equivalency_by_name_it_should_fail()
        {
            // Arrange
            object subject = "one111";
            MyEnum expected = MyEnum.One;

            // Act
            Action act = () => subject.Should().BeEquivalentTo(expected, opt => opt.ComparingEnumsByName());

            // Assert
            act.Should().Throw<XunitException>();
        }

        [Fact]
        public void When_comparing_an_enum_and_a_numeric_for_equivalency_by_name_it_should_fail()
        {
            // Arrange
            MyEnum subject = MyEnum.One;
            object expected = 1;

            // Act
            Action act = () => subject.Should().BeEquivalentTo(expected, opt => opt.ComparingEnumsByName());

            // Assert
            act.Should().Throw<XunitException>();
        }

        [Fact]
        public void When_comparing_a_numeric_and_an_enum_for_equivalency_by_name_it_should_fail()
        {
            // Arrange
            object subject = 1;
            MyEnum expected = MyEnum.One;

            // Act
            Action act = () => subject.Should().BeEquivalentTo(expected, opt => opt.ComparingEnumsByName());

            // Assert
            act.Should().Throw<XunitException>();
        }

        [Fact]
        public void When_comparing_an_enum_and_a_numeric_for_equivalency_by_value_it_should_succeed()
        {
            // Arrange
            MyEnum subject = MyEnum.One;
            object expected = 1;

            // Act
            Action act = () => subject.Should().BeEquivalentTo(expected, opt => opt.ComparingEnumsByValue());

            // Assert
            act.Should().NotThrow();
        }

        [Fact]
        public void When_comparing_a_numeric_and_an_enum_for_equivalency_by_value_it_should_succeed()
        {
            // Arrange
            object subject = 1;
            MyEnum expected = MyEnum.One;

            // Act
            Action act = () => subject.Should().BeEquivalentTo(expected, opt => opt.ComparingEnumsByValue());

            // Assert
            act.Should().NotThrow();
        }

        private enum MyEnum
        {
            One = 1
        }
    }
}
