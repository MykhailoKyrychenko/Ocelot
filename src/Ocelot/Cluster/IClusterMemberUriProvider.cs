using System;

namespace Ocelot.Cluster
{
    public interface IClusterMemberUriProvider
    {
        Uri Get();
    }
}