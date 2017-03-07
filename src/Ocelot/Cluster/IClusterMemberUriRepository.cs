using System;

namespace Ocelot.Cluster
{
    public interface IClusterMemberUriRepository
    {
        Uri Get();
        void Set(Uri uri);
    }
}