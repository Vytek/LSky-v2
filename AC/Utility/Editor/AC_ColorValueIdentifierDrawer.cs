
//-------------------------
// Get Color in RGB values.
//=========================
using UnityEngine; 
using UnityEditor;

namespace AC.Utility
{

	[CustomPropertyDrawer(typeof(AC_ColorValueIdentifier))] 
	public class AC_ColorValueIdentifierDrawer : PropertyDrawer
	{

		public override void OnGUI(Rect rect, SerializedProperty property, GUIContent label)
		{
	
			if(property.propertyType == SerializedPropertyType.Color) 
			{

				rect.height = 20f; EditorGUI.indentLevel = 0;
				EditorGUI.PropertyField(rect, property); 

				float 
				r = property.colorValue.r, 
				g = property.colorValue.g, 
				b = property.colorValue.b, 
				a = property.colorValue.a;

				rect.width = 100f; rect.y += 25;
				EditorGUI.LabelField(rect, "Red: " + r, EditorStyles.miniLabel);

				rect.x  += rect.width;
				EditorGUI.LabelField(rect, "Green: " + g, EditorStyles.miniLabel);

				rect.x  += rect.width;
				EditorGUI.LabelField(rect, "Blue: " + b, EditorStyles.miniLabel);

				rect.x -= rect.width * 2; rect.y += 20;
				EditorGUI.LabelField(rect, "Alpha: " + a, EditorStyles.miniLabel);
			}
			else EditorGUI.HelpBox(rect, "Only use with Color structure", MessageType.Warning);
		}

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label) 
		{
			return base.GetPropertyHeight(property, label) + 45;
		}
	}
}