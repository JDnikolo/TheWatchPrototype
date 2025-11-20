using Managers.Persistent;
using UnityEngine;

namespace Interactables.Actions
{
	[AddComponentMenu("Interactables/Load Settings Interactable")]
	public sealed class InteractableLoadSettings : Interactable
	{
		public override void Interact() => SettingsManager.Instance.Load();
	}
}