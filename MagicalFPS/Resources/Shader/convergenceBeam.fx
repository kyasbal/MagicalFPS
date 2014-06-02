float4x4 matWVP:WORLDVIEWPROJECTION;
texture2D mapedDisplay:SPRITETEXTURE;
texture2D noiseTex:NOISETEX;
float time : TIME;
float3 circleCol :CIRCLECOLOR;
float3 TargetPos:TARGETPOS;
float3 StartPos:STARTPOS;
float3 Upvec:UP;
float uvOffset : UVOFFSET;
float3 Eyepos:EYE;
float beamHeight : BH=1;
const float w = 0.1;
const float h = 0.2;
const float Wy = 0.1;
SamplerState mySampler{
	AddressU = Mirror;
	AddressV = Mirror;
};

float calcDenominatorFx()
{
	return w*(3 - 4 * w + w*w);
}

float calcDenominatorFy()
{
	return (-1 + Wy)*(-1 + Wy)*Wy;
}

float Fx(float x)
{
	float Cw = calcDenominatorFx();
	return x*x*x*(-((2 * h - 2 * h*w - w*w) / (w*Cw))) - x*x*((-3 * h + 3 * h*w*w + 2 * w*w*w) / (Cw*w)) - x*(6 * h - 6 * h*w - w*w*w);
}

float Fy(float x)
{
	float Cw = calcDenominatorFy();
	return -x*x*x*(-1 + 2 * Wy) / (Cw*Wy) - x*x*(1 - 3 * Wy*Wy) / (Cw*Wy) -x* (-2 + 3 * Wy) / Cw;
}

struct VS_OUTPUT
{
	float4 position:SV_Position;
	float4 rawPos:POSITION;
	float2 uv:UV;
};

VS_OUTPUT Sprite_VS(float4 pos:POSITION, float2 uv : UV)
{
	float3 s2t = TargetPos - StartPos;
		pos.xyz = s2t*Fx(uv.x) + (Fy(min(uv.x,1)))*Upvec;
	float3 diffH = cross(normalize(pos.xyz - Eyepos), normalize(s2t));
	pos.xyz += diffH*uv.y*beamHeight;
	VS_OUTPUT vo;
	vo.position = mul(pos, matWVP);
	vo.rawPos = pos;
	vo.uv = uv;
	return vo;
}

float pix(float x,float time)
{
	float n = 0.01, c = 100;
	return n*x*x - 2 * n*c*time + 1;
}

float4 TSprite_PS(VS_OUTPUT vo) :SV_Target
{
	if (time < 0)return 0.0.xxxx;
	float4 result = 1.0.xxxx;
	result = noiseTex.Sample(mySampler, float2(vo.uv.y*0.2+uvOffset, vo.uv.x));
	result.xyzw *= 1 - abs(time*0.5  - vo.uv.x);
	return result;
}


technique10 defaultTechnique
{
	pass TransParentColorEnabledPass
	{
		SetVertexShader(CompileShader(vs_4_0, Sprite_VS()));
		SetPixelShader(CompileShader(ps_4_0, TSprite_PS()));
	}

}
