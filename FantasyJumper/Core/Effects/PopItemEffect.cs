using FantasyJumper.Core.Sprites.Animations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FantasyJumper.Core.Effects
{
    public class PopItemEffect
    {
        public bool Completed { get; set; }
        private Texture2D _itemTexture;
        private IAnimation _animation;
        private Vector2 _position;
        private Vector2 _velocity;
        private float _fade;

        public PopItemEffect(Texture2D texture, Vector2 position, IAnimation animation = null) 
        {
            _itemTexture = texture;
            _animation = animation;
            _position = position;
            Completed = false;
            _fade = 2f;
            _velocity = new Vector2(0, -7);
        }

        public void Update(GameTime gameTime) 
        {
            if (_animation != null)
            {
                _animation.Update(gameTime);
            }

            _fade -= 0.05f;
            _velocity += new Vector2(0, 0.5f);
            _position += _velocity;

            if (_fade < 0) { Completed = true; }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            var frame = _animation?.GetCurrentFrame();

            spriteBatch.Draw(
                _itemTexture,
                _position,
                frame,
                Color.White * _fade
            );
        }
    }
}
