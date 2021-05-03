// Copyright (c) RaaLabs. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Diagnostics.CodeAnalysis;
using RaaLabs.Edge.Modules.EdgeHub;

namespace RaaLabs.Edge.IdentityMapper.Events
{
    [ExcludeFromCodeCoverage]
    [InputName("events")]
    public class EdgeHubDataPointReceived : IEdgeHubIncomingEvent
    {
        public string Source { get; set; }

        public string Tag { get; set; }

        public dynamic Value { get; set; }

        public long Timestamp { get; set; }
    }
}
