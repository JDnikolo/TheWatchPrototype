using UnityEngine;
using AudioClip = Audio.AudioClip;

namespace Interactables.Actions.Audio
{
	[AddComponentMenu("Interactables/Audio/Play Audio")]
	public sealed class InteractableAudio : Interactable
	{
		[SerializeField] private AudioSource source;
		[SerializeField] private AudioClip clip;
		
		public override void Interact()
		{
			if (!source || !clip) return;
			clip.Settings.Apply(source, clip.Group);
			source.clip = clip.Clip;
			source.Play();
		}
	}
}