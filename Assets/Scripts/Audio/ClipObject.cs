using UnityEngine;

namespace Audio
{
	[CreateAssetMenu(fileName = "Clip", menuName = "Audio/Clip")]
	public sealed class ClipObject : AudioObject
	{
		[SerializeField] private AudioClip clip;
		
		public AudioClip Clip => clip;
	}
}