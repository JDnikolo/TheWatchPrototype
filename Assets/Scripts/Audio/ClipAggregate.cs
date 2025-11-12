using UnityEngine;

namespace Audio
{
	[CreateAssetMenu(fileName = "Aggregate", menuName = "Audio/Clip Aggregate")]
	public sealed class ClipAggregate : AudioObject
	{
		[SerializeField] private AudioClip[] clips;
		
		public AudioClip[] Clips => clips;
	}
}