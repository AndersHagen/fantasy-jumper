using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace FantasyJumper.Core.Sprites.Animations
{
    public class Animation : IAnimation
    {
        private int _frameCount;

        private int _currentFrame;

        private int _frameDuration;

        private bool _repeating;

        private int _frameWidth;
        private int _frameHeight;

        private int _atlasRowIndex;
        private int _msSinceLastUpdate;

        private List<Rectangle> _frames;

        public bool IsPlaying => _currentFrame < _frameCount;

        public Animation(int frameCount, int frameDuration, bool repeating, int frameWidth, int frameHeight, int atlasRowIndex)
        {
            _frameCount = frameCount;
            _currentFrame = 0;
            _frameDuration = frameDuration;
            _repeating = repeating;
            _frameWidth = frameWidth;
            _frameHeight = frameHeight;
            _atlasRowIndex = atlasRowIndex;
            _msSinceLastUpdate = 0;
            _frames = new List<Rectangle>();

            for (var x = 0; x < _frameCount; x++)
            {
                _frames.Add(new Rectangle(x * _frameWidth, _atlasRowIndex * frameHeight, _frameWidth, _frameHeight));
            }
        }

        public void Update(GameTime gameTime)
        {
            _msSinceLastUpdate += gameTime.ElapsedGameTime.Milliseconds;

            if (_msSinceLastUpdate > _frameDuration)
            {
                if (_repeating)
                {
                    _currentFrame = (_currentFrame + 1) % _frameCount;
                } else
                {
                    _currentFrame++;
                    if (_currentFrame >= _frameCount) 
                    {
                        _currentFrame = _frameCount;
                    }
                }

                _msSinceLastUpdate = 0;
            }
        }

        public Rectangle GetCurrentFrame()
        {
            var frame = _currentFrame < _frameCount ? _currentFrame : _frameCount - 1;

            return _frames[frame];
        }

        public void Reset()
        {
            _currentFrame = 0;
        }
    }
}
