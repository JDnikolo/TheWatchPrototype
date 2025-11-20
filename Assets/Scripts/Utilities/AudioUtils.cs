using System;
using UnityEngine;
using UnityEngine.Audio;

namespace Utilities
{
	public static partial class Utils
	{
		public static float AudioToPercentage(this float value) => Mathf.Pow(10f, value / 20f);
		
		public static float PercentageToAudio(this float value) => Mathf.Log10(value) * 20f;

		public static float GetFloatSafe(this AudioMixer mixer, string name)
		{
			if (!mixer.GetFloat(name, out var result))
				throw new InvalidOperationException($"Mixer has no float variable called {name}");
			return result;
		}
	}
}