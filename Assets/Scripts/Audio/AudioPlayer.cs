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
        [SerializeField] [AutoAssigned(AssignMode.Self, typeof(AudioSource))]
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
            if (AudioSource) AudioSource.Settings.Apply(audioSource, group);
        }
        
        public void OnFrameUpdate()
        {
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
            audioSource.clip = clip.Clip;
            PostPlay(clip);
        }

        public void Play(AudioAggregate aggregate)
        {
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
            if (standalone && AudioSource) AudioSource.Group.RemoveCallback(this);
            audioSource.Stop();
            AudioSource = null;
            m_updatable.SetUpdating(false, this);
        }

        public void Pause()
        {
            if (AudioSource) audioSource.Pause();
        }

        public void UnPause()
        {
            if (AudioSource) audioSource.UnPause();
        }

        private void Start()
        {
            if (pausable) PauseManager.Instance?.AddPausedCallback(this);
        }

        private void OnDestroy()
        {
            m_updatable.SetUpdating(false, this);
            if (pausable) PauseManager.Instance?.RemovePausedCallback(this);
        }
    }
}
