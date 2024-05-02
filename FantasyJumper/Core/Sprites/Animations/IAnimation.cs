using Microsoft.Xna.Framework;

namespace FantasyJumper.Core.Sprites.Animations
{
    public interface IAnimation
    {
        public bool IsPlaying { get; }

        public void Reset();

        public void Update(GameTime gameTime);

        public Rectangle GetCurrentFrame();
    }
}
