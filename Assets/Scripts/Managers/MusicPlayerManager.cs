using System;
using System.Linq;
using Attributes;
using Audio;
using Callbacks.Pausing;
using Managers.Persistent;
using Runtime;
using Runtime.Automation;
using Runtime.FrameUpdate;
using UnityEngine;
using Utilities;

namespace Managers
{
    public class MusicPlayerManager : Singleton<MusicPlayerManager>, IPauseCallback
    {
        protected override bool Override { get; } = true;
        [SerializeField] [AutoAssigned(AssignMode.Child, typeof(AudioSource))]
        private MusicPlayer[] musicPlayers;

        public FrameUpdatePosition FrameUpdateOrder { get; } = FrameUpdatePosition.Default;
        
        
        public void FadeIntoClip(ClipObject audioClip, float fadeDuration = 1.0f)
        {
            foreach (var source in musicPlayers.Where(source => source.IsPlaying))
            {
                source.FadeOut(duration: fadeDuration);
            }

            if (!GetFreeAudioSource(out var freeSourceIndex)) return;
            
            musicPlayers[freeSourceIndex].SetClip(audioClip);
            musicPlayers[freeSourceIndex].FadeIn(fadeDuration);
        }

        public void PlayClipImmediate(ClipObject audioClip , bool setLooping = true)
        {
            StopAllSources();
            musicPlayers[0].SetClip(audioClip, setLooping);
            musicPlayers[0].PlayImmediate();
        }
        
        
        public void StopAllSources()
        {
            foreach (var source in musicPlayers)
            {
               source.StopImmediate();
            }
        }

        public void PauseAllSources()
        {
            foreach (var source in musicPlayers)
            {
                source.Pause();
            }
        }

        public void ResumeAllSources()
        {
            foreach (var source in musicPlayers)
            {
                source.Resume();
            }
        }
        
        private bool GetFreeAudioSource(out int freeSourceIndex)
        {
            for (var i = 0; i < musicPlayers.Length; i++)
            {
                if (musicPlayers[i].IsPlaying) continue;
                freeSourceIndex = i;
                return true;
            }
            freeSourceIndex = -1;
            return false;
        }

        private void Start()
        {
            PauseManager.Instance.AddPausedCallback(this);
        }

        private new void OnDestroy()
        {
            PauseManager.Instance?.RemovePausedCallback(this);
            base.OnDestroy();
        }

        public void OnPauseChanged(bool paused)
        {
            foreach (var player in musicPlayers)
            {
                if (!player.IsPlaying) continue;
                if (player.State == MusicPlayerState.FadingIn || player.State == MusicPlayerState.FadingOut)
                {
                    if (paused) player.Pause();
                    else player.Resume();
                }
                player.SetVolumePercent(0.5f);
            }
        }
    }
}