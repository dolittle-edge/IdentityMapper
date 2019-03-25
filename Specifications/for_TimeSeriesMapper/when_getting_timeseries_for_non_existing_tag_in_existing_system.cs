/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using Dolittle.Edge.Modules;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Dolittle.Edge.IdentityMapper.for_TimeSeriesMapper
{
    public class when_getting_timeseries_for_non_existing_tag_in_existing_system
    {
        const string system = "MySystem";
        const string tag = "MyTag";

        static Exception result;

        static TimeSeriesMapper mapper;
        Establish context = () =>
        {
            mapper = new TimeSeriesMapper(new TimeSeriesMap(
                new Dictionary<System, TimeSeriesByTag>
                {
                    { system, new TimeSeriesByTag(new Dictionary<Tag, TimeSeries>()) }
                }
            ));
        };

        Because of = () => result = Catch.Exception(() => mapper.GetTimeSeriesFor(system,tag));

        It should_throw_missing_tag_in_system = () => result.ShouldBeOfExactType<MissingTagInSystem>();
    }
}