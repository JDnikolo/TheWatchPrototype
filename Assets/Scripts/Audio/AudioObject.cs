using UnityEngine;

namespace Audio
{
	public abstract class AudioObject : BaseObject
	{
		[SerializeField] private AudioSettings settings;
		[SerializeField] private AudioGroup group;
		
		public AudioSettings Settings => settings;
		
		public AudioGroup Group => group;
	}
}