/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

namespace IdentityMapper
{
    /// <summary>
    /// Represents an implementation of <see cref="ITagAndSystemToTimeSeries"/>
    /// </summary>
    public class TagAndSystemToTimeSeries : ITagAndSystemToTimeSeries
    {
        /// <inheritdoc/>
        public TimeSeries GetTimeSeriesFor(System system, Tag tag)
        {
            throw new global::System.NotImplementedException();
        }

        /// <inheritdoc/>
        public bool HasTimeSeriesFor(System system, Tag tag)
        {
            throw new global::System.NotImplementedException();
        }
    }
}