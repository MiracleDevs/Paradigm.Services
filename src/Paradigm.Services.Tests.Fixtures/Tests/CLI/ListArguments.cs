using System;
using System.Collections.Generic;
using Microsoft.Extensions.CommandLineUtils;
using Paradigm.Services.CLI;

namespace Paradigm.Services.Tests.Fixtures.Tests.CLI
{
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
}