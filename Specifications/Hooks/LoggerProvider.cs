using TechTalk.SpecFlow;
using Serilog;
using BoDi;

namespace RaaLabs.Edge.IdentityMapper.Specs.Hooks
{
    [Binding]
    public sealed class LoggerProvider
    {
        // For additional details on SpecFlow hooks see http://go.specflow.org/doc-hooks

        private readonly IObjectContainer _container;

        public LoggerProvider(IObjectContainer container)
        {
            _container = container;
        }

        [BeforeScenario]
        public void SetupLogger()
        {
            var logger = new LoggerConfiguration().CreateLogger();
            _container.RegisterInstanceAs<ILogger>(logger);
        }
    }
}
