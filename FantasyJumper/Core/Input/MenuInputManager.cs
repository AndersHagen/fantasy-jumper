using FantasyJumper.Core.Input.Commands;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace FantasyJumper.Core.Input
{
    public class MenuInputManager : IInputManager
    {
        private MouseState _previousState;

        public MenuInputManager() 
        {
        }

        public List<GameCommand> HandleInput(GameTime gameTime)
        {
            var mouseState = Mouse.GetState();
            var commands = new List<GameCommand>
            {
                new MouseHoverCommand(mouseState.X, mouseState.Y)
            };

            if (mouseState.LeftButton == ButtonState.Pressed && _previousState.LeftButton == ButtonState.Released) 
            {
                commands.Add(new MouseClickedCommand(mouseState.X, mouseState.Y));
            }

            _previousState = mouseState;

            return commands;
        }
    }
}
