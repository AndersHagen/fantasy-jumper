using FantasyJumper.Core.World.Tiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace FantasyJumper.Core
{
    public static class TextureManager
    {
        public static SpriteFont GameFont;
        public static SpriteFont MenuFont;
        public static Texture2D Solid;
        public static Texture2D PlayerAtlas;
        public static Texture2D DungeonBackground;
        public static Texture2D MenuBackground;

        public static Dictionary<string, TileSet> TileSets;
        public static Dictionary<string, Texture2D> EnemyTextures;

        public static void Init(ContentManager contentManager, SpriteBatch spriteBatch)
        {
            var graphics = spriteBatch.GraphicsDevice;

            EnemyTextures = new Dictionary<string, Texture2D>();

            Solid = new Texture2D(graphics, 1, 1);
            Solid.SetData(new Color[] { Color.White });

            GameFont = contentManager.Load<SpriteFont>("gamefont");
            MenuFont = contentManager.Load<SpriteFont>("menufont");
            PlayerAtlas = contentManager.Load<Texture2D>("characters/atlas_player");
            EnemyTextures.Add("demon_archer_1", contentManager.Load<Texture2D>("characters/atlas_demon_archer_1"));
            EnemyTextures.Add("demon_archer_2", contentManager.Load<Texture2D>("characters/atlas_demon_archer_2"));
            EnemyTextures.Add("demon_archer_3", contentManager.Load<Texture2D>("characters/atlas_demon_archer_3"));
            EnemyTextures.Add("dark_demon_1", contentManager.Load<Texture2D>("characters/atlas_demon_of_darkness_1"));
            EnemyTextures.Add("dark_demon_2", contentManager.Load<Texture2D>("characters/atlas_demon_of_darkness_2"));
            EnemyTextures.Add("dark_demon_3", contentManager.Load<Texture2D>("characters/atlas_demon_of_darkness_3"));
            EnemyTextures.Add("devil", contentManager.Load<Texture2D>("characters/atlas_devil"));
            EnemyTextures.Add("hell_knight", contentManager.Load<Texture2D>("characters/atlas_hell_knight"));
            EnemyTextures.Add("magician_demon_1", contentManager.Load<Texture2D>("characters/atlas_magician_demon_1"));
            EnemyTextures.Add("magician_demon_2", contentManager.Load<Texture2D>("characters/atlas_magician_demon_2"));
            EnemyTextures.Add("magician_demon_3", contentManager.Load<Texture2D>("characters/atlas_magician_demon_3"));
            EnemyTextures.Add("succubus", contentManager.Load<Texture2D>("characters/atlas_succubus"));
            DungeonBackground = contentManager.Load<Texture2D>("backgrounds/dungeon");
            MenuBackground = contentManager.Load<Texture2D>("backgrounds/menu_background_image");

            var dungeonTileset = new TileSet();
            dungeonTileset.LoadContent(contentManager, spriteBatch, "dungeon");

            TileSets = new Dictionary<string, TileSet>
            {
                { "dungeon", dungeonTileset }
            };
        }
    }
}
