using System.Runtime.InteropServices;
using SlimDX;
using SlimDX.Direct3D11;
using SlimDX.DXGI;

namespace MagicalFPS.MagicEffect.BaseShape
{
    struct PositionWithNormalInputLayout
    {
        public static readonly InputElement[] VertexElements =
        {
            new InputElement
            {
                SemanticName="POSITION",
                Format = Format.R32G32B32A32_Float
            },
            new InputElement()
            {
                SemanticName = "NORMAL",
                Format = Format.R32G32B32_Float,
                AlignedByteOffset = InputElement.AppendAligned
            }, 
        };

        public Vector4 Position;

        public Vector3 Normal;

        public static int SizeInBytes
        {
            get { return Marshal.SizeOf(typeof (PositionWithNormalInputLayout)); }
        }
    }
}
