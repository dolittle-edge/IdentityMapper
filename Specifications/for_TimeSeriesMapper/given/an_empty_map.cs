/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using Machine.Specifications;
using Moq;

namespace IdentityMapper.for_TimeSeriesMapper.given
{
    public class an_empty_map
    {
        protected static TimeSeriesMapper mapper;
        protected static Dictionary<System, IDictionary<Tag,TimeSeries>> actual_map;

        Establish context = () =>
        {
            var map = new Mock<ITimeSeriesMap>();
            actual_map = new Dictionary<System, IDictionary<Tag,TimeSeries>>();
            map.Setup(_ => _.Get()).Returns(actual_map);

            mapper = new TimeSeriesMapper(map.Object);
        };
    }
}