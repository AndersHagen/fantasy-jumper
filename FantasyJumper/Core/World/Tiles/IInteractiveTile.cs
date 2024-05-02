using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FantasyJumper.Core.World.Tiles
{
    public interface IInteractiveTile
    {
        public void Interact(Vector2 impactDirection);
        public void Update(GameTime gameTime);
        public void Draw(SpriteBatch spriteBatch);

    }
}
