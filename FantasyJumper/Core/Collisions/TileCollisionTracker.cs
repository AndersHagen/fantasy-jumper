namespace FantasyJumper.Core.Collisions
{
    public class TileCollisionTracker
    {
        public const uint HIT_FROM_ABOVE = 1;
        public const uint HIT_FROM_BELOW = 1 << 1;
        public const uint HIT_FROM_LEFT = 1 << 2;
        public const uint HIT_FROM_RIGHT = 1 << 3;

        private uint _hits;

        public TileCollisionTracker() 
        {
        }

        public void SetHitOccurred(uint hit)
        {
            _hits |= hit;
        }

        public bool HasHitOccurred(uint hit)
        {
            return (_hits & hit) != 0;
        }

        public void Reset()
        {
            _hits = 0;
        }
    }
}
