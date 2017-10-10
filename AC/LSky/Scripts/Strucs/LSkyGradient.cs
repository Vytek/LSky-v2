
//---------------------
// Gradient Struct.
//=====================

using System;
using UnityEngine;

namespace AC.LSky
{

	[Serializable] public struct LSkyGradient
	{

		[SerializeField] private LSkyGradientMode m_GradientMode;
		[SerializeField] private Color m_Color;
		[SerializeField] private Gradient m_Gradient;
		[SerializeField] private float m_EvaluateTime;

		public LSkyGradientMode GradientMode 
		{
			get{ return this.m_GradientMode;  } 
			set{ this.m_GradientMode = value; } 
		}

		public Color Color
		{
			get{ return this.m_Color; }
			set{ this.m_Color = value; }
		}

		public Gradient Gradient 
		{
			get{ return this.m_Gradient; }
			set{ this.m_Gradient = value; }
		}

		public float EvaluateTime
		{
			get{ return this.m_EvaluateTime; }
			set{ this.m_EvaluateTime = value; }
		}

		public Color ColorValue
		{
			get
			{
				return(m_GradientMode == LSkyGradientMode.gradientValue) ? m_Gradient.Evaluate(m_EvaluateTime) : m_Color;
			}
		}


		public LSkyGradient(LSkyGradientMode _gradientMode, Color _color, Gradient _gradient, float _evaluateTime)
		{
			this.m_GradientMode = _gradientMode;
			this.m_Color        = _color;
			this.m_Gradient     = _gradient;
			this.m_EvaluateTime = _evaluateTime;
		}



	}
}