using FantasyJumper.Core.Sprites;
using FantasyJumper.Core.World.Tiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace FantasyJumper.Core.World
{
    public static class LevelFileParser
    {
        public static Level[] ParseLevels(SpriteBatch spriteBatch)
        {
            var levels = new List<RawLevel>();

            using (var s = new StreamReader("levels/levels.json"))
            {
                var content = s.ReadToEnd();
                levels = JsonSerializer.Deserialize<List<RawLevel>>(content);
            }

            return levels.Select(l => l.ToLevel(spriteBatch)).ToArray();
        }



        private class RawLevel
        {
            public int Id { get; set; }
            public int Time { get; set; }

            public RawCoords StartPosition { get; set; }

            public RawTileMapData TileMapData { get; set; }

            public List<RawEnemy> Enemies { get; set; }

            public List<RawPlatform> Platforms { get; set; }

            public Level ToLevel(SpriteBatch spriteBatch)
            {
                return new Level(
                    Time,
                    new Vector2(StartPosition.X, StartPosition.Y),
                    TileMapData.ToTileMap(),
                    Enemies.Select(e => e.ToEnemy()).ToList(),
                    Platforms.Select(p => p.ToPlatform()).ToList(),
                    spriteBatch
                    ) ;
            }
        }

        private class RawCoords
        {
            public int X { get; set; }
            public int Y { get; set; }
        }

        private class RawTileMapData
        {
            public string TileSet { get; set; }
            public int Width { get; set; }
            public int Height { get; set; }
            public int TileWidth { get; set; }
            public int TileHeight { get; set; }

            public List<string> Map {  get; set; }

            internal TileMap ToTileMap()
            {
                return new TileMap(
                    TextureManager.TileSets[TileSet],
                    Map.ToArray(),
                    Width,
                    Height,
                    TileWidth,
                    TileHeight
                );
            }
        }

        private class RawEnemy
        {
            public string Texture { get; set; }
            public RawCoords StartPosition { get; set; }
            public int MinXPosition { get; set; }
            public int MaxXPosition { get; set; }

            internal Enemy ToEnemy()
            {
                return new Enemy(
                    TextureManager.EnemyTextures[Texture],
                    new Vector2(StartPosition.X, StartPosition.Y),
                    MinXPosition,
                    MaxXPosition
                    );
            }
        }

        private class RawPlatform 
        {
            public RawCoords StartTileCoord { get; set; }

            public int NumberOfTilesWide { get; set; }

            public bool MovesVertically { get; set; }

            public int MinTilePosition { get; set; }
            public int MaxTilePosition { get; set; }

            public Platform ToPlatform()
            {
                return new Platform(
                    new Point(StartTileCoord.X, StartTileCoord.Y),
                    NumberOfTilesWide,
                    MovesVertically,
                    new Point(MinTilePosition, MaxTilePosition)
                    );
            }
        }
    }
}
