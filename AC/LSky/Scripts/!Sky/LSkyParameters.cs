
///////////////////
/// Parameters. ///
///////////////////

using System;
using UnityEngine;


namespace AC.LSky
{

	public partial class LSky : MonoBehaviour 
	{



		#region Atmosphere

		// Wavelenghts.
		[LSkyCurveRange(0.0f, 1000f, 0.0f, 0.0f, 1.0f, 1000f)]
		public LSkyCurve wavelengthR = new LSkyCurve()
		{

			CurveMode = LSkyCurveMode.floatValue,
			FValue    = 650f,
			Curve     = AnimationCurve.Linear(0.0f, 650f, 1.0f, 650f),
			EvaluateTime = 0.0f
		};

		[LSkyCurveRange(0.0f, 1000f, 0.0f, 0.0f, 1.0f, 1000f)]
		public LSkyCurve wavelengthG = new LSkyCurve()
		{

			CurveMode = LSkyCurveMode.floatValue,
			FValue    = 570f,
			Curve     = AnimationCurve.Linear(0.0f, 570f, 1.0f, 570f),
			EvaluateTime = 0.0f
		};


		[LSkyCurveRange(0.0f, 1000f, 0.0f, 0.0f, 1.0f, 1000f)]
		public LSkyCurve wavelengthB = new LSkyCurve()
		{

			CurveMode = LSkyCurveMode.floatValue,
			FValue    = 475f,
			Curve     = AnimationCurve.Linear(0.0f, 475f, 1.0f, 475f),
			EvaluateTime = 0.0f
		};

		[LSkyCurveRange(0.0f, 50f, 0.0f, 0.0f, 1.0f, 50f)]
		public LSkyCurve atmosphereThickness = new LSkyCurve()
		{
			CurveMode = LSkyCurveMode.floatValue,
			FValue    = 1.0f,
			Curve     = AnimationCurve.Linear(0.0f, 1.0f, 1.0f, 1.0f),
			EvaluateTime = 0.0f
		};
		//----------------------------------------------------------------------------------------

		// Tint
		public LSkyGradient dayAtmosphereTint = new LSkyGradient()
		{

			GradientMode = LSkyGradientMode.gradientValue,
			Color        = Color.white,  
			Gradient     = new Gradient()
			{
				colorKeys = new GradientColorKey[]
				{
					new GradientColorKey(new Color(1.0f, 1.0f, 1.0f, 1.0f), 0.0f),
					new GradientColorKey(new Color(1.0f, 1.0f, 1.0f, 1.0f), 0.5f),
					new GradientColorKey(new Color(1.0f, 1.0f, 1.0f, 1.0f), 1.0f)
				},

				alphaKeys = new GradientAlphaKey[] 
				{
					new GradientAlphaKey(1.0f, 0.0f),
					new GradientAlphaKey(1.0f, 1.0f)
				}
			},
			EvaluateTime = 0.0f
		};

		[SerializeField]
		private LSkyNightColorType m_NightColorType = LSkyNightColorType.Atmospheric;
		public LSkyNightColorType NightColorType 
		{
			get{ return m_NightColorType; }
			set{ m_NightColorType = value;  }
		}
			
		[SerializeField]
		private bool m_MoonInfluence = true; // Moon affected in atmosphere.
		public bool MoonInfluence
		{
			get{ return m_MoonInfluence; }
			set{ m_MoonInfluence = value; }
		}

		public LSkyGradient nightAtmosphereTint = new LSkyGradient()
		{

			GradientMode    = LSkyGradientMode.gradientValue,
			Color   = new Color(0.039f, 0.079f, 0.111f, 1.0f),  

			Gradient     = new Gradient()
			{
				colorKeys = new GradientColorKey[]
				{
					new GradientColorKey(new Color(0.039f, 0.079f, 0.111f, 1.0f), 0.0f),
					new GradientColorKey(new Color(0.039f, 0.079f, 0.111f, 1.0f), 0.5f),
					new GradientColorKey(new Color(0.039f, 0.079f, 0.111f, 1.0f), 1.0f)
				},

				alphaKeys = new GradientAlphaKey[] 
				{
					new GradientAlphaKey(1.0f, 0.0f),
					new GradientAlphaKey(1.0f, 1.0f)
				}
			},
			EvaluateTime = 0.0f

		};
		//----------------------------------------------------------------------------------------



		//----------------------------------------------------------------------------------------

