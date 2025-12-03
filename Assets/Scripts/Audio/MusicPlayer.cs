using System;
using Attributes;
using Managers;
using Managers.Persistent;
using Runtime.Automation;
using Runtime.FrameUpdate;
using UnityEngine;
using Utilities;

namespace Audio
{
    public sealed class MusicPlayer : MonoBehaviour, IFrameUpdatable
    {
        public FrameUpdatePosition FrameUpdateOrder { get; } = FrameUpdatePosition.Default;
    
        [SerializeField] [AutoAssigned(AssignMode.Self, typeof(AudioSource))]
        private AudioSource audioSource;

        public MusicPlayerState State { get; private set; } = MusicPlayerState.NotPlaying;
    
        public bool IsPlaying => State is MusicPlayerState.Playing or MusicPlayerState.FadingIn or MusicPlayerState.FadingOut;

        public bool IsPaused => State is MusicPlayerState.Paused or MusicPlayerState.PausedFadeIn or MusicPlayerState.PausedFadeOut;
    
        private ClipObject m_currentClip;
        private float m_currentMaxVolume;
    
        private float m_fadeTimer;
        private float m_fadeDuration;

        public bool IsLooping { get; private set; } = false;

        public void SetClip(ClipObject clipObject, bool setLooping = true)
        {
            if (!clipObject)
            {
                Debug.LogError("The clip object is null.");
                return;
            }
            m_currentClip = clipObject;
            IsLooping = setLooping;
        
            audioSource.loop = IsLooping;
        
            audioSource.clip = clipObject.Clip;
            clipObject.Settings.Apply(audioSource);
            m_currentMaxVolume = 0f + audioSource.volume;
        
            audioSource.volume = 0;
            State = MusicPlayerState.NotPlaying;
        }

        public void FadeIn(float duration)
        {
            m_fadeTimer = 0f;
            m_fadeDuration = duration;
            State = MusicPlayerState.FadingIn;
        }

        public void FadeOut(float duration)
        {
            m_fadeTimer = 0f;
            m_fadeDuration = duration;
            State = MusicPlayerState.FadingOut;
        }

        public void PlayImmediate()
        {
            m_currentClip.Settings.Apply(audioSource);
            audioSource.Play();
            State = MusicPlayerState.Playing;
        }

        public void StopImmediate()
        {
            audioSource.Stop();
            State = MusicPlayerState.NotPlaying;
        }

        public void SetVolumePercent(float value) => audioSource.volume = Mathf.Clamp01(value * m_currentMaxVolume);

        public void Pause()
        {
            if (State == MusicPlayerState.NotPlaying) return;
            audioSource.Pause();
            State = State switch
            {
                MusicPlayerState.Playing => MusicPlayerState.Paused,
                MusicPlayerState.FadingIn => MusicPlayerState.PausedFadeIn,
                MusicPlayerState.FadingOut => MusicPlayerState.PausedFadeOut,
                MusicPlayerState.NotPlaying => MusicPlayerState.NotPlaying,
                MusicPlayerState.Paused => MusicPlayerState.Paused,
                MusicPlayerState.PausedFadeIn => MusicPlayerState.PausedFadeIn,
                MusicPlayerState.PausedFadeOut => MusicPlayerState.PausedFadeOut,
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        public void Resume()
        {
            if (State == MusicPlayerState.NotPlaying) return;
            audioSource.Play();
            State = State switch
            {
                MusicPlayerState.Playing => MusicPlayerState.Playing,
                MusicPlayerState.FadingIn => MusicPlayerState.FadingIn,
                MusicPlayerState.FadingOut => MusicPlayerState.FadingOut,
                MusicPlayerState.NotPlaying => MusicPlayerState.NotPlaying,
                MusicPlayerState.Paused => MusicPlayerState.Playing,
                MusicPlayerState.PausedFadeIn => MusicPlayerState.FadingIn,
                MusicPlayerState.PausedFadeOut => MusicPlayerState.FadingOut,
                _ => throw new ArgumentOutOfRangeException()
            };
        }


        public void OnFrameUpdate()
        {
            transform.position = PlayerManager.Instance.PlayerCamera.transform.position;
            if (State == MusicPlayerState.Playing && !audioSource.isPlaying) State = MusicPlayerState.NotPlaying;
        
            if (State == MusicPlayerState.FadingIn)
            {
                m_fadeTimer += Time.deltaTime;
                audioSource.volume = Mathf.Lerp(0, m_currentMaxVolume, m_fadeTimer / m_fadeDuration);
                if (m_fadeTimer > m_fadeDuration) State = MusicPlayerState.Playing;
            }
            else if (State == MusicPlayerState.FadingIn)
            {
                m_fadeTimer += Time.deltaTime;
                audioSource.volume = Mathf.Lerp(m_currentMaxVolume, 0, m_fadeTimer / m_fadeDuration);
                if (m_fadeTimer > m_fadeDuration)
                {
                    audioSource.Stop();
                    State = MusicPlayerState.NotPlaying;
                }
            }
        }

        private void Awake()
        {
            GameManager.Instance.AddFrameUpdateSafe(this);
        }

        private void OnDestroy()
        {
            GameManager.Instance?.RemoveFrameUpdateSafe(this);
        }
    
    
    }
}
