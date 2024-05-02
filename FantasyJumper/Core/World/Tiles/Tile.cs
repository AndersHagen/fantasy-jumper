using FantasyJumper.Core.Collisions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FantasyJumper.Core.World.Tiles
{
    public class Tile
    {
        protected Texture2D _texture;
        public Vector2 Position { get; private set; }
        public bool Solid { get; private set; }
        public bool Interactive { get; protected set; }
        public bool Active { get; private set; }
        public CollisionBox CollisionBox { get; private set; }
        protected float _scale;

        public Tile(Texture2D texture, Vector2 position, float scale = 0.5f)
        {
            _texture = texture;
            Position = position;
            Solid = true;
            Active = true;
            _scale = scale;
            Interactive = false;
            CollisionBox = new CollisionBox(Position, (int)(_texture.Height * _scale), (int)(_texture.Width * _scale));
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (!Active) return;

            spriteBatch.Draw(
                _texture, 
                Position, 
                null,
                Color.White,
                0f,
                Vector2.Zero,
                _scale,
                SpriteEffects.None,
                1f
                );
        }
    }
}
