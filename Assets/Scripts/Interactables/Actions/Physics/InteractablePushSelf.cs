using Attributes;
using Managers;
using UnityEngine;

namespace Interactables.Actions.Physics
{
    public class InteractablePushSelfFromPlayer : Interactable
    {
        [SerializeField] [AutoAssigned(AssignModeFlags.Parent | AssignModeFlags.Self, typeof(Rigidbody))]
        private Rigidbody selfRigidbody;
        
        [SerializeField] private float force = 10;

        public override void Interact()
        {
            var direction = (selfRigidbody.transform.position - 
                             PlayerManager.Instance.PlayerCamera.transform.position).normalized;
            if (selfRigidbody.isKinematic) selfRigidbody.isKinematic = false;
            selfRigidbody.AddForce(direction * force, ForceMode.Impulse);
        }
    }
}