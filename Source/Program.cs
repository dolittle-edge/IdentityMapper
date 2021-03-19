using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;
using RaaLabs.Edge.Modules.EventHandling;
using RaaLabs.Edge.Modules.EdgeHub;
using RaaLabs.Edge.Modules.Configuration;
using RaaLabs.Edge.Modules;
using System.Collections.Generic;
using System.Linq;
using RaaLabs.Edge;

namespace RaaLabs.IdentityMapper
{
    [ExcludeFromCodeCoverage]
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

            await application.Run();
        }
    }
}
