/*---------------------------------------------------------------------------------------------
 *  Copyright (c) RaaLabs. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using TechTalk.SpecFlow;
using System;
using FluentAssertions;
using System.Collections.Generic;

namespace RaaLabs.Edge.IdentityMapper.Specs.StepDefinitions
{
    [Binding]
    public class TimeSeriesMapperSteps
    {
        private readonly TimeSeriesMapper _mapper;
        private string _mappedValue;
        private Dictionary<(string, string), string> _existingValues;
        private HashSet<(string, string)> _nonExistingValues;
        private Exception _exception;
        public TimeSeriesMapperSteps(TimeSeriesMapper mapper)
        {
            _mapper = mapper;
            _existingValues = new Dictionary<(string, string), string>();
            _nonExistingValues = new HashSet<(string, string)>();
        }

        [When(@"source (.*) and tag (.*) is requested")]
        public void WhenTagIsRequested(string source, string tag)
        {
            try
            {
                _mappedValue = _mapper.GetTimeSeriesFor(source, tag);
                var mappedValue = _mapper.GetTimeSeriesFor(source, tag);
                _existingValues[(source, tag)] = mappedValue;
            }
            catch (Exception ex)
            {
                _nonExistingValues.Add((source, tag));
                _exception = ex;
            }
        }

        [When(@"the following tags are requested")]
        public void TheFollowingTagsAreRequested(Table table)
        {
            foreach (var row in table.Rows)
            {
                var source = row["Source"];
                var tag = row["Tag"];
                try
                {
                    var mappedValue = _mapper.GetTimeSeriesFor(source, tag);
                    _existingValues[(source, tag)] = mappedValue;

                }
                catch (Exception)
                {
                    _nonExistingValues.Add((source, tag));
                }
            }

        }

        [Then(@"the mapped value should be ""(.*)""")]
        public void ThenTheMappedValueShouldBe(string timeseries)
        {
            _mappedValue.Should().Be(timeseries);
        }

        [Then(@"the following tags will be mapped")]
        public void ThenTheFollowingTagsWillBeMapped(Table identities)
        {
            foreach (var row in identities.Rows)
            {
                var source = row["Source"];
                var tag = row["Tag"];
                var timeseries = row["TimeSeries"];
                _existingValues[(source, tag)].Should().Be(timeseries);
            }
        }
        [Then(@"the following tags will not be mapped")]
        public void ThenTheFollowingTagsWillNotBeMapped(Table identities)
        {
            foreach (var row in identities.Rows)
            {
                var source = row["Source"];
                var tag = row["Tag"];
                _nonExistingValues.Should().Contain((source, tag));
            }
        }
        [Then(@"the exception ""(.*)"" is thrown with message ""(.*)""")]
        public void ThenTheExceptionMissingTagInSourceWillWillBeThrown(string type, string message)
        {
            _exception.Message.Should().Be(message);
            _exception.GetType().Name.Should().Be(type);
        }

    }
}
