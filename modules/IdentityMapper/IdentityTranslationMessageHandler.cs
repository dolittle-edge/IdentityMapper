/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Text;
using System.Threading.Tasks;
using Dolittle.Edge.Modules;
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

        /// <summary>
        /// Initializes a new instance of <see cref="IdentityTranslationMessageHandler"/>
        /// </summary>
        /// <param name="client"><see cref="IClient"/> to use for communication</param>
        /// <param name="mapper"><see cref="ITimeSeriesMapper"/> for mapping</param>
        /// <param name="serializer"><see cref="ISerializer">JSON serializer</see></param>
        public IdentityTranslationMessageHandler(IClient client, ITimeSeriesMapper mapper, ISerializer serializer)
        {
            _client = client;
            _mapper = mapper;
            _serializer = serializer;
        }

        /// <inheritdoc/>
        public Input Input => "Messages";

        /// <inheritdoc/>
        public async Task<MessageResponse> Handle(Message inputMessage)
        {
            var inputMessageBytes = inputMessage.GetBytes();
            var inputMessageString = Encoding.UTF8.GetString(inputMessageBytes);
            var inputDataPoint = _serializer.FromJson<InputDataPoint>(inputMessageString);
            var outputDatapoint = new OutputDataPoint
            {
                
                Value = inputDataPoint.Value,
                Timestamp = inputDataPoint.Timestamp
            };

            var outputMessageString = _serializer.ToJson(outputDatapoint);
            var outputMessageBytes = Encoding.UTF8.GetBytes(outputMessageString);
            var outputMessage = new Message(outputMessageBytes);
            await _client.SendEvent(Output, outputMessage);

            return MessageResponse.Completed;
        }
    }
}