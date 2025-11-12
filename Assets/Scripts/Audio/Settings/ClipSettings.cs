using UnityEngine;

namespace Audio.Settings
{
	public abstract class ClipSettings : ScriptableObject
	{
		[SerializeField] [Range(0f, 256f)] private int priority = 128;
		[SerializeField] [Range(0f, 1f)] private float volume = 1f;
		[SerializeField] [Range(-3f, 3f)] private float pitch = 1f;
		[SerializeField] [Range(-1f, 1f)] private float stereoPan = 0f;
		[SerializeField] [Range(0f, 1.1f)] private float reverbZoneMix = 1f;
		
		public virtual void Apply(AudioSource source)
		{
			source.priority = priority;
			source.volume = volume;
			source.pitch = pitch;
			source.panStereo = stereoPan;
			source.reverbZoneMix = reverbZoneMix;
		}
	}
}