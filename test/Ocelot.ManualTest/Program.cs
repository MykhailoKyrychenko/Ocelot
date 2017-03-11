using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Ocelot.Cluster;

namespace Ocelot.ManualTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = new WebHostBuilder();
            
            var arguments = ArgumentParser.Parse(args);

            if(!arguments.IsError)
            {
                builder.ConfigureServices(s => {
                    s.AddSingleton<Arguments>(arguments.Data);
                });
            }

            builder.ConfigureServices(s => {
                s.AddSingleton(builder);
                s.AddSingleton(arguments);
            });

            builder.UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<Startup>();

            var host = builder.Build();

            host.Run();
        }
    }
}
