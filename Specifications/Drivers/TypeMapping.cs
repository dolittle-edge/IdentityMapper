using System;
using System.Collections.Generic;


namespace RaaLabs.Edge.IdentityMapper.Specs.Drivers
{
    public class TypeMapping : Dictionary<string, Type>
    {
        public TypeMapping()
        {
            Add("IdentityMapperHandler", typeof(IdentityMapperHandler));
            Add("EdgeHubDataPointReceived", typeof(events.EdgeHubDataPointReceived));
            Add("EdgeHubDataPointRemapped", typeof(events.EdgeHubDataPointRemapped));
        }
    }
}