		[LSkyCurveRange(0.0f, 100f, 0.0f, 0.0f, 1.0f, 100f)]
		public LSkyCurve sunBrightness = new LSkyCurve()
		{

			CurveMode    = LSkyCurveMode.floatValue,
			FValue       = 30f,
			Curve        = AnimationCurve.Linear(0.0f, 30f, 1.0f, 30f),
			EvaluateTime = 0.0f
		};

	
		[LSkyCurveRange(0.0f, 0.5f, 0.0f, 0.0f, 1.0f, 0.5f)]
		public LSkyCurve mie = new LSkyCurve()
		{

			CurveMode    = LSkyCurveMode.floatValue,
			FValue       = 0.010f,
			Curve        = AnimationCurve.Linear(0.0f, 0.010f, 1.0f, 0.010f),
			EvaluateTime = 0.0f

		};
		//----------------------------------------------------------------------------------------

		public LSkyGradient sunMieColor = new LSkyGradient()
		{

			GradientMode = LSkyGradientMode.colorValue,
			Color   = new Color(1.0f, 0.95f, 0.83f, 1.0f),  

			Gradient = new Gradient()
			{
				colorKeys = new GradientColorKey[]
				{
					new GradientColorKey(new Color(1.0f, 0.95f, 0.83f, 1.0f), 0.0f),
					new GradientColorKey(new Color(1.0f, 0.95f, 0.83f, 1.0f), 0.5f),
					new GradientColorKey(new Color(1.0f, 0.95f, 0.83f, 1.0f), 1.0f)
				},

				alphaKeys = new GradientAlphaKey[] 
				{
					new GradientAlphaKey(1.0f, 0.0f),
					new GradientAlphaKey(1.0f, 1.0f)
				}
			},

			EvaluateTime = 0.0f

		};

		[LSkyCurveRange(0.0f, 0.999f, 0.0f, 0.0f, 1.0f, 0.999f)]
		public LSkyCurve sunMieAnisotropy = new LSkyCurve()
		{

			CurveMode    = LSkyCurveMode.floatValue,
			FValue       = 0.75f,
			Curve        = AnimationCurve.Linear(0.0f, 0.75f, 1.0f, 0.75f),
			EvaluateTime = 0.0f
		};

		[LSkyCurveRange(0.0f, 5.0f, 0.0f, 0.0f, 1.0f, 5.0f)]
		public LSkyCurve sunMieScattering = new LSkyCurve()
		{

			CurveMode    = LSkyCurveMode.floatValue,
			FValue       = 0.5f,
			Curve        = AnimationCurve.Linear(0.0f, 0.5f, 1.0f, 0.5f),
			EvaluateTime = 0.0f
		};

		//----------------------------------------------------------------------------------------

		public LSkyGradient moonMieColor = new LSkyGradient()
		{

			GradientMode = LSkyGradientMode.colorValue,
			Color        = new Color(0.507f, 0.695f, 1.0f, 1.0f),  

			Gradient = new Gradient()
			{
				colorKeys = new GradientColorKey[]
				{
					new GradientColorKey(new Color(0.507f, 0.695f,  1.0f, 1.0f), 0.0f),
					new GradientColorKey(new Color(0.507f, 0.6951f, 1.0f, 1.0f), 0.5f),
					new GradientColorKey(new Color(0.507f, 0.695f,  1.0f, 1.0f), 1.0f)
				},

				alphaKeys = new GradientAlphaKey[] 
				{
					new GradientAlphaKey(1.0f, 0.0f),
					new GradientAlphaKey(1.0f, 1.0f)
				}
			},

			EvaluateTime = 0.0f
		};

		[LSkyCurveRange(0.0f, 0.999f, 0.0f, 0.0f, 1.0f, 0.999f)]
		public LSkyCurve moonMieAnisotropy = new LSkyCurve()
		{

			CurveMode    = LSkyCurveMode.floatValue,
			FValue       = 0.93f,
			Curve        = AnimationCurve.Linear(0.0f, 0.93f, 1.0f, 0.93f),
			EvaluateTime = 0.0f

		};

		[LSkyCurveRange(0.0f, 5.0f, 0.0f, 0.0f, 1.0f, 5.0f)]
		public LSkyCurve moonMieScattering = new LSkyCurve()
		{

			CurveMode    = LSkyCurveMode.floatValue,
			FValue       = 0.5f,
			Curve        = AnimationCurve.Linear(0.0f, 0.5f, 1.0f, 0.5f),
			EvaluateTime = 0.0f
		};

