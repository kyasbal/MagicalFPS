using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MMF;
using MMF.Model;
using MMF.Utility;
using SlimDX;
using SlimDX.Direct3D11;

namespace MagicalFPS.Effect.BaseShape
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
