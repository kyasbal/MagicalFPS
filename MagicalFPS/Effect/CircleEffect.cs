using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MagicalFPS.Effect.BaseShape;
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
        private readonly GameContext _context;
        private SlimDX.Direct3D11.Effect effect;

        private SlimDX.Direct3D11.Effect beffect;

        private PlaneBoard frontCircle;

        private PlaneBoard[] subCircle=new PlaneBoard[3];

        private DividedPlaneBoard[] beamEffects=new DividedPlaneBoard[5];

        private ShaderResourceView noiseView;

        private ShaderResourceView beamView;

        private Quaternion rotationQuat = Quaternion.Identity;

        private float lastTime;

        private int currentBeamIndex = 0;

        public CircleEffect(GameContext context)
        {
            _context = context;
            effect = CGHelper.CreateEffectFx5("Resources\\Shader\\circleShader.fx",
                context.RenderContext.DeviceManager.Device);
            beffect = CGHelper.CreateEffectFx5("Resources\\Shader\\beam.fx",
                context.RenderContext.DeviceManager.Device);
            noiseView = context.LoadTexture("Resources\\Effect\\noise.png");
            beamView = context.LoadTexture("Resources\\Effect\\lazer.png");
            ShaderResourceView view = context.LoadTexture("Resources\\Effect\\circle.png");
            frontCircle = new DividedPlaneBoard(context.RenderContext, view,100,
                new Vector2(100, 100),
                new PlaneBoard.PlaneBoardDescription(BlendStateManager.BlendStates.Add, effect, false, RenderFront));
            frontCircle.Initialize();
            for (int i = 0; i < 3; i++)
            {
                subCircle[i] = new PlaneBoard(context.RenderContext, view, new Vector2(30, 30),
                    new PlaneBoard.PlaneBoardDescription(BlendStateManager.BlendStates.Add, effect, false, RenderFront));
                subCircle[i].Initialize();
            }
            for (int i = 0; i < 5; i++)
            {
                beamEffects[i]=new DividedPlaneBoard(context.RenderContext, view, 100,
                new Vector2(100, 100),
                new PlaneBoard.PlaneBoardDescription(BlendStateManager.BlendStates.Add, beffect, false, Renderbeam));
                beamEffects[i].Initialize();
            }
        }

        /// <summary>
        /// Renderbeams the specified arg1.
        /// </summary>
        /// <param name="arg1">The arg1.</param>
        /// <param name="arg2">The arg2.</param>
        /// <param name="arg3">The arg3.</param>
        private void Renderbeam(PlaneBoard arg1, SlimDX.Direct3D11.Effect arg2, ShaderResourceView arg3)
        {
            arg1.DefaultEffectSubscribe();
            beffect.GetVariableBySemantic("NOISETEX").AsResource().SetResource(beamView);
            beffect.GetVariableBySemantic("TIME").AsScalar().Set(lastTime);
            beffect.GetVariableBySemantic("STARTPOS").AsVector().Set(Vector3.Zero);
            beffect.GetVariableBySemantic("TARGETPOS").AsVector().Set(new Vector3(0,0,300));
            beffect.GetVariableBySemantic("UP").AsVector().Set(Vector3.TransformNormal(Vector3.UnitY*100,Matrix.RotationZ((float) (2*Math.PI/beamEffects.Length*currentBeamIndex))));
            beffect.GetVariableBySemantic("EYE").AsVector().Set(_context.RenderContext.CurrentTargetContext.MatrixManager.ViewMatrixManager.CameraPosition);
            beffect.GetVariableBySemantic("UVOFFSET").AsScalar().Set((float) (0.2*currentBeamIndex));
            beffect.GetVariableBySemantic("BH").AsScalar().Set(5);
            currentBeamIndex++;
            if (currentBeamIndex == 5) currentBeamIndex = 0;
        }

        private void RenderFront(PlaneBoard arg1, SlimDX.Direct3D11.Effect arg2, ShaderResourceView arg3)
        {
            arg1.DefaultEffectSubscribe();
            effect.GetVariableBySemantic("NOISETEX").AsResource().SetResource(noiseView);
            effect.GetVariableBySemantic("TIME").AsScalar().Set(lastTime);
        }

        protected override void Draw(long time)
        {
            foreach (var dividedPlaneBoard in beamEffects)
            {
                dividedPlaneBoard.Draw();
            }
            frontCircle.Draw();
            foreach (var drawable in subCircle)
            {
                drawable.Draw();
            }
            
        }

        protected override void Update(long time)
        {
            lastTime = time/1000f;
            frontCircle.Transformer.Rotation = Quaternion.RotationAxis(Vector3.UnitZ, time/1000f);
            frontCircle.Transformer.Scale =new Vector3( 1f - (float)Math.Exp(-(time/100f)));
            for (int i = 0; i < subCircle.Length; i++)
            {
                subCircle[i].Transformer.Scale = new Vector3(1f - (float)Math.Exp(-(time / 100f)));
                subCircle[i].Transformer.Position = new Vector3((float)(30 * Math.Cos(2 * Math.PI / subCircle.Length * i - time / 500f)), (float)(30 * Math.Sin(2 * Math.PI / subCircle.Length * i - time / 500f)), 5);
            }
            
        }

        public override void Dispose()
        {
            frontCircle.Dispose();
            foreach (var drawable in subCircle)
            {
                drawable.Dispose();
            }
            effect.Dispose();
            noiseView.Dispose();
        }
    }
}
