using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace FantasyJumper.Core.Input
{
    public class KeyboardWrapper
    {
        private KeyboardState _currentState;
        private KeyboardState _previousState;

        private int _bufferLifeTime;

        private Dictionary<Keys, int> _inputBuffer;

        public KeyboardWrapper()
        {
            _inputBuffer = new Dictionary<Keys, int>();
            _bufferLifeTime = 1000;
        }

        public KeyboardState GetState(GameTime gameTime)
        {
            UpdateInputBuffer(gameTime);

            _previousState = _currentState;
            _currentState = Keyboard.GetState();

            return _currentState;
        }

        public bool CombosWith(Keys key)
        {
            return _inputBuffer.ContainsKey(key) && _inputBuffer[key] < 250;
        }

        private void UpdateInputBuffer(GameTime gameTime)
        {
            foreach (var key in _inputBuffer.Keys) 
            {
                if (_inputBuffer[key] >= _bufferLifeTime)
                {
                    _inputBuffer.Remove(key);
                    continue;
                }

                _inputBuffer[key] += gameTime.ElapsedGameTime.Milliseconds;
            }

            foreach(var key in _currentState.GetPressedKeys())
            {
                if (KeyHasBeenPressed(key)) 
                {
                    _inputBuffer[key] = 0;
                }
            }
        }

        public bool IsKeyDown(Keys key)
        {
            return _currentState.IsKeyDown(key);
        }

        public bool KeyHasBeenPressed(Keys key)
        {
            return _currentState.IsKeyDown(key) && !_previousState.IsKeyDown(key);
        }
    }
}
