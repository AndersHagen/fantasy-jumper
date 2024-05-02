using Microsoft.Xna.Framework;

namespace FantasyJumper.Core.Collisions
{
    public static class CollisionHelper
    {
        public static bool FromLeftHitOccurred(Rectangle r1, Rectangle r2)
        {
            if (r1.Center.X < r2.Right) return false;

            var intersection = Rectangle.Intersect(r1, r2);

            return intersection.Height > intersection.Width;
        }

        public static bool FromRightHitOccurred(Rectangle r1, Rectangle r2)
        {
            if (r1.Center.X > r2.Left) return false;

            var intersection = Rectangle.Intersect(r1, r2);

            return intersection.Height > intersection.Width;
        }

        public static bool FromAboveHitOccurred(Rectangle r1, Rectangle r2)
        {
            if (r1.Center.Y > r2.Top) return false;

            var intersection = Rectangle.Intersect(r1, r2);

            return intersection.Width >= intersection.Height;
        }

        public static bool FromBelowHitOccurred(Rectangle r1, Rectangle r2)
        {
            if (r1.Center.Y < r2.Bottom) return false;

            var intersection = Rectangle.Intersect(r1, r2);

            return intersection.Width >= intersection.Height;
        }
    }
}
