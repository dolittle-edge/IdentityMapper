using RaaLabs.Edge.Modules.Configuration;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace RaaLabs.IdentityMapper
{
    [Name("identities.json")]
    class Identities : ReadOnlyDictionary<string, TimeSeriesByTag>, IConfiguration
    {
        public Identities(IDictionary<string, TimeSeriesByTag> configuration) : base(configuration) { }
    }

    class TimeSeriesByTag : ReadOnlyDictionary<string, string>
    {
        public TimeSeriesByTag(IDictionary<string, string> configuration) : base(configuration) { }
    }
}
