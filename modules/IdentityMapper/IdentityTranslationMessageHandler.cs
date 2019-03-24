/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Text;
using System.Threading.Tasks;
using Dolittle.Edge.Modules;
using Dolittle.Logging;
using Dolittle.Serialization.Json;
using Microsoft.Azure.Devices.Client;

namespace Dolittle.Edge.IdentityMapper
{
    /// <summary>
    /// Represents a <see cref="ICanHandleMessages">message handler</see> that can translate identities
    /// </summary>
    public class IdentityTranslationMessageHandler : ICanHandleMessages
    {
        /// <summary>
        /// Gets the output name
        /// </summary>
        public const string Output = "Translated";

        readonly ISerializer _serializer;
        readonly IClient _client;
        private readonly ITimeSeriesMapper _mapper;
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of <see cref="IdentityTranslationMessageHandler"/>
        /// </summary>
        /// <param name="client"><see cref="IClient"/> to use for communication</param>
        /// <param name="mapper"><see cref="ITimeSeriesMapper"/> for mapping</param>
        /// <param name="serializer"><see cref="ISerializer">JSON serializer</see></param>
        /// <param name="logger"><see cref="ILogger"/> for logging</param>
        public IdentityTranslationMessageHandler(
            IClient client,
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
        public async Task<MessageResponse> Handle(Message inputMessage)
        {
            _logger.Information($"Handle incoming message");
            var inputMessageBytes = inputMessage.GetBytes();
            var inputMessageString = Encoding.UTF8.GetString(inputMessageBytes);
            _logger.Information($"Event received '{inputMessageString}'");
            var inputDataPoint = _serializer.FromJson<InputDataPoint>(inputMessageString);

            if (!_mapper.HasTimeSeriesFor(inputDataPoint.System, inputDataPoint.Tag))
            {
                _logger.Warning($"There is no time series for tag '{inputDataPoint.Tag}' on system '{inputDataPoint.System}'");
                return MessageResponse.Completed;
            }

            var outputDatapoint = new OutputDataPoint
            {
                Value = inputDataPoint.Value,
                DeviceId = _mapper.GetTimeSeriesFor(inputDataPoint.System, inputDataPoint.Tag),
                Timestamp = inputDataPoint.Timestamp
            };

            var outputMessageString = _serializer.ToJson(outputDatapoint, SerializationOptions.CamelCase);
            var outputMessageBytes = Encoding.UTF8.GetBytes(outputMessageString);
            var outputMessage = new Message(outputMessageBytes);

            _logger.Information($"Translated event to '{outputMessageString}'");
            await _client.SendEvent(Output, outputMessage);

            return MessageResponse.Completed;
        }
    }
}