using Attributes;
using LookupTables;
using UnityEngine;

namespace Interactables.Triggers
{
    [AddComponentMenu("Interactables/Triggers/On Shout Trigger")]
    public sealed class InteractableShoutTrigger : InteractableTrigger
    {
        [SerializeField] [AutoAssigned(AssignMode.Self | AssignMode.Forced|AssignMode.Child, typeof(Collider))]
        [CanBeNullInPrefab]
        private new Collider collider;

        private void Start() => ColliderTable.Instance?.Add(collider, this);

        public void OnGettingShouted() => OnInteract();
    }
}