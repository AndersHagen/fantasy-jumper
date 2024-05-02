using FantasyJumper.Core;
using FantasyJumper.Core.Input.Commands;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace FantasyJumper.GameStates.Menu
{
    public class MenuComponent
    {
        private int _width;
        private int _height;
        private Vector2 _position;
        private Vector2 _textPosition;
        private string _text;

        private bool _mouseHover;
        private Rectangle _bound;

        private GameCommand _onClickCommand;

        public MenuComponent(int yPos, string text, GameCommand onClickCommand) 
        {
            _text = text;
            var dim = TextureManager.MenuFont.MeasureString(text);
            _width = (int)dim.X; 
            _height = (int)dim.Y; 
            _position = new Vector2(960 - _width / 2, yPos);
            _textPosition = new Vector2(960 - dim.X / 2, yPos);

            _bound = new Rectangle((int)_position.X, (int)_position.Y, _width, _height);
            _mouseHover = false;
            _onClickCommand = onClickCommand;
        }

        public void Update(GameTime gameTime)
        {

        }

        public GameCommand OnClick(int x, int y)
        {
            if (!_bound.Contains(x, y))
            {
                return new EmptyCommand();
            }

            return _onClickCommand;
        }

        public void CheckMouseHover(int x, int y)
        {
            _mouseHover = _bound.Contains(x, y);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            var hover = _mouseHover ? 1f : 0.7f;

            spriteBatch.DrawString(TextureManager.MenuFont, _text, _textPosition, Color.LightGoldenrodYellow * hover);
        }
    }
}
