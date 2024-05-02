using FantasyJumper.Core.Collisions;
using FantasyJumper.Core.Sprites.Animations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace FantasyJumper.Core.Sprites
{
    public class Enemy : SpriteBase
    {
        private int _minX;
        private int _maxX;

        public EnemyAnimations EnemyAnimations => (Animations as EnemyAnimations);

        public Enemy(Texture2D texture, Vector2 position, int minX, int maxX) : base(texture, position) 
        {
            _minX = minX;
            _maxX = maxX;
            Animations = new EnemyAnimations();
            CurrentAnimation = EnemyAnimations.WalkingAnimation;
            Velocity = new Vector2(-3, 0);
            CollisionBox = new CollisionBox(Position, 36, 76, new Vector2(45, 30));
        }

        public override void Update(GameTime gameTime)
        {
            CurrentAnimation.Update(gameTime);

            if (Position.X < _minX || Position.X > _maxX)
            {
                Velocity *= -1;
            } 

            Flipped = Velocity.X < 0;
            
            SetPosition(Position + Velocity);
        }

        public override bool CollisionWithObject(object gameObject)
        {
            throw new NotImplementedException();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            var effect = Flipped ? SpriteEffects.FlipHorizontally : SpriteEffects.None;

            spriteBatch.Draw(
                Texture,
                Position,
                CurrentAnimation.GetCurrentFrame(),
                Color.White,
                0f,
                Vector2.Zero,
                1f,
                effect,
                0
            );
        }
    }
}
