/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Dolittle.Booting;

namespace Dolittle.Edge.IdentityMapper
{
    /// <summary>
    /// Represents a <see cref="ICanPerformBootProcedure">boot procedure</see> that initiates <see cref="IInputHandlers"/>
    /// </summary>
    public class InputHandlersBootProcedure : ICanPerformBootProcedure
    {
        readonly IInputHandlers _inputHandlers;

        /// <summary>
        /// Initializes a new instance of <see cref="InputHandlersBootProcedure"/>
        /// </summary>
        /// <param name="inputHandlers"><see cref="IInputHandlers"/> to initialize</param>
        public InputHandlersBootProcedure(IInputHandlers inputHandlers)
        {
            _inputHandlers = inputHandlers;
        }

        /// <inheritdoc/>
        public bool CanPerform() => true;

        /// <inheritdoc/>
        public void Perform()
        {
            _inputHandlers.Initialize();
        }
    }
}