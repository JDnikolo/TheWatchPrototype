using UnityEngine;

namespace Audio
{
	[CreateAssetMenu(fileName = "AudioGroups", menuName = "Audio/All Groups")]
	public sealed class AudioGroups : ScriptableObject
	{
		[SerializeField] private AudioGroup[] groups;
		
		public AudioGroup[] Groups => groups;
	}
}