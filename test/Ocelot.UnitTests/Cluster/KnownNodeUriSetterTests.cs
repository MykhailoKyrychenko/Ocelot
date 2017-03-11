using System;
using Moq;
using Ocelot.Cluster;
using TestStack.BDDfy;
using Xunit;

namespace Ocelot.UnitTests.Cluster
{
    public class KnownNodeUriSetterTests
    {
        private Arguments _args;
        private Mock<IKnownNodeUriRepository> _repo;
        private KnownNodeUriSetter _setter;

        public KnownNodeUriSetterTests()
        {
            _repo = new Mock<IKnownNodeUriRepository>();
        }

        [Fact]
        public void should_set_cluster_member()
        {
            var arguments = new Arguments(new Uri("http://localhost:5000"));
            
            this.Given(x => GivenTheArgumentParserReturns(arguments))
                .When(x => WhenISetTheClusterMember())
                .Then(x => ThenTheClusterMemberRepoIsCalledCorrectly())
                .BDDfy();
        }

        private void GivenTheArgumentParserReturns(Arguments args)
        {
            _args = args;
        }

        private void WhenISetTheClusterMember()
        {
            _setter = new KnownNodeUriSetter(_repo.Object, _args);
            _setter.Set();
        }

        private void ThenTheClusterMemberRepoIsCalledCorrectly()
        {
            _repo
                .Verify(x => x.Set(_args.KnownNodeUri), Times.Once);
        }
    }
}