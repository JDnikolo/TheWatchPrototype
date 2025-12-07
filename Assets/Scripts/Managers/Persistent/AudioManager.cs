using Audio;
using Callbacks.Audio;
using Runtime;
using UnityEngine;
using UnityEngine.Audio;
using Utilities;
using AudioClip = Audio.AudioClip;

namespace Managers.Persistent
{
	[AddComponentMenu("Managers/Persistent/Audio Manager")]
	public sealed partial class AudioManager : Singleton<AudioManager>, IAudioGroupVolumeChanged
	{
		[SerializeField] private AudioMixer mixer;
		[SerializeField] private AudioPlayer musicPlayer;
		[SerializeField] private AudioPlayer fadePlayer;
		[SerializeField] private float fadeInTime = 1f;
		[SerializeField] private float fadeOutTime = 1f;

		private float m_musicFadeInTime;
		private float m_musicFadeInTimer;
		private float m_musicFadeInVolume;
		private float m_musicFadeOutTime;
		private float m_musicFadeOutTimer;
		private float m_musicFadeOutVolume;
		private bool m_musicDelayedFade;

		private AudioSnapshot m_currentSnapshot;
		private AudioSnapshot m_fadeSnapshot;
		private float m_snapshotFadeInTime;
		private float m_snapshotFadeInTimer;
		private float m_snapshotFadeOutTime;
		private float m_snapshotFadeOutTimer;
		private SnapshotFadeMode m_snapshotFadeMode;

		private float m_pauseFadeInTime;
		private float m_pauseFadeOutTime;
		private bool m_pauseDelayedFade;

		public enum SnapshotFadeMode : byte
		{
			None,
			FadeMixed,
			FadeDelayed,
		}

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

		public State PauseState
		{
			get => new()
			{
				CurrentSnapshot = m_currentSnapshot,
				FadeInTime = m_pauseFadeInTime,
				FadeOutTime = m_pauseFadeOutTime,
				DelayedFade = m_pauseDelayedFade
			};
			set => SetSnapshot(value.CurrentSnapshot, value.DelayedFade, value.FadeInTime, value.FadeOutTime);
		}

		public void OnAudioGroupVolumeChanged(AudioGroup group)
		{
			AudioObject source;
			if ((source = musicPlayer.AudioSource) && source.Group == group)
			{
				m_musicFadeInVolume = source.Settings.GetVolume(group);
				if (musicPlayer.IsPlaying && m_musicFadeInTimer <= 0f) musicPlayer.Volume = m_musicFadeInVolume;
			}
			else if ((source = fadePlayer.AudioSource) && source.Group == group)
			{
				m_musicFadeOutVolume = source.Settings.GetVolume(group);
				if (fadePlayer.IsPlaying && m_musicFadeInTimer > 0f)
					fadePlayer.Volume = m_musicFadeInVolume * Utils.ZeroToOne(m_musicFadeOutTimer, m_musicFadeOutTime);
			}
		}

		public void PreparePause(bool delayedFade = false, float fadeInTime = -1f, float fadeOutTime = -1f)
		{
			m_pauseFadeInTime = fadeInTime;
			m_pauseFadeOutTime = fadeOutTime;
			m_pauseDelayedFade = delayedFade;
		}

		public void OnFrameUpdate()
		{
			var deltaTime = Time.deltaTime;
			FadeMusic(ref deltaTime);
			FadeSnapshot(ref deltaTime);
			CheckUpdate();
		}

		private void FadeMusic(ref float deltaTime)
		{
			if (m_musicFadeOutTimer > 0f)
			{
				m_musicFadeOutTimer -= deltaTime;
				if (m_musicFadeOutTimer <= 0f) StopPlayer(fadePlayer);
				else
				{
					fadePlayer.Volume = Utils.ZeroToOne(m_musicFadeOutTimer, m_musicFadeOutTime) * m_musicFadeOutVolume;
					if (m_musicDelayedFade) return;
				}
			}

			if (m_musicFadeInTimer > 0f)
			{
				m_musicFadeInTimer -= deltaTime;
				musicPlayer.Volume = Utils.OneToZero(m_musicFadeInTimer, m_musicFadeInTime) * m_musicFadeInVolume;
			}
		}

		private void FadeSnapshot(ref float deltaTime)
		{
			if (m_snapshotFadeMode == SnapshotFadeMode.FadeDelayed && m_snapshotFadeOutTimer > 0f)
			{
				m_snapshotFadeOutTimer -= deltaTime;
				if (m_snapshotFadeOutTimer > 0f)
				{
					m_fadeSnapshot.ApplyTo(null, Utils.ZeroToOne(m_snapshotFadeOutTimer, m_snapshotFadeOutTime));
					return;
				}

				m_fadeSnapshot = null;
				m_snapshotFadeMode = SnapshotFadeMode.FadeMixed;
			}

			if (m_snapshotFadeMode == SnapshotFadeMode.FadeMixed)
			{
				if (m_snapshotFadeInTimer > 0f)
				{
					m_snapshotFadeInTimer -= deltaTime;
					m_currentSnapshot.ApplyFrom(m_fadeSnapshot,
						Utils.OneToZero(m_snapshotFadeInTimer, m_snapshotFadeInTime));
				}

				if (m_snapshotFadeInTimer <= 0f)
				{
					m_fadeSnapshot = null;
					m_snapshotFadeMode = SnapshotFadeMode.None;
				}
			}
		}

