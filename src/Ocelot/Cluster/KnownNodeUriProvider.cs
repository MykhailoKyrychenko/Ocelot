using System;

namespace Ocelot.Cluster
{
    public class KnownNodeUriProvider : IKnownNodeUriProvider
    {
        private readonly IKnownNodeUriRepository _repo;

        public KnownNodeUriProvider(IKnownNodeUriRepository repo)
        {
            _repo = repo;
        }

        public Uri Get()
        {
            return _repo.Get();
        }
    }
}