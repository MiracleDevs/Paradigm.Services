using System;
using System.Collections.Generic;
using Microsoft.Extensions.CommandLineUtils;
using Paradigm.Services.CLI;

namespace Paradigm.Services.Tests.Fixtures.Tests
{
    public class Arguments
    {
        [ArgumentOption("-byte",  "--long-byte", "", CommandOptionType.SingleValue)]
        public byte Byte { get; set; }

        [ArgumentOption("-ushort", "--long-ushort", "", CommandOptionType.SingleValue)]
        public ushort UShort { get; set; }

        [ArgumentOption("-uint", "--long-uint", "", CommandOptionType.SingleValue)]
        public uint UInt { get; set; }

        [ArgumentOption("-ulong", "--long-ulong", "", CommandOptionType.SingleValue)]
        public ulong ULong { get; set; }

        [ArgumentOption("-sbyte", "--long-sbyte", "", CommandOptionType.SingleValue)]
        public sbyte SByte { get; set; }

        [ArgumentOption("-short", "--long-short", "", CommandOptionType.SingleValue)]
        public ushort Short { get; set; }

        [ArgumentOption("-int", "--long-int", "", CommandOptionType.SingleValue)]
        public int Int { get; set; }

        [ArgumentOption("-long", "--long-long", "", CommandOptionType.SingleValue)]
        public long Long { get; set; }

        [ArgumentOption("-float", "--long-float", "", CommandOptionType.SingleValue)]
        public float Float { get; set; }

        [ArgumentOption("-double", "--long-double", "", CommandOptionType.SingleValue)]
        public double Double { get; set; }

        [ArgumentOption("-decimal", "--long-decimal", "", CommandOptionType.SingleValue)]
        public decimal Decimal { get; set; }

        [ArgumentOption("-datetime", "--long-datetime", "", CommandOptionType.SingleValue)]
        public DateTime DateTime { get; set; }

        [ArgumentOption("-timespan", "--long-timespan", "", CommandOptionType.SingleValue)]
        public TimeSpan TimeSpan { get; set; }

        [ArgumentOption("-dtoffset", "--long-dtoffset", "", CommandOptionType.SingleValue)]
        public DateTimeOffset DateTimeOffset { get; set; }

        [ArgumentOption("-guid", "--long-guid", "", CommandOptionType.SingleValue)]
        public Guid Guid { get; set; }

        [ArgumentOption("-string", "--long-string", "", CommandOptionType.SingleValue)]
        public string String { get; set; }

        [ArgumentOption("-enum", "--long-enum", "", CommandOptionType.SingleValue)]
        public Enumeration Enumeration { get; set; }
    }

    public class NullableArguments
    {
        [ArgumentOption("-byte", "--long-byte", "", CommandOptionType.SingleValue, 1)]
        public byte? Byte { get; set; }

        [ArgumentOption("-ushort", "--long-ushort", "", CommandOptionType.SingleValue, 2)]
        public ushort? UShort { get; set; }

        [ArgumentOption("-uint", "--long-uint", "", CommandOptionType.SingleValue, 3)]
        public uint? UInt { get; set; }

        [ArgumentOption("-ulong", "--long-ulong", "", CommandOptionType.SingleValue, 4)]
        public ulong? ULong { get; set; }

        [ArgumentOption("-sbyte", "--long-sbyte", "", CommandOptionType.SingleValue,5)]
        public sbyte? SByte { get; set; }

        [ArgumentOption("-short", "--long-short", "", CommandOptionType.SingleValue, 6)]
        public ushort? Short { get; set; }

        [ArgumentOption("-int", "--long-int", "", CommandOptionType.SingleValue, 7)]
        public int? Int { get; set; }

        [ArgumentOption("-long", "--long-long", "", CommandOptionType.SingleValue, 8)]
        public long? Long { get; set; }

        [ArgumentOption("-float", "--long-float", "", CommandOptionType.SingleValue, 9)]
        public float? Float { get; set; }

        [ArgumentOption("-double", "--long-double", "", CommandOptionType.SingleValue, 10)]
        public double? Double { get; set; }

        [ArgumentOption("-decimal", "--long-decimal", "", CommandOptionType.SingleValue, 11)]
        public decimal? Decimal { get; set; }

        [ArgumentOption("-datetime", "--long-datetime", "", CommandOptionType.SingleValue)]
        public DateTime? DateTime { get; set; }

        [ArgumentOption("-timespan", "--long-timespan", "", CommandOptionType.SingleValue)]
        public TimeSpan? TimeSpan { get; set; }

        [ArgumentOption("-dtoffset", "--long-dtoffset", "", CommandOptionType.SingleValue)]
        public DateTimeOffset? DateTimeOffset { get; set; }

        [ArgumentOption("-guid", "--long-guid", "", CommandOptionType.SingleValue)]
        public Guid? Guid { get; set; }

        [ArgumentOption("-string", "--long-string", "", CommandOptionType.SingleValue)]
        public string String { get; set; }

        [ArgumentOption("-enum", "--long-enum", "", CommandOptionType.SingleValue, Tests.Enumeration.Value1)]
        public Enumeration? Enumeration { get; set; }
    }

