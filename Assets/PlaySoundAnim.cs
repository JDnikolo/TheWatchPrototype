using Audio;
using UnityEngine;
using AudioClip = Audio.AudioClip;

public class PlaySoundAnim : MonoBehaviour
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