using Attributes;
using Audio;
using UnityEngine;

namespace Interactables.Actions.Audio
{
	[AddComponentMenu("Interactables/Input/Toggle Looping Audio")]
	public sealed class InteractableToggleAudio : Interactable
	{
		[SerializeField] [AutoAssigned(AssignMode.Self, typeof(AudioPlayer))]
		private AudioPlayer player;

		[SerializeField] private bool play;

		public override void Interact()
		{
			if (play) player.Resume();
			else player.StopForResume();
		}
	}
}