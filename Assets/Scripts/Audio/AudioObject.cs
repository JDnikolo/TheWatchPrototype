using Audio.Settings;
using UnityEngine;

namespace Audio
{
	public abstract class AudioObject : ScriptableObject
	{
		[SerializeField] private ClipSettings settings;
		
		public ClipSettings Settings => settings;
	}
}