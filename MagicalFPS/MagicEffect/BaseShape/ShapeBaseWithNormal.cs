using System.Collections.Generic;
using MMF;
using MMF.Model;
using MMF.Utility;
using SlimDX.Direct3D11;

namespace MagicalFPS.MagicEffect.BaseShape
{
    abstract class ShapeBaseWithNormal:DefaultDrawableImpl
    {
        protected abstract void InitializeVerticies(List<PositionWithNormalInputLayout> verticies);

        protected abstract void InitializeIndicies(IndexBufferBuilder bufferBuilder);

        protected abstract SlimDX.Direct3D11.Effect Effect
        {
            get;
        }

        protected InputLayout inputLayout;

        public void Initialize()
        {
            
        }

        public ShapeBaseWithNormal(RenderContext context)
        {
            
        }

        public override int SubsetCount
        {
            get { return 1; }
        }

        public override void Draw()
        {
            
        }

        public override void Update()
        {
            
        }

        public override void Dispose()
        {
            
        }
    }
}
