using FantasyJumper.Core.Collisions;
using FantasyJumper.Core.Sprites.Animations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FantasyJumper.Core.Sprites
{
    public abstract class SpriteBase
    {
        protected Texture2D Texture;
        protected Vector2 Position;
        protected Vector2 Velocity;
        protected bool Flipped;
        protected IAnimation CurrentAnimation;
        protected AnimationCollection Animations;

        protected bool GoingUp => Velocity.Y < 0;
        protected bool GoingDown => Velocity.Y > 0;
        protected bool GoingLeft => Velocity.X < 0;
        protected bool GoingRight => Velocity.X > 0;


        public CollisionBox CollisionBox { get; protected set; }

        public SpriteBase(Texture2D texture, Vector2 position)
        {
            Texture = texture;
            Position = position;
            Flipped = false;
        }

        public abstract void Update(GameTime gameTime);

        public abstract void Draw(SpriteBatch spriteBatch);

        public abstract bool CollisionWithObject(object gameObject);

        protected virtual void SetPosition(Vector2 position) 
        {
            Position = position;
            CollisionBox.RePosition(position);
        }
    }
}
