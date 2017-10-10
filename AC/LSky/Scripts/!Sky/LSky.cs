

//-----------
// LSky.
//===========
using System;
using UnityEngine;

namespace AC.LSky
{
	[AddComponentMenu("AC/LSky/LSky Manager")]
	[ExecuteInEditMode] public partial class LSky : MonoBehaviour 
	{

	
		#region Unity
		void Awake()
		{

			if(!IsReady && Application.isPlaying) 
			{
				CacheComponents();
			}
		}

		void LateUpdate()
		{

			if(!IsReady) 
			{
				if (!Application.isPlaying)
					CacheComponents ();

				return;
			}

			SetEvaluateTime();
			Sun();
			Moon();
			OuterSpace();
			Atmosphere();
			ColorCorrection();
			Lighting();
		}
		#endregion

		#region Celestials
		void Sun()
		{

			Shader.SetGlobalMatrix("LSky_SunMatrix", m_SunLightTransform.worldToLocalMatrix); 
			Shader.SetGlobalVector("LSky_SunDir", SunDirection);

			if (!m_EnableSunDisc)
			{
				skyboxMaterial.DisableKeyword("LSKY_ENABLE_SUN_DISC");
				return;
			}

			skyboxMaterial.EnableKeyword("LSKY_ENABLE_SUN_DISC");
			skyboxMaterial.SetColor("_SunDiscColor", sunDiscColor.ColorValue);
			skyboxMaterial.SetFloat("_SunDiscSize", sunDiscSize.Value);
		}
		//--------------------------------------------------------------------------------------------------------------

		void Moon()
		{

			Shader.SetGlobalVector("LSky_MoonDir", MoonDirection);
			Shader.SetGlobalMatrix("LSky_MoonMatrix", m_MoonLightTransform.worldToLocalMatrix);

			if(!m_EnableMoon) 
			{
				skyboxMaterial.DisableKeyword("LSKY_ENABLE_MOON");
				return;
			}

			skyboxMaterial.EnableKeyword("LSKY_ENABLE_MOON");
			skyboxMaterial.SetTexture("_MoonTexture", moonTexture);
			skyboxMaterial.SetFloat("_MoonSize", moonSize.Value);
			skyboxMaterial.SetColor("_MoonColor", moonColor.ColorValue);
			skyboxMaterial.SetFloat("_MoonIntensity", moonIntensity.Value * moonMultiplier.Value);

		}
		//--------------------------------------------------------------------------------------------------------------

		public Matrix4x4 outerSpaceMatrix{ get{ return Matrix4x4.TRS (Vector3.zero, Quaternion.Euler(m_OuterSpaceOffset), Vector3.one); } }

		float SNSC;
		void OuterSpace()
		{

			if(m_EnableStars || m_EnableNebula)
			{
				skyboxMaterial.SetTexture("_OuterSpaceCube", outerSpaceCube);
				skyboxMaterial.SetMatrix("_OuterSpaceMatrix", outerSpaceMatrix);
			}

			if(!m_EnableStars) 
			{
				skyboxMaterial.DisableKeyword ("LSKY_ENABLE_STARS");
			} 
			else 
			{

				// Get stars twinkle speed.
				SNSC += Time.deltaTime * starsScintillationSpeed.Value; 

				// Get noise matrix.
				Matrix4x4 starsNoiseMatrix = Matrix4x4.TRS(Vector3.zero, Quaternion.Euler (SNSC, 0, 0), Vector3.one); 	
				//--------------------------------------------------------------------------------------------------------

				skyboxMaterial.EnableKeyword("LSKY_ENABLE_STARS");
				skyboxMaterial.SetTexture("_StarsCube", starsCube);
				skyboxMaterial.SetTexture("_StarsNoiseCube", starsNoiseCube);
				skyboxMaterial.SetMatrix("_StarsNoiseMatrix", starsNoiseMatrix);
				skyboxMaterial.SetColor("_StarsColor", starsColor.ColorValue);
				skyboxMaterial.SetFloat("_StarsIntensity", starsIntensity.Value);
				skyboxMaterial.SetFloat("_StarsScintillation", starsScintillation.Value);
			}

			if(!m_EnableNebula) 
			{
				skyboxMaterial.DisableKeyword("LSKY_ENABLE_NEBULA");
				return;
			}

			skyboxMaterial.EnableKeyword("LSKY_ENABLE_NEBULA");
			skyboxMaterial.SetColor("_NebulaColor", nebulaColor.ColorValue);
			skyboxMaterial.SetFloat("_NebulaIntensity", nebulaIntensity.Value);

		}
		//--------------------------------------------------------------------------------------------------------------

		#endregion


		#region Atmosphere.
		void Atmosphere()
		{

			// Atmosphere based in GPUGems2(Sean Oneil).

			// Inverse wave legths (reciprocal).
			Vector3 InvWavelength = new Vector3 () 
			{

				x = 1.0f / Mathf.Pow(wavelengthR.Value * 1e-3f, 4.0f),
				y = 1.0f / Mathf.Pow(wavelengthG.Value * 1e-3f, 4.0f),
				z = 1.0f / Mathf.Pow(wavelengthB.Value * 1e-3f, 4.0f)
			};
			//-----------------------------------------------------------------------------------------------------------------------------

			float kCameraHeight = 0.0001f;
			float kInnerRadius  = 1.0f;
			float kInnerRadius2 = 1.0f;
			float kOuterRadius  = 1.025f;
			float kOuterRadius2 = kOuterRadius * kOuterRadius;
			//-----------------------------------------------------------------------------------------------------------------------------

			float kScale               = (1.0f / (kOuterRadius - 1.0f));
			float kScaleDepth          = 0.25f;
			float kScaleOverScaleDepth = kScale / kScaleDepth;
			//-----------------------------------------------------------------------------------------------------------------------------

			float kSunBrightness = sunBrightness.Value;
			float kMie           = mie.Value;
			float kKmESun        = kMie * kSunBrightness;
			float kKm4PI         = kMie * 4.0f * Mathf.PI;
			//-----------------------------------------------------------------------------------------------------------------------------

			float kRayleigh      = 0.0025f * atmosphereThickness.Value;
			float kKrESun        = kRayleigh * kSunBrightness;
			float kKr4PI         = kRayleigh * 4.0f * Mathf.PI;
			//-----------------------------------------------------------------------------------------------------------------------------


			Shader.SetGlobalFloat("LSky_kCameraHeight", kCameraHeight);
			Shader.SetGlobalFloat("LSky_kInnerRadius",  kInnerRadius);
			Shader.SetGlobalFloat("LSky_kInnerRadius2", kInnerRadius2);
			Shader.SetGlobalFloat("LSky_kOuterRadius",  kOuterRadius);
			Shader.SetGlobalFloat("LSky_kOuterRadius2", kOuterRadius2);
			//-----------------------------------------------------------------------------------------------------------------------------

			Shader.SetGlobalFloat("LSky_kScale",               kScale);
			Shader.SetGlobalFloat("LSky_kScaleDepth",          kScaleDepth);
			Shader.SetGlobalFloat("LSky_kScaleOverScaleDepth", kScaleOverScaleDepth);
			//-----------------------------------------------------------------------------------------------------------------------------

			Shader.SetGlobalFloat("LSky_kKm4PI",  kKm4PI);
			Shader.SetGlobalFloat("LSky_kKmESun", kKmESun);
			Shader.SetGlobalFloat("LSky_kKrESun", kKrESun);
			Shader.SetGlobalFloat("LSky_kKr4PI",  kKr4PI);
			//-----------------------------------------------------------------------------------------------------------------------------

			Shader.SetGlobalVector("LSky_InvWavelength", InvWavelength);
			//-----------------------------------------------------------------------------------------------------------------------------


			Shader.SetGlobalColor("LSky_DayAtmosphereTint", dayAtmosphereTint.ColorValue);
			Shader.SetGlobalVector("LSky_SunBetaMiePhase", BetaMiePhase(sunMieAnisotropy.Value, true));
			Shader.SetGlobalFloat("LSky_SunMieScattering", sunMieScattering.Value);
			Shader.SetGlobalColor("LSky_SunMieColor", sunMieColor.ColorValue);
			//-----------------------------------------------------------------------------------------------------------------------------

			if(m_NightColorType == LSkyNightColorType.Atmospheric)
			{
				Shader.EnableKeyword("LSKY_NIGHT_COLOR_ATMOSPHERIC"); 
				Shader.DisableKeyword("LSKY_NIGHT_COLOR_SIMPLE"); 
			}
			else
			{
				Shader.EnableKeyword("LSKY_NIGHT_COLOR_SIMPLE"); 
				Shader.DisableKeyword("LSKY_NIGHT_COLOR_ATMOSPHERIC"); 
			}

			if(m_MoonInfluence)
				Shader.EnableKeyword("LSKY_MOON_INFLUENCE"); 
			else 
				Shader.DisableKeyword("LSKY_MOON_INFLUENCE"); 

			Shader.SetGlobalColor("LSky_NightAtmosphereTint", nightAtmosphereTint.ColorValue);
			//-----------------------------------------------------------------------------------------------------------------------------

			Shader.SetGlobalVector("LSky_MoonBetaMiePhase", BetaMiePhase(moonMieAnisotropy.Value, false));
			Shader.SetGlobalFloat("LSky_MoonMieScattering", moonMieScattering.Value * moonMieMultiplier.Value);
			Shader.SetGlobalColor("LSky_MoonMieColor", moonMieColor.ColorValue);

		}

		/// <summary>
		/// Beta mie phase(One part of the HenyeyGreenstein).
		/// </summary>
		/// <returns>The mie phase.</returns>
		/// <param name="g">The green component.</param>
		/// <param name="cornnete">If set to <c>true</c> cornnete.</param>
		public Vector3 BetaMiePhase(float g, bool HQ)
		{

			Vector3 result; 
			{
				float g2 = g * g; 
				result.x = HQ ? (1.0f - g2) / (2.0f + g2) : 1.0f - g2;
				result.y = 1.0f + g2;
				result.z = 2.0f * g;
			}
			return result;
		}
		#endregion

		#region Color Correction

		void ColorCorrection()
		{

			Shader.SetGlobalFloat("LSky_Exposure", exposure.Value);

			if(!m_HDR) Shader.DisableKeyword("LSKY_HDR"); 
			else     Shader.EnableKeyword("LSKY_HDR");
			//------------------------------------------------------------------

			if(m_EnableDithering) 
			{
				Shader.EnableKeyword("LSKY_DITHERING"); 
				Shader.SetGlobalTexture("LSky_BlueNoiseTex", blueNoiseTexture);
			} 
			else
			{
				Shader.DisableKeyword("LSKY_DITHERING"); 
			}

			switch(m_ColorSpace)
			{

				case LSkyColorSpace.Gamma:
					Shader.EnableKeyword("LSKY_GAMMA_COLOR_SPACE");
				break;

				case LSkyColorSpace.Linear:
					Shader.DisableKeyword("LSKY_GAMMA_COLOR_SPACE");
				break;

				case LSkyColorSpace.Automatic:
				if(QualitySettings.activeColorSpace == ColorSpace.Gamma)
					Shader.EnableKeyword("LSKY_GAMMA_COLOR_SPACE");
				else
					Shader.DisableKeyword("LSKY_GAMMA_COLOR_SPACE");
				break;
			}
			//------------------------------------------------------------------
		}
		#endregion

		#region Lighting


		private bool m_SunLightEnable;
		void Lighting()
		{

			m_SunLightEnable = !CheckDirLightEnable(Mathf.Max(0.0f, Mathf.Min(1.0f, SunDirection.y + 0.30f)), m_SunLightThreshold);

			m_SunLight.color     = sunLightColor.ColorValue;
			m_SunLight.intensity = sunLightIntensity.Value;
			m_SunLight.enabled   = m_SunLightEnable;
			//---------------------------------------------------------------------------------------------------------------------------

			m_MoonLight.color     = moonLightColor.ColorValue;
			m_MoonLight.intensity = moonLightIntensity.Value * moonLightMultiplier.Value;
			m_MoonLight.enabled   = !m_SunLight.enabled;
			//---------------------------------------------------------------------------------------------------------------------------

			if(applySkybox)
				RenderSettings.skybox = skyboxMaterial;


			RenderSettings.ambientMode = m_AmbientMode;
			switch(m_AmbientMode) 
			{

				case UnityEngine.Rendering.AmbientMode.Skybox:
				DynamicGI.UpdateEnvironment();
				RenderSettings.ambientIntensity = ambientIntensity.Value;
				break;

				case UnityEngine.Rendering.AmbientMode.Trilight: 
				RenderSettings.ambientSkyColor     = ambientSkyColor.ColorValue;
				RenderSettings.ambientEquatorColor = ambientEquatorColor.ColorValue;
				break;

				case UnityEngine.Rendering.AmbientMode.Flat :
				RenderSettings.ambientSkyColor     = ambientSkyColor.ColorValue;
				break;

			}

			RenderSettings.ambientGroundColor   = ambientGroundColor.ColorValue;
			Shader.SetGlobalColor("LSky_GroundColor", ambientGroundColor.ColorValue);
			//---------------------------------------------------------------------------------------------------------------------------

			RenderSettings.fog = m_EnableUnityFog;
			if(m_EnableUnityFog)
			{
				RenderSettings.fogMode  = m_UnityFogMode;
				RenderSettings.fogColor = unityFogColor.ColorValue;

				if (m_UnityFogMode == FogMode.Linear)
				{
					RenderSettings.fogStartDistance = unityFogStartDistance.Value;
					RenderSettings.fogEndDistance   = unityFogEndDistance.Value;
				}
				else
					RenderSettings.fogDensity = unityFogDensity.Value;

			}
		}

		bool CheckDirLightEnable(float theta, float threshold = 0.20f)
		{
			return(Mathf.Abs(theta) < threshold) ? true : false;
		}

		#endregion


		#region Day States

		public bool IsDay{ get{ return m_SunLightEnable; } }
		public bool IsNight{ get{ return !IsDay; }}

		#endregion


	}
}
