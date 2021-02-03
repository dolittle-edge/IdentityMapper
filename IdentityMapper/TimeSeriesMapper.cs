using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaaLabs.IdentityMapper
{
    class TimeSeriesMapper
    {
        private readonly Identities _identities;

        public TimeSeriesMapper(Identities identities)
        {
            _identities = identities;
        }

        public string GetTimeSeriesFor(string source, string tag)
        {
            ThrowIfMissingSystem(source);
            ThrowIfTagIsMissingInSystem(source, tag);
            return _identities[source][tag];
        }

        void ThrowIfMissingSystem(string source)
        {
            if (!_identities.ContainsKey(source)) throw new Exception($"Missing source: {source}");
        }

        void ThrowIfTagIsMissingInSystem(string source, string tag)
        {
            if (!_identities[source].ContainsKey(tag)) throw new Exception($"Missing tag '{tag}' in '{source}'");
        }
    }
}
