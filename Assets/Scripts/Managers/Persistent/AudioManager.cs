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
			ResetUpdate();
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
			TestUpdate();
		}

		public void StopMusic(float fadeOutTime = -1f)
		{
			ResetUpdate();
			if (musicPlayer.IsPlaying)
			{
				if (fadePlayer.IsPlaying) fadePlayer.Stop();
				(musicPlayer, fadePlayer) = (fadePlayer, musicPlayer);
				SetupFadeOut(ref fadeOutTime);
			}
			
			TestUpdate();
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

		private void ResetUpdate() => m_fadeInTime = m_fadeOutTime = m_fadeInTimer = m_fadeOutTimer = 0f;

		private void TestUpdate() => RequireUpdate = m_fadeInTime > 0f || m_fadeOutTime > 0f;
#if UNITY_EDITOR
		public float FadeInTime => m_fadeInTime;
		
		public float FadeOutTime => m_fadeOutTime;
		
		public float FadeInVolume => m_fadeInVolume;
		
		public float FadeOutVolume => m_fadeOutVolume;
		
		public float FadeInTimer => m_fadeInTimer;
		
		public float FadeOutTimer => m_fadeOutTimer;
		
		public bool DelayedFade => m_delayedFade;
#endif
	}
}