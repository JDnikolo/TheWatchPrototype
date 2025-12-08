using Attributes;
using LookupTables;
using UnityEngine;

namespace Interactables.Triggers
{
    [AddComponentMenu("Interactables/Triggers/On Shout Trigger")]
    public sealed class InteractableShoutTrigger : InteractableTrigger
    {
        [SerializeField] [AutoAssigned(AssignMode.Self | AssignMode.Child, typeof(Collider))] [CanBeNullInPrefab]
        private new Collider collider;

        private void Start() => ColliderTable.Instance?.Add(collider, this);

        public void OnGettingShouted() => OnInteract();

        public void SetCollider(Collider newCollider)
        {
            if (!newCollider) return;
            if (collider) ColliderTable.Instance?.Remove(collider);
            collider = newCollider;
            ColliderTable.Instance?.Add(collider, this);
        }
    }
}