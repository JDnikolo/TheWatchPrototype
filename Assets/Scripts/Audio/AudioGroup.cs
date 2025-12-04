using UnityEngine;

namespace Audio
{
	[CreateAssetMenu(fileName = "Group", menuName = "Audio/Group")]
	public sealed class AudioGroup : ScriptableObject
	{
		[SerializeField] private AudioGroup parent;
		
		private float m_volumeOverride;

		public float VolumeOverride
		{
			get => parent ? m_volumeOverride * parent.m_volumeOverride : m_volumeOverride;
			internal set => m_volumeOverride = value;
		}
	}
}