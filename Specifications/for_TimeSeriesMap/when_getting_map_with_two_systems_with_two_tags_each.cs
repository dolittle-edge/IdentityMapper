/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using Dolittle.Concepts.Serialization.Json;
using Dolittle.IO;
using Dolittle.Serialization.Json;
using Dolittle.Types;
using Machine.Specifications;
using Moq;
using Newtonsoft.Json;
using It = Machine.Specifications.It;

namespace IdentityMapper.for_TimeSeriesMap
{
    public class when_getting_map_with_two_systems_with_two_tags_each
    {
        const string first_system = "FirstSystem";
        const string second_system = "SecondSystem";

        const string first_tag = "FirstTag";
        const string second_tag = "SecondTag";

        static Guid first_time_series = Guid.Parse("1b6d4fc3-87c3-4829-8a40-97753bbc69ec");
        static Guid second_time_series = Guid.Parse("4838a239-5a07-4b64-ad46-d7fa69ff9abe");
        static Guid third_time_series = Guid.Parse("696b5720-5e19-40c9-b262-148121b6725d");
        static Guid fourth_time_series = Guid.Parse("21aecb01-5c87-4a89-96d1-f01f23fdda60");

        static readonly string json =
            "{" +
            $"\"{first_system}\": {{"+
            $" \"{first_tag}\": \"{first_time_series}\", "+
            $" \"{second_tag}\": \"{second_time_series}\" "+
            "},"+
            $"\"{second_system}\": {{"+
            $" \"{first_tag}\": \"{third_time_series}\", "+
            $" \"{second_tag}\": \"{fourth_time_series}\" "+
            "}"+
            "}";

        static TimeSeriesMap time_series_map;

        static IDictionary<System, IDictionary<Tag,TimeSeries>> map;

        Establish context = () => 
        {
            var fileSystem = new Mock<IFileSystem>();
            fileSystem.Setup(_ => _.ReadAllText(Moq.It.IsAny<string>())).Returns(json);
            var converters = new JsonConverter[] { new ConceptConverter(), new ConceptDictionaryConverter() };
            var converterProviders = new Mock<IInstancesOf<ICanProvideConverters>>();
            var converter = new Mock<ICanProvideConverters>();
            converter.Setup(_ => _.Provide()).Returns(converters);
            converterProviders.Setup(_ => _.GetEnumerator()).Returns(new ICanProvideConverters[] {
                converter.Object
            }.ToList().GetEnumerator());


            var serializer = new Serializer(converterProviders.Object);
            time_series_map = new TimeSeriesMap(fileSystem.Object, serializer);
        };

        Because of = () => map = time_series_map.Get();

        It should_contain_first_tag_in_first_system_mapped_to_first_time_series = () => map[(System)first_system][(Tag)first_tag].ShouldEqual((TimeSeries)first_time_series);
        It should_contain_second_tag_in_first_system_mapped_to_second_time_series = () => map[(System)first_system][(Tag)second_tag].ShouldEqual((TimeSeries)second_time_series);
        It should_contain_first_tag_in_second_system_mapped_to_third_time_series = () => map[(System)second_system][(Tag)first_tag].ShouldEqual((TimeSeries)third_time_series);
        It should_contain_second_tag_in_second_system_mapped_to_fourth_time_series = () => map[(System)second_system][(Tag)second_tag].ShouldEqual((TimeSeries)fourth_time_series);
    }
}