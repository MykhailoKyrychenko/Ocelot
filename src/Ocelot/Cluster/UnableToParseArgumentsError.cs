using Ocelot.Errors;

namespace Ocelot.Cluster
{
    public class UnableToParseArgumentsError : Error
    {
        public UnableToParseArgumentsError(string message) : base(message, OcelotErrorCode.UnableToParseArgumentsError)
        {
        }
    }
}