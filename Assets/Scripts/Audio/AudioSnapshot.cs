using UnityEngine;

namespace Audio
{
	[CreateAssetMenu(fileName = "Snapshot", menuName = "Audio/Snapshot")]
	public sealed class AudioSnapshot : BaseObject
	{
		[SerializeField] [HideInInspector] private AudioGroups groups;

		[SerializeField] [Range(0f, 1f), HideInInspector]
		private float[] volumes;

		public void ApplyFrom(AudioSnapshot other, float delta)
		{
			var audioGroups = groups.Groups;
			var length = volumes.Length;
			if (!other)
				for (var i = 0; i < length; i++)
					audioGroups[i].VolumeOverride = Mathf.Lerp(0f, volumes[i], delta);
			else
				for (var i = 0; i < length; i++)
					audioGroups[i].VolumeOverride = Mathf.Lerp(other.volumes[i], volumes[i], delta);
		}

		public void ApplyTo(AudioSnapshot other, float delta)
		{
			var audioGroups = groups.Groups;
			var length = volumes.Length;
			if (!other)
				for (var i = 0; i < length; i++)
					audioGroups[i].VolumeOverride = Mathf.Lerp(volumes[i], 0f, delta);
			else
				for (var i = 0; i < length; i++)
					audioGroups[i].VolumeOverride = Mathf.Lerp(volumes[i], other.volumes[i], delta);
		}
	}
}