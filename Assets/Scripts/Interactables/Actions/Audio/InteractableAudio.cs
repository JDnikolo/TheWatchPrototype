using Audio;
using UnityEngine;

namespace Interactables.Actions.Audio
{
	[AddComponentMenu("Interactables/Audio/Play Audio")]
	public sealed class InteractableAudio : Interactable
	{
		[SerializeField] private AudioSource source;
		[SerializeField] private ClipObject clip;
		
		public override void Interact()
		{
			if (!source || !clip) return;
			clip.Settings.Apply(source);
			source.clip = clip.Clip;
			source.Play();
		}
	}
}