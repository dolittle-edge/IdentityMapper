using System.Threading.Tasks;
using RaaLabs.Edge.Modules.EventHandling;
using RaaLabs.Edge.Modules.EdgeHub;
using RaaLabs.Edge.Modules.Configuration;
using RaaLabs.Edge.Modules;
using System.Collections.Generic;
using System.Linq;
using RaaLabs.Edge;

namespace RaaLabs.IdentityMapper
{
    class Program
    {
        static void Main(string[] args)
        {
            Run().Wait();
        }

        private static async Task Run()
        {
            var application = new ApplicationBuilder()
                .WithModule<EventHandling>()
                .WithModule<EdgeHub>()
                .WithModule<Configuration>()
                .WithHandler<IdentityMapperHandler>()
                .WithType<TimeSeriesMapper>()
                .Build();

            var builder = new ApplicationBuilder();

            await application.Run();
        }
    }
}
