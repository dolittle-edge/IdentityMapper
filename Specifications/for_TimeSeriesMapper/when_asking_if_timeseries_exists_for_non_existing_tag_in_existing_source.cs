/*---------------------------------------------------------------------------------------------
 *  Copyright (c) RaaLabs. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using RaaLabs.TimeSeries.Modules;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace RaaLabs.TimeSeries.IdentityMapper.for_TimeSeriesMapper
{
    public class when_asking_if_timeseries_exists_for_non_existing_tag_in_existing_source
    {
        const string source = "MySource";
        const string tag = "MyTag";

        static bool result;

        static TimeSeriesMapper mapper;
        Establish context = () =>
        {
            mapper = new TimeSeriesMapper(new TimeSeriesMap(
                new Dictionary<Source, TimeSeriesByTag>
                {
                    { source, new TimeSeriesByTag(new Dictionary<Tag, RaaLabs.TimeSeries.TimeSeries>()) }
                }
            ));
        };


        Because of = () => result = mapper.HasTimeSeriesFor(source,tag);

        It should_consider_not_having_it = () => result.ShouldBeFalse();
    }
}