/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System;

namespace IdentityMapper
{
    /// <summary>
    /// Represents an input message
    /// </summary>
    public class OutputDataPoint
    {
        /// <summary>
        /// Gets or sets the TimeSeries this value belong to
        /// </summary>
        public Guid TimeSeries { get; set; }

        /// <summary>
        /// Gets or sets the value
        /// </summary>
        public double Value { get; set; }

        /// <summary>
        /// Gets or sets the timestamp in the form of EPOCH milliseconds granularity
        /// </summary>
        public long Timestamp { get; set; }
    }
}