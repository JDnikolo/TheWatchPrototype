using Attributes;
using Managers;
using Managers.Persistent;
using Runtime.Automation;
using Runtime.FixedUpdate;
using UnityEngine;

namespace Audio.Ambiance
{
    public sealed class AmbientSoundArea : MonoBehaviour, IFixedUpdatable
    {
        [SerializeField] [AutoAssigned(AssignMode.Self, typeof(Collider))]
        private Collider boxCollider;

        [SerializeField] [AutoAssigned(AssignMode.Child, typeof(AudioPlayer))]
        private AudioPlayer player;

        [SerializeField] private AudioAggregate ambientAudios;

        public FixedUpdatePosition FixedUpdateOrder => FixedUpdatePosition.Default;

        public void OnFixedUpdate()
        {
            if (!player.IsPlaying) player.Play(ambientAudios);
            player.transform.position = boxCollider.ClosestPoint(
                PlayerManager.Instance.PlayerObject.transform.position);
        }
        
        private void Start() => GameManager.Instance.AddFixedUpdate(this);

        private void OnDestroy() => GameManager.Instance?.RemoveFixedUpdate(this);
    }
}