		[LSkyCurveRange(0.0f, 5.0f, 0.0f, 0.0f, 1.0f, 5.0f)]
		public LSkyCurve moonMieMultiplier = new LSkyCurve()
		{

			CurveMode    = LSkyCurveMode.curveValue,
			FValue   = 1.0f,
			Curve        = AnimationCurve.Linear(0.0f, 1.0f, 1.0f, 1.0f),
			EvaluateTime = 0.0f

		};

		#endregion

		#region Celestials
		[SerializeField] private bool m_EnableSunDisc = true;

		[LSkyCurveRange(0.0f, 0.5f, 0.0f, 0.0f, 1.0f, 0.5f)]
		public LSkyCurve sunDiscSize = new LSkyCurve()
		{

			CurveMode   = LSkyCurveMode.floatValue,
			FValue   = 0.05f,
			Curve        = AnimationCurve.Linear(0.0f, 0.05f, 1.0f, 0.05f),
			EvaluateTime = 0.0f
		};

		public LSkyGradient sunDiscColor = new LSkyGradient()
		{

			GradientMode = LSkyGradientMode.colorValue,
			Color        = Color.white,  
			Gradient     = new Gradient()
			{
				colorKeys = new GradientColorKey[]
				{
					new GradientColorKey(new Color(1.0f, 1.0f, 1.0f, 1.0f), 0.0f),
					new GradientColorKey(new Color(1.0f, 1.0f, 1.0f, 1.0f), 0.5f),
					new GradientColorKey(new Color(1.0f, 1.0f, 1.0f, 1.0f), 1.0f)
				},

				alphaKeys = new GradientAlphaKey[] 
				{
					new GradientAlphaKey(1.0f, 0.0f),
					new GradientAlphaKey(1.0f, 1.0f)
				}
			},

			EvaluateTime = 0.0f
		};
		//----------------------------------------------------------------------------------------

		[SerializeField] private bool m_EnableMoon = true;

		[LSkyCurveRange(0.0f, 1.0f, 0.0f, 0.0f, 1.0f, 1.0f)]
		public LSkyCurve moonSize = new LSkyCurve()
		{

			CurveMode    = LSkyCurveMode.floatValue,
			FValue   = 0.3f,
			Curve        = AnimationCurve.Linear(0.0f, 0.3f, 1.0f, 0.3f),
			EvaluateTime = 0.0f

		};


		public LSkyGradient moonColor = new LSkyGradient()
		{

			GradientMode   = LSkyGradientMode.colorValue,
			Color   = new Color(1.0f, 1.0f, 1.0f, 1.0f),  

			Gradient     = new Gradient()
			{
				colorKeys = new GradientColorKey[]
				{
					new GradientColorKey(new Color(1.0f, 1.0f, 1.0f, 1.0f), 0.0f),
					new GradientColorKey(new Color(1.0f, 1.0f, 1.0f, 1.0f), 0.5f),
					new GradientColorKey(new Color(1.0f, 1.0f, 1.0f, 1.0f), 1.0f)
				},

				alphaKeys = new GradientAlphaKey[] 
				{
					new GradientAlphaKey(1.0f, 0.0f),
					new GradientAlphaKey(1.0f, 1.0f)
				}
			},

			EvaluateTime = 0.0f

		};

		[LSkyCurveRange(0.0f, 5.0f, 0.0f, 0.0f, 1.0f, 5.0f)]
		public LSkyCurve moonIntensity = new LSkyCurve()
		{
			CurveMode    = LSkyCurveMode.floatValue,
			FValue       = 1.0f,
			Curve        = AnimationCurve.Linear(0.0f, 1.0f, 1.0f, 1.0f),
			EvaluateTime = 0.0f

		};

		[LSkyCurveRange(0.0f, 5.0f, 0.0f, 0.0f, 1.0f, 5.0f)]
		public LSkyCurve moonMultiplier = new LSkyCurve()
		{

			CurveMode    = LSkyCurveMode.floatValue,
			FValue       = 1.0f,
			Curve        = AnimationCurve.Linear(0.0f, 1.0f, 1.0f, 1.0f),
			EvaluateTime = 0.0f

		};

		[SerializeField] private bool m_EnableStars = true;

