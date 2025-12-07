using System;
using Attributes;
using UnityEngine;

namespace Interactables.Triggers
{
	public abstract class InteractableTrigger : BaseBehaviour
	{
		[SerializeField] private Interactable interactable;
		//TODO Replace this with a single interactable
		[Obsolete, SerializeField] private Interactable[] interactables;

		protected void OnInteract() => interactable.Interact();
	}
}