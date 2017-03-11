using Ocelot.Cluster;

namespace Ocelot.Configuration.Creator
{
    public class ClusterMembershipCreator
    {
        private IKnownNodeUriSetter _setter;
        private IAvailableOcelotNodesGetter _getter;

        public ClusterMembershipCreator(IKnownNodeUriSetter setter, IAvailableOcelotNodesGetter getter)
        {
            _getter = getter;
            _setter = setter;
        }

        public void Create()
        {
            //persist the known cluster member uri
            _setter.Set();
            //call it and get all the available nodes
            var knownNodes = _getter.Get();
            //persist them for raft
            //start election
            //be elected or elect someone else
            //take updates from leader
            //do raft - heartbeat, elections etc
        }
    }
}