using SlimDX.Direct3D11;

namespace MagicalFPS.MagicEffect
{
    class BulletEffect:EffectBase
    {
        private Effect effect;

        private GameContext game;

        public BulletEffect(GameContext context):base(context)
        {
            game = context;
        }

        protected override void Draw(long time)
        {
                
        }

        protected override void Update(long time)
        {
            
        }

        public override void Dispose()
        {
            
        }
    }
}
