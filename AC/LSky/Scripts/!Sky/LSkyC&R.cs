
//------------------------------
// Resources and components. //
//==============================
using System;
using UnityEngine;

namespace AC.LSky
{

	public partial class LSky : MonoBehaviour 
	{

		public bool      applySkybox    = true;  // Send skybox material to Lighting window.
		public Material  skyboxMaterial = null;  // Skybox material.
		public Texture2D moonTexture    = null;  // Moon texture.
		public Cubemap   outerSpaceCube = null;  // OuterSpace Background
		public Cubemap   starsCube      = null;  // RGB: Stars Field, Alpha: Stars Field Twinkle.
		public Cubemap   starsNoiseCube = null;  // Stars noise texture.
		public Texture2D blueNoiseTexture = null; // Blue noise texture for dithering.
		//------------------------------------------------------------------------------------

		[SerializeField] private Light m_SunLight = null;        // Sun light component.
		private Transform m_SunLightTransform;                   // Sun light transform component.

		[SerializeField] private Light m_MoonLight = null;       // Moon light component.
		private Transform m_MoonLightTransform;                  // Moon light transform component.
		//------------------------------------------------------------------------------------

		// Cache necessary components.
		void CacheComponents()
		{
			if(m_SunLight  != null) 
				m_SunLightTransform  = m_SunLight.transform;
			else
				m_SunLightTransform  = null;

			if(m_MoonLight != null) 
				m_MoonLightTransform = m_MoonLight.transform;
			else
				m_MoonLightTransform = null;

			if(!IsReady) enabled = false;
		}
		//------------------------------------------------------------------------------------

		// Check components and resources.
		public bool IsReady
		{
			get
			{ 
				if(m_SunLight == null)
					return false;

				if(m_SunLightTransform == null)
					return false;

				if(m_MoonLight == null)
					return false;

				if(m_MoonLightTransform == null)
					return false;

				if(moonTexture == null)
					return false;

				if(skyboxMaterial == null)
					return false;

				if(outerSpaceCube == null)
					return false;

				if(starsCube == null)
					return false;

				if(starsNoiseCube == null)
					return false;

				return true;
			}
		}
		//------------------------------------------------------------------------------------
	}
}
