using Autofac;
using RaaLabs.Edge;
using System.Threading.Tasks;


namespace RaaLabs.IdentityMapper.Specs.Drivers
{
    public class ScopeExposer : IRunAsync
    {
        public ScopeHolder ScopeHolder { get; set; }

        public ScopeExposer(ILifetimeScope scope, ScopeHolder scopeHolder)
        {
            ScopeHolder = scopeHolder;
            ScopeHolder.Scope = scope;
        }

        public async Task Run()
        {
            await Task.CompletedTask;
        }

    }
    public class ScopeHolder
    {
        public ILifetimeScope Scope { get; set; }

    }
}