using System;

namespace Ocelot.Cluster
{
    public class ClusterMemberUriSetter : IClusterMemberUriSetter
    {
        private IArgumentParser _parser;
        private IClusterMemberUriRepository _repo;
        
        public ClusterMemberUriSetter(IArgumentParser parser, IClusterMemberUriRepository repo)
        {
            _parser = parser;
            _repo = repo;
        }
        public void Set()
        {
            var args = _parser.Parse();
            _repo.Set(args.Uri);
        }
    }

    public interface IClusterMemberUriSetter
    {
        void Set();
    }
}