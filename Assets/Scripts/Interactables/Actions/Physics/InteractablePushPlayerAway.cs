using Managers;
using UnityEngine;

namespace Interactables.Actions.Physics
{
	public class InteractablePushPlayerAway : Interactable
	{
		[SerializeField] private float force;

		public override void Interact() => PlayerManager.Instance.PlayerRigidbody.AddForce(Vector3.ProjectOnPlane(
			PlayerManager.Instance.PlayerRigidbody.transform.position - transform.position, 
			Vector3.up).normalized * force, ForceMode.Impulse);
	}
}