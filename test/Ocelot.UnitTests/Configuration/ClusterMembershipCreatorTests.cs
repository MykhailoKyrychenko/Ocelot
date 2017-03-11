using System;
using Moq;
using Ocelot.Cluster;
using Ocelot.Configuration.Creator;
using TestStack.BDDfy;
using Xunit;

namespace Ocelot.UnitTests.Configuration
{
    public class ClusterMembershipCreatorTests
    {
        private ClusterMembershipCreator _creator;
        private Mock<IKnownNodeUriSetter> _setter;
        private Mock<IAvailableOcelotNodesGetter> _getter;

        public ClusterMembershipCreatorTests()
        {
            _setter = new Mock<IKnownNodeUriSetter>();
            //_creator = new ClusterMembershipCreator(_setter.Object);
        }

        [Fact]
        public void should_call_cluster_member_uri_setter()
        {
            this.Given(x => WhenICallTheSetter())
                .Then(x => ThenTheSetterIsCalledCorrectly())
                .BDDfy();
        }

        [Fact]
        public void should_call_get_available_ocelots()
        {
            this.Given(x => WhenICallTheSetter())
                .Then(x => ThenTheGetterIsCalledCorrectly())
                .BDDfy();
        }

        private void ThenTheGetterIsCalledCorrectly()
        {
            _getter
                .Verify(x => x.Get(), Times.Once);
        }

        private void WhenICallTheSetter()
        {
            _creator.Create();
        }

        private void ThenTheSetterIsCalledCorrectly()
        {
            _setter
                .Verify(x => x.Set(), Times.Once);
        }
    }
}