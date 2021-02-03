using Autofac;
using Serilog;
using System.Threading.Tasks;

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
            ContainerBuilder builder = new ContainerBuilder();

            builder.RegisterModule<Common.Modules.EventHandling.EventHandling>();
            builder.RegisterModule<Common.Modules.EdgeHub.EdgeHub>();
            builder.RegisterModule<Common.Modules.Configuration.Configuration>();
            builder.RegisterModule<Common.Modules.Logging.Logging>();

            builder.RegisterType<IdentityMapperHandler>();
            builder.RegisterType<TimeSeriesMapper>();

            var container = builder.Build();


            using var scope = container.BeginLifetimeScope();

            var logger = scope.Resolve<ILogger>();
            logger.Information("Context built, starting application.");

            var handler = scope.Resolve<IdentityMapperHandler>();

            while (true)
            {
                await Task.Delay(1000);
            }
        }
    }
}
