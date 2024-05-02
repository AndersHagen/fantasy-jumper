using FantasyJumper.Core.Input.Commands;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace FantasyJumper.Core.Input
{
    public interface IInputManager
    {
        public List<GameCommand> HandleInput(GameTime gameTime);
    }
}
