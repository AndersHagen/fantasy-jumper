using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace FantasyJumper.Core.Sprites.Animations
{
    public class ChainedAnimation : IAnimation
    {
        private int _currentAnimation;
        private List<IAnimation> _animations;

        public ChainedAnimation(List<IAnimation> parts)
        {
            _animations = parts;
            _currentAnimation = 0;
        }

        public bool IsPlaying => _currentAnimation < _animations.Count && _animations[_currentAnimation].IsPlaying;

        public Rectangle GetCurrentFrame()
        {
            return _animations[_currentAnimation].GetCurrentFrame();
        }

        public void Reset()
        {
            foreach (var animation in _animations)
            {
                animation.Reset();
            }
            _currentAnimation = 0;
        }

        public void Update(GameTime gameTime)
        {
            if (!_animations[_currentAnimation].IsPlaying)
            {
                _currentAnimation++;
            }

            if (_currentAnimation == _animations.Count)
            {
                _currentAnimation--;
            }

            _animations[_currentAnimation].Update(gameTime);
        }
    }
}
