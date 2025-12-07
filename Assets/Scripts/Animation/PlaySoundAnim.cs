using UnityEngine;
using AudioClip = Audio.AudioClip;

namespace Animation
{
	public class PlaySoundAnim : BaseBehaviour
	{
		[SerializeField] private AudioSource source;
		[SerializeField] private AudioClip clip;

		public void PlaySound()
		{
			if (!source || !clip) return;
			clip.Settings.Apply(source, clip.Group);
			source.clip = clip.Clip;
			source.Play();
		}
	}
}