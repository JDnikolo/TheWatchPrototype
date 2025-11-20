using Runtime;
using UnityEngine;

namespace Interactables.Triggers
{
	[AddComponentMenu("Interactables/Triggers/Interactable Scene Start Trigger")]
	public sealed class InteractableBeforePlayTrigger : MonoBehaviour, IBeforePlay
	{
		public void OnBeforePlay()
		{
			throw new System.NotImplementedException();
		}
	}
}