		public LSkyGradient starsColor = new LSkyGradient()
		{

			GradientMode   = LSkyGradientMode.gradientValue,
			Color         = new Color(1.0f, 1.0f, 1.0f, 1.0f),  

			Gradient      = new Gradient()
			{
				colorKeys = new GradientColorKey[]
				{
					new GradientColorKey(new Color(1.0f, 1.0f, 1.0f, 1.0f), 0.0f),
					new GradientColorKey(new Color(1.0f, 1.0f, 1.0f, 1.0f), 0.5f),
					new GradientColorKey(new Color(1.0f, 1.0f, 1.0f, 1.0f), 1.0f)
				},

				alphaKeys = new GradientAlphaKey[] 
				{
					new GradientAlphaKey(1.0f, 0.0f),
					new GradientAlphaKey(1.0f, 1.0f)
				}
			},

			EvaluateTime = 0.0f

		};


		[LSkyCurveRange(0.0f, 10f, 0.0f, 0.0f, 1.0f, 10f)]
		public LSkyCurve starsIntensity = new LSkyCurve()
		{

			CurveMode    = LSkyCurveMode.curveValue,
			FValue       = 1.0f,
			Curve        = AnimationCurve.Linear(0.0f, 1.0f, 1.0f, 1.0f),
			EvaluateTime = 0.0f
		};


		[LSkyCurveRange(0.0f, 1.0f, 0.0f, 0.0f, 1.0f, 1.0f)]
		public LSkyCurve starsScintillation = new LSkyCurve()
		{

			CurveMode    = LSkyCurveMode.floatValue,
			FValue       = 0.7f,
			Curve        = AnimationCurve.Linear(0.0f, 0.7f, 1.0f, 0.7f),
			EvaluateTime = 0.0f

		};

		[LSkyCurveRange(0.0f, 50f, 0.0f, 0.0f, 1.0f, 50f)]
		public LSkyCurve starsScintillationSpeed = new LSkyCurve()
		{

			CurveMode    = LSkyCurveMode.floatValue,
			FValue   = 10f,
			Curve        = AnimationCurve.Linear(0.0f, 10f, 1.0f, 10f),
			EvaluateTime = 0.0f

		};
		//----------------------------------------------------------------------------------------


		[SerializeField] private bool m_EnableNebula = true;

		public LSkyGradient nebulaColor = new LSkyGradient()
		{

			GradientMode   = LSkyGradientMode.gradientValue,
			Color          = new Color(1.0f, 1.0f, 1.0f, 1.0f),  

			Gradient = new Gradient()
			{
				colorKeys = new GradientColorKey[]
				{
					new GradientColorKey(new Color(1.0f, 1.0f, 1.0f, 1.0f), 0.0f),
					new GradientColorKey(new Color(1.0f, 1.0f, 1.0f, 1.0f), 0.5f),
					new GradientColorKey(new Color(1.0f, 1.0f, 1.0f, 1.0f), 1.0f)
				},

				alphaKeys = new GradientAlphaKey[] 
				{
					new GradientAlphaKey(1.0f, 0.0f),
					new GradientAlphaKey(1.0f, 1.0f)
				}
			},

			EvaluateTime = 0.0f

		};

		[LSkyCurveRange(0.0f, 10f, 0.0f, 0.0f, 1.0f, 10f)]
		public LSkyCurve nebulaIntensity = new LSkyCurve()
		{

			CurveMode    = LSkyCurveMode.floatValue,
			FValue       = 1.0f,
			Curve        = AnimationCurve.Linear(0.0f, 1.0f, 1.0f, 1.0f),
			EvaluateTime = 0.0f
		};

		[SerializeField] private Vector3 m_OuterSpaceOffset = Vector3.zero;
		public Vector3 OuterSpaceOffset
		{ 
			get { return m_OuterSpaceOffset; }
			set{ m_OuterSpaceOffset = value; }
		}

		#endregion


		#region Color Correction

		[SerializeField] private bool m_HDR = false;
		[SerializeField] private bool m_EnableDithering = true; 

		[LSkyCurveRange(0.0f, 10f, 0.0f, 0.0f, 1.0f, 10f)]
		public LSkyCurve exposure = new LSkyCurve()
		{

			CurveMode   = LSkyCurveMode.floatValue,
			FValue   = 1.3f,
			Curve        = AnimationCurve.Linear(0.0f, 1.3f, 1.0f, 1.3f),
			EvaluateTime = 0.0f

		};
		//--------------------------------------------------------------------------------------

		enum LSkyColorSpace{ Gamma, Linear, Automatic }
		[SerializeField] private LSkyColorSpace m_ColorSpace = LSkyColorSpace.Automatic;

		#endregion

