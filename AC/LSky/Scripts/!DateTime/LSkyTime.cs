

//----------------
// Time.
//================

using System;
using UnityEngine;

namespace AC.LSky
{

	public sealed class LSkyTime : Behaviour
	{

		/// <summary>
		/// Delta time for timeline(Add).
		/// </summary>
		/// <returns>The delta.</returns>
		/// <param name="divider">Divider.</param>
		public static float TimelineDelta(float divider, float length)
		{
			return (divider != 0) ? (Time.deltaTime / divider) * length: 0.0f;
		}
			
		/// <summary>
		/// Return time in a float value.
		/// </summary>
		/// <returns>The to timeline.</returns>
		/// <param name="hour">Hour.</param>
		/// <param name="minute">Minute.</param>
		/// <param name="second">Second.</param>
		public static float TimeToFloat(int hour, int minute, int second)
		{
			return (float)hour + ((float)minute / 60f) + ((float)second / 3600f);
		}

		/// <summary>
		/// Return time in a float value.
		/// </summary>
		/// <returns>The to float.</returns>
		/// <param name="hour">Hour.</param>
		/// <param name="minute">Minute.</param>
		/// <param name="second">Second.</param>
		/// <param name="millisecond">Millisecond.</param>
		public static float TimeToFloat(int hour, int minute, int second, int millisecond)
		{
			return (float)hour + (float)minute / 60f + (float)second / 3600f + (float)millisecond / 3600000f;
		}

		/// <summary>
		/// Get Hour in timeline.
		/// </summary>
		/// <returns>The timeline hour.</returns>
		/// <param name="timeline">Timeline.</param>
		public static int GetTimelineHour(float timeline)
		{
			return (int)Mathf.Floor(timeline);
		}

		/// <summary>
		/// Get the minutes in timeline.
		/// </summary>
		/// <returns>The timeline minute.</returns>
		/// <param name="timeline">Timeline.</param>
		public static int GetTimelineMinute(float timeline)
		{
			return (int)Mathf.Floor((timeline - (int)Mathf.Floor(timeline)) * 60);
		}


		/// <summary>
		/// Hour and minute to string.
		/// </summary>
		/// <returns>The to string.</returns>
		/// <param name="hour">Hour.</param>
		/// <param name="minute">Minute.</param>
		public static string TimeToString(int hour, int minute)
		{
			string h = hour   < 10 ? "0" + hour.ToString()   : hour.ToString();
			string m = minute < 10 ? "0" + minute.ToString() : minute.ToString();

			return h + ":" + m;
		}

		/// <summary>
		/// Hour, minute and second to string.
		/// </summary>
		/// <returns>The to string.</returns>
		/// <param name="hour">Hour.</param>
		/// <param name="minute">Minute.</param>
		/// <param name="second">Second.</param>
		public static string TimeToString(int hour, int minute, int second)
		{
			string h = hour   < 10 ? "0" + hour.ToString()   : hour.ToString();
			string m = minute < 10 ? "0" + minute.ToString() : minute.ToString();
			string s = second < 10 ? "0" + second.ToString() : second.ToString();

			return h + ":" + m + ":" + s;
		}

	}
}
