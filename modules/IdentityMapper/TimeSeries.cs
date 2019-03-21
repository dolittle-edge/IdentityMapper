/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Dolittle.Concepts;

namespace Dolittle.Edge.IdentityMapper
{
    /// <summary>
    /// Represents the concept of an System
    /// </summary>
    public class TimeSeries : ConceptAs<Guid>
    {
        /// <summary>
        /// Implicitly convert from <see cref="Guid"/> to <see cref="TimeSeries"/>
        /// </summary>
        /// <param name="value">TimeSeries as <see cref="Guid"/></param>
        public static implicit operator TimeSeries(Guid value)
        {
            return new TimeSeries {Value = value};
        }
    }
}
