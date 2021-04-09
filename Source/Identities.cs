using RaaLabs.Edge.Modules.Configuration;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace RaaLabs.Edge.IdentityMapper
{
    [Name("identities.json")]
    public class Identities : ReadOnlyDictionary<string, TimeSeriesByTag>, IConfiguration
    {
        public Identities(IDictionary<string, TimeSeriesByTag> configuration) : base(configuration) { }
    }

    public class TimeSeriesByTag : ReadOnlyDictionary<string, string>
    {
        public TimeSeriesByTag(IDictionary<string, string> configuration) : base(configuration) { }
    }
}
