using Managers;
using UnityEngine;
using Utilities;

namespace Interactables.Actions.Physics
{
    public class InteractablePushPlayerAway : Interactable
    {
        [SerializeField] private float force;
        public override void Interact()
        {
            var direction = (PlayerManager.Instance.PlayerRigidbody.transform.position.ToFlatVector() - transform.position.ToFlatVector()).normalized;
            PlayerManager.Instance.PlayerRigidbody.AddForce(new Vector3(direction.x,0, direction.y) * force, ForceMode.Impulse);
        }
    }
}