    public class ListArguments
    {
        [ArgumentOption("-byte", "--long-byte", "", CommandOptionType.MultipleValue)]
        public List<byte> Byte { get; set; }

        [ArgumentOption("-ushort", "--long-ushort", "", CommandOptionType.MultipleValue)]
        public List<ushort> UShort { get; set; }

        [ArgumentOption("-uint", "--long-uint", "", CommandOptionType.MultipleValue)]
        public List<uint> UInt { get; set; }

        [ArgumentOption("-ulong", "--long-ulong", "", CommandOptionType.MultipleValue)]
        public List<ulong> ULong { get; set; }

        [ArgumentOption("-sbyte", "--long-sbyte", "", CommandOptionType.MultipleValue)]
        public List<sbyte> SByte { get; set; }

        [ArgumentOption("-short", "--long-short", "", CommandOptionType.MultipleValue)]
        public List<ushort> Short { get; set; }

        [ArgumentOption("-int", "--long-int", "", CommandOptionType.MultipleValue)]
        public List<int> Int { get; set; }

        [ArgumentOption("-long", "--long-long", "", CommandOptionType.MultipleValue)]
        public List<long> Long { get; set; }

        [ArgumentOption("-float", "--long-float", "", CommandOptionType.MultipleValue)]
        public List<float> Float { get; set; }

        [ArgumentOption("-double", "--long-double", "", CommandOptionType.MultipleValue)]
        public List<double> Double { get; set; }

        [ArgumentOption("-decimal", "--long-decimal", "", CommandOptionType.MultipleValue)]
        public List<decimal> Decimal { get; set; }

        [ArgumentOption("-datetime", "--long-datetime", "", CommandOptionType.MultipleValue)]
        public List<DateTime> DateTime { get; set; }

        [ArgumentOption("-timespan", "--long-timespan", "", CommandOptionType.MultipleValue)]
        public List<TimeSpan> TimeSpan { get; set; }

        [ArgumentOption("-dtoffset", "--long-dtoffset", "", CommandOptionType.MultipleValue)]
        public List<DateTimeOffset> DateTimeOffset { get; set; }

        [ArgumentOption("-guid", "--long-guid", "", CommandOptionType.MultipleValue)]
        public List<Guid> Guid { get; set; }

        [ArgumentOption("-string", "--long-string", "", CommandOptionType.MultipleValue)]
        public List<string> String { get; set; }

        [ArgumentOption("-enum", "--long-enum", "", CommandOptionType.MultipleValue)]
        public List<Enumeration> Enumeration { get; set; }
    }

    public class NullableListArguments
    {
        [ArgumentOption("-byte", "--long-byte", "", CommandOptionType.MultipleValue, 1)]
        public List<byte?> Byte { get; set; }

        [ArgumentOption("-ushort", "--long-ushort", "", CommandOptionType.MultipleValue, 2)]
        public List<ushort?> UShort { get; set; }

        [ArgumentOption("-uint", "--long-uint", "", CommandOptionType.MultipleValue, 3)]
        public List<uint?> UInt { get; set; }

        [ArgumentOption("-ulong", "--long-ulong", "", CommandOptionType.MultipleValue, 4)]
        public List<ulong?> ULong { get; set; }

        [ArgumentOption("-sbyte", "--long-sbyte", "", CommandOptionType.MultipleValue, 5)]
        public List<sbyte?> SByte { get; set; }

        [ArgumentOption("-short", "--long-short", "", CommandOptionType.MultipleValue, 6)]
        public List<ushort?> Short { get; set; }

        [ArgumentOption("-int", "--long-int", "", CommandOptionType.MultipleValue, 7)]
        public List<int?> Int { get; set; }

        [ArgumentOption("-long", "--long-long", "", CommandOptionType.MultipleValue, 8)]
        public List<long?> Long { get; set; }

        [ArgumentOption("-float", "--long-float", "", CommandOptionType.MultipleValue, 9)]
        public List<float?> Float { get; set; }

        [ArgumentOption("-double", "--long-double", "", CommandOptionType.MultipleValue, 10)]
        public List<double?> Double { get; set; }

        [ArgumentOption("-decimal", "--long-decimal", "", CommandOptionType.MultipleValue, 11)]
        public List<decimal?> Decimal { get; set; }

        [ArgumentOption("-datetime", "--long-datetime", "", CommandOptionType.MultipleValue)]
        public List<DateTime?> DateTime { get; set; }

        [ArgumentOption("-timespan", "--long-timespan", "", CommandOptionType.MultipleValue)]
        public List<TimeSpan?> TimeSpan { get; set; }

        [ArgumentOption("-dtoffset", "--long-dtoffset", "", CommandOptionType.MultipleValue)]
        public List<DateTimeOffset?> DateTimeOffset { get; set; }

        [ArgumentOption("-guid", "--long-guid", "", CommandOptionType.MultipleValue)]
        public List<Guid?> Guid { get; set; }

        [ArgumentOption("-string", "--long-string", "", CommandOptionType.MultipleValue)]
        public List<string> String { get; set; }

        [ArgumentOption("-enum", "--long-enum", "", CommandOptionType.MultipleValue, Tests.Enumeration.Value1)]
        public List<Enumeration?> Enumeration { get; set; }
    }

    public enum Enumeration
    {
        Value1 = 0,
        Value2
    }

}