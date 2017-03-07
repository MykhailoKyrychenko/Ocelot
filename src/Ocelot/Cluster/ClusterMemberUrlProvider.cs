using System;

namespace Ocelot.Cluster
{
    public class ClusterMemberUriProvider : IClusterMemberUriProvider
    {
        private readonly IClusterMemberUriRepository _repo;

        public ClusterMemberUriProvider(IClusterMemberUriRepository repo)
        {
            _repo = repo;
        }

        public Uri Get()
        {
            return _repo.Get();
        }
    }
}