using System;
using Ocelot.Responses;

namespace Ocelot.Cluster
{
    public static class ArgumentParser
    {
        public static Response<Arguments> Parse(string[] args)
        {
            try
            {
                var arguments = new Arguments(new Uri(args[0]));
                return new OkResponse<Arguments>(arguments);
            }
            catch(Exception ex)
            {
                var error = new UnableToParseArgumentsError(ex.Message);
                return new ErrorResponse<Arguments>(error);;
            }
        }
    }
}