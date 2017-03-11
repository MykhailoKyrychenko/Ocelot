using System;
using Ocelot.Cluster;
using Ocelot.Responses;
using Shouldly;
using TestStack.BDDfy;
using Xunit;

namespace Ocelot.UnitTests.Cluster
{
    public class ArgumentParserTests
    {
        private string[] _args;
        private Response<Arguments> _result;

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
        }

        private void WhenIParse()
        {
            _result = ArgumentParser.Parse(_args);
        }

        private void ThenTheFollowingIsReturned(Arguments expected)
        {
            _result.Data.KnownNodeUri.ShouldBe(expected.KnownNodeUri);
        }
    }
}