using FantasyJumper.Core.Input;
using FantasyJumper.Core.Input.Commands;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace FantasyJumper.GameStates
{
    public abstract class BaseGameState
    {
        public IInputManager InputManager { get; protected set; }

        public BaseGameState()
        {
        }

        public abstract GameCommand Update(GameTime gameTime);

        public abstract void Draw(SpriteBatch spriteBatch);
    }
}
