using System;
using Ocelot.Cluster;
using Shouldly;
using TestStack.BDDfy;
using Xunit;

namespace Ocelot.UnitTests.Cluster
{
    public class ArgumentParserTests
    {
        private ArgumentParser _parser;
        private string[] _args;
        private Arguments _result;

        public ArgumentParserTests()
        {
        }

        [Fact]
        public void should_return_url()
        {
            var args = new string[]{"http://127.0.0.1:5000"};
            var expected = new Arguments(new Uri(args[0]));

            this.Given(x => GivenTheFollowingArgs(args))
                .When(x => WhenIParse())
                .Then(x => ThenTheFollowingIsReturned(expected))
                .BDDfy();
        }

        private void GivenTheFollowingArgs(string[] args)
        {
            _args = args;
            _parser = new ArgumentParser(_args);
        }

        private void WhenIParse()
        {
            _result = _parser.Parse();
        }

        private void ThenTheFollowingIsReturned(Arguments expected)
        {
            _result.Uri.ShouldBe(expected.Uri);
        }
    }
}