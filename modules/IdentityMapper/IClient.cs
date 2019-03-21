/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Threading.Tasks;
using Microsoft.Azure.Devices.Client;

namespace Dolittle.Edge.IdentityMapper
{
    /// <summary>
    /// Defines a client for the module
    /// </summary>
    public interface IClient
    {
        /// <summary>
        /// Registers a new delegate for the particular input. If a delegate is already associated
        /// with the input, it will be replaced with the new delegate.
        /// </summary>
        /// <param name="input">The <see cref="Input"/> to register for</param>
        /// <param name="messageHandler"><see cref="MessageHandler"/> to register</param>
        /// <param name="userContext">Any context associated with the handler</param>
        /// <returns></returns>
        Task SetInputMessageHandler(Input input, MessageHandler messageHandler, object userContext);

        /// <summary>
        /// Send an event to a specific <see cref="Output"/>
        /// </summary>
        /// <param name="output"><see cref="Output"/> to send to</param>
        /// <param name="message"><see cref="Message"/> to send</param>
        /// <returns>Awaitable <see cref="Task"/></returns>
        Task SendEvent(Output output, Message message);
    }
}