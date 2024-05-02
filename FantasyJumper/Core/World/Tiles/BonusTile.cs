using FantasyJumper.Core.Effects;
using FantasyJumper.Core.Sprites.Animations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FantasyJumper.Core.World.Tiles
{
    public class BonusTile : Tile, IInteractiveTile
    {
        public bool Popped => Value == 0;
        private Vector2 _offest;
        public int Value { get; private set; }

        public BonusTile(Texture2D texture, Vector2 position, int pops = 1, float scale = 0.5F) : base(texture, position, scale)
        {
            Interactive = true;
            _offest = Vector2.Zero;
            Value = pops;
        }

        public void Update(GameTime gameTime)
        {
            if (_offest.Y < 0)
            {
                _offest += new Vector2(0, 0.2f);
                if (_offest.Y > 0 )
                {
                    _offest = Vector2.Zero;
                }
            }
        }

        public void Interact(Vector2 impactDirection)
        {
            if (Popped || impactDirection.Y >= 0) return;

            _offest = new Vector2(0, -6);
            EffectManager.AddEffect(CreateCoinPopEffect());
            Value--;
        }

        private PopItemEffect CreateCoinPopEffect()
        {
            return new PopItemEffect(TextureManager.TileSets["dungeon"].GetCoinTexture(), Position + new Vector2(16, -16), new Animation(6, 70, true, 32, 32, 0));
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            var shade = Popped ? Color.Gray : Color.White;

            spriteBatch.Draw(
                _texture,
                Position + _offest,
                null,
                shade,
                0f,
                Vector2.Zero,
                _scale,
                SpriteEffects.None,
                1f
            );
        }
    }
}
