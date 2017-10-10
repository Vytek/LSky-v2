
//--------------
// Curve Range.
//==============
using System; 
using UnityEngine;

namespace AC.Utility
{

	public class AC_CurveRange : PropertyAttribute
	{
		public readonly float timeStart;
		public readonly float valueStart;
		public readonly float timeEnd; 
		public readonly float valueEnd; 

		public AC_CurveRange(float timeStart, float valueStart, float timeEnd, float valueEnd)
		{
			this.timeStart  = timeStart; 
			this.valueStart = valueStart;
			this.timeEnd    = timeEnd;   
			this.valueEnd   = valueEnd;
		}
	}
}