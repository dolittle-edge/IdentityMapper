/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using IdentityMapper;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace IdentityMapper.for_TimeSeriesMapper
{
    public class when_asking_if_timeseries_exists_for_existing_tag_in_existing_system : given.an_empty_map
    {
        const string system = "MySystem";
        const string other_system = "MyOtherSystem";
        const string tag = "MyTag";
        const string other_tag = "MyOtherTag";
        static Guid time_series = Guid.NewGuid();
        static Guid other_time_series = Guid.NewGuid();

        static bool result;

        Establish context = () => 
        {
            actual_map[system] = new Dictionary<Tag, TimeSeries> { { tag, time_series } };
            actual_map[other_system] = new Dictionary<Tag, TimeSeries> { { other_tag, other_time_series } };
        };

        Because of = () => result = mapper.HasTimeSeriesFor(system,tag);

        It should_consider_having_it = () => result.ShouldBeTrue();
    }
}