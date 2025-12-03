using Managers;
using UnityEngine;

namespace Interactables.Actions.Physics
{
    public class InteractablePushSelfFromPlayer : Interactable
    {
        [SerializeField] private float force;
        [SerializeField] private Rigidbody selfRigidbody;
        
        public override void Interact()
        {
            var direction = (selfRigidbody.transform.position - PlayerManager.Instance.PlayerObject.transform.position).normalized;
            if (selfRigidbody.isKinematic) selfRigidbody.isKinematic = false;
            selfRigidbody.AddForce(direction * force, ForceMode.Impulse);
        }
    }
}