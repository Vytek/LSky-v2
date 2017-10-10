using System;
using UnityEngine;
using UnityEngine.UI;

namespace AC.LSky
{

	[RequireComponent(typeof(Text))]
	[ExecuteInEditMode] public class LSkyTimeDateUI : MonoBehaviour 
	{

		enum Mode{ Date, Time }
		[SerializeField] private Mode m_Mode = Mode.Time;
		[SerializeField] private Text m_Text = null;

		public bool show12Hours;

		void Awake()
		{
			m_Text = GetComponent<Text>();
		}
			
		void Update()
		{

			if(m_Mode == Mode.Time)
			{
				m_Text.text =  LSkyTimeDate.Instance.TimeDate.ToString(show12Hours ? "hh:mm:ss tt" : "HH:mm:ss");

					//+ AC_Time.TimeToString (AC_TimeDate.Instance.TimeDate.Hour, AC_TimeDate.Instance.TimeDate.Minute,
					//AC_TimeDate.Instance.TimeDate.Second);
			} 
			else 
			{
				m_Text.text = LSkyTimeDate.Instance.TimeDate.ToString("dd/MM/yyyy");
			}
		}

	}
}
