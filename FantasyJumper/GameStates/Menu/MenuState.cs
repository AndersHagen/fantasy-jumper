using FantasyJumper.Core;
using FantasyJumper.Core.Input.Commands;
using FantasyJumper.Core.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace FantasyJumper.GameStates.Menu
{
    public class MenuState : BaseGameState
    {

        private List<MenuComponent> _menuButtons;

        public MenuState(bool gamePaused = false)
        {
            InputManager = new MenuInputManager();
            _menuButtons = new List<MenuComponent>
            { 
                new (400, "NEW GAME", new StartGameCommand()),
                new (480, "TOP SCORES", new EmptyCommand()),
                new (560, "OPTIONS", new EmptyCommand()),
                new (640, "EXIT", new ExitCommand()),
            };

            if (gamePaused )
            {
                _menuButtons.Add(new MenuComponent(280, "CONTINUE GAME", new ResumeGameCommand()));
            }
        }

        public override GameCommand Update(GameTime gameTime)
        {
            var commands = InputManager.HandleInput(gameTime);

            if (commands.Any(c => c is ExitCommand))
            {
                return new ExitCommand();
            }

            foreach (var command in commands)
            {
                if (command is MouseHoverCommand)
                {
                    foreach (var button in _menuButtons)
                    {
                        button.CheckMouseHover((command as MouseHoverCommand).X, (command as MouseHoverCommand).Y);
                    }
                }

                if (command is MouseClickedCommand)
                {
                    foreach (var button in _menuButtons)
                    {
                        var cmd = button.OnClick((command as MouseClickedCommand).X, (command as MouseClickedCommand).Y);

                        if (cmd is not EmptyCommand) return cmd;
                    }
                }
            }

            return new EmptyCommand();
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(TextureManager.MenuBackground, Vector2.Zero, new Rectangle(0, 280, 1024, 900), Color.White * 0.5f, 0f, Vector2.Zero, 1.88f, SpriteEffects.None, 1f);

            foreach (var button in _menuButtons)
            { 
                button.Draw(spriteBatch); 
            }
        }
    }
}
