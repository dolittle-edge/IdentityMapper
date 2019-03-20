/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Microsoft.Azure.Devices.Client;

namespace IdentityMapper
{

    /// <summary>
    /// Defines a system that can handle messages
    /// </summary>
    public interface ICanHandleMessages
    {
        /// <summary>
        /// Get the input the messages is expected on
        /// </summary>
        Input Input {  get; }

        /// <summary>
        /// Handle a message coming in from the <see cref="Input"/>
        /// </summary>
        /// <param name="message"><see cref="Message"/> received</param>
        void Handle(Message message);
    }
}