		#region Lighting
		public LSkyGradient sunLightColor = new LSkyGradient()
		{

			GradientMode    = LSkyGradientMode.gradientValue,
			Color   = new Color(1.0f, 1.0f, 1.0f, 1.0f),  

			Gradient     = new Gradient()
			{
				colorKeys = new GradientColorKey[]
				{
					new GradientColorKey(new Color(1.0f, 0.956f, 0.839f, 1.0f), 0.0f),
					new GradientColorKey(new Color(1.0f, 0.956f, 0.839f, 1.0f), 0.25f),
					new GradientColorKey(new Color(1.0f, 0.523f, 0.264f, 1.0f), 0.50f),
					new GradientColorKey(new Color(0.0f, 0.0f, 0.0f, 0.0f), 0.55f),
					new GradientColorKey(new Color(0.0f, 0.0f, 0.0f, 0.0f), 1.0f)
				},

				alphaKeys = new GradientAlphaKey[] 
				{
					new GradientAlphaKey(1.0f, 0.0f),
					new GradientAlphaKey(1.0f, 1.0f)
				}
			},

			EvaluateTime = 0.0f

		};

		[LSkyCurveRange(0.0f, 10f, 0.0f, 0.0f, 1.0f, 10f)]
		public LSkyCurve sunLightIntensity = new LSkyCurve()
		{

			CurveMode    = LSkyCurveMode.floatValue,
			FValue   = 1.0f,
			Curve        = AnimationCurve.Linear(0.0f, 1.0f, 1.0f, 1.0f),
			EvaluateTime = 0.0f

		};

		[SerializeField] private float m_SunLightThreshold = 0.20f;
		public float SunLightThreshold{ get { return m_SunLightThreshold; } }

		public LSkyGradient moonLightColor = new LSkyGradient()
		{

			GradientMode  = LSkyGradientMode.colorValue,
			Color         = new Color(0.632f, 0.794f, 1.0f, 1.0f),  

			Gradient     = new Gradient()
			{
				colorKeys = new GradientColorKey[]
				{
					new GradientColorKey(new Color(0.632f, 0.794f, 1.0f, 1.0f), 0.0f),
					new GradientColorKey(new Color(0.632f, 0.794f, 1.0f, 1.0f), 0.5f),
					new GradientColorKey(new Color(0.632f, 0.794f, 1.0f, 1.0f), 1.0f)
				},

				alphaKeys = new GradientAlphaKey[] 
				{
					new GradientAlphaKey(1.0f, 0.0f),
					new GradientAlphaKey(1.0f, 1.0f)
				}
			},

			EvaluateTime = 0.0f

		};
		//--------------------------------------------------------------------------------------

		[LSkyCurveRange(0.0f, 10f, 0.0f, 0.0f, 1.0f, 10f)]
		public LSkyCurve moonLightIntensity = new LSkyCurve()
		{

			CurveMode    = LSkyCurveMode.floatValue,
			FValue       = 0.3f,
			Curve        = AnimationCurve.Linear(0.0f, 0.3f, 1.0f, 0.3f),
			EvaluateTime = 0.0f
		};

		[LSkyCurveRange(0.0f, 5.0f, 0.0f, 0.0f, 1.0f, 5.0f)]
		public LSkyCurve moonLightMultiplier = new LSkyCurve()
		{

			CurveMode   = LSkyCurveMode.floatValue,
			FValue   = 1.0f,
			Curve        = AnimationCurve.Linear(0.0f, 1.0f, 1.0f, 1.0f),
			EvaluateTime = 0.0f

		};
		//--------------------------------------------------------------------------------------

		[SerializeField]
		private UnityEngine.Rendering.AmbientMode m_AmbientMode = UnityEngine.Rendering.AmbientMode.Skybox;

		public LSkyGradient ambientSkyColor = new LSkyGradient()
		{

			GradientMode   = LSkyGradientMode.gradientValue,
			Color   = new Color(0.443f, 0.552f, 0.737f, 1.0f),  

			Gradient     = new Gradient()
			{
				colorKeys = new GradientColorKey[]
				{
					new GradientColorKey(new Color(0.443f, 0.552f, 0.737f, 1.0f), 0.0f),
					new GradientColorKey(new Color(0.443f, 0.552f, 0.737f, 1.0f), 0.45f),
					new GradientColorKey(new Color(0.231f, 0.290f, 0.352f, 1.0f), 0.50f),
					new GradientColorKey(new Color(0.047f, 0.094f, 0.180f, 1.0f), 0.55f),
					new GradientColorKey(new Color(0.047f, 0.094f, 0.180f, 1.0f), 1.0f)
				},

				alphaKeys = new GradientAlphaKey[] 
				{
					new GradientAlphaKey(1.0f, 0.0f),
					new GradientAlphaKey(1.0f, 1.0f)
				}
			},

			EvaluateTime = 0.0f
		};

