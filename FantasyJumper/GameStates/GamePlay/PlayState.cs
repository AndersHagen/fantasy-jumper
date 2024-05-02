using FantasyJumper.Core;
using FantasyJumper.Core.Effects;
using FantasyJumper.Core.Input;
using FantasyJumper.Core.Input.Commands;
using FantasyJumper.Core.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;

namespace FantasyJumper.GameStates.GamePlay
{
    public class PlayState : BaseGameState
    {
        private static Level[] _levels;
        private int _currentLevel;
        private int _levelChangeTimer;

        private GameState _gameState;

        public Level CurrentLevel => _levels[_currentLevel];

        public PlayState(SpriteBatch spriteBatch)
        {
            InputManager = new PlayingInputManager();

            Reset(spriteBatch);
        }

        public void Reset(SpriteBatch spriteBatch)
        {
            _levels = LevelFileParser.ParseLevels(spriteBatch);

            _currentLevel = 4;

            _levelChangeTimer = 0;
            _gameState = GameState.Running;
        }

        public override GameCommand Update(GameTime gameTime)
        {
            var commands = InputManager.HandleInput(gameTime);

            if (commands.Any(c => c is ExitCommand))
            {
                return new MainMenuCommand(true);
            }

            if (CurrentLevel.IsComplete)
            {
                _gameState = GameState.Victory;
                if (_levelChangeTimer == 0)
                {
                    _levelChangeTimer = 1;
                }
            }

            if (CurrentLevel.PlayerDead || CurrentLevel.Time == 0 && _gameState != GameState.Victory)
            {
                CurrentLevel.Update(gameTime);
                _gameState = GameState.GameOver;
                return new EmptyCommand();
            }

            _levelChangeTimer += _levelChangeTimer == 0 ? 0 : gameTime.ElapsedGameTime.Milliseconds;

            if (_levelChangeTimer > 0)
            {
                if (_levelChangeTimer > 5000)
                {
                    NextLevel();
                    _levelChangeTimer = 0;
                }
                return new EmptyCommand();
            }

            CurrentLevel.HandlePlayerCommands(commands.Where(c => c is PlayerCommand).ToList());

            CurrentLevel.Update(gameTime);

            EffectManager.Update(gameTime);

            CurrentLevel.CheckForCollisions();

            return new EmptyCommand();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            CurrentLevel.Draw(spriteBatch);

            EffectManager.Draw(spriteBatch);

            if (_gameState == GameState.Victory)
            {
                spriteBatch.DrawString(TextureManager.GameFont, "YOU WIN!!", new Vector2(800, 450), Color.Yellow, 0f, Vector2.Zero, 3f, SpriteEffects.None, 0);
            }

            if (_gameState == GameState.GameOver)
            {
                spriteBatch.DrawString(TextureManager.GameFont, "GAME OVER", new Vector2(800, 450), Color.Yellow, 0f, Vector2.Zero, 3f, SpriteEffects.None, 0);
            }
        }

        private void NextLevel()
        {
            _gameState = GameState.Running;
            _currentLevel = (_currentLevel + 1) % _levels.Length;
        }
    }
}
