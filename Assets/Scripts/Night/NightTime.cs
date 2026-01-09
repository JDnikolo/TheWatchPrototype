using System;
using Runtime.Automation;
using UnityEditor;
using Utilities;

namespace Night
{
	[Serializable]
	public struct NightTime : IEquatable<NightTime>
#if UNITY_EDITOR
		, IEditorDisplayable
#endif
	{
		public int second;
		public int minute;
		public int hour;

		public NightTime(int second, int minute, int hour)
		{
			this.second = second;
			this.minute = minute;
			this.hour = hour;
		}

		public bool Update(ref float timer)
		{
			var result = DecreaseBy(ref timer, 60f * 60f, ref hour) |
						DecreaseBy(ref timer, 60f, ref minute) |
						DecreaseBy(ref timer, 1f, ref second) |
						DecreaseBy(ref second, 60, ref minute) |
						DecreaseBy(ref minute, 60, ref hour);
			if (result)
			{
				const int divident = 24;
				var divisor = hour / divident;
				if (divisor > 0) hour -= divisor * divident;
			}

			return result;
		}

		public bool Passed(NightTime other) => hour >= other.hour && minute >= other.minute && second >= other.second;

		private static bool DecreaseBy(ref float value, float divident, ref int increase)
		{
			var divisor = value / divident;
			if (divisor > 0)
			{
				var wholeDivisor = MathF.Floor(divisor);
				increase += (int) wholeDivisor;
				value -= wholeDivisor * divident;
				return true;
			}

			return false;
		}
			
		private static bool DecreaseBy(ref int value, int divident, ref int increase)
		{
			var divisor = value / divident;
			if (divisor > 0)
			{
				increase += divisor;
				value -= divisor * divident;
				return true;
			}

			return false;
		}
			
		public bool Equals(NightTime other) => second == other.second && minute == other.minute && hour == other.hour;

		public override bool Equals(object obj) => obj is NightTime other && Equals(other);

		public override int GetHashCode() => HashCode.Combine(second, minute, hour);

		public override string ToString() => $"S: {second} M: {minute} H: {hour}";
#if UNITY_EDITOR
		public void DisplayInEditor()
		{
			EditorGUILayout.IntField("Second", second);
			EditorGUILayout.IntField("Minute", minute);
			EditorGUILayout.IntField("Hour", hour);
		}

		public void DisplayInEditor(string name) => name.DisplayIndented(DisplayInEditor);
#endif
	}
}