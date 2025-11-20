using Runtime;
using UnityEngine;
using UnityEngine.Audio;
using Utilities;

namespace Managers.Persistent
{
	[AddComponentMenu("Managers/Persistent/Audio Manager")]
	public sealed class AudioManager : Singleton<AudioManager>
	{
		[SerializeField] private AudioMixer mixer;

		protected override bool Override => false;

		public float AmbianceVolume
		{
			get => mixer.GetFloatSafe(nameof(AmbianceVolume)).AudioToPercentage();
			set => mixer.SetFloat(nameof(AmbianceVolume), value.PercentageToAudio());
		}
		
		public float EffectsVolume
		{
			get => mixer.GetFloatSafe(nameof(EffectsVolume)).AudioToPercentage();
			set => mixer.SetFloat(nameof(EffectsVolume), value.PercentageToAudio());
		}
		
		public float MasterVolume
		{
			get => mixer.GetFloatSafe(nameof(MasterVolume)).AudioToPercentage();
			set => mixer.SetFloat(nameof(MasterVolume), value.PercentageToAudio());
		}
		
		public float MusicVolume
		{
			get => mixer.GetFloatSafe(nameof(MusicVolume)).AudioToPercentage();
			set => mixer.SetFloat(nameof(MusicVolume), value.PercentageToAudio());
		}
		
		public float SpeakerVolume
		{
			get => mixer.GetFloatSafe(nameof(SpeakerVolume)).AudioToPercentage();
			set => mixer.SetFloat(nameof(SpeakerVolume), value.PercentageToAudio());
		}
	}
}