using FantasyJumper.Core.Collisions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FantasyJumper.Core.World.Tiles
{
    public class Platform
    {
        public int _numberOfTilesWide { get; private set; }

        private Texture2D _texture;

        public Vector2 Position { get; private set; }
        public Vector2 Velocity { get; private set; }

        public CollisionBox CollisionBox { get; private set; }
        private CollisionBox _playerOnBoardChecker;
        private Vector2 _playerCheckerOffset;
        private Point _minMax;
        private bool _isVertical;

        public Platform(Point postion, int tilesWide, bool vertical, Point minMax) 
        {
            _minMax = new Point(minMax.X * 64, minMax.Y * 64);
            _isVertical = vertical;

            _texture = TextureManager.TileSets["dungeon"].GetTexture(TileType.Bridge1);

            Position = new Vector2(postion.X * 64, postion.Y * 64);
            _numberOfTilesWide = tilesWide;
            Velocity = _isVertical ? new Vector2(0, -1) : new Vector2(-1, 0);
            _playerCheckerOffset = new Vector2(0, -10);

            CollisionBox = new CollisionBox(Position, _texture.Width * _numberOfTilesWide, _texture.Height);
            _playerOnBoardChecker = new CollisionBox(Position + _playerCheckerOffset, _texture.Width * _numberOfTilesWide, _texture.Height);
        }

        public void Update(GameTime gameTime)
        {
            Position += Velocity;
            CollisionBox.RePosition(Position);
            _playerOnBoardChecker.RePosition(Position + _playerCheckerOffset);

            var axis = _isVertical ? Position.Y : Position.X;

            if (axis > _minMax.Y || axis < _minMax.X)
            {
                Velocity *= -1;
            }
        }
            
        public bool PlayerOnPlatform(CollisionBox playerBox)
        {
            return _playerOnBoardChecker.CollisionWith(playerBox);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (var x = 0; x < _numberOfTilesWide; x++)
            {
                spriteBatch.Draw(
                    _texture,
                    Position + new Vector2(x * _texture.Width, -4),
                    Color.White
                );
            }
        }
    }
}
