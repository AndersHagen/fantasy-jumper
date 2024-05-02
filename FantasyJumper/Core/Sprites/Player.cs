using FantasyJumper.Core.Collisions;
using FantasyJumper.Core.Sprites.Animations;
using FantasyJumper.Core.World.Tiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Diagnostics;
using static FantasyJumper.Core.Sprites.States;

namespace FantasyJumper.Core.Sprites
{
    public class Player : SpriteBase
    {
        private float _friction = 0.1f;
        private Vector2 _gravity = new Vector2(0, 0.5f);

        private float _jumpPower;

        private float _walkingSpeed;
        private float _runningSpeed;

        private bool _inAir => State == PlayerState.Jumping || State == PlayerState.Falling;

        private TileCollisionTracker _tileCollitionTracker;

        private PlayerState _state;

        public Vector2 PlayerPosition => Position;

        public PlayerState State { 
            get { return _state; } 
            private set 
            {
                if (_state != PlayerState.Dead)
                {
                    _state = value;
                }
            } 
        }

        public PlayerAnimations PlayerAnimations => (Animations as PlayerAnimations);

        public Player(Texture2D texture, Vector2 position) : base (texture, position)
        {
            Animations = new PlayerAnimations();
            CurrentAnimation = PlayerAnimations.IdleAnimation;

            State = PlayerState.Idle;

            Velocity = Vector2.Zero;
            _walkingSpeed = 2f;
            _runningSpeed = 4.5f;
            _jumpPower = 15f;
            _tileCollitionTracker = new TileCollisionTracker();

            CollisionBox = new CollisionBox(Position, 36, 76, new Vector2(45, 30));
        }

        public override void Update(GameTime gameTime) 
        {
            _tileCollitionTracker.Reset();
            CurrentAnimation.Update(gameTime);

            if (State == PlayerState.Dead)
            {
                return;
            }

            if (_inAir) 
            { 
                Velocity += _gravity;
                if (Velocity.Y > 15)
                {
                    Velocity = new Vector2(Velocity.X, 15);
                }
            }

            if (Math.Abs(Velocity.X) < _friction)
            {
                Velocity = new Vector2(0, Velocity.Y);
            } 
            else
            {
                var vx = 0f;
                var vy = 0f;

                if (_inAir)
                {
                    vx = Velocity.X;
                    vy = Velocity.Y;
                } else
                {
                    vx = Velocity.X > 0 ? Velocity.X - _friction : Velocity.X + _friction;
                }

                Velocity = new Vector2(vx, vy);
            }

            Position += Velocity;

            var x = Position.X;
            var y = Position.Y;

            if (x < -40)
            {
                x = -40;
            }

            if (x > 1830)
            {
                x = 1830;
            }

            SetPosition(new Vector2(x, y));

            SetStateByVelocity();
        }

        public void AdjustPosition(Vector2 adjustment)
        {
            SetPosition(Position + adjustment);
        }

        public void Jump() 
        {
            if (State == PlayerState.Idle || State == PlayerState.Walking || State == PlayerState.Running)
            {
                Velocity = new Vector2(Velocity.X, -_jumpPower);
                CurrentAnimation = PlayerAnimations.FullJumpAnimation;
                State = PlayerState.Jumping;
            }
        }

        public void Fall()
        {
            if (GoingUp) return;

            State = PlayerState.Falling;
            CurrentAnimation = PlayerAnimations.FallingAnimation;
        }

        public void Die()
        {
            CurrentAnimation = PlayerAnimations.DyingAnimation;
            State = PlayerState.Dead;
        }

        public void StopFall(int snapToY)
        {
            if (State == PlayerState.Jumping)
            {
                PlayerAnimations.FullJumpAnimation.Reset();
            }

            if (State == PlayerState.Falling)
            {
                PlayerAnimations.FallingAnimation.Reset();
            }

            Velocity = new Vector2 (Velocity.X, 0);

            State = PlayerState.Idle;
            SetPosition(new Vector2(Position.X, snapToY - 105));

            SetStateByVelocity();
        }

        private void SetStateByVelocity()
        {
            if (State == PlayerState.Dead)
            {
                return;
            }

            if (Velocity.Y != 0) 
            {
                State = Velocity.Y <= _jumpPower ? PlayerState.Jumping : PlayerState.Falling;
            }
            else
            {
                var speedX = Math.Abs(Velocity.X);

                if (speedX > _walkingSpeed)
                {
                    State = PlayerState.Running;
                } 
                else if (speedX > _friction)
                {
                    State = PlayerState.Walking;
                }
                else
                {
                    State = PlayerState.Idle;
                }
            }

            SetAnimation();
        }

