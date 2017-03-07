using System;

namespace Ocelot.Cluster
{
    public class InMemoryClusterMemberUriRepository : IClusterMemberUriRepository
    {
        private Uri _uri;
        
        public Uri Get()
        {
            return _uri;
        }

        public void Set(Uri uri)
        {
            _uri = uri;
        }
    }
}