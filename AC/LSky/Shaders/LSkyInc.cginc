
#ifndef LSKY_INCLUDED
#define LSKY_INCLUDED

/////////////////////
/// LSky Include. ///
/////////////////////

#include "LSkyVariablesInc.cginc"  

#define LSky_PI14 0.07957747  // (1 / (4*PI))
//-------------------------------------------------------------------------------------------

inline half Desaturate(half3 color)
{
 	return (color.r + color.g + color.b) * 0.3333333h;
}
//-------------------------------------------------------------------------------------------

// Color correction
//#define DARKNESS(color) pow(color, LSky_Darkness);
#define FAST_TONEMAPING(color) 1.0 - exp(LSky_Exposure * -color)
#define GAMMA_TO_LINEAR(color) pow(color, 0.45454545) // aproximate.

inline void ColorCorrection(inout half3 color)
{

	color = clamp(color, 0.01, color);   // Prevent divide by zero.
	color = sqrt(color * color * color); // color ^ 1.5.
	//color *= color; // color ^ 2.

	#ifndef LSKY_HDR
		color = FAST_TONEMAPING(color);
	#else
		color *= LSky_Exposure;
	#endif

	#ifdef LSKY_GAMMA_COLOR_SPACE
		color = GAMMA_TO_LINEAR(color);
	#endif

}

inline void ColorCorrection(inout half3 color, inout half3 groundColor)
{


	color = clamp(color, 0.01, color);   // Prevent divide by zero.
	color = sqrt(color * color * color); // color ^ 1.5.
	//color *= color; // color ^ 2.

	#ifndef LSKY_HDR
		color = FAST_TONEMAPING(color);
	#else
		color *= LSky_Exposure;
	#endif

	#ifdef LSKY_GAMMA_COLOR_SPACE
		color = GAMMA_TO_LINEAR(color);
	#else
		groundColor *= groundColor;
	#endif
}

// Dithering.
// See: http://momentsingraphics.de/?p=127
inline float BlueNoiseDithering(float2 uv)
{

	float blueNoise = tex2D(LSky_BlueNoiseTex, uv.xy * 50).a * 2.0 - 1.0;
	// blueNoise = mad(blueNoise, 2.0, 1.0);

	blueNoise = sign(blueNoise) * (1.0 - sqrt(1.0 - abs(blueNoise)));

	return blueNoise / 255;
}

//-------------------------------------------------------------------------------------------

// Sun.
uniform half  _SunDiscSize;
uniform half3 _SunDiscColor;

#define SUN_COORDS(vertex) mul((float3x3)LSky_SunMatrix, vertex.xyz)

inline half3 SunDisc(float3 dir)
{

	half    dist = length(dir);
	return  100 * (1.0-step(_SunDiscSize, dist)) * _SunDiscColor;
}
//-------------------------------------------------------------------------------------------

// Moon.
uniform sampler2D _MoonTexture;
uniform half      _MoonSize;
uniform half4     _MoonColor;
uniform half      _MoonIntensity;

#define MOON_COORDS(vertex) (mul((float3x3)LSky_MoonMatrix, vertex.xyz) / _MoonSize + 0.5)

inline half4 Moon(float3 coords, float cosTheta)
{

	half4 color  = tex2D(_MoonTexture, coords.xy) * saturate(cosTheta);
	color.rgb   *= _MoonColor.rgb *  _MoonIntensity;
	half mask    = (1.0 - color.a);

	return half4(color.rgb, mask); // RGB = Moon, Alpha = Mask.
}

//-------------------------------------------------------------------------------------------

// Outer space.
uniform samplerCUBE _OuterSpaceCube;
uniform samplerCUBE _StarsCube;
uniform samplerCUBE _StarsNoiseCube;

uniform float4x4 _OuterSpaceMatrix;
uniform float4x4 _StarsNoiseMatrix;

uniform half3 _StarsColor;
uniform half  _StarsIntensity;
uniform half  _StarsScintillation;
uniform half3 _NebulaColor;
uniform half  _NebulaIntensity;


#define OUTER_SPACE_COORDS(sunCoords) mul((float3x3)_OuterSpaceMatrix, sunCoords.xyz)

#define STARS_NOISE_COORDS(sunCoords) mul((float3x3)_StarsNoiseMatrix, sunCoords.xyz)

inline half StarsScintillation(float3 coords)
{
	half noise = texCUBE(_StarsNoiseCube, coords).r;
	return lerp(1, 2 * noise,  _StarsScintillation);
}



inline half3 OuterSpace(float3 coords, float3 noiseCoords)
{

	half3 starsField = 0.0, nebula = 0.0;

	#ifdef LSKY_ENABLE_STARS

		half4 starsCube = texCUBE(_StarsCube, coords);
		half noise = StarsScintillation(noiseCoords);

		half a = starsCube.a * noise;

		starsField = (starsCube.rgb + a  ) * _StarsColor * _StarsIntensity;

	#endif

	#ifdef LSKY_ENABLE_NEBULA

		nebula = texCUBE(_OuterSpaceCube, coords).rgb * _NebulaColor  * _NebulaIntensity;

	#endif

	return starsField + nebula;

}

inline half3 OuterSpace(float3 coords, float3 noiseCoords, half nebulaExponent)
{

	half3 starsField = 0.0, nebula = 0.0;

	#ifdef LSKY_ENABLE_STARS

		half4 starsCube = texCUBE(_StarsCube, coords);
		half noise = StarsScintillation(noiseCoords);

		half a = starsCube.a * noise;

		starsField = (starsCube.rgb + a  ) * _StarsColor * _StarsIntensity;

	#endif

	#ifdef LSKY_ENABLE_NEBULA

		half3 nebulaCube = texCUBE(_OuterSpaceCube, coords).rgb;

		if(abs(nebulaExponent) > 1)
			nebulaCube.rgb = pow(nebulaCube.rgb, nebulaExponent);

		nebula = nebulaCube  * _NebulaColor  * _NebulaIntensity;

	#endif

	return starsField + nebula;

}

//-------------------------------------------------------------------------------------------



#endif