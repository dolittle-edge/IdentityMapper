// Copyright (c) RaaLabs. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using RaaLabs.Edge.Modules.EdgeHub;

namespace RaaLabs.Edge.IdentityMapper.Events
{
    /// <summary>
    /// The data point on the format it should be sent to the rest of the Raa Labs edge modules
    /// </summary>
    [OutputName("Translated")]
    public class EdgeHubDataPointRemapped : IEdgeHubOutgoingEvent
    {
        /// <summary>
        /// The time series id
        /// </summary>
        public Guid TimeSeries { get; set; }

        /// <summary>
        /// The timestamp, represented as epoc time with milliseconds
        /// </summary>
        public long Timestamp { get; set; }

        /// <summary>
        /// The value of the data point
        /// </summary>
        public dynamic Value { get; set; }
    }
}
