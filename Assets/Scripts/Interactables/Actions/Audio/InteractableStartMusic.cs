using Managers.Persistent;
using UnityEngine;
using AudioClip = Audio.AudioClip;

namespace Interactables.Actions.Audio
{
	[AddComponentMenu("Interactables/Audio/Start Music")]
	public sealed class InteractableStartMusic : Interactable
	{
		[SerializeField] private AudioClip music;
		[SerializeField] private bool delayedFade;
		[SerializeField] private float fadeInTime = -1f;
		[SerializeField] private float fadeOutTime = -1f;
		
		public override void Interact() => 
			AudioManager.Instance.StartMusic(music, delayedFade, fadeInTime, fadeOutTime);
	}
}