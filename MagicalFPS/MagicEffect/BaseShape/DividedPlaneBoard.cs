using System.Collections.Generic;
using MMF;
using MMF.Model.Shape;
using SlimDX;
using SlimDX.Direct3D11;
using CGHelper = MMF.Utility.CGHelper;

namespace MagicalFPS.MagicEffect.BaseShape
{
    class DividedPlaneBoard:PlaneBoard
    {
        private readonly int _divCount;

        public DividedPlaneBoard(RenderContext context, ShaderResourceView resView,int divCount, PlaneBoardDescription desc = null) : base(context, resView, desc)
        {
            _divCount = divCount;
        }

        public DividedPlaneBoard(RenderContext context, ShaderResourceView resView,int divCount, Vector2 size, PlaneBoardDescription desc = null) : base(context, resView, size, desc)
        {
            _divCount = divCount;
        }

        protected override void GenerateVerticies(float width, float height, List<byte> vertexBytes)
        {
            float stride = width*2/_divCount;
            float uvStride=1f/_divCount;
            for (int i = 0; i < _divCount; i++)
            {
                AddSquare(new Vector3[]
                {
                    new Vector3(-width+stride*i,height,0),
                    new Vector3(-width+stride*(i+1),height,0),
                    new Vector3(-width+stride*(i+1),-height,0), 
                    new Vector3(-width+stride*i,-height,0),
                    
                },new Vector2[]
                {
                    new Vector2(uvStride*i,0),
                    new Vector2(uvStride*(i+1),0),
                    new Vector2(uvStride*(i+1),1),
                    new Vector2(uvStride*i,1), 
                },vertexBytes);
            }
        }

        private void AddSquare(Vector3[] positions,Vector2[] uvs,List<byte> verticies )
        {
            CGHelper.AddListBuffer(positions[0],verticies);
            CGHelper.AddListBuffer(uvs[0], verticies);
            CGHelper.AddListBuffer(positions[1], verticies);
            CGHelper.AddListBuffer(uvs[1], verticies);
            CGHelper.AddListBuffer(positions[3], verticies);
            CGHelper.AddListBuffer(uvs[3], verticies);
            CGHelper.AddListBuffer(positions[1], verticies);
            CGHelper.AddListBuffer(uvs[1], verticies);
            CGHelper.AddListBuffer(positions[2], verticies);
            CGHelper.AddListBuffer(uvs[2], verticies);
            CGHelper.AddListBuffer(positions[3], verticies);
            CGHelper.AddListBuffer(uvs[3], verticies);
        }
    }
}
