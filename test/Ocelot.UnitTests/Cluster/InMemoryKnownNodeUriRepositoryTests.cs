using System;
using Ocelot.Cluster;
using Shouldly;
using TestStack.BDDfy;
using Xunit;

namespace Ocelot.UnitTests.Cluster
{
    public class InMemoryKnownNodeUriRepositoryTests
    {
        private Uri _uri;
        private InMemoryKnownNodeUriRepository _repo;
        private Uri _result;

        public InMemoryKnownNodeUriRepositoryTests()
        {
            _repo = new InMemoryKnownNodeUriRepository();
        }
        
        [Fact]
        public void should_set_member_uri()
        {
            Uri uri = new Uri("http://www.bbc.co.uk");

            this.Given(x => GivenTheFollowingUri(uri))
                .When(x => WhenISet())
                .Then(x => ThenTheUriIsSet())
                .BDDfy();
        }

        [Fact]
        public void should_get_member_uri()
        {
            var uri = new Uri("http://www.bbc.co.uk");

            this.Given(x => GivenAUriIsAlreadySet(uri))
                .When(x => WhenIGet())
                .Then( x=> x.ThenTheFollowingIsReturned(uri))
                .BDDfy();
        }

        private void GivenAUriIsAlreadySet(Uri uri)
        {
            _uri = uri;
            WhenISet();
        }

        private void WhenIGet()
        {
            _result = _repo.Get();
        }

        private void ThenTheFollowingIsReturned(Uri expected)
        {
            _result.ShouldBe(expected);
        }

        private void GivenTheFollowingUri(Uri uri)
        {
            _uri = uri;
        }

        private void WhenISet()
        {
            _repo.Set(_uri);
        }

        private void ThenTheUriIsSet()
        {
            var result = _repo.Get();
            result.ShouldNotBeNull();
        }
    }
}