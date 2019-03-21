/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Dolittle.Edge.IdentityMapper.for_TimeSeriesMapper
{
    public class when_asking_if_timeseries_exists_for_non_existing_tag_in_existing_system : given.an_empty_map
    {
        const string system = "MySystem";
        const string tag = "MyTag";

        static bool result;

        Establish context = () => actual_map[system] = new Dictionary<Tag, TimeSeries>();

        Because of = () => result = mapper.HasTimeSeriesFor(system,tag);

        It should_consider_not_having_it = () => result.ShouldBeFalse();
    }
}