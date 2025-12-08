using Managers.Persistent;
using UnityEngine;
using AudioClip = Audio.AudioClip;

namespace Interactables.Actions.Audio
{
	[AddComponentMenu("Interactables/Audio/Start Music")]
	public sealed class InteractableStartMusic : Interactable
	{
		[SerializeField] private AudioClip music;
		[SerializeField] private float fadeInTime = -1f;
		[SerializeField] private float fadeOutTime = -1f;
		[SerializeField] private bool delayedFade;
		
		public override void Interact() => 
			AudioManager.Instance.StartMusic(music, delayedFade, fadeInTime, fadeOutTime);
	}
}