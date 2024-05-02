using FantasyJumper.Core;
using FantasyJumper.Core.Input.Commands;
using FantasyJumper.GameStates;
using FantasyJumper.GameStates.GamePlay;
using FantasyJumper.GameStates.Menu;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FantasyJumper
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private BaseGameState _currentState;
        private PlayState _playState;
        private StateTransition _stateTransition;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = 1920;
            _graphics.PreferredBackBufferHeight = 1080;
            _graphics.IsFullScreen = true;

            _graphics.ApplyChanges();

            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            TextureManager.Init(Content, _spriteBatch);

            _currentState = new MenuState();
            _playState = new PlayState(_spriteBatch);
        }

        protected override void Update(GameTime gameTime)
        {
            if (_stateTransition != null && _stateTransition.InTransition) 
            {
                _stateTransition.Update(gameTime);
                return;
            }

            var stateCommand = _currentState.Update(gameTime);

            if (stateCommand is ExitCommand)
            {
                Exit();
            }

            if (stateCommand is StartGameCommand)
            {
                var next = _playState;
                next.Reset(_spriteBatch);
                _stateTransition = new StateTransition(_currentState, next, _spriteBatch);
                _currentState = next;
            }

            if (stateCommand is ResumeGameCommand)
            {
                var next = _playState;
                _stateTransition = new StateTransition(_currentState, next, _spriteBatch);
                _currentState = next;
            }

            if (stateCommand is MainMenuCommand)
            {
                var next = new MenuState((stateCommand as MainMenuCommand).IsPaused);
                _stateTransition = new StateTransition(_currentState, next, _spriteBatch);
                _currentState = next;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();

            if (_stateTransition != null && _stateTransition.InTransition)
            {
                _stateTransition.Draw(_spriteBatch);
            } 
            else 
            {
                _currentState.Draw(_spriteBatch);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
