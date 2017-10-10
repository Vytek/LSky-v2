
//--------------------
// Curve Struct.
//====================
using System; 
using UnityEngine;

namespace AC.LSky
{

	[Serializable] public struct LSkyCurve 
	{

		[SerializeField] private LSkyCurveMode m_CurveMode;
		[SerializeField] private float m_FValue;
		[SerializeField] private AnimationCurve m_Curve;
		[SerializeField] private float m_EvaluateTime;

		public LSkyCurveMode CurveMode 
		{
			get{ return this.m_CurveMode;  }
			set{ this.m_CurveMode = value; }
		}

		public float FValue 
		{
			get{ return this.m_FValue; }
			set{ this.m_FValue = value; }
		}

		public AnimationCurve Curve
		{
			get{ return this.m_Curve; }
			set{ this.m_Curve = value; }
		}

		public float EvaluateTime
		{
			get{ return this.m_EvaluateTime; }
			set{ this.m_EvaluateTime = value; }
		}
			
		public float Value
		{
			get
			{
				return (m_CurveMode == LSkyCurveMode.curveValue) ? m_Curve.Evaluate (m_EvaluateTime) : m_FValue;
			}
		}
			
		public LSkyCurve(LSkyCurveMode _curveMode, float _fValue, AnimationCurve _curve, float _EvaluateTime)
		{

			this.m_CurveMode = _curveMode;
			this.m_FValue    = _fValue;
			this.m_Curve     = _curve;
			this.m_EvaluateTime = _EvaluateTime;
		}
	}
}