using System.Threading.Tasks;
using RaaLabs.Edge.Modules.EventHandling;
using RaaLabs.Edge.Modules.EdgeHub;
using RaaLabs.Edge.Modules.Configuration;
using RaaLabs.Edge.Modules.Logging;
using RaaLabs.Edge.Modules;

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
                .WithModule<Logging>()
                .WithHandler<IdentityMapperHandler>()
                .WithType<TimeSeriesMapper>()
                .Build();

            await application.Run();
        }
    }
}
