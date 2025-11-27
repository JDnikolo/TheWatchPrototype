using System;
using Callbacks.Slider;
using Managers.Persistent;
using UnityEngine;

namespace UI.Slider
{
	[AddComponentMenu("UI/Elements/Slider/Audio Slider")]
	public sealed class AudioSlider : SliderReceiver, ISliderReceiver
	{
		[SerializeField] private AudioType type;

		private enum AudioType : byte
		{
			Ambiance,
			Effects,
			Master,
			Music,
			Speaker,
		}
		
		public override void OnPrewarm()
		{
			base.OnPrewarm();
			float value;
			switch (type)
			{
				case AudioType.Ambiance:
					value = AudioManager.Instance.AmbianceVolume;
					break;
				case AudioType.Effects:
					value = AudioManager.Instance.EffectsVolume;
					break;
				case AudioType.Master:
					value = AudioManager.Instance.MasterVolume;
					break;
				case AudioType.Music:
					value = AudioManager.Instance.MusicVolume;
					break;
				case AudioType.Speaker:
					value = AudioManager.Instance.SpeakerVolume;
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
			
			Slider.SetFloat(value, false);
		}
		
		public override void OnSliderChanged(float value)
		{
			switch (type)
			{
				case AudioType.Ambiance:
					AudioManager.Instance.AmbianceVolume = value;
					break;
				case AudioType.Effects:
					AudioManager.Instance.EffectsVolume = value;
					break;
				case AudioType.Master:
					AudioManager.Instance.MasterVolume = value;
					break;
				case AudioType.Music:
					AudioManager.Instance.MusicVolume = value;
					break;
				case AudioType.Speaker:
					AudioManager.Instance.SpeakerVolume = value;
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		public override void OnSliderChanged(int value) => throw new InvalidOperationException();
	}
}