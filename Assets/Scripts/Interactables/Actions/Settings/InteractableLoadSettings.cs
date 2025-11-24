using Managers.Persistent;
using UnityEngine;

namespace Interactables.Actions.Settings
{
	[AddComponentMenu("Interactables/Settings/Load Settings")]
	public sealed class InteractableLoadSettings : Interactable
	{
		public override void Interact() => SettingsManager.Instance.Load();
	}
}