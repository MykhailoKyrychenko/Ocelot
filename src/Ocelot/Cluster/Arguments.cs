using System;

namespace Ocelot.Cluster
{
    public class Arguments
    {
        public Arguments(Uri uri)
        {
            Uri = uri;
        }

        public Uri Uri { get; private set; }
    }
}