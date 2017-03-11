using System;

namespace Ocelot.Cluster
{
    public class Arguments
    {
        public Arguments(Uri uri)
        {
            KnownNodeUri = uri;
        }

        public Uri KnownNodeUri { get; private set; }
    }
}