using FantasyJumper.Core.Collisions;
using System.Collections.Generic;

namespace FantasyJumper.Core.World.Tiles
{
    public class TileGroup
    {
        private bool _isHorizontal;
        private int _numberOfTiles;
        private List<Tile> _tiles;
        private int _tileHeight;
        private int _tileWidth;
        private CollisionBox _collisionBox;

        public TileGroup(bool isHorizontal, int numberOfTiles, List<Tile> tiles, int tileHeight, int tileWidth, CollisionBox collisionBox)
        {
            _isHorizontal = isHorizontal;
            _numberOfTiles = numberOfTiles;
            _tiles = tiles;
            _tileHeight = tileHeight;
            _tileWidth = tileWidth;
            SetCollisionBox();
        }

        private void SetCollisionBox()
        {
            if (_isHorizontal)
            {
                _collisionBox = new CollisionBox(_tiles[0].Position, _tileWidth * _numberOfTiles, _tileHeight);
            }
            else
            {
                _collisionBox = new CollisionBox(_tiles[0].Position, _tileWidth, _tileHeight * _numberOfTiles);
            }
        }
    }
}
