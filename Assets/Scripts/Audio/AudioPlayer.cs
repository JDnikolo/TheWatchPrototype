using System;
using Attributes;
using Callbacks.Audio;
using Callbacks.Pausing;
using Managers;
using Runtime;
using Runtime.FrameUpdate;
using UnityEngine;
using Utilities;

namespace Audio
{
    [AddComponentMenu("Audio/Audio Player")]
    public sealed class AudioPlayer : BaseBehaviour, IFrameUpdatable, IPauseCallback, IAudioGroupVolumeChanged
    {
        [SerializeField, AutoAssigned(AssignModeFlags.Self, typeof(AudioSource))]
        private AudioSource audioSource;

        [SerializeField] private bool standalone = true;
        [SerializeField] private bool pausable;

        private Updatable m_updatable;
        
        public FrameUpdatePosition FrameUpdateOrder => FrameUpdatePosition.Audio;
        
        public AudioObject AudioSource { get; private set; }
        
        public bool IsPlaying => audioSource.isPlaying;
        
        public float Volume
        {
            get => audioSource.volume;
            set => audioSource.volume = value;
        }

        public void OnAudioGroupVolumeChanged(AudioGroup group)
        {
            if (!audioSource) return;
            if (AudioSource) AudioSource.Settings.Apply(audioSource, group);
        }
        
        public void OnFrameUpdate()
        {
			if (!audioSource) return;
            if (audioSource.isPlaying) return;
            Stop();
        }
        
        public void OnPauseChanged(bool paused)
        {
            if (paused) Pause();
            else UnPause();
        }
        
        public void Play(AudioClip clip)
        {
            if (!audioSource) return;
            audioSource.clip = clip.Clip;
            PostPlay(clip);
        }

        public void Play(AudioAggregate aggregate)
        {
            if (!audioSource) return;
            audioSource.clip = aggregate.Clips.GetRandom();
            PostPlay(aggregate);
        }

        private void PostPlay(AudioObject obj)
        {
            Stop();
            AudioSource = obj;
            if (standalone) AudioSource.Group.AddCallback(this);
            AudioSource.Settings.Apply(audioSource, obj.Group);
            audioSource.Play();
            if (!AudioSource.Settings.Loop) m_updatable.SetUpdating(true, this);
        }

        public void Stop()
        {
            if (!audioSource) return;
            if (standalone && AudioSource) AudioSource.Group.RemoveCallback(this);
            audioSource.Stop();
            AudioSource = null;
            m_updatable.SetUpdating(false, this);
        }

        public void Resume()
        {
            if (!audioSource) return;
            if (!AudioSource) throw new Exception("Set source first!");
            if (!AudioSource.Settings.Loop) throw new Exception("Source must loop to resume!");
            audioSource.Play();
        }
        
        public void StopForResume()
        {
            if (!audioSource) return;
            if (!AudioSource) throw new Exception("Set source first!");
            if (!AudioSource.Settings.Loop) throw new Exception("Source must loop to stop for resume!");
            audioSource.Stop();
        }

        public void Pause()
        {
            if (!audioSource) return;
            if (AudioSource) audioSource.Pause();
        }

        public void UnPause()
        {
            if (!audioSource) return;
            if (AudioSource) audioSource.UnPause();
        }

        private void Start()
        {
            if (!audioSource) return;
            if (pausable) PauseManager.Instance?.AddPausedCallback(this);
        }

        private void OnDestroy()
        {
            m_updatable.SetUpdating(false, this);
            if (pausable) PauseManager.Instance?.RemovePausedCallback(this);
            if (standalone && AudioSource) AudioSource.Group.RemoveCallback(this);
        }
    }
}
