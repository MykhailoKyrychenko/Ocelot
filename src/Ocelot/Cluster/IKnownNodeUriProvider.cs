using System;

namespace Ocelot.Cluster
{
    public interface IKnownNodeUriProvider
    {
        Uri Get();
    }
}