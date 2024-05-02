using System.Collections.Generic;

namespace FantasyJumper.Core.Sprites.Animations
{
    public class EnemyAnimations : AnimationCollection
    {
        public Animation DyingAnimation { get; }
        public Animation WalkingAnimation { get; }
        public Animation RunAnimation { get; }
        public Animation JumpingAnimation { get; }
        public Animation FallingAnimation { get; }
        public Animation StartJumpAnimation { get; }
        public ChainedAnimation FullJumpAnimation { get; }

        public EnemyAnimations()
        {
            IdleAnimation = new Animation(18, 50, true, 128, 128, 3);
            DyingAnimation = new Animation(15, 50, false, 128, 128, 0);
            WalkingAnimation = new Animation(24, 30, true, 128, 128, 16);
            RunAnimation = new Animation(12, 30, true, 128, 128, 10);
            JumpingAnimation = new Animation(6, 30, true, 128, 128, 5);
            FallingAnimation = new Animation(6, 30, false, 128, 128, 1);
            StartJumpAnimation = new Animation(6, 30, false, 128, 128, 6);
            FullJumpAnimation = new ChainedAnimation(new List<IAnimation>() { StartJumpAnimation, JumpingAnimation });
        }
    }
}
