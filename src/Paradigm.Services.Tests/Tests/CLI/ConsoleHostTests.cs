using System;
using FluentAssertions;
using NUnit.Framework;
using Paradigm.Services.CLI;
using Paradigm.Services.Tests.Fixtures.Tests;
using Paradigm.Services.Tests.Fixtures.Tests.CLI;

namespace Paradigm.Services.Tests.Tests.CLI
{
    [TestFixture]
    public class ConsoleHostTests
    {
        [TestCase]
        public void ShouldCreateNewInstance()
        {
            ConsoleHost.Create().Should().NotBeNull();
        } 

        [TestCase]
        public void ShouldAcceptArguments()
        {
            var args = new[] { "-byte", "1", "-ushort", "2", "-uint", "3", "-ulong", "4", "-sbyte", "5", "-short", "6", "-int", "7", "-long", "8", "-float", "9", "-double", "10", "-decimal", "11", "-datetime", "12/12/2012 12:12:12", "-timespan", "12:12:12", "-dtoffset", "12/12/2012 12:12:12", "-guid", "7deca82b-b15e-43e3-a6a3-ea771362b1ab", "-string", "hello world", "-enum", "Value1" };
            var consoleHost = ConsoleHost.Create().ParseArguments<Arguments>(args);

            consoleHost.ArgumentParser.Should().NotBeNull();
            consoleHost.ArgumentParser.LineArguments.Should().HaveCount(args.Length);
        }


        [TestCase]
        public void ShouldSetTheVersion()
        {
            var args = new[] { "-byte", "1", "-ushort", "2", "-uint", "3", "-ulong", "4", "-sbyte", "5", "-short", "6", "-int", "7", "-long", "8", "-float", "9", "-double", "10", "-decimal", "11", "-datetime", "12/12/2012 12:12:12", "-timespan", "12:12:12", "-dtoffset", "12/12/2012 12:12:12", "-guid", "7deca82b-b15e-43e3-a6a3-ea771362b1ab", "-string", "hello world", "-enum", "Value1" };
            var consoleHost = ConsoleHost.Create().ParseArguments<Arguments>(args).SetVersion("1.0", "1.0.0");
            consoleHost.ArgumentParser.ShortVersion.Should().Be("1.0");
            consoleHost.ArgumentParser.LongVersion.Should().Be("1.0.0");
        }

        [TestCase]
        public void ShouldntLetSetTheVersionBeforeParseArguments()
        {
            ConsoleHost.Create().Invoking(x => x.SetVersion("1.0", "1.0.0")).ShouldThrow<Exception>();
        }

        [TestCase]
        public void ShouldSetupSyncStartup()
        {
            var consoleHost = ConsoleHost.Create().UseStartup<SyncStartup>();

            (consoleHost.Startup as SyncStartup).Should().NotBeNull();
            (consoleHost.Startup as SyncStartup).Configuration.Should().BeNull(); /* there is no configuration configured here */
            (consoleHost.Startup as SyncStartup).ServiceCollection.Should().NotBeNull();
        }


        [TestCase]
        public void ShouldFailIfNoSuitableConstructor()
        {
            ConsoleHost.Create().Invoking(x => x.UseStartup<PrivateStartup>()).ShouldThrow<Exception>();
        }

        [TestCase]
        public void ShouldSetupAsnycStartup()
        {
            var consoleHost = ConsoleHost.Create().UseStartup<AsyncStartup>();

            (consoleHost.Startup as AsyncStartup).Should().NotBeNull();
            (consoleHost.Startup as AsyncStartup).Configuration.Should().BeNull();
            (consoleHost.Startup as AsyncStartup).ServiceCollection.Should().NotBeNull();
        }

        [TestCase]
        public void ShouldSetupTwoConfigure()
        {
            var consoleHost = ConsoleHost.Create().UseStartup<TwoConfigureStartup>();

            (consoleHost.Startup as TwoConfigureStartup).Should().NotBeNull();
            (consoleHost.Startup as TwoConfigureStartup).Configuration.Should().BeNull();
            (consoleHost.Startup as TwoConfigureStartup).ServiceCollection.Should().NotBeNull();
        }

        [TestCase]
        public void ShouldRunSyncMethod()
        {
            var consoleHost = ConsoleHost.Create();
            var result = consoleHost.UseStartup<SyncStartup>().Run(ex => throw ex);

            result.Should().Be(0);
            (consoleHost.Startup as SyncStartup).ServiceProvider.Should().NotBeNull();
        }

        [TestCase]
        public void ShouldRunAsyncMethod()
        {
            var consoleHost = ConsoleHost.Create();
            var result = consoleHost.UseStartup<AsyncStartup>().Run(ex => throw ex);

            result.Should().Be(0);
            (consoleHost.Startup as AsyncStartup).ServiceProvider.Should().NotBeNull();
        }

        [TestCase]
        public void ShouldRunWithTwoRunMethods()
        {
            var consoleHost = ConsoleHost.Create();
            var result = consoleHost.UseStartup<TwoRunStartup>().Run(ex => throw ex);

            result.Should().Be(0);
            (consoleHost.Startup as TwoRunStartup).ServiceProvider.Should().NotBeNull();
        }

        [TestCase]
        public void ShouldntRunIfStartupWasntSetup()
        {
            ConsoleHost.Create().Invoking(x => x.Run(ex => throw ex)).ShouldThrow<Exception>();
        }

        [TestCase]
        public void ShouldNotRunIfNoRunMethodPresent()
        {
            ConsoleHost.Create().UseStartup<EmptyStartup>().Invoking(x => x.Run(ex => throw ex)).ShouldThrow<Exception>();
        }

        [TestCase]
        public void ShouldIntegrateAllMethods()
        {
            var consoleHost = ConsoleHost.Create();
            var args = new[] { "-byte", "1", "-ushort", "2", "-uint", "3", "-ulong", "4", "-sbyte", "5", "-short", "6", "-int", "7", "-long", "8", "-float", "9", "-double", "10", "-decimal", "11", "-datetime", "12/12/2012 12:12:12", "-timespan", "12:12:12", "-dtoffset", "12/12/2012 12:12:12", "-guid", "7deca82b-b15e-43e3-a6a3-ea771362b1ab", "-string", "hello world", "-enum", "Value1" };

            consoleHost
                .ParseArguments<Arguments>(args)
                .UseStartup<SyncStartup>()
                .Run(ex => throw ex);

            consoleHost.ServiceProvider.Should().NotBeNull();
            consoleHost.ServiceCollection.Should().NotBeNull();
            consoleHost.ArgumentParser.Should().NotBeNull();
            consoleHost.Startup.Should().NotBeNull();
            (consoleHost.Startup as SyncStartup).Should().NotBeNull();
            (consoleHost.Startup as SyncStartup).Configuration.Should().BeNull();
            (consoleHost.Startup as SyncStartup).ServiceCollection.Should().NotBeNull();
            (consoleHost.Startup as SyncStartup).ServiceProvider.Should().NotBeNull();
        }
    }
}