using System;
using MagicalFPS.MagicEffect.BaseShape;
using MMDFileParser;
using MMF.DeviceManager;
using MMF.Model.Shape;
using SlimDX;
using SlimDX.Direct3D11;
using CGHelper = MMF.Utility.CGHelper;
using MMF.Utility;

namespace MagicalFPS.MagicEffect
{
    class BulletEffect:EffectBase
    {
        private Effect effect;

        private GameContext game;

        private PlaneBoard board;

        private ShaderResourceView bulletTexture;

        private float lastTime;

        public BulletEffect(GameContext context):base(context)
        {
            game = context;
            effect=CGHelper.CreateEffectFx5("Resources\\Shader\\bulletShader.fx",context.RenderContext.DeviceManager.Device);
            bulletTexture = context.LoadTexture("Resources\\Effect\\thunder.png");
            board=new DividedPlaneBoard(context.RenderContext,bulletTexture,100,new Vector2(10,10),new PlaneBoard.PlaneBoardDescription(BlendStateManager.BlendStates.Add, effect,false,SubscribeLazerEffectVariable));
            board.Initialize();
        }

        private void SubscribeLazerEffectVariable(PlaneBoard planeBoard, Effect effect, ShaderResourceView arg3)
        {
            planeBoard.DefaultEffectSubscribe();
            int uvOffset = (int) (lastTime/100f)%8;
            effect.GetVariableBySemantic("TIME").AsScalar().Set(lastTime/100f);
            effect.GetVariableBySemantic("DIRECTION").AsVector().Set(Vector3.UnitZ);
            effect.GetVariableBySemantic("LENGTH").AsScalar().Set(200);//長さ
            effect.GetVariableBySemantic("UP").AsVector().Set(Vector3.UnitY);
            effect.GetVariableBySemantic("EYE")
                .AsVector()
                .Set(game.RenderContext.CurrentTargetContext.MatrixManager.ViewMatrixManager.CameraPosition);
            effect.GetVariableBySemantic("UVOFFSET").AsScalar().Set(uvOffset);
            effect.GetVariableBySemantic("LAZERHEIGHT").AsScalar().Set(40*(1-(float)Math.Exp(-lastTime/500d)));//レーザーの幅
        }

        protected override void Draw(long time)
        {
            board.Draw();
        }

        protected override void Update(long time)
        {
            lastTime = time;
        }

        public override void Dispose()
        {
            
        }

        public override void ReloadEffectFile()
        {
            base.ReloadEffectFile();
            /*try
            {
                var effect = CGHelper.CreateEffectFx5("..\\..\\Resources\\Shader\\bulletShader.fx", game.RenderContext.DeviceManager.Device);
                this.effect = effect;
                board.Description.Effect = effect;
            }
            catch (Exception e)
            {
                Tracer.e("エフェクトコンパイルエラー:{0}", e.ToString());
            }*/

        }
    }
}
