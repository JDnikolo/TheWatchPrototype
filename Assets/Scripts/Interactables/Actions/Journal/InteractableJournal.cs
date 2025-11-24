using Managers;
using UnityEngine;

namespace Interactables.Actions.Journal
{
	[AddComponentMenu("Interactables/Journal/Set Can Open Journal")]
	public sealed class InteractableJournal : Interactable
	{
		[SerializeField] private bool target;
		
		public override void Interact() => JournalManager.Instance.CanOpenJournal = target;
	}
}