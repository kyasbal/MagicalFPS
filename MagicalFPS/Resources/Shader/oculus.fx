float4x4 matT:TRANSFORM;
texture2D viewTex:VIEWTEX;
float2 lensCenterOffset:LENSOFFSET = { 0, 0 };
float aspectRatio = 640 / 800.0;
float scaling=2.0;
float4 u_distortion = {1,0.22,0.24,0};
SamplerState basicSampler
{

};
struct VS_INPUT
{
	float4 Position:POSITION;
	float2 uv:UV;
};

struct VS_OUTPUT
{
	float4 Position:SV_Position;
	float2 uv:UV;
};

float distortionScale(float2 offset) {
	// Note that this performs piecewise multiplication,
	// NOT a dot or cross product
	float2 offsetSquared = float2(offset.x*offset.x, offset.y*offset.y);
	// Since the power to which we raise r when multiplying against each K is even
	// there's no need to find r, as opposed to r^2
	float radiusSquared = offsetSquared.x + offsetSquared.y;
	float distortionScale = //
		u_distortion[0] + //
		u_distortion[1] * radiusSquared + //
		u_distortion[2] * radiusSquared * radiusSquared + //
		u_distortion[3] * radiusSquared * radiusSquared * radiusSquared;
	return distortionScale;
}

VS_OUTPUT VS(VS_INPUT input)
{

	VS_OUTPUT output;
	float2 uvResult = input.uv;
	uvResult *= 2;
	uvResult -= 1;
	uvResult -= lensCenterOffset;
	uvResult.y /= aspectRatio;
	output.Position = mul(input.Position,matT);
	output.uv = uvResult;//output.Position = mul(input.Position, matWVP);
	return output;
}

float4 PS(VS_OUTPUT output) :SV_Target
{
	float s = distortionScale(output.uv);
	output.uv /= scaling;
	output.uv.y *= aspectRatio;
	float2 calcedUV = s*output.uv + 0.5.xx;
		if (calcedUV.x > 1 || calcedUV.x < 0 || calcedUV.y>1 || calcedUV.y < 0)return float4(0, 0, 0, 1);
	return viewTex.Sample(basicSampler, calcedUV);
}

technique10 DefaultTechnique
{
	pass Basic
	{
		SetVertexShader(CompileShader(vs_4_0, VS()));
		SetPixelShader(CompileShader(ps_4_0, PS()));
	}
}