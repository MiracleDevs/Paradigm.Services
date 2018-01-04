using System;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Paradigm.Services.CLI;
using Paradigm.Services.Tests.Fixtures.Tests.CLI;

namespace Paradigm.Services.Tests.Tests.CLI
{
    [TestFixture]
    public class ConsoleHostTest
    {
        [TestCase]
        public void ShouldCreateNewInstance()
        {
            var host = ConsoleHost.Create();

            host.Should().NotBeNull();
            host.HostingEnvironment.Should().NotBeNull();
            host.ServiceCollection.Should().NotBeNull();
        }

        [TestCase]
        public void ShouldAcceptArguments()
        {
            var args = new[] { "-byte", "1", "-ushort", "2", "-uint", "3", "-ulong", "4", "-sbyte", "5", "-short", "6", "-int", "7", "-long", "8", "-float", "9", "-double", "10", "-decimal", "11", "-datetime", "12/12/2012 12:12:12", "-timespan", "12:12:12", "-dtoffset", "12/12/2012 12:12:12", "-guid", "7deca82b-b15e-43e3-a6a3-ea771362b1ab", "-string", "hello world", "-enum", "Value1" };
            var parser = ConsoleHost.Create().ParseArguments<Arguments>(args);

            parser.Should().NotBeNull();
            parser.LineArguments.Should().HaveCount(args.Length);
        }

        [TestCase]
        public void ShouldSetTheVersion()
        {
            var args = new[] { "-byte", "1", "-ushort", "2", "-uint", "3", "-ulong", "4", "-sbyte", "5", "-short", "6", "-int", "7", "-long", "8", "-float", "9", "-double", "10", "-decimal", "11", "-datetime", "12/12/2012 12:12:12", "-timespan", "12:12:12", "-dtoffset", "12/12/2012 12:12:12", "-guid", "7deca82b-b15e-43e3-a6a3-ea771362b1ab", "-string", "hello world", "-enum", "Value1" };
            var parser = ConsoleHost.Create().ParseArguments<Arguments>(args);

            parser.SetVersion("1.0", "1.0.0");

            parser.ShortVersion.Should().Be("1.0");
            parser.LongVersion.Should().Be("1.0.0");
        }

        [TestCase]
        public void ShouldSetupSyncStartup()
        {
            var consoleHost = ConsoleHost.Create().UseStartup<SyncStartup>().Build();

            (consoleHost.Startup as SyncStartup).Should().NotBeNull();
            (consoleHost.Startup as SyncStartup).Configuration.Should().BeNull(); /* there is no configuration configured here */
            (consoleHost.Startup as SyncStartup).ServiceCollection.Should().NotBeNull();
            (consoleHost.Startup as SyncStartup).ServiceProvider.Should().NotBeNull();
        }


        [TestCase]
        public void ShouldFailIfNoSuitableConstructor()
        {
            ConsoleHost.Create().Invoking(x => x.UseStartup<PrivateStartup>().Build()).ShouldThrow<Exception>();
        }

        [TestCase]
        public void ShouldSetupAsnycStartup()
        {
            var consoleHost = ConsoleHost.Create().UseStartup<AsyncStartup>().Build();

            (consoleHost.Startup as AsyncStartup).Should().NotBeNull();
            (consoleHost.Startup as AsyncStartup).Configuration.Should().BeNull();
            (consoleHost.Startup as AsyncStartup).ServiceCollection.Should().NotBeNull();
            (consoleHost.Startup as AsyncStartup).ServiceProvider.Should().NotBeNull();
        }

        [TestCase]
        public void ShouldSetupTwoConfigure()
        {
            var consoleHost = ConsoleHost.Create().UseStartup<TwoConfigureStartup>().Build();

            (consoleHost.Startup as TwoConfigureStartup).Should().NotBeNull();
            (consoleHost.Startup as TwoConfigureStartup).Configuration.Should().BeNull();
            (consoleHost.Startup as TwoConfigureStartup).ServiceCollection.Should().NotBeNull();
            (consoleHost.Startup as TwoConfigureStartup).ServiceProvider.Should().NotBeNull();
        }

