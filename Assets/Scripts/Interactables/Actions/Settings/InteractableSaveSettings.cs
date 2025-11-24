using Managers.Persistent;
using UnityEngine;

namespace Interactables.Actions.Settings
{
	[AddComponentMenu("Interactables/Settings/Save Settings")]
	public sealed class InteractableSaveSettings : Interactable
	{
		public override void Interact() => SettingsManager.Instance.Save();
	}
}