		public LSkyGradient ambientEquatorColor = new LSkyGradient()
		{

			GradientMode    = LSkyGradientMode.gradientValue,
			Color   = new Color(0.901f, 0.956f, 0.968f, 1.0f),  

			Gradient     = new Gradient()
			{
				colorKeys = new GradientColorKey[]
				{
					new GradientColorKey(new Color(0.901f, 0.956f, 0.968f, 1.0f), 0.0f),
					new GradientColorKey(new Color(0.901f, 0.956f, 0.968f, 1.0f), 0.45f),
					new GradientColorKey(new Color(0.650f, 0.607f, 0.349f, 1.0f), 0.50f),
					new GradientColorKey(new Color(0.121f, 0.239f, 0.337f, 1.0f), 0.55f),
					new GradientColorKey(new Color(0.121f, 0.239f, 0.337f, 1.0f), 1.0f)
				},

				alphaKeys = new GradientAlphaKey[] 
				{
					new GradientAlphaKey(1.0f, 0.0f),
					new GradientAlphaKey(1.0f, 1.0f)
				}
			},

			EvaluateTime = 0.0f

		};

		public LSkyGradient ambientGroundColor = new LSkyGradient()
		{

			GradientMode    = LSkyGradientMode.gradientValue,
			Color   = new Color(0.466f, 0.435f, 0.415f, 1.0f),  

			Gradient     = new Gradient()
			{
				colorKeys = new GradientColorKey[]
				{
					new GradientColorKey(new Color(0.466f, 0.435f, 0.415f, 1.0f), 0.0f),
					new GradientColorKey(new Color(0.355f, 0.305f, 0.269f, 1.0f), 0.45f),
					new GradientColorKey(new Color(0.227f, 0.156f, 0.101f, 1.0f), 0.50f),
					new GradientColorKey(new Color(0.0f, 0.0f, 0.0f, 1.0f), 0.55f),
					new GradientColorKey(new Color(0.0f, 0.0f, 0.0f, 1.0f), 1.0f)
				},

				alphaKeys = new GradientAlphaKey[] 
				{
					new GradientAlphaKey(1.0f, 0.0f),
					new GradientAlphaKey(1.0f, 1.0f)
				}
			},

			EvaluateTime = 0.0f

		};

		[LSkyCurveRange(0.0f, 8.0f, 0.0f, 0.0f, 1.0f, 8.0f)]
		public LSkyCurve ambientIntensity = new LSkyCurve()
		{

			CurveMode    = LSkyCurveMode.floatValue,
			FValue  = 1.0f,
			Curve        = AnimationCurve.Linear(0.0f, 1.0f, 1.0f, 1.0f),
			EvaluateTime = 0.0f
		};
		//--------------------------------------------------------------------------------------

		[SerializeField] private bool m_EnableUnityFog = false;
		[SerializeField] private FogMode m_UnityFogMode = FogMode.ExponentialSquared;

		public LSkyGradient unityFogColor = new LSkyGradient()
		{

			GradientMode    = LSkyGradientMode.gradientValue,
			Color   = new Color(0.901f, 0.956f, 0.968f, 1.0f),  

			Gradient     = new Gradient()
			{
				colorKeys = new GradientColorKey[]
				{
					new GradientColorKey(new Color(0.901f, 0.956f, 0.968f, 1.0f), 0.0f),
					new GradientColorKey(new Color(0.901f, 0.956f, 0.968f, 1.0f), 0.45f),
					new GradientColorKey(new Color(0.650f, 0.607f, 0.349f, 1.0f), 0.50f),
					new GradientColorKey(new Color(0.121f, 0.239f, 0.337f, 1.0f), 0.55f),
					new GradientColorKey(new Color(0.121f, 0.239f, 0.337f, 1.0f), 1.0f)
				},

				alphaKeys = new GradientAlphaKey[] 
				{
					new GradientAlphaKey(1.0f, 0.0f),
					new GradientAlphaKey(1.0f, 1.0f)
				}
			},

			EvaluateTime = 0.0f

		};
				
		[LSkyCurveRange(0.0f, 1.0f, 0.0f, 0.0f, 1.0f, 1.0f)]
		public LSkyCurve unityFogDensity = new LSkyCurve()
		{

			CurveMode    = LSkyCurveMode.floatValue,
			FValue       = 0.01f,
			Curve        = AnimationCurve.Linear(0.0f, 0.01f, 1.0f, 0.01f),
			EvaluateTime = 0.0f

		};
				
