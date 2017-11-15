using System;
using Microsoft.Extensions.CommandLineUtils;
using Paradigm.Services.CLI;

namespace Paradigm.Services.Tests.Fixtures.Tests.CLI
{
    public class DefaultArguments
    {
        [ArgumentOption("-byte", "--long-byte", "", CommandOptionType.SingleValue, 1)]
        public byte Byte { get; set; }

        [ArgumentOption("-ushort", "--long-ushort", "", CommandOptionType.SingleValue, 2)]
        public ushort UShort { get; set; }

        [ArgumentOption("-uint", "--long-uint", "", CommandOptionType.SingleValue, 3)]
        public uint UInt { get; set; }

        [ArgumentOption("-ulong", "--long-ulong", "", CommandOptionType.SingleValue, 4)]
        public ulong ULong { get; set; }

        [ArgumentOption("-sbyte", "--long-sbyte", "", CommandOptionType.SingleValue, 5)]
        public sbyte SByte { get; set; }

        [ArgumentOption("-short", "--long-short", "", CommandOptionType.SingleValue, 6)]
        public ushort Short { get; set; }

        [ArgumentOption("-int", "--long-int", "", CommandOptionType.SingleValue, 7)]
        public int Int { get; set; }

        [ArgumentOption("-long", "--long-long", "", CommandOptionType.SingleValue, 8)]
        public long Long { get; set; }

        [ArgumentOption("-float", "--long-float", "", CommandOptionType.SingleValue, 9)]
        public float Float { get; set; }

        [ArgumentOption("-double", "--long-double", "", CommandOptionType.SingleValue, 10)]
        public double Double { get; set; }

        [ArgumentOption("-decimal", "--long-decimal", "", CommandOptionType.SingleValue, 11)]
        public decimal Decimal { get; set; }

        [ArgumentOption("-datetime", "--long-datetime", "", CommandOptionType.SingleValue, "12/12/2012 12:12:12")]
        public DateTime DateTime { get; set; }

        [ArgumentOption("-timespan", "--long-timespan", "", CommandOptionType.SingleValue, "12:12:12")]
        public TimeSpan TimeSpan { get; set; }

        [ArgumentOption("-dtoffset", "--long-dtoffset", "", CommandOptionType.SingleValue, "12/12/2012 12:12:12")]
        public DateTimeOffset DateTimeOffset { get; set; }

        [ArgumentOption("-guid", "--long-guid", "", CommandOptionType.SingleValue, "7deca82b-b15e-43e3-a6a3-ea771362b1ab")]
        public Guid Guid { get; set; }

        [ArgumentOption("-string", "--long-string", "", CommandOptionType.SingleValue, "hello world")]
        public string String { get; set; }

        [ArgumentOption("-enum", "--long-enum", "", CommandOptionType.SingleValue, CLI.Enumeration.Value1)]
        public Enumeration Enumeration { get; set; }
    }
}