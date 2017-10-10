
using System.Collections;
using UnityEngine;

namespace AC.LSky
{

	public abstract class LSkyCelestialsCalculations : MonoBehaviour 
	{

		///-----------------------------------------------
		/// Planetary position based in 
		/// Paul Schlyter papers.
		///================================================

		internal struct OrbitalElements
		{

			/// <summary>
			/// Longitude of the ascending node.
			/// </summary>
			public float N;

			/// <summary>
			/// The Inclination to the ecliptic.
			/// </summary>
			public float i; 

			/// <summary>
			/// Argument of perihelion.
			/// </summary>
			public float w; 

			/// <summary>
			/// Semi-major axis, or mean distance from sun
			/// </summary>
			public float a;  

			/// <summary>
			/// Eccentricity
			/// </summary>
			public float e; 

			/// <summary>
			/// Mean anomaly.
			/// </summary>
			public float M; 

			public OrbitalElements(float N, float i, float w, float a, float e, float M)
			{
				this.N = N;
				this.i = i;
				this.w = w;
				this.a = a; 
				this.e = e;
				this.M = M;
			}
		}
			
		public virtual float Hour{ get; }
		public virtual int Year{ get;  }
		public virtual int Month{ get; }
		public virtual int Day{ get; }


		[SerializeField, Range(-90f, 90f)] protected float m_Latitude;
		[SerializeField, Range(-18f, 18f)] protected float m_Longitude;
		[SerializeField, Range(-12f, 12f)] protected float m_UTC;



		// Celestial.
		private float m_SunDistance;        // Sun distance(r).
		private float m_TrueSunLongitude;   // True sun longitude.
		private float m_MeanSunLongitude;   // Mean sun longitude.
		private float m_SideralTime;        // Sideral time.

		private float HOUR_UTC_APPLY{ get { return Hour - m_UTC; } }
		private float Latitude_Rad{ get { return Mathf.Deg2Rad * m_Latitude; } }

		protected const float k_HalfPI = 1.570796f; // PI/2

		// Time Scale(d).
		private float TimeScale
		{ 
			get
			{ 
				return (367 * Year - (7 * (Year + ((Month + 9) / 12))) / 4 + (275 * Month) / 9 + Day - 730530) + Hour / 24;
			} 
		}

		// Obliquity of the ecliptic.
		private float Oblecl{ get{return Mathf.Deg2Rad * (23.4393f - 3.563e-7f * TimeScale);} }


		// Sun orbital elements.
		private OrbitalElements SunOrbitalElements
		{

			get
			{

				OrbitalElements elements = new OrbitalElements() 
				{
					N = 0,
					i = 0,
					w = 282.9404f + 4.70935e-5f   * TimeScale,
					a = 0,
					e = 0.016709f - 1.151e-9f     * TimeScale,
					M = 356.0470f + 0.9856002585f * TimeScale
				};

				// Solve M.
				elements.M = Rev(elements.M);

				return elements;
			}
		}

		/// <summary>
		/// Get sun coordinates.
		/// x = azimuth, y = altitude, z = zenith.
		/// </summary>
		/// <value>The sun coordinates.</value>
		public Vector3 SunCoordinates{ get{ return ComputeSunCoords (); } }


		/// <summary>
		/// Get moon coordinates (Simplifield).
		/// x = azimuth, y = altitude, z = zenith.
		/// </summary>
		/// <value>The moon coordinates.</value>
		public Vector3 MoonCoordinates{ get{ return ComputeMoonCoords(); } }

