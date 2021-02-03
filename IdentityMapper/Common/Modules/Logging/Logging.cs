using Autofac;
using Serilog;

namespace RaaLabs.IdentityMapper.Common.Modules.Logging
{
    class Logging : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(_ => CreateLogger()).As<ILogger>();
        }

        private Serilog.Core.Logger CreateLogger()
        {
            var log = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();

            return log;
        }
    }
}
