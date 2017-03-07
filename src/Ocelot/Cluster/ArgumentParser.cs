using System;

namespace Ocelot.Cluster
{
    public class ArgumentParser : IArgumentParser
    {
        private string[] _args;

        public ArgumentParser(string[] args)
        {
            _args = args;
        }
        public Arguments Parse()
        {
            return new Arguments(new Uri(_args[0]));
        }
    }
}