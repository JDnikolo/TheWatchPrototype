using System.Collections.Generic;
using Callbacks.Audio;
using UnityEngine;

namespace Audio
{
	[CreateAssetMenu(fileName = "Group", menuName = "Audio/Group")]
	public sealed class AudioGroup : ScriptableObject, IAudioGroupVolumeChanged
	{
		[SerializeField] private AudioGroup parent;
			
		private readonly HashSet<IAudioGroupVolumeChanged> m_volumeChanged = new();
		private float m_volumeOverride;
		private float m_volumeOverrideResult;

		public float VolumeOverride
		{
			get => m_volumeOverrideResult;
			internal set
			{
				if (m_volumeOverride == value) return;
				m_volumeOverride = value;
				RecalculateVolume();
			}
		}
		
		public void AddCallback(IAudioGroupVolumeChanged callback) => m_volumeChanged.Add(callback);
		
		public void RemoveCallback(IAudioGroupVolumeChanged callback) => m_volumeChanged.Remove(callback);
		
		public void OnAudioGroupVolumeChanged(AudioGroup group) => RecalculateVolume();

		private void RecalculateVolume()
		{
			m_volumeOverrideResult = parent ? m_volumeOverride * parent.m_volumeOverride : m_volumeOverride;
			foreach (var callback in m_volumeChanged) callback.OnAudioGroupVolumeChanged(this);
		}

		private void OnEnable()
		{
			if (parent) parent.AddCallback(this);
		}

		private void OnDestroy()
		{
			if (parent) parent.RemoveCallback(this);
		}

#if UNITY_EDITOR
		public HashSet<IAudioGroupVolumeChanged> VolumeChanged => m_volumeChanged;
		
		public float ThisOverride => m_volumeOverride;
		
		public float ParentOverride => parent ? parent.m_volumeOverride : -1f;
#endif
	}
}