		Vector3 ComputeSunCoords()
		{


			Vector3 result;

			#region Orbital Elements.
			float w = SunOrbitalElements.w; float e = SunOrbitalElements.e; float M = SunOrbitalElements.M; 
			float M_Rad = M * Mathf.Deg2Rad; // M in radians.
			#endregion

			#region Eccentric Anomaly.
			float E     = M + Mathf.Rad2Deg * e * Mathf.Sin(M_Rad) * (1 + e * Mathf.Cos(M_Rad)); //Debug.Log(E);
			float E_Rad = Mathf.Deg2Rad * E;  // E in radians.
			#endregion

			#region Rectangular Coordinates.
			// Rectangular coordinates of the sun in the plane of the ecliptic.
			float xv = (Mathf.Cos(E_Rad) - e); //Debug.Log(xv);
			float yv = (Mathf.Sin(E_Rad) * Mathf.Sqrt(1 - e*e)); //Debug.Log(yv);

			// Convert to distance and true anomaly(r = radians, v = degrees).
			float r = Mathf.Sqrt(xv * xv + yv * yv); //Debug.Log(r);
			float v = Mathf.Rad2Deg * Mathf.Atan2(yv, xv); //Debug.Log(v);

			m_SunDistance = r; // Get sun distance.
			#endregion

			#region True Longitude.
			float lonsun = v + w; // True sun longitude.
			lonsun       = Rev(lonsun); //Debug.Log(lonsun) // Solve sun longitude.;

			float lonsun_Rad   = Mathf.Deg2Rad * lonsun; // True sun longitude in radians.
			m_TrueSunLongitude = lonsun_Rad; // Get true sun longitude.
			#endregion

			#region Ecliptic And Equatorial Coordinates.
			// Ecliptic rectangular coordinates(radians):
			float xs = r * Mathf.Cos(lonsun_Rad);
			float ys = r * Mathf.Sin(lonsun_Rad);

			// Ecliptic rectangular coordinates rotate these to equatorial coordinates(radians).
			float oblecl_Cos = Mathf.Cos(Oblecl);
			float oblecl_Sin = Mathf.Sin(Oblecl);

			float xe = xs;
			float ye = ys * oblecl_Cos - 0.0f * oblecl_Sin;
			float ze = ys * oblecl_Sin + 0.0f * oblecl_Cos;
			#endregion

			#region Ascension And Declination.
			float RA = Mathf.Rad2Deg * Mathf.Atan2(ye, xe)/15; // Right ascension(degrees):
			float Decl = Mathf.Atan2(ze, Mathf.Sqrt(xe * xe + ye * ye)); // Declination(radians).
			#endregion

			#region Mean Longitude.
			float L = w + M;        // Mean sun longitude(degrees).
			L = Rev(L);   // Solve mean sun longitude.
			m_MeanSunLongitude = L; // Get mean sun longitude.
			#endregion

			#region Sideral Time.
			// Sideral time(degrees).
			float GMST0   = (L  / 15) + 12;
			m_SideralTime = (GMST0 + HOUR_UTC_APPLY) + (m_Longitude / 15); 

			float HA     = (m_SideralTime - RA) * 15; // Hour angle(degrees).
			float HA_Rad = Mathf.Deg2Rad * HA;  // Hour angle in radians.
			#endregion

			#region Hour Angle And Declination In Rectangular Coordinates.
			// HA anf Decl in rectangular coordinates(radians).
			float Decl_Cos = Mathf.Cos(Decl);

			float x = Mathf.Cos(HA_Rad) * Decl_Cos; // X axis points to the celestial equator in the south
			float y = Mathf.Sin(HA_Rad) * Decl_Cos; // Y axis points to the horizon in the west
			float z = Mathf.Sin(Decl);              // Z axis points to the north celestial pole.

			// Rotate the rectangualar coordinates system along of the Y axis(radians).
			float sinLatitude = Mathf.Sin(Latitude_Rad); 
			float cosLatitude = Mathf.Cos(Latitude_Rad);

			float xhor =  x * sinLatitude - z * cosLatitude; 
			float yhor =  y;
			float zhor =  x * cosLatitude + z * sinLatitude; 
			#endregion

			#region Azimuth, Altitude And Zenith[Radians].
			result.x  =  Mathf.Atan2(yhor, xhor) + Mathf.PI;
			result.y  =  Mathf.Asin(zhor);
			result.z  =  k_HalfPI  - result.y;
			#endregion

			return result;
		}