        private void SetAnimation()
        {
            switch (State)
            {
                case PlayerState.Idle:
                    CurrentAnimation = PlayerAnimations.IdleAnimation;
                    break;
                case PlayerState.Walking:
                    CurrentAnimation = PlayerAnimations.WalkingAnimation;
                    break;
                case PlayerState.Running:
                    CurrentAnimation = PlayerAnimations.RunAnimation;
                    break;
                case PlayerState.Jumping:
                    CurrentAnimation = PlayerAnimations.FullJumpAnimation;
                    break;
                case PlayerState.Falling:
                    CurrentAnimation = PlayerAnimations.FallingAnimation;
                    break;
                case PlayerState.Dead:
                    CurrentAnimation = PlayerAnimations.DyingAnimation;
                    break;
            }
        }

        public void Move(int deltax, bool run = false)
        {
            float speed;

            if (_inAir)
            {
                speed = Velocity.X;
            }
            else if (State == PlayerState.Running) 
            {
                speed = _runningSpeed * deltax;
            } else if (run && State != PlayerState.Running)
            {
                speed = _runningSpeed * deltax;
            } else
            {
                speed = _walkingSpeed* deltax;
            }

            Velocity = new Vector2(speed, Velocity.Y);
            Flipped = speed < 0;
        }

        public override void Draw(SpriteBatch spriteBatch) 
        {
            var effect = Flipped ? SpriteEffects.FlipHorizontally : SpriteEffects.None;

            spriteBatch.Draw(
                Texture, 
                Position,
                CurrentAnimation.GetCurrentFrame(), 
                Color.White,
                0f,
                Vector2.Zero,
                1f,
                effect,
                0
            );
        }

        public bool HitTile(CollisionBox hitBox)
        {
            var tBox = hitBox.Bound;

            if (GoingDown || Velocity.Y == 0)
            {
                if (CollisionHelper.FromAboveHitOccurred(CollisionBox.Bound, tBox) && !_tileCollitionTracker.HasHitOccurred(TileCollisionTracker.HIT_FROM_ABOVE))
                {
                    StopFall(hitBox.Bound.Top);
                    _tileCollitionTracker.SetHitOccurred(TileCollisionTracker.HIT_FROM_ABOVE);
                    return false;
                }
            }

            if (GoingLeft)
            {
                if (CollisionHelper.FromLeftHitOccurred(CollisionBox.Bound, tBox) && !_tileCollitionTracker.HasHitOccurred(TileCollisionTracker.HIT_FROM_LEFT))
                {
                    Velocity = Vector2.Zero;
                    SetPosition(new Vector2(hitBox.Bound.Right - 40, Position.Y));
                    _tileCollitionTracker.SetHitOccurred(TileCollisionTracker.HIT_FROM_LEFT);
                    return !_tileCollitionTracker.HasHitOccurred(TileCollisionTracker.HIT_FROM_ABOVE); // true;
                }
            }

            if (GoingRight)
            {
                if (CollisionHelper.FromRightHitOccurred(CollisionBox.Bound, tBox) && !_tileCollitionTracker.HasHitOccurred(TileCollisionTracker.HIT_FROM_RIGHT))
                {
                    Velocity = Vector2.Zero;
                    SetPosition(new Vector2(hitBox.Bound.Left - 83, Position.Y));
                    _tileCollitionTracker.SetHitOccurred(TileCollisionTracker.HIT_FROM_RIGHT);
                    return !_tileCollitionTracker.HasHitOccurred(TileCollisionTracker.HIT_FROM_ABOVE); // true;
                }
            }

            if (GoingUp)
            {
                if (CollisionHelper.FromBelowHitOccurred(CollisionBox.Bound, tBox) && !_tileCollitionTracker.HasHitOccurred(TileCollisionTracker.HIT_FROM_BELOW))
                {
                    Velocity = new Vector2(Velocity.X, 0);
                    SetPosition(new Vector2(Position.X, hitBox.Bound.Bottom - 29));
                    _tileCollitionTracker.SetHitOccurred(TileCollisionTracker.HIT_FROM_BELOW);
                    return !_tileCollitionTracker.HasHitOccurred(TileCollisionTracker.HIT_FROM_ABOVE); // true;
                }
            }

            return false;
        }

        public override bool CollisionWithObject(object gameObject)
        {
            var momentum = Velocity;

            if (gameObject is Tile)
            {
                var hitResult = HitTile((gameObject as Tile).CollisionBox);

                if (gameObject is BonusTile) 
                {
                    (gameObject as BonusTile).Interact(momentum);
                }

                return hitResult;
            }

            if (gameObject is Platform)
            {
                var p = (gameObject as Platform);
                var hitResult = HitTile(p.CollisionBox);

                return hitResult;
            }

            if (gameObject is Enemy)
            {
                return HitEnemy();
            }

            return false;
        }

        private bool HitEnemy()
        {
            if (State != PlayerState.Dead)
            {
                Debug.WriteLine("Oops I died!");
                Die();
            }

            return false;
        }
    }
}
