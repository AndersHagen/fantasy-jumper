using Microsoft.Xna.Framework;

namespace FantasyJumper.Core.Collisions
{
    public class CollisionBox
    {
        private Rectangle _box;

        private Vector2 _offset;

        public Rectangle Bound => _box;

        public CollisionBox(Vector2 parentXY, int width, int height, Vector2? offset = null)
        {
            _offset = offset ?? Vector2.Zero;

            var xy = parentXY + _offset;

            _box = new Rectangle(xy.ToPoint(), new Point(width, height));
        }

        public bool CollisionWith(CollisionBox other, Vector2? otherOffset = null)
        {
            return _box.Intersects(other.Bound);
        }

        public void RePosition(Vector2 position)
        {
            _box.Location = (position + _offset).ToPoint();
        }
    }
}
