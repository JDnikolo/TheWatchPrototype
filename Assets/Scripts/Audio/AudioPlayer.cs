using Attributes;
using Callbacks.Pausing;
using Managers;
using Runtime;
using Runtime.Automation;
using Runtime.FrameUpdate;
using UnityEngine;

namespace Audio
{
    [AddComponentMenu("Audio/Audio Player")]
    public sealed class AudioPlayer : MonoBehaviour, IFrameUpdatable, IPauseCallback
    {
        [SerializeField] [AutoAssigned(AssignMode.Self, typeof(AudioSource))]
        private AudioSource audioSource;

        [SerializeField] private bool pausable;

        private Updatable m_updatable;
        
        public FrameUpdatePosition FrameUpdateOrder => FrameUpdatePosition.Audio;
        
        public AudioClip CurrentClip { get; private set; }
        
        public bool IsPlaying => audioSource.isPlaying;
        
        public float Volume
        {
            get => audioSource.volume;
            set => audioSource.volume = value;
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
            CurrentClip = clip;
            audioSource.clip = CurrentClip.Clip;
            CurrentClip.Settings.Apply(audioSource, CurrentClip.Group);
            audioSource.Play();
            if (!clip.Settings.Loop) m_updatable.SetUpdating(true, this);
        }

        public void Stop()
        {
            audioSource.Stop();
            CurrentClip = null;
            m_updatable.SetUpdating(false, this);
        }

        public void Pause()
        {
            if (CurrentClip) audioSource.Pause();
        }

        public void UnPause()
        {
            if (CurrentClip) audioSource.UnPause();
        }

        private void Start()
        {
            if (pausable) PauseManager.Instance.AddPausedCallback(this);
        }

        private void OnDestroy()
        {
            m_updatable.SetUpdating(false, this);
            if (pausable) PauseManager.Instance?.RemovePausedCallback(this);
        }
    }
}
