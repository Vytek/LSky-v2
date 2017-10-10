

using System.Collections;
using UnityEngine;

namespace AC.LSky
{

	[ExecuteInEditMode] public class LSkyPlanetary : LSkyCelestialsCalculations
	{


		[SerializeField] private Transform m_SunLightTransform = null;
		[SerializeField] private Transform m_MoonLightTransform = null;

		public override float Hour 
		{
			get{ return  LSkyTimeDate.Instance.Timeline; }
		}

		public override int Year 
		{
			get{ return LSkyTimeDate.Instance.Year; }
		}

		public override int Month
		{
			get{ return LSkyTimeDate.Instance.Month; }
		}

		public override int Day 
		{
			get{ return LSkyTimeDate.Instance.Day; }
		}

		void LateUpdate()
		{

			if(m_SunLightTransform == null || m_MoonLightTransform == null) 
			{
				return;
			}

			Vector3 sunPos = SphericalToCartesian(SunCoordinates.z, SunCoordinates.x - Mathf.PI);
			m_SunLightTransform.LookAt(transform.position-sunPos);

			Vector3 moonPos = SphericalToCartesian(MoonCoordinates.z, MoonCoordinates.x - Mathf.PI);
			m_MoonLightTransform.LookAt(transform.position-moonPos);
		}

	}

}