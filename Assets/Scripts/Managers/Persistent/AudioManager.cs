using Audio;
using Runtime;
using UnityEngine;
using UnityEngine.Audio;
using Utilities;
using AudioClip = Audio.AudioClip;

namespace Managers.Persistent
{
	[AddComponentMenu("Managers/Persistent/Audio Manager")]
	public sealed class AudioManager : Singleton<AudioManager>
	{
		[SerializeField] private AudioMixer mixer;
		[SerializeField] private AudioPlayer musicPlayer;
		[SerializeField] private AudioPlayer fadePlayer;
		[SerializeField] private float fadeInTime = 1f;
		[SerializeField] private float fadeOutTime = 1f;
		
		private float m_fadeInTime;
		private float m_fadeInTimer;
		private float m_fadeInVolume;
		private float m_fadeOutTime;
		private float m_fadeOutTimer;
		private float m_fadeOutVolume;
		private bool m_delayedFade;
		
		protected override bool Override => false;

		public bool RequireUpdate { get; private set; }

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

		public void OnFrameUpdate()
		{
			var deltaTime = Time.deltaTime;
			if (m_fadeOutTimer > 0f)
			{
				m_fadeOutTimer -= deltaTime;
				fadePlayer.Volume = Mathf.Clamp01(m_fadeOutTimer / fadeOutTime) * m_fadeOutVolume;
				if (m_fadeOutTimer <= 0f) fadePlayer.Stop();
				else if (m_delayedFade) return;
			}
			
			if (m_fadeInTimer > 0f)
			{
				m_fadeInTimer -= deltaTime;
				fadePlayer.Volume = (-Mathf.Clamp01(m_fadeInTimer / fadeInTime) + 1f) * m_fadeInVolume;
			}

			if (m_fadeInTimer <= 0f && m_fadeOutTimer <= 0f) RequireUpdate = false;
		}
		
		public void StartMusic(AudioClip music, bool delayedFade = false, 
			float fadeInTime = -1f, float fadeOutTime = -1f)
		{
			m_delayedFade = delayedFade;
			if (musicPlayer.IsPlaying) (musicPlayer, fadePlayer) = (fadePlayer, musicPlayer);
			if (fadeInTime < 0f) fadeInTime = this.fadeInTime;
			m_fadeInTime = fadeInTime;
			musicPlayer.Play(music);
			if (m_fadeInTime != 0f)
			{
				m_fadeInTimer = m_fadeInTime;
				m_fadeInVolume = music.Settings.Volume;
				musicPlayer.Volume = 0f;
			}
			
			if (fadePlayer.IsPlaying) SetupFadeOut(ref fadeOutTime);
		}

		public void StopMusic(float fadeOutTime = -1f)
		{
			if (!musicPlayer.IsPlaying) return;
			if (fadePlayer.IsPlaying) fadePlayer.Stop();
			(musicPlayer, fadePlayer) = (fadePlayer, musicPlayer);
			SetupFadeOut(ref fadeOutTime);
		}

		private void SetupFadeOut(ref float fadeOutTime)
		{
			if (fadeOutTime < 0f) fadeOutTime = this.fadeOutTime;
			m_fadeOutTime = fadeOutTime;
			if (m_fadeOutTime == 0f) fadePlayer.Stop();
			else
			{
				var trueVolume = fadePlayer.CurrentClip.Settings.Volume;
				m_fadeOutTimer = m_fadeOutTime * (fadePlayer.Volume / trueVolume);
				m_fadeOutVolume = trueVolume;
			}
		}
	}
}