		public void StartMusic(AudioClip music, bool delayedFade = false,
			float fadeInTime = -1f, float fadeOutTime = -1f)
		{
			if (fadeInTime < 0f) fadeInTime = this.fadeInTime;
			if (fadeOutTime < 0f) fadeOutTime = this.fadeOutTime;
			m_musicFadeInTime = m_musicFadeInTimer = m_musicFadeOutTime = m_musicFadeOutTimer = 0f;
			m_musicDelayedFade = delayedFade;
			m_musicFadeInVolume = music.Settings.GetVolume(music.Group);
			if (musicPlayer.IsPlaying) (musicPlayer, fadePlayer) = (fadePlayer, musicPlayer);
			SetupMusicFadeIn(ref fadeInTime);
			StartPlayer(musicPlayer, music);
			if (fadePlayer.IsPlaying) SetupMusicFadeOut(ref fadeOutTime);
			CheckUpdate();
		}

		private void SetupMusicFadeIn(ref float fadeInTime)
		{
			m_musicFadeInTime = fadeInTime;
			if (m_musicFadeInTime == 0f) musicPlayer.Volume = m_musicFadeInVolume;
			else
			{
				m_musicFadeInTimer = m_musicFadeInTime;
				musicPlayer.Volume = 0f;
			}
		}

		public void StopMusic(float fadeOutTime = -1f)
		{
			if (fadeOutTime < 0f) fadeOutTime = this.fadeOutTime;
			if (musicPlayer.IsPlaying)
			{
				if (fadePlayer.IsPlaying) StopPlayer(fadePlayer);
				(musicPlayer, fadePlayer) = (fadePlayer, musicPlayer);
				SetupMusicFadeOut(ref fadeOutTime);
			}

			CheckUpdate();
		}

		private void SetupMusicFadeOut(ref float fadeOutTime)
		{
			var elapsedPercentage = m_musicFadeOutTimer <= 0f
				? 1f
				: Utils.ZeroToOne(m_musicFadeOutTimer, m_musicFadeOutTime);
			m_musicFadeOutTime = fadeOutTime;
			if (m_musicFadeOutTime == 0f) StopPlayer(fadePlayer);
			else
			{
				m_musicFadeOutTimer = m_musicFadeOutTime * elapsedPercentage;
				var source = fadePlayer.AudioSource;
				m_musicFadeOutVolume = source.Settings.GetVolume(source.Group) * elapsedPercentage;
			}
		}

		public void SetSnapshot(AudioSnapshot snapshot, bool delayedFade = false,
			float fadeInTime = -1f, float fadeOutTime = -1f)
		{
			m_snapshotFadeInTimer = m_snapshotFadeOutTimer = 0f;
			m_snapshotFadeMode = SnapshotFadeMode.None;
			if (fadeInTime < 0f) fadeInTime = this.fadeInTime;
			if (fadeOutTime < 0f) fadeOutTime = this.fadeOutTime;
			if (!m_currentSnapshot) m_fadeSnapshot = null;
			else (m_currentSnapshot, m_fadeSnapshot) = (m_fadeSnapshot, m_currentSnapshot);

			if (!m_fadeSnapshot) delayedFade = false;
			else if (fadeOutTime <= 0f)
			{
				m_fadeSnapshot.ApplyTo(null, 1f);
				m_fadeSnapshot = null;
			}
			else m_snapshotFadeOutTime = m_snapshotFadeOutTimer = fadeOutTime;

			m_currentSnapshot = snapshot;
			if (fadeInTime <= 0f)
			{
				m_currentSnapshot.ApplyFrom(m_fadeSnapshot, 1f);
				if (!m_fadeSnapshot) return;
			}
			else m_snapshotFadeInTime = m_snapshotFadeInTimer = fadeInTime;

			m_snapshotFadeMode = !delayedFade ? SnapshotFadeMode.FadeMixed : SnapshotFadeMode.FadeDelayed;
			CheckUpdate();
		}

		private void CheckUpdate() => RequireUpdate = m_musicFadeInTimer > 0f || m_musicFadeOutTimer > 0f ||
													m_snapshotFadeMode != SnapshotFadeMode.None;

		private void StartPlayer(AudioPlayer player, AudioClip clip)
		{
			clip.Group.AddCallback(this);
			player.Play(clip);
		}

		private void StopPlayer(AudioPlayer player)
		{
			if (player.AudioSource) player.AudioSource.Group.RemoveCallback(this);
			player.Stop();
		}
#if UNITY_EDITOR
		public float MusicFadeInTime => m_musicFadeInTime;

		public float MusicFadeInTimer => m_musicFadeInTimer;

		public float MusicFadeInVolume => m_musicFadeInVolume;

		public float MusicFadeOutTime => m_musicFadeOutTime;

		public float MusicFadeOutTimer => m_musicFadeOutTimer;

		public float MusicFadeOutVolume => m_musicFadeOutVolume;

		public bool MusicDelayedFade => m_musicDelayedFade;

		public AudioSnapshot CurrentSnapshot => m_currentSnapshot;

		public AudioSnapshot FadeSnapshotEditor => m_fadeSnapshot;

		public float SnapshotFadeInTime => m_snapshotFadeInTime;

		public float SnapshotFadeInTimer => m_snapshotFadeInTimer;

		public float SnapshotFadeOutTime => m_snapshotFadeOutTime;

		public float SnapshotFadeOutTimer => m_snapshotFadeOutTimer;

		public SnapshotFadeMode SnapshotFadeModeEditor => m_snapshotFadeMode;
#endif
	}
}