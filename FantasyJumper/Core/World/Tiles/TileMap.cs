using FantasyJumper.Core.Sprites.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FantasyJumper.Core.World.Tiles
{
    public class TileMap
    {
        private List<Tile> _tiles;
        private List<Coin> _coins;
        private List<IInteractiveTile> _interactiveTiles;

        public Texture2D StaticTiles { get; private set; }

        private int _width;
        private int _height;

        private int _tileWidth;
        private int _tileHeight;

        private TileSet _tileSet;

        public TileMap(TileSet tileSet, string[] map, int width, int height, int tileWidth, int tileHeight)
        {
            _width = width;
            _height = height;
            _tileWidth = tileWidth;
            _tileHeight = tileHeight;

            _tileSet = tileSet;

            _tiles = new List<Tile>();
            _coins = new List<Coin>();

            ParseMap(map);

            _interactiveTiles = _tiles.Where(t => t is IInteractiveTile).Cast<IInteractiveTile>().ToList();
        }

        public List<Tile> Tiles => _tiles;
        public List<Coin> Coins => _coins;

        public int RemainingCoins => _coins.Count + _interactiveTiles.Where(t => t is BonusTile).Sum(t => (t as BonusTile).Value);

        public void Update(GameTime gameTime)
        {
            _coins = _coins.Where(c => c.Active).ToList();

            foreach (var coin in  _coins)
            {
                coin.Update(gameTime);
            }

            foreach(var tile in _interactiveTiles)
            {
                tile.Update(gameTime);
            }
        }

        public void Initialize(SpriteBatch spriteBatch)
        {
            var gfx = spriteBatch.GraphicsDevice;

            var target = new RenderTarget2D(gfx, _width * _tileWidth, _height * _tileHeight);

            gfx.SetRenderTarget(target);

            gfx.Clear(new Color(Color.Black, 0f));

            spriteBatch.Begin();

            spriteBatch.Draw(
                TextureManager.DungeonBackground,
                Vector2.Zero,
                null,
                Color.White,
                0f,
                Vector2.Zero,
                1.44f,
                SpriteEffects.None,
                1f
                );

            foreach (var tile in  _tiles)
            {
                if (!tile.Interactive)
                { 
                    tile.Draw(spriteBatch); 
                }
            }

            spriteBatch.End();

            StaticTiles = target;

            gfx.SetRenderTarget(null);
        }

        private void ParseMap(string[] map)
        {
            if (map.Length != _height)
            {
                throw new ArgumentException("Map height");
            }

            for (var y = 0; y < _height; y++)
            {
                if (map[y].Length != _width)
                {
                    throw new ArgumentException("Map width");
                }

                for (var x = 0; x < _width; x++)
                {
                    var c = map[y][x];

                    if (c == '$')
                    {
                        _coins.Add(new Coin(_tileSet.GetCoinTexture(), new Vector2(x * _tileWidth + 16, y * _tileHeight + 16)));
                        continue;
                    }

                    var tileType = GetTile(c);

                    if (tileType == TileType.Blank) continue;

                    if (tileType == TileType.BonusTile)
                    {
                        _tiles.Add(new BonusTile(_tileSet.GetTexture(tileType), new Vector2(x * _tileWidth, y * _tileHeight)));
                        continue;
                    }

                    _tiles.Add(new Tile(_tileSet.GetTexture(tileType), new Vector2(x * _tileWidth, y * _tileHeight)));
                }
            }
        }

        private TileType GetTile(char t)
        {
            switch (t)
            {
                case ' ':
                    return TileType.Blank;
                case '=':
                    return TileType.PlatformMid;
                case '<':
                    return TileType.PlatformLeft;
                case '>':
                    return TileType.PlatformRight;
                case '-':
                    return TileType.GroundTop;
                case '@':
                    return TileType.GroundSolid;
                case '?':
                    return TileType.BonusTile;
                case '~':
                    return TileType.Bridge;
                case 'B':
                    return TileType.Barrel;
                case 'b':
                    return TileType.Box;
            }

            return TileType.Blank;
        }

        internal void PopCoin(Coin coin)
        {
            coin.Active = false;
        }

        internal void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(StaticTiles, Vector2.Zero, Color.White);

            foreach (var tile in _interactiveTiles)
            {
                tile.Draw(spriteBatch);
            }
        }
    }
}
