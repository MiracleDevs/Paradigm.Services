using System;
using Microsoft.Extensions.CommandLineUtils;
using Paradigm.Services.CLI;

namespace Paradigm.Services.Tests.Fixtures.Tests.CLI
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
}