using UnityEngine;

namespace Audio
{
	[CreateAssetMenu(fileName = "Snapshot", menuName = "Audio/Snapshot")]
	public sealed class AudioSnapshot : ScriptableObject
	{
		[SerializeField] [HideInInspector] private AudioGroups groups;

		[SerializeField] [Range(0f, 1f), HideInInspector]
		private float[] volumes;

		public float[] Volumes => volumes;
	}
}