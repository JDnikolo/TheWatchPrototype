using System;
using Callbacks.Pausing;
using Managers;
using UnityEngine;

namespace Interactables.Triggers
{
	[AddComponentMenu("Interactables/Triggers/On Pause-Changed Trigger")]
	public sealed class InteractablePauseTrigger : MonoBehaviour, IPauseCallback
	{
		[SerializeField] private Interactable onPaused;
		[SerializeField] private Interactable onUnpaused;
		
		public void OnPauseChanged(bool paused)
		{
			if (paused) onPaused?.Interact();
			else onUnpaused?.Interact();
		}

		private void Start() => PauseManager.Instance.AddPausedCallback(this);

		private void OnDestroy() => PauseManager.Instance?.RemovePausedCallback(this);
	}
}