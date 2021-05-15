// Copyright (c) RaaLabs. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Newtonsoft.Json;
using RaaLabs.Edge.Modules.EventHandling;
using RaaLabs.Edge.IdentityMapper.Events;
using Serilog;
using System;

namespace RaaLabs.Edge.IdentityMapper
{
    public class IdentityMapperHandler : IConsumeEvent<EdgeHubDataPointReceived>, IProduceEvent<EdgeHubDataPointRemapped>
    {
        private readonly TimeSeriesMapper _mapper;
        private readonly ILogger _logger;

        public event EventEmitter<EdgeHubDataPointRemapped> SendEvent;

        public IdentityMapperHandler(TimeSeriesMapper mapper, ILogger logger)
        {
            _mapper = mapper;
            _logger = logger;
        }

        public void Handle(EdgeHubDataPointReceived @event)
        {
            try
            {
                _logger.Information("Received event: {EdgeHubEvent}", JsonConvert.SerializeObject(@event));
                var remapped = RemapDataPoint(@event);
                _logger.Information("Remapped: {EdgeHubEvent}", JsonConvert.SerializeObject(remapped));

                SendEvent(remapped);
            }
            catch (Exception e)
            {
                _logger.Error("Exception: {Exception}", JsonConvert.SerializeObject(e));
            }
        }

        private EdgeHubDataPointRemapped RemapDataPoint(EdgeHubDataPointReceived incoming)
        {
            var timeseries = _mapper.GetTimeSeriesFor(incoming.Source, incoming.Tag);

            return new EdgeHubDataPointRemapped
            {
                TimeSeries = timeseries,
                Value = incoming.Value,
                Timestamp = incoming.Timestamp,
            };
        }
    }
}
