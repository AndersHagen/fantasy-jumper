using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace FantasyJumper.Core.Effects
{
    public static class EffectManager
    {
        private static List<PopItemEffect> _effects = new List<PopItemEffect>();

        public static void AddEffect(PopItemEffect effect)
        {
            _effects.Add(effect);
        }

        public static void Update(GameTime gameTime)
        {
            var completed = new List<PopItemEffect>();

            foreach (var effect in _effects)
            {
                if (effect.Completed)
                {
                    completed.Add(effect);
                    continue;
                }

                effect.Update(gameTime);
            }

            foreach (var complete in completed)
            {
                _effects.Remove(complete);
            }
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            foreach (var effect in _effects)
            {
                effect.Draw(spriteBatch);
            }
        }
    }
}
