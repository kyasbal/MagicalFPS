using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MMF.DeviceManager;
using MMF.Model;
using MMF.Model.Shape;
using MMF.Utility;
using SlimDX;
using SlimDX.Direct3D11;

namespace MagicalFPS.Effect
{

    class CircleEffect:EffectBase
    {
        private SlimDX.Direct3D11.Effect effect;

        private IDrawable frontCircle;

        private ShaderResourceView noiseView;

        private Quaternion rotationQuat = Quaternion.Identity;

        private float lastTime;

        public CircleEffect(GameContext context)
        {
            effect = CGHelper.CreateEffectFx5("Resources\\Shader\\circleShader.fx",
                context.RenderContext.DeviceManager.Device);
            noiseView = context.LoadTexture("Resources\\Effect\\noise.png");
            frontCircle=new PlaneBoard(context.RenderContext,context.LoadTexture("Resources\\Effect\\circle.png"),new Vector2(100,100),new PlaneBoard.PlaneBoardDescription(BlendStateManager.BlendStates.Add,effect,false,RenderFront));
        }

        private void RenderFront(PlaneBoard arg1, SlimDX.Direct3D11.Effect arg2, ShaderResourceView arg3)
        {
            arg1.DefaultEffectSubscribe();
            effect.GetVariableBySemantic("NOISETEX").AsResource().SetResource(noiseView);
            effect.GetVariableBySemantic("TIME").AsScalar().Set(lastTime);
        }

        protected override void Draw(long time)
        {
            frontCircle.Draw();
        }

        protected override void Update(long time)
        {
            lastTime = time/1000f;
            frontCircle.Transformer.Rotation = Quaternion.RotationAxis(Vector3.UnitZ, time/1000f);
            frontCircle.Transformer.Scale =new Vector3( 1f - (float)Math.Exp(-(time/100f)));
        }
    }
}
