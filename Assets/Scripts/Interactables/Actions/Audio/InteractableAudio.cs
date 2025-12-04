using Attributes;
using Audio;
using Runtime.Automation;
using UnityEngine;
using AudioClip = Audio.AudioClip;

namespace Interactables.Actions.Audio
{
	[AddComponentMenu("Interactables/Audio/Play Audio")]
	public sealed class InteractableAudio : Interactable
	{
		[SerializeField] [AutoAssigned(AssignMode.Self, typeof(AudioPlayer))]
		private AudioPlayer player;
		
		[SerializeField] private AudioClip clip;
		
		public override void Interact()
		{
			if (!clip)
			{
				Debug.LogError("Audio clip is null!", this);
				return;
			}
			
			player.Play(clip);
		}
	}
}