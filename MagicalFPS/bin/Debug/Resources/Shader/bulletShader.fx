float4x4 matWVP:WORLDVIEWPROJECTION;
texture2D mapedDisplay:SPRITETEXTURE;
float time : TIME;
float3 direction:DIRECTION;
float beamLength : LENGTH;
float3 upVector:UP;
float3 eyePos:EYE;
float uvOffset : UVOFFSET;
float height : LAZERHEIGHT;
SamplerState mySampler{
	AddressU = Mirror;
	AddressV = Mirror;
};

struct VS_OUTPUT
{
	float4 position:SV_Position;
	float4 rawPos:POSITION;
	float2 uv:UV;
};

VS_OUTPUT Sprite_VS(float4 pos:POSITION,float2 uv:UV)
{
	VS_OUTPUT vo;
	pos.xyz =direction*uv.x*beamLength+upVector*uv.y;
	float3 diffH = cross(normalize(pos.xyz - eyePos), normalize(direction));
	pos.xyz += diffH*uv.y * height*(1-exp(-pos.z/10));
	pos.y -=height / 2;
	vo.position=mul(pos,matWVP);

	vo.rawPos = pos;
	vo.uv=uv;
	return vo;
}

float4 TSprite_PS(VS_OUTPUT vo):SV_Target
{
	int mf = time / 1000;
	float4 d = mapedDisplay.Sample(mySampler, float2(vo.uv.x, vo.uv.y / 8 + uvOffset / 8.0));
		return d;
}

technique10 defaultTechnique
{
	pass TransParentColorEnabledPass
	{
		SetVertexShader(CompileShader(vs_4_0,Sprite_VS()));
		SetPixelShader(CompileShader(ps_4_0,TSprite_PS()));
	}

}
