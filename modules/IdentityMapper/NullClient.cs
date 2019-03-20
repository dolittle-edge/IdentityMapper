/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Threading.Tasks;
using Microsoft.Azure.Devices.Client;

namespace IdentityMapper
{
    /// <summary>
    /// Represents a null implementation of <see cref="IClient"/>
    /// </summary>
    public class NullClient : IClient
    {
        /// <inheritdoc/>
        public Task SendEvent(Output output, Message message)
        {
            return Task.CompletedTask;
        }

        /// <inheritdoc/>
        public Task SetInputMessageHandler(Input input, MessageHandler messageHandler, object userContext)
        {
            return Task.CompletedTask;
        }
    }
}