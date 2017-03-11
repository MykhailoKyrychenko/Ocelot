namespace Ocelot.Cluster
{
    public class KnownNodeUriSetter : IKnownNodeUriSetter
    {
        private IKnownNodeUriRepository _repo;
        private Arguments _args;
        
        public KnownNodeUriSetter(IKnownNodeUriRepository repo, Arguments args)
        {
            _repo = repo;
            _args = args;
        }
        public void Set()
        {
            _repo.Set(_args.KnownNodeUri);
        }
    }
}