using UnityEngine;
using AudioSettings = Audio.AudioSettings;

namespace Audio
{
	public abstract class AudioObject : ScriptableObject
	{
		[SerializeField] private AudioSettings settings;
		[SerializeField] private AudioGroup group;
		
		public AudioSettings Settings => settings;
		
		public AudioGroup Group => group;
	}
}