using Attributes;
using Callbacks.Pausing;
using Managers;
using Managers.Persistent;
using Runtime.Automation;
using Runtime.FixedUpdate;
using UnityEngine;
using UnityEngine.Serialization;
using Utilities;

namespace Audio.Ambiance
{
    public sealed class AmbientSoundArea : MonoBehaviour, IFixedUpdatable, IPauseCallback
    {
        [FormerlySerializedAs("m_boxCollider")] [SerializeField] [AutoAssigned(AssignMode.Self, typeof(Collider))]
        private Collider boxCollider;

        [FormerlySerializedAs("m_audioSource")] [SerializeField] [AutoAssigned(AssignMode.Child, typeof(AudioSource))]
        private AudioSource audioSource;

        [SerializeField] private AudioAggregate ambientAudios;

        public FixedUpdatePosition FixedUpdateOrder => FixedUpdatePosition.Default;

        public void OnFixedUpdate()
        {
            if (!audioSource.isPlaying)
            {
                audioSource.clip = ambientAudios.Clips.GetRandom();
                ambientAudios.Settings.Apply(audioSource, ambientAudios.Group);
                audioSource.Play();
            }

            audioSource.transform.position = boxCollider.ClosestPoint(
                PlayerManager.Instance.PlayerObject.transform.position);
        }

        public void OnPauseChanged(bool paused)
        {
            if (paused) audioSource.Pause();
            else audioSource.UnPause();
        }

        private void Start()
        {
            GameManager.Instance.AddFixedUpdate(this);
            PauseManager.Instance.AddPausedCallback(this);
        }

        private void OnDestroy()
        {
            GameManager.Instance?.RemoveFixedUpdate(this);
            PauseManager.Instance?.RemovePausedCallback(this);
        }
    }
}