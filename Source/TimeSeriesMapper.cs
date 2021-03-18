using System;

namespace RaaLabs.IdentityMapper
{
    public class TimeSeriesMapper
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
            if (!_identities.ContainsKey(source)) throw new MissingSource(source);
        }

        void ThrowIfTagIsMissingInSystem(string source, string tag)
        {
            if (!_identities[source].ContainsKey(tag)) throw new MissingTagInSource(source, tag);
        }
    }
}
