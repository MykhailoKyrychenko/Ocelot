using System;
using Moq;
using Ocelot.Cluster;
using Shouldly;
using TestStack.BDDfy;
using Xunit;

namespace Ocelot.UnitTests.Cluster
{
    public class KnownNodeUriProviderTests
    {
        private Uri _uri;
        private Mock<IKnownNodeUriRepository> _repo;
        private KnownNodeUriProvider _provider;
        private Uri _result;

        public KnownNodeUriProviderTests()
        {
            _repo = new Mock<IKnownNodeUriRepository>();
            _provider = new KnownNodeUriProvider(_repo.Object);
        }

        [Fact]
        public void should_return_cluster_member_uri()
        {
            var uri = new Uri("http://www.bbc.co.uk/");

            this.Given(x => GivenTheClusterMemberRepoReturns(uri))
                .When(x => WhenIGet())
                .Then(x => ThenTheFollowingIsReturned(uri))
                .BDDfy();
        }

        private void GivenTheClusterMemberRepoReturns(Uri uri)
        {
            _uri = uri;
            _repo
                .Setup(x => x.Get())
                .Returns(_uri);
        }    

        private void WhenIGet()
        {   
            _result = _provider.Get();
        }

        private void ThenTheFollowingIsReturned(Uri expected)
        {
            _result.ShouldBe(expected);
        }
    }
}