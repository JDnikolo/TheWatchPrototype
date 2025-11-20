using Managers.Persistent;
using UnityEngine;

namespace Interactables.Actions
{
	[AddComponentMenu("Interactables/Save Settings Interactable")]
	public sealed class InteractableSaveSettings : Interactable
	{
		public override void Interact() => SettingsManager.Instance.Save();
	}
}