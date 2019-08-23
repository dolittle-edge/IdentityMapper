/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Threading.Tasks;
using Dolittle.Logging;
using Dolittle.Serialization.Json;
using Dolittle.TimeSeries.Modules;

namespace Dolittle.TimeSeries.IdentityMapper
{
    /// <summary>
    /// Represents a <see cref="ICanHandle{T}">message handler</see> that can translate identities
    /// </summary>
    public class TagDataPointHandler : ICanHandle<TagDataPoint<object>>
    {
        /// <summary>
        /// Gets the output name
        /// </summary>
        public const string Output = "Translated";

        readonly ISerializer _serializer;
        readonly ICommunicationClient _client;
        readonly ITimeSeriesMapper _mapper;
        readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of <see cref="TagDataPointHandler"/>
        /// </summary>
        /// <param name="client"><see cref="ICommunicationClient"/> to use for communication</param>
        /// <param name="mapper"><see cref="ITimeSeriesMapper"/> for mapping</param>
        /// <param name="serializer"><see cref="ISerializer">JSON serializer</see></param>
        /// <param name="logger"><see cref="ILogger"/> for logging</param>
        public TagDataPointHandler(
            ICommunicationClient client,
            ITimeSeriesMapper mapper,
            ISerializer serializer,
            ILogger logger)
        {
            _client = client;
            _mapper = mapper;
            _serializer = serializer;
            _logger = logger;
            _logger.Information("Identity Translation Message Handler");
        }

        /// <inheritdoc/>
        public Input Input => "events";

        /// <inheritdoc/>
        public async Task Handle(TagDataPoint<object> tagDataPoint)
        {
            try
            {
                _logger.Information($"Handle data point from source '{tagDataPoint.Source}' with tag '{tagDataPoint.Tag}'");
                if (!_mapper.HasTimeSeriesFor(tagDataPoint.Source, tagDataPoint.Tag))
                {
                    _logger.Warning($"There is no time series for tag '{tagDataPoint.Tag}' on system '{tagDataPoint.Source}'");
                    await Task.CompletedTask;
                    return;
                }

                var outputDatapoint = new DataPoint<dynamic>
                {
                    Value = tagDataPoint.Value,
                    TimeSeries = _mapper.GetTimeSeriesFor(tagDataPoint.Source, tagDataPoint.Tag),
                    Timestamp = tagDataPoint.Timestamp
                };

                await _client.SendAsJson(Output, outputDatapoint);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Problems handling data point from source '{tagDataPoint.Source}' with tag '{tagDataPoint.Tag}' - with message '{ex.Message}' and stack trace '{ex.StackTrace}'");
            }

            await Task.CompletedTask;
        }
    }
}