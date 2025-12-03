using System;
using System.Collections.Generic;
using Attributes;
using Audio;
using Interactables.Actions.Animation;
using Managers;
using UnityEditor;
using UnityEngine;

namespace Interactables.Actions.Audio
{
    public class InteractableMusic: Interactable
    {
        [SerializeField] private InteractableMusicOptions mode;
        
        [SerializeField] private ClipObject musicToStart;
        [SerializeField] private bool loopTrack;
        [SerializeField] private float fadeDuration;
        
        public override void Interact()
        {
            switch (mode)
            {
                case InteractableMusicOptions.TrackPlayImmediate:
                    MusicPlayerManager.Instance.PlayClipImmediate(musicToStart, loopTrack);
                    break;
                case InteractableMusicOptions.TrackFadeIn:
                    MusicPlayerManager.Instance.FadeIntoClip(musicToStart, fadeDuration);
                    break;
                case InteractableMusicOptions.StopAllTracks:
                    MusicPlayerManager.Instance.StopAllSources();
                    break;
                case InteractableMusicOptions.PauseAllTracks:
                    MusicPlayerManager.Instance.PauseAllSources();
                    break;
                case InteractableMusicOptions.ResumeAllTracks:
                    MusicPlayerManager.Instance.ResumeAllSources();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
#if UNITY_EDITOR
        [CustomEditor(typeof(InteractableMusic))]
        class InteractableMusicEditor : Editor
        {
            public override void OnInspectorGUI()
            {
                var self = (InteractableMusic)target;
                serializedObject.Update();
                var propertiesToExclude = new List<string>();
                switch (self.mode)
                {
                    case InteractableMusicOptions.TrackPlayImmediate:
                        propertiesToExclude.Add("fadeDuration");
                        break;
                    case InteractableMusicOptions.TrackFadeIn:
                        break;
                    case InteractableMusicOptions.StopAllTracks:
                    case InteractableMusicOptions.PauseAllTracks:
                    case InteractableMusicOptions.ResumeAllTracks:
                        propertiesToExclude.Add("fadeDuration");
                        propertiesToExclude.Add("loopTrack");
                        propertiesToExclude.Add("musicToStart");
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                
                DrawPropertiesExcluding(serializedObject,propertiesToExclude.ToArray());
                serializedObject.ApplyModifiedProperties();
            }
        }
#endif

        private enum InteractableMusicOptions
        {
            TrackPlayImmediate, TrackFadeIn, StopAllTracks, PauseAllTracks, ResumeAllTracks
        }
    }
}