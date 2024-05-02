using FantasyJumper.Core.Input.Commands;
using FantasyJumper.Core.Sprites;
using FantasyJumper.Core.Sprites.Items;
using FantasyJumper.Core.World.Tiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Diagnostics;
using static FantasyJumper.Core.Sprites.States;

namespace FantasyJumper.Core.World
{
    public class Level
    {
        public int Time { get; set; }

        public Vector2 StartPosition { get; set; }

        public TileMap TileMap { get; set; }

        public List<Enemy> Enemies { get; set; }

        public bool IsComplete => TileMap.RemainingCoins == 0;

        private int _timerTick;

        public Player Player { get; private set; }

        public bool PlayerDead => Player.State == PlayerState.Dead;

        public List<Platform> Platforms { get; private set; }

        public Level(int time, Vector2 startPosition, TileMap map, List<Enemy> enemies, List<Platform> platforms, SpriteBatch spriteBatch) 
        {
            Time = time;
            _timerTick = 0;
            StartPosition = startPosition;
            TileMap = map;
            Enemies = enemies;
            Platforms = platforms;

            Player = new Player(TextureManager.PlayerAtlas, StartPosition);

            TileMap.Initialize(spriteBatch);
        }

        public void AddPlatform(Platform platform)
        {
            Platforms.Add(platform);
        }

        public void Update(GameTime gameTime)
        {
            _timerTick += gameTime.ElapsedGameTime.Milliseconds;

            if (_timerTick >= 1000)
            {
                _timerTick = 0;
                Time--;
            }
                
            if (Time < 0) { Time = 0; }


            foreach (var platform in Platforms)
            {
                platform.Update(gameTime);
                if (platform.PlayerOnPlatform(Player.CollisionBox))
                {
                    Player.AdjustPosition(platform.Velocity);
                    Debug.WriteLine($"Player: {Player.PlayerPosition} - Platform: {platform.Position}");
                }
            }

            foreach (var enemy in Enemies)
            {
                enemy.Update(gameTime);
            }

            Player.Update(gameTime);

            TileMap.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch, bool debug = false)
        {
            TileMap.Draw(spriteBatch);

            foreach (var coin in TileMap.Coins)
            {
                coin.Draw(spriteBatch);
            }

            foreach (var enemy in Enemies)
            {
                enemy.Draw(spriteBatch);
            }

            foreach (var platform in Platforms)
            {
                platform.Draw(spriteBatch);
            }

            spriteBatch.DrawString(TextureManager.GameFont, $"COINS LEFT: {TileMap.RemainingCoins}", new Vector2(10, 10), Color.LightGoldenrodYellow);

            spriteBatch.DrawString(TextureManager.GameFont, $"TIME: {Time}", new Vector2(10, 40), GetTimeDisplayColor());

            Player.Draw(spriteBatch);

            if (debug)
            {
                spriteBatch.Draw(TextureManager.Solid, Player.CollisionBox.Bound, Color.Red * 0.5f);
            }
        }

        private Color GetTimeDisplayColor()
        {
            if (Time <= 10 && Time % 2 == 0) return Color.Red;

            return Color.LightGoldenrodYellow;
        }

        public void CheckForCollisions()
        {
            var playerBox = Player.CollisionBox;

            var fall = true;

            foreach (var enemy in Enemies)
            {
                if (playerBox.CollisionWith(enemy.CollisionBox))
                {
                    fall = Player.CollisionWithObject(enemy);
                }
            }

            foreach (var tile in TileMap.Tiles)
            {
                if (playerBox.CollisionWith(tile.CollisionBox))
                {
                    fall = Player.CollisionWithObject(tile);
                }
            }

            foreach (var coin in TileMap.Coins)
            {
                if (playerBox.CollisionWith(coin.CollisionBox))
                {
                    PopCoin(coin);
                }
            }

            foreach (var platform in Platforms)
            {
                if (playerBox.CollisionWith(platform.CollisionBox, platform.Velocity))
                {
                    fall = Player.CollisionWithObject(platform);
                }
            }

            if (fall)
            {
                Player.Fall();
            }
        }

        public void PopCoin(Coin coin)
        {
            TileMap.PopCoin(coin);
        }

        public void HandlePlayerCommands(List<GameCommand> gameCommands)
        {
            foreach (var command in gameCommands)
            {
                if (command is MoveCommand)
                {
                    Player.Move((command as MoveCommand).DeltaX, (command as MoveCommand).Run);
                }

                if (command is JumpCommand)
                {
                    Player.Jump();
                }
            }
        }
    }
}
