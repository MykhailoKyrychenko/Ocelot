using System;
using Moq;
using Ocelot.Cluster;
using TestStack.BDDfy;
using Xunit;

namespace Ocelot.UnitTests.Cluster
{
    public class SetClusterMemberTests
    {
        private Arguments _args;
        private Mock<IArgumentParser> _parser;
        private Mock<IClusterMemberUriRepository> _repo;
        private ClusterMemberSetter _setter;

        public SetClusterMemberTests()
        {
            _parser = new Mock<IArgumentParser>();
            _repo = new Mock<IClusterMemberUriRepository>();
            _setter = new ClusterMemberSetter(_parser.Object, _repo.Object);
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
            _parser
                .Setup(x => x.Parse())
                .Returns(_args);
        }

        private void WhenISetTheClusterMember()
        {
            _setter.Set();
        }

        private void ThenTheClusterMemberRepoIsCalledCorrectly()
        {
            _repo
                .Verify(x => x.Set(_args.Uri), Times.Once);
        }
    }
}