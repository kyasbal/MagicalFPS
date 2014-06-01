float4x4 matWVP:WORLDVIEWPROJECTION;
texture2D mapedDisplay:SPRITETEXTURE;
texture2D noiseTex:NOISETEX;
float time : TIME;
float3 circleCol :CIRCLECOLOR;
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
	vo.position=mul(pos,matWVP);
	vo.rawPos=pos;
	vo.uv=uv;
	return vo;
}

float4 TSprite_PS(VS_OUTPUT vo):SV_Target
{
	float4 color = mapedDisplay.Sample(mySampler, float2(vo.uv.x, vo.uv.y));
	if (color.a != 0)
	{
		float4 col = float4(0, 1, 1, color.a);
		col.xyz *= noiseTex.Sample(mySampler, float2(vo.uv.x +sin(time/10)*10, vo.uv.y + cos(3*time/10)*5)).xyz;
		return col;
	}
	return float4(0, 0, 0, 0);
}

float4 Sprite_PS(VS_OUTPUT vo):SV_Target
{
	float4 d= mapedDisplay.Sample(mySampler,float2(vo.uv.x,vo.uv.y));
	if (d.a == 0)
	{
		return float4(0, 0, 0, 0);
	}
	
	return d;
}

technique10 defaultTechnique
{
	pass TransParentColorEnabledPass
	{
		SetVertexShader(CompileShader(vs_4_0,Sprite_VS()));
		SetPixelShader(CompileShader(ps_4_0,TSprite_PS()));
	}
	pass TransParentColorDisabledPass
	{
		SetVertexShader(CompileShader(vs_4_0,Sprite_VS()));
		SetPixelShader(CompileShader(ps_4_0,Sprite_PS()));
	}

}
