/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/


using Dolittle.Lifecycle;
using Dolittle.Types;

namespace IdentityMapper
{
    /// <summary>
    /// Represents an implementation of <see cref="IInputHandlers"/>
    /// </summary>
    [Singleton]
    public class InputHandlers : IInputHandlers
    {
        readonly IInstancesOf<ICanHandleMessages> _handlers;
        readonly IClient _client;

        /// <summary>
        /// Initializes a new instance of <see cref="InputHandlers"/>
        /// </summary>
        /// <param name="client"><see cref="IClient"/> to use</param>
        /// <param name="handlers"><see cref="IInstancesOf{ICanHandleMessages}">Message handlers</see></param>
        public InputHandlers(IClient client, IInstancesOf<ICanHandleMessages> handlers)
        {
            _client = client;
            _handlers = handlers;           
        }

        /// <inheritdoc/>
        public void Initialize()
        {
            //_handlers.ForEach()
            
        }
    }
}