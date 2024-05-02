using FantasyJumper.Core.Input.Commands;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;

namespace FantasyJumper.Core.Input
{
    public class PlayingInputManager : IInputManager
    {
        private KeyboardWrapper _keyboardWrapper;

        public PlayingInputManager()
        {
            _keyboardWrapper = new KeyboardWrapper();
        }

        public List<GameCommand> HandleInput(GameTime gameTime)
        {
            var commands = new List<GameCommand>();

            var state = _keyboardWrapper.GetState(gameTime);

            var pressed = state.GetPressedKeys().Cast<int>().ToList();

            if (state.IsKeyDown(Keys.Escape))
            {
                commands.Add(new ExitCommand());
                return commands;
            }

            if (state.IsKeyDown(Keys.Space))
            {
                commands.Add(new JumpCommand());
            }

            if (state.IsKeyDown(Keys.Left))
            {
                if (TestCombo(Keys.Left, Keys.Left))
                {
                    commands.Add(new MoveCommand(-1, true));
                }
                else
                {
                    commands.Add(new MoveCommand(-1));
                }
            }
            else if (state.IsKeyDown(Keys.Right))
            {
                if (TestCombo(Keys.Right, Keys.Right))
                {
                    commands.Add(new MoveCommand(1, true));
                }
                else
                {
                    commands.Add(new MoveCommand(1));
                }
            }

            return commands;
        }

        private bool TestCombo(Keys pressedKey, Keys comboKey)
        {
            return _keyboardWrapper.KeyHasBeenPressed(pressedKey) && _keyboardWrapper.CombosWith(comboKey);
        }
    }
}
