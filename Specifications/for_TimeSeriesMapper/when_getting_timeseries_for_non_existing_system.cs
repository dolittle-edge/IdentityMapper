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
    public class when_getting_timeseries_for_non_existing_system : given.an_empty_map
    {
        const string system = "MySystem";
        const string tag = "MyTag";

        static Exception result;

        Because of = () => result = Catch.Exception(() => mapper.GetTimeSeriesFor(system,tag));

        It should_throw_missing_system = () => result.ShouldBeOfExactType<MissingSystem>();
    }
}