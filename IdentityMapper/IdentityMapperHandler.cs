using Newtonsoft.Json;
using RaaLabs.Edge.Modules.EdgeHub;
using RaaLabs.Edge.Modules.EventHandling;
using RaaLabs.IdentityMapper.events;
using Serilog;
using System;

namespace RaaLabs.IdentityMapper
{
    class IdentityMapperHandler : IConsumeEvent<EdgeHubDataPointReceived>, IProduceEvent<EdgeHubDataPointRemapped>
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
            var tag = _mapper.GetTimeSeriesFor(incoming.Source, incoming.Tag);

            return new EdgeHubDataPointRemapped
            {
                Timestamp = incoming.Timestamp,
                Value = incoming.Value,
                Timeseries = tag,
            };
        }
    }
}
