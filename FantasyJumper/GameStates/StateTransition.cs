using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FantasyJumper.GameStates
{
    public class StateTransition
    {
        private Texture2D _current;
        private Texture2D _next;
        private float _fadeAmount;
        public bool InTransition => _fadeAmount > 0;

        public StateTransition(BaseGameState currentState, BaseGameState nextState, SpriteBatch spriteBatch)
        {
            _current = GenerateSnapShot(currentState, spriteBatch);
            _next = GenerateSnapShot(nextState, spriteBatch);
            _fadeAmount = 1f;
        }

        public void Update(GameTime gameTime)
        {
            if (_fadeAmount <= 0f)
            {
                _fadeAmount = 0f;
            } else
            {
                _fadeAmount -= 0.02f;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_next, Vector2.Zero, Color.White);
            spriteBatch.Draw(_current, Vector2.Zero, Color.White * _fadeAmount);
        }
        
        private Texture2D GenerateSnapShot(BaseGameState state, SpriteBatch spriteBatch)
        {
            var gfx = spriteBatch.GraphicsDevice;
            var target = new RenderTarget2D(gfx, 1920, 1080);

            gfx.SetRenderTarget(target);

            gfx.Clear(Color.Black);

            spriteBatch.Begin();

            state.Draw(spriteBatch);

            spriteBatch.End();

            gfx.SetRenderTarget(null);

            return target;
        }
    }
}
