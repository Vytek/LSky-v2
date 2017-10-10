
using System; 
using UnityEngine;

namespace AC.LSky
{

	[Serializable] public class LSkyCurveRange : PropertyAttribute 
	{

		// Value range.
		public readonly float minValue;
		public readonly float maxValue;

		// Curve rect.
		public readonly float timeStart;
		public readonly float valueStart;
		public readonly float timeEnd;
		public readonly float valueEnd;

		public LSkyCurveRange(float minValue, float maxValue, float timeStart, float valueStart, float timeEnd, float valueEnd)
		{
			this.minValue   = minValue;
			this.maxValue   = maxValue;
			this.timeStart  = timeStart;
			this.valueStart = valueStart;
			this.timeEnd    = timeEnd;
			this.valueEnd   = valueEnd;
		}
	}
}