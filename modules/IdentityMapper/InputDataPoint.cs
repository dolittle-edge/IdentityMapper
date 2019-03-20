/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

namespace IdentityMapper
{
    /// <summary>
    /// Represents an input message
    /// </summary>
    public class InputDataPoint
    {
        /// <summary>
        /// Gets or sets the originating system 
        /// </summary>
        public string System { get; set; }

        /// <summary>
        /// Gets or sets the tag name
        /// </summary>
        public string Tag { get; set; }

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