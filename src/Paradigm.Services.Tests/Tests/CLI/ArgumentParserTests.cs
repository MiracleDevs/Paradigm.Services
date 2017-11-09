using System;
using FluentAssertions;
using NUnit.Framework;
using Paradigm.Services.CLI;
using Paradigm.Services.Tests.Fixtures.Tests;

namespace Paradigm.Services.Tests.Tests.CLI
{
    [TestFixture]
    public class ArgumentParserTests
    {
        #region Not Nullable

        [TestCase]
        public void ShouldParseAllNotNullableTypes()
        {
            var parser = new ArgumentParser();
            
            parser.ParseArguments<Arguments>("-byte 1 -ushort 2 -uint 3 -ulong 4 -sbyte 5 -short 6 -int 7 -long 8 -float 9 -double 10 -decimal 11 -datetime \"12/12/12 12:12:12\" -timespan \"12:12:12\" -dtoffset \"12/12/12 12:12:12\" -guid \"7deca82b-b15e-43e3-a6a3-ea771362b1ab\" -string \"hello world\" -enum Value1".Split(" "));
            parser.Run((type, arguments) =>
            {
                var args = arguments as Arguments;

                args.Byte.Should().Be(1);
                args.UShort.Should().Be(2);
                args.UInt.Should().Be(3);
                args.ULong.Should().Be(4);
                args.SByte.Should().Be(5);
                args.Short.Should().Be(6);
                args.Int.Should().Be(7);
                args.Long.Should().Be(8);
                args.Float.Should().Be(9);
                args.Double.Should().Be(10);
                args.Decimal.Should().Be(11);
                args.DateTime.Should().Be(new DateTime(12, 12, 12, 12, 12, 12));
                args.TimeSpan.Should().Be(new TimeSpan(0, 12, 12, 12));
                args.DateTimeOffset.Should().Be(new DateTime(12, 12, 12, 12, 12, 12));
                args.Guid.Should().Be(Guid.Parse("deca82b-b15e-43e3-a6a3-ea771362b1ab"));
                args.String.Should().Be("hello world");
                args.Enumeration.Should().Be(Enumeration.Value1);
            });
        }

        [TestCase]
        public void ShouldThrowIfOneOfTheNotNullableTypesIsNull()
        {
            var parser = new ArgumentParser();
            Exception exception = null;

            parser.ParseArguments<Arguments>("".Split(" "));
            parser.Run((t, e) => {/* nothing */}, ex =>
            {
                exception = ex; 
            });

            exception.Should().NotBeNull();
            exception.GetType().Should().Be(typeof(AggregateException));
            (exception as AggregateException).InnerExceptions.Count.Should().Be(16);
        }

        #endregion

        #region Nullable

        [TestCase]
        public void ShouldParseAllNullableTypes()
        {
            var parser = new ArgumentParser();

            parser.ParseArguments<NullableArguments>("-byte 1 -ushort 2 -uint 3 -ulong 4 -sbyte 5 -short 6 -int 7 -long 8 -float 9 -double 10 -decimal 11 -datetime \"12/12/12 12:12:12\" -timespan \"12:12:12\" -dtoffset \"12/12/12 12:12:12\" -guid \"7deca82b-b15e-43e3-a6a3-ea771362b1ab\" -string \"hello world\" -enum Value1".Split(" "));
            parser.Run((type, arguments) =>
            {
                var args = arguments as NullableArguments;

                args.Byte.Should().Be(1);
                args.UShort.Should().Be(2);
                args.UInt.Should().Be(3);
                args.ULong.Should().Be(4);
                args.SByte.Should().Be(5);
                args.Short.Should().Be(6);
                args.Int.Should().Be(7);
                args.Long.Should().Be(8);
                args.Float.Should().Be(9);
                args.Double.Should().Be(10);
                args.Decimal.Should().Be(11);
                args.DateTime.Should().Be(new DateTime(12, 12, 12, 12, 12, 12));
                args.TimeSpan.Should().Be(new TimeSpan(0, 12, 12, 12));
                args.DateTimeOffset.Should().Be(new DateTime(12, 12, 12, 12, 12, 12));
                args.Guid.Should().Be(Guid.Parse("deca82b-b15e-43e3-a6a3-ea771362b1ab"));
                args.String.Should().Be("hello world");
                args.Enumeration.Should().Be(Enumeration.Value1);
            });
        }

