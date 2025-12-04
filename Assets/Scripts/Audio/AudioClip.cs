using UnityEngine;

namespace Audio
{
	[CreateAssetMenu(fileName = "Clip", menuName = "Audio/Clip")]
	public sealed class AudioClip : AudioObject
	{
		[SerializeField] private UnityEngine.AudioClip clip;
		
		public UnityEngine.AudioClip Clip => clip;
	}
}