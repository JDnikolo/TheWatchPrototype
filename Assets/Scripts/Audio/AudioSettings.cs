using UnityEngine;

namespace Audio
{
	public abstract class AudioSettings : ScriptableObject
	{
		[SerializeField] private bool bypassEffects;
		[SerializeField] private bool bypassListenerEffects;
		[SerializeField] private bool bypassReverbZones;
		[SerializeField] private bool loop;
		[SerializeField] [Range(0f, 256f)] private int priority = 128;
		[SerializeField] [Range(0f, 1f)] private float volume = 1f;
		[SerializeField] [Range(-3f, 3f)] private float pitch = 1f;
		[SerializeField] [Range(-1f, 1f)] private float stereoPan = 0f;
		[SerializeField] [Range(0f, 1.1f)] private float reverbZoneMix = 1f;

		public bool Loop => loop;
		
		public float Volume => volume;
		
		public float GetVolume(AudioGroup group) => volume * group.VolumeOverride;
		
		public virtual void Apply(AudioSource source, AudioGroup group)
		{
			if (!group)
			{
				Debug.LogError("Audio group is null!", this);
				return;
			}
			
			source.bypassEffects = bypassEffects;
			source.bypassListenerEffects = bypassListenerEffects;
			source.bypassReverbZones = bypassReverbZones;
			source.loop = loop;
			source.priority = priority;
			source.volume = GetVolume(group);
			source.pitch = pitch;;
			source.panStereo = stereoPan;
			source.reverbZoneMix = reverbZoneMix;
		}
	}
}