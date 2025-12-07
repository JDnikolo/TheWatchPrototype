using Attributes;
using UnityEngine;

namespace Audio
{
	[CreateAssetMenu(fileName = "Aggregate", menuName = "Audio/Clip Aggregate")]
	public sealed class AudioAggregate : AudioObject
	{
		//[MinCount(2)] 
		[SerializeField] private UnityEngine.AudioClip[] clips;
		
		public UnityEngine.AudioClip[] Clips => clips;
	}
}