		// Simplifield Moon Calculation [In v3 complete calculations]
		Vector3 ComputeMoonCoords()
		{


			Vector3 result;

			#region Sun Orbital Elements.
			// Sun orbital elements.
			// float ws = SunOrbitalElemtents.w; 
			// float es = SunOrbitalElemtents.e;
			//float Ms = SunOrbitalElements.M; 
			//float Ms_Rad = Mathf.Deg2Rad * Ms;
			#endregion

			#region Orbital Elements.

		//	OrbitalElements orbitalElements = new OrbitalElements() 

			float N = 125.1228f - 0.0529538083f  * TimeScale;
			float i = 5.1454f; 
			float w = 318.0634f + 0.1643573223f  * TimeScale; 
			float a = 60.2666f;
			float e = 0.054900f;
			float M = 115.3654f + 13.0649929509f * TimeScale;

			N = Rev(N);  
			w = Rev(w);  
			M = Rev(M);  

			// Orbital elements in radians. 
			float N_Rad = Mathf.Deg2Rad * N;   
			float i_Rad = Mathf.Deg2Rad * i;  
			float M_Rad = Mathf.Deg2Rad * M;   

			#endregion


			#region Eccentric Anomaly.
			float E = M + Mathf.Rad2Deg * e * Mathf.Sin(M_Rad) * 
				(1 + e * Mathf.Cos(M_Rad));

			// E in radians
			float E_Rad = Mathf.Deg2Rad * E; 
			#endregion

			#region Rectangular Coordinates.
			// Rectangular coordinates of the sun in the plane of the ecliptic.

			float xv = a * (Mathf.Cos(E_Rad) - e); //Debug.Log(xv);
			float yv = a * (Mathf.Sin(E_Rad) * Mathf.Sqrt(1 - e*e)) * Mathf.Sin(E_Rad); //Debug.Log(yv); 

			// Convert to distance and true anomaly(r = radians, v = degrees).
			float r = Mathf.Sqrt(xv * xv + yv * yv); //Debug.Log(r);
			float v = Mathf.Rad2Deg * Mathf.Atan2(yv, xv); //Debug.Log(v);

			v = Rev(v);

			// longitude in radians.
			float l =  Mathf.Deg2Rad * (v + w);

			float Cos_l     = Mathf.Cos(l);
			float Sin_l     = Mathf.Sin(l);
			float Cos_N_Rad = Mathf.Cos(N_Rad);
			float Sin_N_Rad = Mathf.Sin(N_Rad);
			float Cos_i_Rad = Mathf.Cos(i_Rad);

			float xeclip = r * (Cos_N_Rad * Cos_l - Sin_N_Rad * Sin_l * Cos_i_Rad);
			float yeclip = r * (Sin_N_Rad * Cos_l + Cos_N_Rad * Sin_l * Cos_i_Rad);
			float zeclip = r * (Sin_l * Mathf.Sin(i_Rad));

			#endregion

			#region Geocentric Coordinates.

			// Geocentric position for the moon and Heliocentric position for the planets.
			float lonecl =  Mathf.Rad2Deg * Mathf.Atan2(yeclip, xeclip);

			lonecl = Rev(lonecl); //Debug.Log(lonecl);

			float latecl = Mathf.Rad2Deg * Mathf.Atan2(zeclip, Mathf.Sqrt(xeclip * xeclip + yeclip * yeclip)); //Debug.Log(latecl);

			// True sun longitude.
			float lonSun = m_TrueSunLongitude;

			// Ecliptic longitude and latitude in radians.
			float lonecl_Rad = Mathf.Deg2Rad * lonecl; 
			float latecl_Rad = Mathf.Deg2Rad * latecl; 

			float nr = 1.0f; 
			float xh = nr * Mathf.Cos(lonecl_Rad) * Mathf.Cos(latecl_Rad); 
			float yh = nr * Mathf.Sin(lonecl_Rad) * Mathf.Cos(latecl_Rad); 
			float zh = nr * Mathf.Sin(latecl_Rad);

			// Geocentric posisition.
			float xs =  0.0f;
			float ys =  0.0f;

			// Convert the geocentric position to heliocentric position.
			float xg = xh + xs; 
			float yg = yh + ys; 
			float zg = zh; 

			#endregion

			#region Equatorial Coordinates.

			// Convert xg, yg in equatorial coordinates.
			float oblecl_Cos = Mathf.Cos(Oblecl);
			float oblecl_Sin = Mathf.Sin(Oblecl);

			float xe = xg;
			float ye = yg * oblecl_Cos - zg * oblecl_Sin;
			float ze = yg * oblecl_Sin + zg * oblecl_Cos;

			#endregion

			#region Ascension, Declination And Hour Angle.

			// Right ascension.
			float RA = Mathf.Rad2Deg * Mathf.Atan2(ye,xe);
			RA = Rev(RA); //Debug.Log(RA);

			// Declination.
			float Decl = Mathf.Rad2Deg * Mathf.Atan2(ze, Mathf.Sqrt(xe*xe + ye*ye));

			// Declination in radians.
			float Decl_Rad = Mathf.Deg2Rad * Decl;

			// Hour angle.
			float HA = (m_SideralTime * 15) - RA;

			HA = Rev(HA); //Debug.Log(HA);

			// Hour angle in radians.
			float HA_Rad =  Mathf.Deg2Rad * HA; 

			#endregion

			#region Declination in rectangular coordinates.

			// HA y Decl in rectangular coordinates.
			float Decl_Cos = Mathf.Cos(Decl_Rad);
			float xr = Mathf.Cos(HA_Rad) * Decl_Cos;
			float yr = Mathf.Sin(HA_Rad) * Decl_Cos; 
			float zr = Mathf.Sin(Decl_Rad);

			// Rotate the rectangualar coordinates system along of the Y axis(radians).
			float sinLatitude = Mathf.Sin(Latitude_Rad); 
			float cosLatitude = Mathf.Cos(Latitude_Rad);

			float xhor =  xr * sinLatitude - zr * cosLatitude; 
			float yhor =  yr;
			float zhor =  xr * cosLatitude + zr * sinLatitude; 
			#endregion

			#region Azimuth, Altitude And Zenith[Radians].
			result.x  =  Mathf.Atan2(yhor, xhor) + Mathf.PI;
			result.y  =  Mathf.Asin(zhor);
			result.z  =  k_HalfPI  - result.y;
			#endregion

			return result;
		}

