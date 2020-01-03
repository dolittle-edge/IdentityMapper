/*---------------------------------------------------------------------------------------------
 *  Copyright (c) RaaLabs. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;
using RaaLabs.TimeSeries.Modules;

namespace RaaLabs.TimeSeries.IdentityMapper.for_TimeSeriesMapper
{
    public class when_asking_if_timeseries_exists_for_existing_tag_in_existing_source
    {
        const string source = "MySource";
        const string other_source = "MyOtherSource";
        const string tag = "MyTag";
        const string other_tag = "MyOtherTag";
        static Guid time_series = Guid.NewGuid();
        static Guid other_time_series = Guid.NewGuid();

        static bool result;

        static TimeSeriesMapper mapper;
        Establish context = () =>
        {
            mapper = new TimeSeriesMapper(new TimeSeriesMap(
                new Dictionary<Source, TimeSeriesByTag>
                {
                    { source, new TimeSeriesByTag(new Dictionary<Tag, RaaLabs.TimeSeries.Modules.TimeSeries> {{ tag, time_series }} )},
                    { other_source, new TimeSeriesByTag(new Dictionary<Tag, RaaLabs.TimeSeries.Modules.TimeSeries> {{ other_tag, other_time_series }} )}
                }
            ));
        };

        Because of = () => result = mapper.HasTimeSeriesFor(source, tag);

        It should_consider_having_it = () => result.ShouldBeTrue();
    }
}