		[LSkyCurveRange(0.0f, 1000f, 0.0f, 0.0f, 1.0f, 1000f)]
		public LSkyCurve unityFogStartDistance = new LSkyCurve()
		{

			CurveMode    = LSkyCurveMode.floatValue,
			FValue       = 0.0f,
			Curve        = AnimationCurve.Linear(0.0f, 0.0f, 1.0f, 0.0f),
			EvaluateTime = 0.0f

		};

		[LSkyCurveRange(0.0f, 1000f, 0.0f, 0.0f, 1.0f, 1000f)]
		public LSkyCurve unityFogEndDistance = new LSkyCurve()
		{

			CurveMode    = LSkyCurveMode.floatValue,
			FValue       = 300f,
			Curve        = AnimationCurve.Linear(0.0f, 300f, 1.0f, 300f),
			EvaluateTime = 0.0f

		};

		#endregion

		#region Get Celestials direction

		public Vector3 SunDirection { get { return -m_SunLightTransform.forward;  } }
		public Vector3 MoonDirection{ get { return -m_MoonLightTransform.forward; } }

		#endregion

		#region Curves and gradients

		/// <summary>
		/// Evaluate time by sun direction.
		/// </summary>
		/// <value>The SU n DI r HAL f EVALUAT e TIM.</value>
		public float SUN_DIR_EVALUATE_TIME{ get{ return GetEvaluateTime(SunDirection.y, true); } }  

		/// <summary>
		/// Evaluate time by sun direction only above the horizon.
		/// </summary>
		/// <value>The SU n DI r HAL f EVALUAT e TIM.</value>
		public float SUN_DIR_HALF_EVALUATE_TIME{ get{ return GetEvaluateTime(SunDirection.y, false); } } 
		public float N_SUN_DIR_HALF_EVALUATE_TIME{ get{ return GetEvaluateTime(-SunDirection.y, false); } } 
		//----------------------------------------------------------------------------------------------------

		/// <summary>
		/// Evaluate time by moon direction.
		/// </summary>
		/// <value>The MOO n DI r EVALUAT e TIM.</value>
		public float MOON_DIR_EVALUATE_TIME{ get{ return GetEvaluateTime(MoonDirection.y, true); } }  

		/// <summary>
		/// Evaluate time by moon direction only above the horizon.
		/// </summary>
		/// <value>The MOO n DI r HAL f EVALUAT e TIM.</value>
		public float MOON_DIR_HALF_EVALUATE_TIME{ get{ return GetEvaluateTime(MoonDirection.y, false); } }  
		//----------------------------------------------------------------------------------------------------

		/// <summary>
		/// Evaluate time by "Y" direction.
		/// </summary>
		/// <returns>The evaluate time.</returns>
		/// <param name="sourceDir">Source dir.</param>
		/// <param name="doubleSide">If set to <c>true</c> double side.</param>
		public float GetEvaluateTime(float sourceDir, bool fullCycle)
		{
			float val = 1.0f - sourceDir;
			return fullCycle ?  val * 0.5f : val; 
		}

