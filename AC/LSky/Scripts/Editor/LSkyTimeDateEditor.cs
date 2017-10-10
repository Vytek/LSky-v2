
using System;
using UnityEngine;
using UnityEditor;
using AC.EditorGUIUtility;

namespace AC.LSky
{

	[CustomEditor(typeof(LSkyTimeDate))]
	public class LSkyTimeDateEditor : Editor
	{


		SerializedObject serObj;
		LSkyTimeDate tar;
		//------------------------------------------

		SerializedProperty m_TimeUpdateMethod;
		SerializedProperty m_DateUpdateMethod;
		//------------------------------------------

		SerializedProperty m_Progress;
		//------------------------------------------

		SerializedProperty m_UseSystemDateTime;
		//------------------------------------------

		SerializedProperty m_DayRange;
		SerializedProperty m_DayLength;
		SerializedProperty m_NightLength;
		SerializedProperty m_Timeline;
		//------------------------------------------

		SerializedProperty m_Day;
		SerializedProperty m_Month;
		SerializedProperty m_Year;
		//------------------------------------------


		void OnEnable()
		{

			serObj  = new SerializedObject(target);
			tar = (LSkyTimeDate)target;
			//---------------------------------------------------------------

			m_TimeUpdateMethod = serObj.FindProperty("m_TimeUpdateMethod");
			m_DateUpdateMethod = serObj.FindProperty("m_DateUpdateMethod");
			//---------------------------------------------------------------

			m_Progress = serObj.FindProperty("m_Progress");
			//---------------------------------------------------------------

			m_UseSystemDateTime = serObj.FindProperty("m_UseSystemDateTime");
			//---------------------------------------------------------------

			m_DayRange    = serObj.FindProperty("m_DayRange");
			m_DayLength   = serObj.FindProperty("m_DayLength");
			m_NightLength = serObj.FindProperty("m_NightLength");
			m_Timeline    = serObj.FindProperty("m_Timeline");
			//----------------------------------------------------------------

			m_Day   = serObj.FindProperty("m_Day");
			m_Month = serObj.FindProperty("m_Month");
			m_Year  = serObj.FindProperty("m_Year");
			//----------------------------------------------------------------

		}


		public override void OnInspectorGUI()
		{

			serObj.Update();

			EditorGUILayout.Separator();
			AC_EditorGUIUtility.ShurikenHeader("Time And Date", TextTitleStyle, 30);

			GUI.color = Color.white;
			EditorGUILayout.BeginVertical(EditorStyles.helpBox);
			{
				EditorGUILayout.PropertyField(m_TimeUpdateMethod, new GUIContent("Time Update Method")); 
				EditorGUILayout.PropertyField(m_DateUpdateMethod, new GUIContent("Date Update Method"));
				EditorGUILayout.PropertyField(m_Progress, new GUIContent ("Progress")); 
			}
			EditorGUILayout.EndVertical();
			EditorGUILayout.Separator();

			EditorGUILayout.BeginVertical(EditorStyles.helpBox);
			{
				EditorGUILayout.PropertyField(m_UseSystemDateTime, new GUIContent("Use System Date Time"));
			}
			EditorGUILayout.EndVertical();
			//EditorGUILayout.Separator();

			if(!m_UseSystemDateTime.boolValue) 
			{

				float min = m_DayRange.vector2Value.x; 
				float max = m_DayRange.vector2Value.y;
				EditorGUILayout.BeginVertical();
				{
					GUILayout.Label("Day Range");
					EditorGUILayout.BeginHorizontal();
					{
						EditorGUILayout.MinMaxSlider(ref min, ref max, 0, 24); 
						m_DayRange.vector2Value = new Vector2 (min, max);
						EditorGUILayout.PropertyField(m_DayRange, new GUIContent ("")); 
					}
					EditorGUILayout.EndHorizontal ();

					string startInfo = "Day Start: " + LSkyTime.TimeToString
					(
							LSkyTime.GetTimelineHour(min),
							LSkyTime.GetTimelineMinute(min)
					);

					string endInfo = "Day End: " + LSkyTime.TimeToString
					(
						LSkyTime.GetTimelineHour(max),
						LSkyTime.GetTimelineMinute(max)
					);
					EditorGUILayout.HelpBox(startInfo + " | " + endInfo, MessageType.Info); 

					EditorGUILayout.Separator ();

					EditorGUILayout.PropertyField (m_DayLength, new GUIContent ("Day In Minutes")); 
					EditorGUILayout.PropertyField (m_NightLength, new GUIContent ("Night In Minutes")); 
				}
				EditorGUILayout.EndVertical();
				EditorGUILayout.Separator();
			}

			EditorGUILayout.BeginVertical(EditorStyles.helpBox);
			{
				EditorGUILayout.PropertyField(m_Timeline, new GUIContent("Timeline")); 
				EditorGUILayout.PropertyField(m_Day, new GUIContent("Day")); 
				EditorGUILayout.PropertyField(m_Month, new GUIContent("Month")); 
				EditorGUILayout.PropertyField(m_Year, new GUIContent("Year")); 
			}
			EditorGUILayout.EndVertical();
			EditorGUILayout.HelpBox("Hour: " + tar.TimeDate.ToString("hh:mm:ss"), MessageType.Info); 

			serObj.ApplyModifiedProperties();
		}

		private GUIStyle TextTitleStyle
		{

			get 
			{

				GUIStyle style = new GUIStyle(EditorStyles.label); 
				style.fontStyle = FontStyle.Bold;
				style.fontSize = 15;

				return style;
			}
		}
	}
}