        [TestCase]
        public void ShouldConfigureArguments()
        {
            var args = new[] { "-byte", "1", "-ushort", "2", "-uint", "3", "-ulong", "4", "-sbyte", "5", "-short", "6", "-int", "7", "-long", "8", "-float", "9", "-double", "10", "-decimal", "11", "-datetime", "12/12/2012 12:12:12", "-timespan", "12:12:12", "-dtoffset", "12/12/2012 12:12:12", "-guid", "7deca82b-b15e-43e3-a6a3-ea771362b1ab", "-string", "hello world", "-enum", "Value1" };

            var consoleHost = ConsoleHost.Create();
            consoleHost.ParseArguments<Arguments>(args);
            consoleHost.UseStartup<SyncStartup>();
            consoleHost.Build();

            var arguments = (consoleHost.Startup as SyncStartup).ServiceProvider.GetService<Arguments>();

            arguments.Should().NotBeNull();
            arguments.Byte.Should().Be(1);
            arguments.UShort.Should().Be(2);
            arguments.UInt.Should().Be(3);
            arguments.ULong.Should().Be(4);
            arguments.SByte.Should().Be(5);
            arguments.Short.Should().Be(6);
            arguments.Int.Should().Be(7);
            arguments.Long.Should().Be(8);
            arguments.Float.Should().Be(9);
            arguments.Double.Should().Be(10);
            arguments.Decimal.Should().Be(11);
            arguments.DateTime.Should().Be(new DateTime(2012, 12, 12, 12, 12, 12));
            arguments.TimeSpan.Should().Be(new TimeSpan(0, 12, 12, 12));
            arguments.DateTimeOffset.Should().Be(new DateTime(2012, 12, 12, 12, 12, 12));
            arguments.Guid.Should().Be(Guid.Parse("7deca82b-b15e-43e3-a6a3-ea771362b1ab"));
            arguments.String.Should().Be("hello world");
            arguments.Enumeration.Should().Be(Enumeration.Value1);
        }

        [TestCase]
        public void ShouldntRunIfStartupWasntSetup()
        {
            ConsoleHost.Create().Invoking(x => x.Build()).ShouldThrow<Exception>();
        }

        [TestCase]
        public void ShouldNotRunIfNoRunMethodPresent()
        {
            ConsoleHost.Create().UseStartup<EmptyStartup>().Invoking(x => x.Build()).ShouldThrow<Exception>();
        }

        [TestCase]
        public void ShouldIntegrateAllMethods()
        {
            var consoleHost = ConsoleHost.Create();
            var args = new[] { "-byte", "1", "-ushort", "2", "-uint", "3", "-ulong", "4", "-sbyte", "5", "-short", "6", "-int", "7", "-long", "8", "-float", "9", "-double", "10", "-decimal", "11", "-datetime", "12/12/2012 12:12:12", "-timespan", "12:12:12", "-dtoffset", "12/12/2012 12:12:12", "-guid", "7deca82b-b15e-43e3-a6a3-ea771362b1ab", "-string", "hello world", "-enum", "Value1" };

            consoleHost.ParseArguments<Arguments>(args)
                       .SetVersion("1.0", "1.0.0");

            consoleHost.UseConfiguration();

            consoleHost.UseStartup<SyncStartup>()
                       .HandleErrors(x => throw x)
                       .HandleExit(() => {/* nothing */})
                       .Build();

            consoleHost.ServiceProvider.Should().NotBeNull();
            consoleHost.ServiceCollection.Should().NotBeNull();
            consoleHost.ArgumentParser.Should().NotBeNull();
            consoleHost.Startup.Should().NotBeNull();
            (consoleHost.Startup as SyncStartup).Should().NotBeNull();
            (consoleHost.Startup as SyncStartup).Configuration.Should().NotBeNull();
            (consoleHost.Startup as SyncStartup).ServiceCollection.Should().NotBeNull();
            (consoleHost.Startup as SyncStartup).ServiceProvider.Should().NotBeNull();
        }
    }
}