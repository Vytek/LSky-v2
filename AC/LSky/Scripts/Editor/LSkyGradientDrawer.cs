
using UnityEngine;
using UnityEditor;

namespace AC.LSky
{

	[CustomPropertyDrawer(typeof(LSkyGradient))]
	public class LSkyColorDrawer : PropertyDrawer
	{


		private string displayName;

		private SerializedProperty gradientMode;
		private SerializedProperty color;
		private SerializedProperty gradient;

		private bool isCached = false;

		private string[] options = new string[]
		{
			"C", "G"
		};
			
		public override void OnGUI(Rect rect, SerializedProperty property, GUIContent label)
		{
			
			if (!isCached) 
			{

				displayName = property.displayName; 
				property.Next (true);

				gradientMode = property.Copy ();
				property.NextVisible (true);

				color = property.Copy ();
				property.NextVisible (true);

				gradient = property.Copy ();
				property.NextVisible (true);

				isCached = true;
			}
			//-----------------------------------------------------------------------------------

			rect.height = 20f; rect.width *= 0.90f; 
			EditorGUI.indentLevel = 0;
			//-----------------------------------------------------------------------------------

			if(gradientMode.enumValueIndex == 0) 
				EditorGUI.PropertyField(rect, color, new GUIContent(displayName));
			else
				EditorGUI.PropertyField(rect, gradient, new GUIContent(displayName));
			//-----------------------------------------------------------------------------------

			Rect switchRect     = rect; 
			switchRect.x       += rect.width; //buttonRect.y     += 2.5f; 
			switchRect.height   = 20; switchRect.width *= 0.1f;
			//-----------------------------------------------------------------------------------

			gradientMode.enumValueIndex = EditorGUI.Popup(switchRect, "", gradientMode.enumValueIndex, options,  EditorStyles.miniLabel); 
		}

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label) 
		{
			return base.GetPropertyHeight(property, label) + 5;
		}
	}
}