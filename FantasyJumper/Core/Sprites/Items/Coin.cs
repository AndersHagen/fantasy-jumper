using FantasyJumper.Core.Sprites.Animations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace FantasyJumper.Core.Sprites.Items
{
    public class Coin : SpriteBase
    {
        private Animation _spinAnimation;
        private float _bounce;
        private Vector2 _bounceOffset;

        public bool Active { get; set; }

        public Coin(Texture2D texture, Vector2 position) :base(texture, position) 
        {
            _spinAnimation = new Animation(6, 70, true, 32, 32, 0);
            Active = true;
            CollisionBox = new Collisions.CollisionBox(Position, 32, 32);
            _bounce = 0f;
            _bounceOffset = Vector2.Zero;
        }

        public override bool CollisionWithObject(object gameObject)
        {
            return false;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            var bounceY = new Vector2(0, (float)Math.Sin(_bounce) * 8);


            if (Active)
            {
                spriteBatch.Draw(
                    Texture,
                    Position + _bounceOffset,
                    _spinAnimation.GetCurrentFrame(),
                    Color.White
                );
            }
        }

        public override void Update(GameTime gameTime)
        {
            _bounce += 0.03f;
            _bounceOffset = new Vector2(0, (float)Math.Sin(_bounce) * 8);
            _spinAnimation.Update(gameTime);
            CollisionBox.RePosition(Position +  _bounceOffset);
        }
    }
}