		void SetEvaluateTime()
		{

			wavelengthR.EvaluateTime         = SUN_DIR_EVALUATE_TIME;
			wavelengthG.EvaluateTime         = SUN_DIR_EVALUATE_TIME;
			wavelengthB.EvaluateTime         = SUN_DIR_EVALUATE_TIME;
			atmosphereThickness.EvaluateTime = SUN_DIR_EVALUATE_TIME;
			//-------------------------------------------------------------------------------------------------------------

			dayAtmosphereTint.EvaluateTime   = SUN_DIR_HALF_EVALUATE_TIME;
			nightAtmosphereTint.EvaluateTime = m_MoonInfluence ? MOON_DIR_HALF_EVALUATE_TIME : N_SUN_DIR_HALF_EVALUATE_TIME;
			//-------------------------------------------------------------------------------------------------------------

			sunBrightness.EvaluateTime = SUN_DIR_EVALUATE_TIME;
			mie.EvaluateTime           = SUN_DIR_EVALUATE_TIME;
			//-------------------------------------------------------------------------------------------------------------

			sunMieColor.EvaluateTime       = SUN_DIR_HALF_EVALUATE_TIME;
			sunMieAnisotropy.EvaluateTime  = SUN_DIR_HALF_EVALUATE_TIME;
			sunMieScattering.EvaluateTime  = SUN_DIR_HALF_EVALUATE_TIME;
			//-------------------------------------------------------------------------------------------------------------

			moonMieColor.EvaluateTime       =  MOON_DIR_HALF_EVALUATE_TIME;
			moonMieAnisotropy.EvaluateTime  =  MOON_DIR_HALF_EVALUATE_TIME;
			moonMieScattering.EvaluateTime  =  MOON_DIR_HALF_EVALUATE_TIME;
			moonMieMultiplier.EvaluateTime  =  SUN_DIR_EVALUATE_TIME;
			//-------------------------------------------------------------------------------------------------------------

			if (m_EnableSunDisc)
			{
				sunDiscSize.EvaluateTime  = SUN_DIR_HALF_EVALUATE_TIME;
				sunDiscColor.EvaluateTime = SUN_DIR_HALF_EVALUATE_TIME;
			}
			//-------------------------------------------------------------------------------------------------------------


			if(m_EnableMoon)
			{

				moonSize.EvaluateTime       = MOON_DIR_HALF_EVALUATE_TIME;
				moonColor.EvaluateTime      = MOON_DIR_HALF_EVALUATE_TIME;
				moonIntensity.EvaluateTime  = MOON_DIR_HALF_EVALUATE_TIME;
				moonMultiplier.EvaluateTime = SUN_DIR_EVALUATE_TIME;
			}
			//-------------------------------------------------------------------------------------------------------------

			if(m_EnableStars) 
			{
				starsColor.EvaluateTime         = SUN_DIR_EVALUATE_TIME;
				starsIntensity.EvaluateTime     = SUN_DIR_EVALUATE_TIME;
				starsScintillation.EvaluateTime = SUN_DIR_EVALUATE_TIME;
			}


			if(m_EnableNebula) 
			{
				nebulaColor.EvaluateTime      = SUN_DIR_EVALUATE_TIME;
				nebulaIntensity.EvaluateTime  = SUN_DIR_EVALUATE_TIME;
			}
			//-------------------------------------------------------------------------------------------------------------

			exposure.EvaluateTime = SUN_DIR_EVALUATE_TIME;
			//-------------------------------------------------------------------------------------------------------------

			sunLightColor.EvaluateTime     = SUN_DIR_EVALUATE_TIME;
			sunLightIntensity.EvaluateTime = SUN_DIR_EVALUATE_TIME;
			//-------------------------------------------------------------------------------------------------------------

			moonLightColor.EvaluateTime      = MOON_DIR_HALF_EVALUATE_TIME;
			moonLightIntensity.EvaluateTime  = MOON_DIR_HALF_EVALUATE_TIME;
			moonLightMultiplier.EvaluateTime = SUN_DIR_EVALUATE_TIME;
			//-------------------------------------------------------------------------------------------------------------

			// Ambient
			switch (m_AmbientMode) 
			{
			case UnityEngine.Rendering.AmbientMode.Skybox:
				ambientIntensity.EvaluateTime = SUN_DIR_EVALUATE_TIME;
				break;

			case UnityEngine.Rendering.AmbientMode.Trilight: 
				ambientSkyColor.EvaluateTime     = SUN_DIR_EVALUATE_TIME;
				ambientEquatorColor.EvaluateTime = SUN_DIR_EVALUATE_TIME;
				break;

			case UnityEngine.Rendering.AmbientMode.Flat :
				ambientSkyColor.EvaluateTime   = SUN_DIR_EVALUATE_TIME;
				break;
			}

			ambientGroundColor.EvaluateTime = SUN_DIR_EVALUATE_TIME;
			//-------------------------------------------------------------------------------------------------------------

			if (m_EnableUnityFog)
			{

				unityFogColor.EvaluateTime = SUN_DIR_EVALUATE_TIME;

				if (m_UnityFogMode == FogMode.Linear)
				{
					unityFogStartDistance.EvaluateTime = SUN_DIR_EVALUATE_TIME;
					unityFogEndDistance.EvaluateTime = SUN_DIR_EVALUATE_TIME;
				} else {
					unityFogDensity.EvaluateTime = SUN_DIR_EVALUATE_TIME;
				}

			}
			//-------------------------------------------------------------------------------------------------------------
		}

		#endregion

		// Editor foldouts.
		#if UNITY_EDITOR

		[HideInInspector]
		public bool CRFoldout  = true,
		atmosphereFoldout      = true,
		celestialsFoldout      = true,
		colorCorrectionFoldout = true,
		lightingFoldout        = true;

		#endif
	}
}
