using System;

namespace Ocelot.Cluster
{
    public interface IKnownNodeUriRepository
    {
        Uri Get();
        void Set(Uri uri);
    }
}