using System.Collections.Generic;

namespace FantasyJumper.Core.Sprites.Animations
{
    public class PlayerAnimations : AnimationCollection
    {
        public Animation DyingAnimation { get; }
        public Animation WalkingAnimation { get; }
        public Animation RunAnimation { get; }
        public Animation JumpingAnimation { get; }
        public Animation FallingAnimation { get; }
        public Animation StartJumpAnimation { get; }
        public ChainedAnimation FullJumpAnimation { get; }

        public PlayerAnimations()
        {
            IdleAnimation = new Animation(18, 50, true, 128, 128, 5);
            DyingAnimation = new Animation(15, 50, false, 128, 128, 2);
            WalkingAnimation = new Animation(24, 30, true, 128, 128, 18);
            RunAnimation = new Animation(12, 30, true, 128, 128, 12);
            JumpingAnimation = new Animation(6, 30, true, 128, 128, 7);
            FallingAnimation = new Animation(6, 30, false, 128, 128, 3);
            StartJumpAnimation = new Animation(6, 30, false, 128, 128, 8);
            FullJumpAnimation = new ChainedAnimation(new List<IAnimation>() { StartJumpAnimation, JumpingAnimation });
        }
    }
}
