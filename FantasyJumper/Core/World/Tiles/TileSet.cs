using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace FantasyJumper.Core.World.Tiles
{
    public class TileSet
    {
        private Dictionary<TileType, Texture2D> _set;
        private Texture2D _coinTexture;

        public TileSet() 
        {
            _set = new Dictionary<TileType, Texture2D>();
        }

        public void LoadContent(ContentManager contentManager, SpriteBatch spriteBatch, string tileset) 
        {
            _set[TileType.BonusTile] = contentManager.Load<Texture2D>($"tiles/{tileset}/Bonus");
            _set[TileType.Brick1] = contentManager.Load<Texture2D>($"tiles/{tileset}/Brick_01");
            _set[TileType.Brick2] = contentManager.Load<Texture2D>($"tiles/{tileset}/Brick_02");
            _set[TileType.Bridge] = MakeBridgeTexture(contentManager, spriteBatch, tileset);
            _set[TileType.Bridge1] = contentManager.Load<Texture2D>($"tiles/{tileset}/Bridge_01");
            _set[TileType.Bridge2] = contentManager.Load<Texture2D>($"tiles/{tileset}/Bridge_02");
            _set[TileType.DecorBrick] = contentManager.Load<Texture2D>($"tiles/{tileset}/Decor_Brick");
            _set[TileType.GroundSlopeUp] = contentManager.Load<Texture2D>($"tiles/{tileset}/Ground_01");
            _set[TileType.GroundTop] = contentManager.Load<Texture2D>($"tiles/{tileset}/Ground_02");
            _set[TileType.GroundSlopeDown] = contentManager.Load<Texture2D>($"tiles/{tileset}/Ground_03");
            _set[TileType.GroundLeftEdge] = contentManager.Load<Texture2D>($"tiles/{tileset}/Ground_04");
            _set[TileType.GroundLeftTopCorner] = contentManager.Load<Texture2D>($"tiles/{tileset}/Ground_05");
            _set[TileType.GroundSolid] = contentManager.Load<Texture2D>($"tiles/{tileset}/Ground_06");
            _set[TileType.GroundRightTopCorner] = contentManager.Load<Texture2D>($"tiles/{tileset}/Ground_07");
            _set[TileType.GroundRightEdge] = contentManager.Load<Texture2D>($"tiles/{tileset}/Ground_08");
            _set[TileType.EarthWallRight] = contentManager.Load<Texture2D>($"tiles/{tileset}/Ground_09");
            _set[TileType.PlatformLeft] = contentManager.Load<Texture2D>($"tiles/{tileset}/Ground_10");
            _set[TileType.PlatformMid] = contentManager.Load<Texture2D>($"tiles/{tileset}/Ground_11");
            _set[TileType.PlatformRight] = contentManager.Load<Texture2D>($"tiles/{tileset}/Ground_12");
            _set[TileType.EarthWallLeft] = contentManager.Load<Texture2D>($"tiles/{tileset}/Ground_13");
            _set[TileType.Ladder] = contentManager.Load<Texture2D>($"tiles/{tileset}/Ladder");
            _set[TileType.Spikes] = contentManager.Load<Texture2D>($"tiles/{tileset}/Spikes");
            _set[TileType.Barrel] = contentManager.Load<Texture2D>($"tiles/{tileset}/Wooden_Barrel");
            _set[TileType.Box] = contentManager.Load<Texture2D>($"tiles/{tileset}/Wooden_Box");
            _coinTexture = contentManager.Load<Texture2D>("game_objects/collectibles/spinning_coin");
        }

        public Texture2D GetTexture(TileType tileType)
        {
            return _set[tileType];
        }

        public Texture2D GetCoinTexture()
        {
            return _coinTexture;
        }
        private Texture2D MakeBridgeTexture(ContentManager contentManager, SpriteBatch spriteBatch, string tileset)
        {
            var gfx = spriteBatch.GraphicsDevice;
            var target = new RenderTarget2D(gfx, 128, 128);

            var part1 = contentManager.Load<Texture2D>($"tiles/{tileset}/Bridge_01");
            var part2 = contentManager.Load<Texture2D>($"tiles/{tileset}/Bridge_02");

            gfx.SetRenderTarget(target);

            gfx.Clear(new Color(Color.Black, 0f));

            spriteBatch.Begin();

            spriteBatch.Draw(part1, Vector2.Zero, Color.White);
            spriteBatch.Draw(part2, new Vector2(part2.Width, 0), Color.White);

            spriteBatch.End();

            gfx.SetRenderTarget(null);

            return target;
        }
    }
}
