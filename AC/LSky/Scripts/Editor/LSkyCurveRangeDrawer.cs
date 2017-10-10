
using UnityEngine;
using UnityEditor;

namespace AC.LSky
{

	[CustomPropertyDrawer(typeof(LSkyCurveRange))]
	public class LSkyCurveRangeDrawer : PropertyDrawer
	{

		private string displayName;

		private SerializedProperty curveMode;
		private SerializedProperty fValue;
		private SerializedProperty curve;

		private bool isCached = false;

		private string[] options = new string[]
		{
			"V", "C",
		};

		public override void OnGUI(Rect rect, SerializedProperty property, GUIContent label)
		{

			if (!isCached) 
			{

				displayName = property.displayName;
				property.Next (true);

				curveMode = property.Copy ();
				property.NextVisible (true);

				fValue = property.Copy ();
				property.NextVisible (true);

				curve = property.Copy ();
				property.NextVisible (true);

				isCached = true;
			}
			//------------------------------------------------------------------------

			rect.height = 20f; rect.width *= 0.90f; 
			EditorGUI.indentLevel = 0;
			//------------------------------------------------------------------------

			LSkyCurveRange attr = attribute as LSkyCurveRange;
			//------------------------------------------------------------------------

			if(curveMode.enumValueIndex == 0) // Input value.
				EditorGUI.Slider(rect, fValue, attr.minValue, attr.maxValue, new GUIContent(displayName));
			else // Input curve.
				EditorGUI.CurveField(rect, curve, Color.white, new Rect(attr.timeStart, attr.valueStart, attr.timeEnd, attr.valueEnd), new GUIContent(displayName));
			//------------------------------------------------------------------------

			Rect switchRect     = rect; 
			switchRect.x       += rect.width; //buttonRect.y     += 2.5f; 
			switchRect.height   = 20;  switchRect.width *= 0.1f;
			//------------------------------------------------------------------------

			curveMode.enumValueIndex = EditorGUI.Popup(switchRect, "", curveMode.enumValueIndex, options, EditorStyles.label); 
		}

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label) 
		{
			return base.GetPropertyHeight(property, label) + 5;
		}
	}
}