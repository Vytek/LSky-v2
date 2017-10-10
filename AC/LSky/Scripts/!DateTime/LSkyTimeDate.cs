
//------------------
// TimeDate Manager.
//==================
using System;
using UnityEngine;

namespace AC.LSky
{

	[ExecuteInEditMode] public class LSkyTimeDate : MonoBehaviour 
	{

		// Instance.
		public static LSkyTimeDate Instance{ get; private set; }
		private LSkyTimeDate(){ Instance = this; }

		// Update Method.
		internal enum UpdateMethod{ Update, LateUpdate, FixedUpdate }
		[SerializeField] private UpdateMethod m_TimeUpdateMethod = UpdateMethod.Update;
		[SerializeField] private UpdateMethod m_DateUpdateMethod = UpdateMethod.Update;

		[SerializeField] private bool m_Progress = true;           // Progress time and date?
		[SerializeField] private bool m_UseSystemDateTime = false; // Sync with system DateTime?

		// Duration cycle.
		[SerializeField] private Vector2 m_DayRange  = new Vector2(6.0f, 19f); // Start and finish of the day.
		[SerializeField] private float m_DayLength   = 15f;  // Day minutes.
		[SerializeField] private float m_NightLength = 7.5f; // Night minutes.

		// Time.
		[SerializeField] private float m_Timeline = 7.50f;     // Time in timeline.
		const float k_TimelineLength = 24f; // Total hours in cycle.

		// Date.
		[SerializeField, Range(1, 31)]   private int m_Day   = 1;   
		[SerializeField, Range(1, 12)]   private int m_Month = 10; 
		[SerializeField, Range(1, 9999)] private int m_Year  = 2017; 


		/// <summary>
		/// Time and date advances?
		/// </summary>
		/// <value>true</value>
		/// <c>false</c>
		public bool ProgressTime
		{
			get{ return this.m_Progress; }
			set{ this.m_Progress = value; }
		}

		/// <summary>
		/// Sync with system DateTime?
		/// </summary>
		/// <value><c>true</c> if use system date time; otherwise, <c>false</c>.</value>
		public bool UseSystemDateTime
		{ 
			get{ return this.m_UseSystemDateTime; } 
			set{ this.m_UseSystemDateTime = value; }
		}

		/// <summary>
		/// Gets the system date time.
		/// </summary>
		/// <value>The system date time.</value>
		public DateTime SystemDateTime{ get{ return DateTime.Now; } }

		/// <summary>
		/// Total cycle duration.
		/// </summary>
		/// <value>The day in minutes.</value>
		public float CycleDuration
		{
			get
			{
				return (m_Timeline >= m_DayRange.x && m_Timeline < m_DayRange.y) ? 60 * m_DayLength * 2 : 60 * m_NightLength * 2;
			}
		}

		/// <summary>
		/// The length of the timeline(Hours per cycle).
		/// </summary>
		/// <value>The length of the timeline.</value>
		public float TimelineLength{ get { return k_TimelineLength; } }

		/// <summary>
		/// Timeline Range: min = 0, max = 24.
		/// </summary>
		/// <value>The time line.</value>
		public float Timeline 
		{
			get
			{ 
				return this.m_Timeline;
			}
			set
			{
				if(value > 0.0f && value < 24.000001f)
				{
					m_Timeline = value;
				}
			}
		}

		/// <summary>
		/// Day.
		/// Range: min 1, max = 31.
		/// </summary>
		/// <value>The day.</value>
		public int Day
		{
			get
			{ 
				return m_Day;
			}
			set
			{ 
				if(value > 0 && value < 32)  
				{
					m_Day = value;  
				}
			}
		}
			

		/// <summary>
		/// Month.
		/// Range: min 1, max = 12.
		/// </summary>
		/// <value>The month.</value>
		public int Month
		{
			get
			{ 
				return m_Month;
			}
			set
			{ 
				if(value > 0 && value < 13) 
				{
					m_Month = value; 
				}
			}
		}

		/// <summary>
		/// Year.
		/// Range: min 1, max = 9999.
		/// </summary>
		/// <value>The year.</value>
		public int Year
		{
			get
			{ 
				return m_Year;
			}
			set
			{ 
				if(value > 0 && value < 10000) 
				{
					m_Year = value; 
				}
			}
		}

		/// <summary>
		/// Get and set DateTime.
		/// </summary>
		/// <value>The custom date time.</value>
		public DateTime TimeDate
		{

			get 
			{
				DateTime dateTime = new DateTime(0, DateTimeKind.Utc);
				RepeatDateTime(); // Repeat full date cycle.

				// Add date and time in DateTime.
				dateTime = dateTime.AddYears(m_Year - 1).AddMonths(m_Month - 1).AddDays(m_Day - 1).AddHours(m_Timeline); 
				//m_Timeline = Mathf.Repeat(m_Timeline, TimelineLength);

				return dateTime;
			}
			set 
			{

				m_Year     = value.Year;
				m_Month    = value.Month;
				m_Day      = value.Day;
				m_Timeline = LSkyTime.TimeToFloat(value.Hour, value.Minute, value.Second, value.Millisecond);
			}
		}
			
		void Update()
		{

			if(!m_Progress) return;

			if(m_TimeUpdateMethod == UpdateMethod.Update)
				ProgressTimeline();

			if(m_DateUpdateMethod == UpdateMethod.Update)
				ProgressDateTime();
		}

		void LateUpdate()
		{
			if(!m_Progress) return;

			if(m_TimeUpdateMethod == UpdateMethod.LateUpdate)
				ProgressTimeline();

			if(m_DateUpdateMethod == UpdateMethod.LateUpdate)
				ProgressDateTime();

		}

		void FixedUpdate()
		{

			if(!m_Progress) return;

			if(m_TimeUpdateMethod == UpdateMethod.FixedUpdate)
				ProgressTimeline();

			if(m_DateUpdateMethod == UpdateMethod.FixedUpdate)
				ProgressDateTime();

		}
			
		void ProgressTimeline()
		{
			if(!m_UseSystemDateTime)
				m_Timeline += (m_Progress && Application.isPlaying) ? LSkyTime.TimelineDelta(CycleDuration, k_TimelineLength) : 0.0f;
			else
				m_Timeline = (float)SystemDateTime.TimeOfDay.TotalHours;
		}

		void ProgressDateTime()
		{

			if(!m_UseSystemDateTime) 
			{
				//GetDateTime();
				TimeDate  = TimeDate;
			}
			else 
			{
				m_Year  = SystemDateTime.Year;
				m_Month = SystemDateTime.Month;
				m_Day   = SystemDateTime.Day;
			}
		}

		/*
		void GetDateTime()
		{

			DateTime DT = new DateTime(0, DateTimeKind.Utc); 
			
			RepeatDateTime(); // Repeat full date cycle.
			
			DT = DT.AddYears(m_Year - 1).AddMonths(m_Month - 1).AddDays(m_Day - 1).AddHours(m_Timeline); // Add date and time in DateTime.
			
			m_Timeline = Mathf.Repeat(m_Timeline, TimelineLength); // repeat timeline.

			_DateTime = DT;
			m_Year = _DateTime.Year;
			m_Month = _DateTime.Month;
			m_Day = _DateTime.Day;
		}*/

		// Repeat date time cycle.
		void RepeatDateTime()
		{
			if(m_Year == 9999 && m_Month == 12 && m_Day == 31 && m_Timeline >= 23.999f)
			{
				m_Timeline = 0.0f;
				m_Day      = 1;
				m_Month    = 1;
				m_Year     = 1;
			}

			if(m_Year == 1 && m_Month == 1 && m_Day == 1 && m_Timeline < 0.0f) 
			{
				m_Timeline = 23.999f;
				m_Day      = 31;
				m_Month    = 12;
				m_Year     = 9999;
			}
		}
	}
}