/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;
using Dolittle.TimeSeries.Modules;

namespace Dolittle.TimeSeries.IdentityMapper.for_TimeSeriesMapper
{
    public class when_getting_timeseries_for_non_existing_source
    {
        const string source = "MySource";
        const string tag = "MyTag";

        static Exception result;
        static TimeSeriesMapper mapper;

        Establish context = () => mapper = new TimeSeriesMapper(new TimeSeriesMap(new Dictionary<Source, TimeSeriesByTag>()));


        Because of = () => result = Catch.Exception(() => mapper.GetTimeSeriesFor(source,tag));

        It should_throw_missing_source = () => result.ShouldBeOfExactType<MissingSource>();
    }
}