		protected float Rev(float x)
		{
			return x - Mathf.Floor(x / 360f) * 360f;
		}

		/// <summary>
		/// Convert sperical coordinates to cartesian coordinates.
		/// </summary>
		/// <returns>The to cartesian.</returns>
		/// <param name="theta">Theta.</param>
		/// <param name="pi">Pi.</param>
		public Vector3 SphericalToCartesian(float theta, float pi) 
		{

			float sinTheta = Mathf.Sin(theta);  
			float cosTheta = Mathf.Cos(theta); 
			float sinPI    = Mathf.Sin(pi); 
			float cosPI    = Mathf.Cos(pi); 

			return new Vector3() 
			{
				x = sinTheta * sinPI,
				y = cosTheta,
				z = sinTheta * cosPI
			};
		}

		/// <summary>
		/// Convert sperical coordinates to cartesian coordinates.
		/// </summary>
		/// <returns>The to cartesian.</returns>
		/// <param name="theta">Theta.</param>
		/// <param name="pi">Pi.</param>
		/// <param name="rad">RAD.</param>
		public Vector3 SphericalToCartesian(float theta, float pi, float rad) 
		{

			rad = Mathf.Max(1.0f, rad);

			float sinTheta = Mathf.Sin(theta);  
			float cosTheta = Mathf.Cos(theta); 
			float sinPI    = Mathf.Sin(pi); 
			float cosPI    = Mathf.Cos(pi); 

			return new Vector3() 
			{
				x = rad * sinTheta * sinPI,
				y = rad * cosTheta,
				z = rad * sinTheta * cosPI
			};
		}
	}

}