        [TestCase]
        public void ShouldSetDefaultValuesOrNullIfNotPresent()
        {
            var parser = new ArgumentParser();

            parser.ParseArguments<NullableArguments>("".Split(" "));
            parser.Run((type, arguments) =>
            {
                var args = arguments as NullableArguments;

                args.Byte.Should().Be(1);
                args.UShort.Should().Be(2);
                args.UInt.Should().Be(3);
                args.ULong.Should().Be(4);
                args.SByte.Should().Be(5);
                args.Short.Should().Be(6);
                args.Int.Should().Be(7);
                args.Long.Should().Be(8);
                args.Float.Should().Be(9);
                args.Double.Should().Be(10);
                args.Decimal.Should().Be(11);
                args.DateTime.Should().Be(null);
                args.TimeSpan.Should().Be(null);
                args.DateTimeOffset.Should().Be(null);
                args.Guid.Should().Be(null);
                args.String.Should().Be(null);
                args.Enumeration.Should().Be(Enumeration.Value1);
            });
        }

        #endregion

        #region Not Nullable List

        [TestCase]
        public void ShouldParseAllNotNullableTypeLists()
        {
            var parser = new ArgumentParser();

            parser.ParseArguments<ListArguments>(("-byte 1 -ushort 2 -uint 3 -ulong 4 -sbyte 5 -short 6 -int 7 -long 8 -float 9 -double 10 -decimal 11 -datetime \"12/12/12 12:12:12\" -timespan \"12:12:12\" -dtoffset \"12/12/12 12:12:12\" -guid \"7deca82b-b15e-43e3-a6a3-ea771362b1ab\" -string \"hello world\" -enum Value1" +
                                              "-byte 1 -ushort 2 -uint 3 -ulong 4 -sbyte 5 -short 6 -int 7 -long 8 -float 9 -double 10 -decimal 11 -datetime \"12/12/12 12:12:12\" -timespan \"12:12:12\" -dtoffset \"12/12/12 12:12:12\" -guid \"7deca82b-b15e-43e3-a6a3-ea771362b1ab\" -string \"hello world\" -enum Value1").Split(" "));
            parser.Run((type, arguments) =>
            {
                var args = arguments as ListArguments;

                args.Byte.Should().Equal(1, 1);
                args.UShort.Should().Equal(2, 2);
                args.UInt.Should().Equal(3,3);
                args.ULong.Should().Equal(4,4);
                args.SByte.Should().Equal(5,5);
                args.Short.Should().Equal(6,6);
                args.Int.Should().Equal(7,7);
                args.Long.Should().Equal(8,8);
                args.Float.Should().Equal(9,9);
                args.Double.Should().Equal(10,10);
                args.Decimal.Should().Equal(11,11);
                args.DateTime.Should().Equal(new DateTime(12, 12, 12, 12, 12, 12), new DateTime(12, 12, 12, 12, 12, 12));
                args.TimeSpan.Should().Equal(new TimeSpan(0, 12, 12, 12), new TimeSpan(0, 12, 12, 12));
                args.DateTimeOffset.Should().Equal(new DateTime(12, 12, 12, 12, 12, 12), new DateTime(12, 12, 12, 12, 12, 12));
                args.Guid.Should().Equal(Guid.Parse("deca82b-b15e-43e3-a6a3-ea771362b1ab"), Guid.Parse("deca82b-b15e-43e3-a6a3-ea771362b1ab"));
                args.String.Should().Equal("hello world", "hello world");
                args.Enumeration.Should().Equal(Enumeration.Value1, Enumeration.Value1);
            });
        }

        #endregion
    }
}